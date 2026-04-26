/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 26 апреля 2026 09:56:57
 * Version: 1.0.22
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Root.Components;
using Root.Services;
using Root.App;
using Root.Presenters;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Text;

// TODO: Player location statistics
// TODO: Player in space randomiser

namespace Root
{
    partial class MainWindow : Form, IMainView
    {
        private MainPresenter presenter;

        private Object logLocker = new Object();
        public event EventHandler ViewLoadEvent;
        public event EventHandler RescanAccountFilesEvent;
        public event EventHandler UpdatePlayerInfoEvent;
        public event EventHandler TestFLHookConnectEvent;
        public event EventHandler DBSaveEvent;
        public event EventHandler ReloadGameDataEvent;
        delegate void UpdateUILogEventDelegate(string msg);
        private readonly ConcurrentQueue<string> logBuffer = new();

        /// <summary>
        /// Коммуникационный сокет для выполнения команд flhook.
        /// </summary>
        private IFLHookSocketService FLHookSocketService { get; set; }  
        /// <summary>
        /// 
        /// </summary>
        private IFLHookEventSocketService FLHookEventSocketService { get; set; }
        /// <summary>
        /// Слушатель сообщений FLHook
        /// </summary>
        private IFLHookListener FLHookListener { get; set; }
        /// <summary>
        /// Служба доступа к базе данных.
        /// </summary>
        public IDataBaseService DataBaseService { get; set; }

        /// <summary>
        /// Текущий загруженный файл символов
        /// </summary>
        private IFLDataFileService FLDataFileService { get; set; }

        /// <summary>
        /// Игровые данные, включая инфокарты.
        /// </summary>
        private IFLGameDataService FLGameDataService { get; set; }

        /// <summary>
        /// Сервис блокировки/разблокировки пользователей
        /// </summary>
        private IBanService BanService { get; set; }

        /// <summary>
        /// Сервис управления взаимодействием с данными персонажей
        /// </summary>
        private ICharService CharService { get; set; }

        /// <summary>
        /// Кешированное состояние прошлого курсора
        /// </summary>
        private Cursor CashCursor { get; set; }

        /// <summary>
        /// The hashcode window.
        /// </summary>
        Form windowHashcode;

        /// <summary>
        /// The general statistics window.
        /// </summary>
        Form windowGeneralStatistics;

        public MainWindow()
        {
            FLDataFileService = new FLDataFileService(true);
            DataBaseService = new DataBaseService();
            FLHookSocketService = new FLHookSocketService();
            FLHookEventSocketService = new FLHookEventSocketService(FLHookListener, this);
            FLHookListener = new FLHookListener(DataBaseService, this);
            FLGameDataService = new FLGameDataService(AppSettings.Default.setFLDir);
            BanService = new BanService(DataBaseService, FLHookSocketService);
            CharService = new CharService(this, DataBaseService, FLHookSocketService,
                FLDataFileService, FLGameDataService);

            InitializeComponent();
            InitializeLogTimer();

            presenter = new MainPresenter(this, FLHookSocketService,
                FLHookEventSocketService, FLHookListener, DataBaseService, 
                FLDataFileService, FLGameDataService, BanService, CharService);

            pIEquipmentTableBindingSource.DataSource = new SortableBindingList<PIEquipment>(DataBaseService.Model.DataSetUI.PIEquipmentList);
            pIFactionTableBindingSource.DataSource = new SortableBindingList<PIFaction>(DataBaseService.Model.DataSetUI.PIFactionList);
        }

        private void InitializeLogTimer()
        {
            var timer = new Timer();
            timer.Interval = 2000;
            timer.Tick += (s, e) => FlushUILogBuffer();
            timer.Start();
        }

        /// <summary>
        /// Событие загрузки формы
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void MainWindow_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(async () =>
            {
                ViewLoadEvent.Invoke(sender, e);
            }));
        }

        /// <summary>
        /// Загрузка игровых данных и баз данных завершена. 
        /// Сохраните базы данных и спросите, хочет ли пользователь просканировать 
        /// файлы учетной записи игрока.
        /// </summary>
        private void LoadItCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //ViewLoaded?.Invoke(this, e);
        }

        /// <summary>
        /// Сохраните данные учетной записи и подключение flhook при закрытии главного окна.
        /// </summary>
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!presenter.ShouldClose())
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Открытие окна свойств
        /// </summary>
        public void ShowProperties()
        {
            var properties = new PropertiesWindow();
            PropertiesPresenter presenter = new PropertiesPresenter(properties);
            presenter.UpdateSettings += Properties_UpdateSettings;
            presenter.Show();
            presenter.BringToFront();
        }

        /// <summary>
        /// Обновление настроек приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Properties_UpdateSettings(object sender, EventArgs e)
        {
            presenter.PropertiesUpdateSettings();
        }

        public DialogResult ConfirmClose(string text, string caption)
        {
            return MessageBox.Show(this,
                text,
                caption,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation);
        }
      
        /// <summary>
        /// Вызывается для отчета о ходе обработки на главном экране. 
        /// Передайте пустую строку, чтобы не обновлять текст статуса.
        /// </summary>
        public void ReportProgress(int progressPercentage, string userState)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => ReportProgress(progressPercentage, userState)));
                return;
            }
           
            progressBar.Value = progressPercentage;
            if (userState is string)
                toolStripStatusLabelStatus.Text = userState;
            Application.DoEvents(); // 🔧 позволяет UI обновиться немедленно
        }

        /// <summary>
        /// Получить запись списка символов текущей выбранной строки в таблице.
        /// </summary>
        /// <returns>Запись или ноль, если запись не была выбрана</returns>
        public string GetCharRecordBySelectedRow()
        {
            // Получить удаленные данные для текущего выбранного игрока в списке персонажей.
            if (charListDataGridView.SelectedRows.Count != 1)
                return null;
            string charFilePath = (string)charListDataGridView.SelectedRows[0].Cells[charPathDataGridViewTextBoxColumn.Index].Value;
            return charFilePath;
        }

        /// <summary>
        /// Обнулить информацию на всех UI блоках
        /// </summary>
        public void ResetPlayerInfoUI()
        {
            piPath.Text = "";

            EnableSimpleElements(false);
            deletePlayerButton.Enabled = false;
            SetupStateCheckFile(false);

            piFileView.Text = "";
            piAccountID.Text = "";
            changeBanButton.Text = "Ban Account";
            buttonBanInfo.Enabled = false;

            piName.Text = "";
            piName.ReadOnly = true;
            changeNameButton.Text = "Change";
            changeNameButton.Enabled = false;

            piMoney.Text = "";
            piMoney.ReadOnly = true;
            changeMoneyButton.Text = "Change";
            changeMoneyButton.Enabled = false;

            piLocation.Text = "";
            changeLocationButton.Enabled = false;

            SetupShipName("");
            changeShipButton.Enabled = false;

            piLastOnline.Text = "";
            buttonResetLastOnline.Enabled = false;

            SetupOnline("");
            SetupKickPlayerState(false);
            piTimePlayed.Text = "";
            piRank.Text = "";
            SetupKills("");
            piCreated.Text = "";

            piIPAddresses.Text = "";

            SetupAffiliation("");

            saveFactionButton.Enabled = false;
            discardFactionButton.Enabled = false;

            addCargoButton.Enabled = false;
            buttonChangeCargo.Enabled = false;
            removeCargoButton.Enabled = false;
            saveCargoButton.Enabled = false;
            discardCargoButton.Enabled = false;

            buttonAddEquipment.Enabled = false;
            changeEquipmentButton.Enabled = false;
            removeEquipmentButton.Enabled = false;
            saveEquipmentButton.Enabled = false;
            discardEquipmentButton.Enabled = false;


            richTextBoxPlayerInfoPlayerText.Clear();
            richTextBoxPlayerInfoAdminText.Clear();
            buttonPlayerInfoSaveAdminText.Enabled = false;
        }

        public void ShowMessage(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(() => ShowMessage(message));
                return;
            }

            MessageBox.Show(this, message, null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public void SortEquipment()
        {
            piEquipmentGrid.Sort(piEquipmentGrid.Columns[itemHardpointDataGridViewTextBoxColumn.Index], ListSortDirection.Descending);
        }

        public void SortCargo()
        {
            piCargoGrid.Sort(piCargoGrid.Columns[itemDescriptionDataGridViewTextBoxColumn.Index], 
                ListSortDirection.Descending);
        }

        public void SortFactions()
        {
            piFactionGrid.Sort(piFactionGrid.Columns[itemDescriptionDataGridViewTextBoxColumn.Index],
                ListSortDirection.Ascending);
        }

        public void SetupKickPlayerState(bool state)
        {
            if (kickPlayerButton.InvokeRequired)
            {
                kickPlayerButton.Invoke(() => SetupKickPlayerState(state));
                return;
            }

            kickPlayerButton.Enabled = state;
        }

        public void SetupStateCheckFile(bool state)
        {
            buttonCheckFile.Enabled = state;
        }

        public void SetupKills(string kills)
        {
            piKills.Text = kills;
        }

        public void EnableSimpleElements(bool state)
        {
            changeBanButton.Enabled = state;
            openDirButton.Enabled = state;
            reloadFileButton.Enabled = state;
            saveFileManualButton.Enabled = state;
        }

        public void EnableCharIsNotDeleteElements()
        {
            deletePlayerButton.Enabled = true;

            changeNameButton.Enabled = true;
            changeMoneyButton.Enabled = true;
            changeLocationButton.Enabled = true;
            changeShipButton.Enabled = true;
            buttonResetLastOnline.Enabled = true;

            saveFactionButton.Enabled = true;
            discardFactionButton.Enabled = true;

            addCargoButton.Enabled = true;
            buttonChangeCargo.Enabled = true;
            removeCargoButton.Enabled = true;
            saveCargoButton.Enabled = true;
            discardCargoButton.Enabled = true;
            buttonAddEquipment.Enabled = true;
            changeEquipmentButton.Enabled = true;
            removeEquipmentButton.Enabled = true;
            saveEquipmentButton.Enabled = true;
            discardEquipmentButton.Enabled = true;

            buttonPlayerInfoSaveAdminText.Enabled = true;
        }

        public void SetupBanStates()
        {
            changeBanButton.Text = "Unban Account";
            buttonBanInfo.Enabled = true;
        }

        public void SetPlayerInfoBasic(CharUIInfo info)
        {
            piAccountID.Text = info.AccID
                    + (info.IsBanned ? " [BANNED]" : "")
                    + (info.IsAdmin ? " [ADMIN]" : "")
                    + (info.IsAuthenticated ? " [AUTHENTICATED]" : "");
            piPath.Text = info.Path;
            piName.Text = info.CharName + (info.IsDeleted ? " [DELETED]" : "");
            piCreated.Text = info.Created;
            piRank.Text = info.Rank;
            piMoney.Text = info.Money;
            piLocation.Text = info.Location;
            piShip.Text = info.Ship;
            piTimePlayed.Text = info.TimePlayed;
            piLastOnline.Text = info.LastOnline;
            richTextBoxPlayerInfoPlayerText.Text = FLUtility.FLXmlToRtf(FLUtility.GetPlayerInfoText(
                AppSettings.Default.setAccountDir, info.CharPath));
            richTextBoxPlayerInfoAdminText.Text = FLUtility.GetPlayerInfoAdminNote(
                AppSettings.Default.setAccountDir, info.CharPath);
            piIPAddresses.Text = "";
        }

        public void SetupAffiliation(string affiliation)
        {
            piAffiliation.Text = affiliation;
        }

        public void SetIPInfo(string info)
        {
            piIPAddresses.Text = info;
        }

        public void SetupShipName(string shipName)
        {
            piShip.Text = shipName;
        }

        public void SetupOnline(string online)
        {
            if (piOnline.InvokeRequired)
            {
                piOnline.Invoke(() => SetupOnline(online));
                return;
            }

            piOnline.Text = online;
        }

        public void SetupHookStateText(string text)
        {
            toolStripHookState.Text = text;
        }

        public void SetupDisplayDBText(string text)
        {
            toolStripDBPending.Text = text;
        }

        public void SetupMenuState(bool state)
        {
            itemListToolStripMenuItem.Enabled = state;
            bannedPlayersToolStripMenuItem.Enabled = state;
            SetupRescanAccountFilesToolStripState(state);
            statisticsToolStripMenuItem.Enabled = state;
            searchIPtoolStripMenuItem.Enabled = state;
            searchLoginIDToolStripMenuItem.Enabled = state;
        }

        public void SetupRescanAccountFilesToolStripState(bool state)
        {
            rescanAccountFilesToolStripMenuItem.Enabled = state;
        }

        public void SetupTimerPeriodicTasksState(bool state)
        {
            switch (state)
            {
                case true:
                    timerPeriodicTasks.Start();
                    break;
                case false:
                    timerPeriodicTasks.Stop();
                    break;
            }
        }

        public void SetupTimerDBSave(bool state)
        {
            switch (state)
            {
                case true:
                    timerDBSave.Start();
                    break;
                case false:
                    timerDBSave.Stop();
                    break;
            }
        }

        public void SetupTimerShutdownState(bool state)
        {
            switch (state)
            {
                case true:
                    timerShutdown.Start();
                    break;
                case false:
                    timerShutdown.Stop();
                    break;
            }
        }

        public void SetupUpdatePlayerInfoTimerState(bool state)
        {
            switch (state)
            {
                case true:
                    updatePlayerInfoTimer.Start();
                    break;
                case false:
                    updatePlayerInfoTimer.Stop();
                    break;
            }
        }

        public void CompleteLoadUIEvent()
        {
            characterListBindingSource.DataSource = new SortableBindingList<CharacterItem>(DataBaseService.Model.DataSetPlayerInfo.CharacterList);
            charListDataGridView.DataSource = characterListBindingSource;
            pICargoListBindingSource.DataSource = new SortableBindingList<PICargo>(DataBaseService.Model.DataSetUI.PICargoList);
            piCargoGrid.DataSource = pICargoListBindingSource;
        }

        public void SetupWaitCursor()
        {
            CashCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
        }

        public void RestoreCursor()
        {
            this.Cursor = CashCursor;
        }

        /// <summary>
        /// Установите фильтр для отображения указанного каталога учетных записей.
        /// </summary>
        /// <param name="accDir">string</param>
        public void FilterOnAccDir(string accDir)
        {
            textBoxFilter.Text = accDir;
        }

        /// <summary>
        /// Откройте окно свойств как модальное диалоговое окно.
        /// </summary>
        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowProperties();
        }

        /// <summary>
        /// Выйдите из приложения.
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void charListDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            var item = presenter.GetSelectedCharRecord();
            if (charListDataGridView.FirstDisplayedCell == null 
                && item != null)
            {
                presenter.SetSelectedCharRecord(null);
                updatePlayerInfoTimer.Start();
                return;
            }

            CharacterItem newSelectedCharRecord = presenter.GetCharRecordBySelectedItem();
            if (newSelectedCharRecord!=null && item != newSelectedCharRecord)
            {
                presenter.SetSelectedCharRecord(newSelectedCharRecord);
                updatePlayerInfoTimer.Start();
            }
        }

        public void ResetCharListEvent()
        {
            charListDataGridView.SelectionChanged -= charListDataGridView_SelectionChanged;
            charListDataGridView.SelectionChanged += charListDataGridView_SelectionChanged;
        }

        /// <summary>
        /// Откройте каталог, содержащий текущий файл персонажа.
        /// </summary>
        private void openDirButton_Click(object sender, EventArgs e)
        {
            try
            {
                CharacterItem charRecord = presenter.GetCharRecordBySelectedItem();
                if (charRecord != null)
                {
                    Process.Start(Path.Combine(AppSettings.Default.setAccountDir, charRecord.AccDir));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error '" + ex.Message + "'", null, 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Обновите область информации об игроке.
        /// </summary>
        private void reloadFileButton_Click(object sender, EventArgs e)
        {
            presenter.UpdateCharacterToServer();
            updatePlayerInfoTimer.Start();
        }

        /// <summary>
        /// Измените имя текущего выбранного персонажа. 
        /// Эта функция использует flhook для смены имени и не будет работать без него.
        /// </summary>
        private void changeNameButton_Click(object sender, EventArgs e)
        {
            CharacterItem charRecord = presenter.GetCharRecordBySelectedItem();
            if (charRecord == null)
                return;

            if (presenter.GetFLDataFileService() == null)
            {
                updatePlayerInfoTimer.Start();
            }
            else
            {
                if (piName.ReadOnly == true)
                {
                    piName.ReadOnly = false;
                    changeNameButton.Text = "Save";
                }
                else
                {
                    piName.ReadOnly = false;
                    changeNameButton.Text = "Change";
                    
                    string newName = piName.Text;
                    presenter.RenameCharacter(newName);

                    updatePlayerInfoTimer.Start();
                }
            }
        }

        /// <summary>
        /// Разрешите редактировать текстовое поле «Деньги» или сохраните изменения.
        /// </summary>
        private void changeMoneyButton_Click(object sender, EventArgs e)
        {
            CharacterItem charRecord = presenter.GetCharRecordBySelectedItem();
            if (charRecord == null)
                return;

            if (presenter.GetFLDataFileService() == null)
            {
                updatePlayerInfoTimer.Start();
            }
            else
            {
                // Если поле «Деньги» доступно только для чтения,
                // то это запрос на редактирование, поэтому разрешите редактирование.
                if (piMoney.ReadOnly == true)
                {
                    piMoney.ReadOnly = false;
                    changeMoneyButton.Text = "Save";
                }
                // В противном случае это должно быть спасение.
                else
                {
                    uint newMoney;
                    if (!UInt32.TryParse(piMoney.Text, out newMoney))
                    {
                        ShowMessage("Invalid money.");
                        return;
                    }
                    presenter.ChangeMoneyCharacter(newMoney);
                }
            }
        }

        /// <summary>
        /// Переключить состояние запрета выбранного персонажа в списке персонажей.
        /// </summary>
        private void changeBanButton_Click(object sender, EventArgs e)
        {
            CharacterItem charRecord = presenter.GetCharRecordBySelectedItem();
            if (charRecord == null)
                return;

            if (changeBanButton.Text == "Ban Account")
                presenter.BanPlayer(charRecord);
            else
                presenter.UnBanPlayer(charRecord);

            updatePlayerInfoTimer.Start();
        }

        /// <summary>
        /// Показать информацию о бане для выбранного игрока.
        /// </summary>
        private void buttonBanInfo_Click(object sender, EventArgs e)
        {
            CharacterItem charRecord = presenter.GetCharRecordBySelectedItem();
            if (charRecord == null)
                return;

            bannedPlayersToolStripMenuItem_Click(null, null);
            presenter.BannedPlayersHighlightRecord(charRecord.AccID);
        }

        /// <summary>
        /// Удалить выбранный символ в списке символов. 
        /// Всегда удалять файл символа, даже если команда flhook файлы.
        /// </summary>
        private void deletePlayerButton_Click(object sender, EventArgs e)
        {
            CharacterItem charRecord = presenter.GetCharRecordBySelectedItem();
            if (charRecord == null)
                return;

            if (MessageBox.Show(this, "Are you sure you want to delete this player?", "Delete Player?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                return;

            presenter.DeleteChar(charRecord.CharName, charRecord.CharPath);
            updatePlayerInfoTimer.Start();
        }

        /// <summary>
        /// Открыть диалог смены локации. Это модально. 
        /// Выгнать игрока перед сменой локации.
        /// </summary>
        private void changeLocationButton_Click(object sender, EventArgs e)
        {
            CharacterItem charRecord = presenter.GetCharRecordBySelectedItem();
            if (charRecord == null)
                return;

            if (presenter.GetFLDataFileService() == null)
            {
                updatePlayerInfoTimer.Start();
            }
            else
            {
                string charPath = Path.Combine(AppSettings.Default.setAccountDir, charRecord.CharPath);
                presenter.OpenChangeLocation();
            }
        }


        /// <summary>
        /// Измените корабль игрока.
        /// </summary>
        private void changeShipButton_Click(object sender, EventArgs e)
        {
            CharacterItem charRecord = presenter.GetCharRecordBySelectedItem();
            if (charRecord == null)
                return;

            if (presenter.GetFLDataFileService() == null)
            {
                updatePlayerInfoTimer.Start();
            }
            else
            {
                string charPath = Path.Combine(AppSettings.Default.setAccountDir, charRecord.CharPath);
                presenter.OpenChangeShip();
            }
        }

        /// <summary>
        /// Выгнать игрока.
        /// </summary>
        private void kickPlayerButton_Click(object sender, EventArgs e)
        {
            CharacterItem charRecord = presenter.GetCharRecordBySelectedItem();
            if (charRecord == null)
                return;

            presenter.KickPlayer(charRecord.CharName);
            updatePlayerInfoTimer.Start();
        }

        /// <summary>
        /// Обновите время последнего пребывания игрока в сети, 
        /// чтобы оно не было удалено при автоматическом удалении данных игрока.
        /// </summary>
        private void buttonResetLastOnline_Click(object sender, EventArgs e)
        {
            CharacterItem charRecord = presenter.GetCharRecordBySelectedItem();
            if (charRecord == null)
                return;

            if (presenter.GetFLDataFileService() == null)
                updatePlayerInfoTimer.Start();
            else presenter.UpdateLastPlayerTime();
        }

        /// <summary>
        /// При изменении выбранных строк обновить редактор репутации. 
        /// Запретить уведомления при обновлении редактора репутации.
        /// </summary>
        private void piFactionGrid_SelectionChanged(object sender, EventArgs e)
        {
            piReputationEdit.ValueChanged -= piReputationEdit_ValueChanged;
            foreach (DataGridViewRow row in piFactionGrid.SelectedRows)
            {
                PIFaction dataRow = (PIFaction)row.DataBoundItem;
                piReputationEdit.Value = Convert.ToDecimal(dataRow.itemRep);
                trackBar1.Value = (int)(piReputationEdit.Value * 10);
            }
            piReputationEdit.ValueChanged += piReputationEdit_ValueChanged;
        }

        /// <summary>
        /// При изменении значения репутации обновите все выбранные строки во фракции, 
        /// указав новое значение.
        /// </summary>
        private void piReputationEdit_ValueChanged(object sender, EventArgs e)
        {
            trackBar1.Value = (int)(piReputationEdit.Value * 10);
            foreach (DataGridViewRow row in piFactionGrid.SelectedRows)
            {
                PIFaction dataRow = (PIFaction)row.DataBoundItem;
                dataRow.itemRep = Convert.ToSingle(piReputationEdit.Value);
            }
        }

        /// <summary>
        /// Когда полоса отслеживания репутации изменится, обновите данные о репутации.
        /// </summary>
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            piReputationEdit.Value = ((decimal)trackBar1.Value)/10;
        }

        /// <summary>
        /// Отмените изменения в редакторе фракций, перезагрузив файл символов.
        /// </summary>
        private void discardFactionButton_Click(object sender, EventArgs e)
        {
            updatePlayerInfoTimer.Start();
        }

        /// <summary>
        /// Сохраните изменения, внесенные в редактор фракций. 
        /// Обратите внимание, что строки фракций ini имеют формат "house = relevance, faction_nick"
        /// </summary>
        private void saveFactionButton_Click(object sender, EventArgs e)
        {
            CharacterItem charRecord = presenter.GetCharRecordBySelectedItem();
            if (charRecord == null)
                return;

            if (presenter.GetFLDataFileService() == null)
                updatePlayerInfoTimer.Start();
            else presenter.SaveFactionPlayer();
        }

        /// <summary>
        /// Отложите загрузку текстового поля просмотра файла, поскольку иногда это может занять много времени.
        /// </summary>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedTabControlChanged();
        }

        public async void SelectedTabControlChanged()
        {
            if (tabControl1.SelectedTab == tabPage5)
            {
                piFileView.Clear();
                try
                {
                    if (piFileView.TextLength == 0 && presenter.FLDataFileServiceIsNull())
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        if (checkBoxShowOriginalFile.Checked)
                        {
                            piFileView.Text = presenter.GetIniFileContents();
                        }
                        else
                        {
                            piFileView.Text = await presenter.PrettyPrintCharFile();
                        }

                        Cursor.Current = Cursors.Default;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Error '" + ex.Message + "'", null,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        /// <summary>
        /// Добавьте единицу груза.
        /// </summary>
        private void buttonAddCargo_Click(object sender, EventArgs e)
        {
            presenter.OpenAddCargoPlayer(null);
        }

        /// <summary>
        /// Изменить грузовой пункт.
        /// </summary>
        private void buttonChangeCargo_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in piCargoGrid.SelectedRows)
            {
                PICargo item = (PICargo)row.DataBoundItem;
                presenter.OpenAddCargoPlayer(item);
            }
        }

        /// <summary>
        /// Удалите все выбранные строки из таблицы данных списка грузов.
        /// </summary>
        private void buttonRemoveCargo_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in piCargoGrid.SelectedRows)
            {
                PICargo item = (PICargo)row.DataBoundItem;
                presenter.RemoveCargoPlayer(item);
            }
        }

        /// <summary>
        /// Сохраните все изменения, внесенные в сетку данных списка грузов.
        /// </summary>
        private void saveCargoButton_Click(object sender, EventArgs e)
        {
            // Выгнать игрока, если он в сети.
            CharacterItem charRecord = presenter.GetCharRecordBySelectedItem();
            if (charRecord == null)
                return;

            presenter.SaveCargoPlayer();
        }

        /// <summary>
        /// Отмените любые изменения в представлении сетки данных о грузе.
        /// </summary>
        private void discardCargoButton_Click(object sender, EventArgs e)
        {
            updatePlayerInfoTimer.Start();
        }

        /// <summary>
        /// Добавить новый элемент внутреннего оборудования на корабль
        /// </summary>
        private void addEquipmentButton_Click(object sender, EventArgs e)
        {
            if (piEquipmentGrid.SelectedRows.Count != 1)
                return;

            presenter.OpenAddEquipment(null);
        }

        /// <summary>
        /// Изменить существующий элемент оборудования на другой тип
        /// </summary>
        private void changeEquipmentButton_Click(object sender, EventArgs e)
        {
            if (piEquipmentGrid.SelectedRows.Count != 1)
                return;

            PIEquipment item = (PIEquipment)piEquipmentGrid.SelectedRows[0].DataBoundItem;
            presenter.OpenAddEquipment(item);
        }

        /// <summary>
        /// Удалите один или несколько элементов оборудования.
        /// </summary>
        private void removeEquipmentButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow selectedRow in piEquipmentGrid.SelectedRows)
            {
                PIEquipment item = (PIEquipment)selectedRow.DataBoundItem;
                presenter.RemoveEquipmentPlayer(item);
            }
        }

        /// <summary>
        /// Сокращенный вариант для смены элемента оборудования.
        /// </summary>
        private void piEquipmentGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            changeEquipmentButton_Click(sender, e);
        }

        /// <summary>
        /// Скопируйте вид сетки списка оборудования обратно в INI-файл и сохраните его.
        /// </summary>
        private void saveEquipmentButton_Click(object sender, EventArgs e)
        {
            // Выгнать игрока, если он в сети.
            CharacterItem charRecord = presenter.GetCharRecordBySelectedItem();
            if (charRecord == null)
                return;

            presenter.SaveEquipmentPlayer();
        }

        /// <summary>
        /// Отмените любые изменения в представлении сетки данных оборудования.
        /// </summary>
        private void discardEquipmentButton_Click(object sender, EventArgs e)
        {
            updatePlayerInfoTimer.Start();
        }

        /// <summary>
        /// Сохраните «сырой» файл символов.
        /// </summary>
        private void saveFileManualButton_Click(object sender, EventArgs e)
        {
            presenter.SaveDataFile(piFileView.Text);
            updatePlayerInfoTimer.Start();
        }

        /// <summary>
        /// Откройте окно забаненных игроков.
        /// </summary>
        private void bannedPlayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.OpenBannedPlayers();
        }

        /// <summary>
        /// Откройте окно статистики или выведите его на передний план.
        /// </summary>
        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.OpenStatistics();
        }

        /// <summary>
        /// Откройте окно хэш-кода или выведите его на передний план.
        /// </summary>
        private void hashcodeListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.OpenHash();
        }

        /// <summary>
        /// Откройте диалоговое окно, отображающее файл readme.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.OpenAbout();
        }

        /// <summary>
        /// Информация об игроке обновляется по таймеру, 
        /// чтобы разорвать любые циклические зависимости от вызовов.
        /// </summary>
        private void updatePlayerInfoTimer_Tick(object sender, EventArgs e)
        {
            updatePlayerInfoTimer.Stop();
            BeginInvoke(new Action(async () =>
            {
                UpdatePlayerInfoEvent.Invoke(sender, EventArgs.Empty);
            }));
        }

        /// <summary>
        /// Ручное сканирование файла учетной записи
        /// </summary>
        private void rescanAccountFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeginInvoke(new Action(async () =>
            {
                RescanAccountFilesEvent.Invoke(this, EventArgs.Empty);
            }));
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            timerFilter.Start();
            if (textBoxFilter.Text.Length > 0)
            {
                checkBoxFilterSameAccount.Checked = false;
                checkBoxFilterSameIP.Checked = false;
                checkBoxFilterSameLoginID.Checked = false;
            }
        }

        private void checkBoxFilterSameAcc_CheckedChanged(object sender, EventArgs e)
        {
            timerFilter.Start();
            if (checkBoxFilterSameAccount.Checked)
            {
                textBoxFilter.Text = "";
                checkBoxFilterSameIP.Checked = false;
                checkBoxFilterSameLoginID.Checked = false;
            }
        }

        private void checkBoxFilterSameLoginID_CheckedChanged(object sender, EventArgs e)
        {
            timerFilter.Start();
            if (checkBoxFilterSameLoginID.Checked)
            {
                textBoxFilter.Text = "";
                checkBoxFilterSameAccount.Checked = false;
                checkBoxFilterSameIP.Checked = false;
            }
        }

        private void checkBoxFilterSameIP_CheckedChanged(object sender, EventArgs e)
        {
            timerFilter.Start();
            if (checkBoxFilterSameIP.Checked)
            {
                textBoxFilter.Text = "";
                checkBoxFilterSameAccount.Checked = false;
                checkBoxFilterSameLoginID.Checked = false;
            }
        }

        private void checkBoxFilterDeleted_CheckedChanged(object sender, EventArgs e)
        {
            timerFilter.Start();
        }

        /// <summary>
        /// Обновите фильтр, применяемый к представлению сетки данных списка персонажей.
        /// </summary>
        private void FilterUpdate(object sender, EventArgs e)
        {
            presenter.FilterUpdate();
        }

        public void FilterUpdateState(bool state)
        {
            switch (state)
            {
                case true:
                    timerFilter.Start();
                    break;
                case false:
                    timerFilter.Stop();
                    break;
            }
        }

        public bool GetStateCheckBoxFilterDeleted()
        {
            return checkBoxFilterDeleted.Checked;
        }

        public string GetTextBoxFilterText()
        {
            return textBoxFilter.Text;
        }

        public bool GetStateCheckBoxFilterSameAccount()
        {
            return checkBoxFilterSameAccount.Checked;
        }

        public bool GetStateCheckBoxFilterSameIP()
        {
            return checkBoxFilterSameIP.Checked;
        }

        public bool GetStateCheckBoxFilterSameLoginID()
        {
            return checkBoxFilterSameLoginID.Checked;
        }

        public void SetupCharacterListBindingSource(List<CharacterItem> characters)
        {
            characterListBindingSource.DataSource = characters;
        }

        public void SetupOldCharacterItem()
        {
            foreach (DataGridViewRow row in charListDataGridView.Rows)
            {
                CharacterItem charRecord = (CharacterItem)row.DataBoundItem;
                if (charRecord == presenter.GetSelectedCharRecord())
                {
                    row.Selected = true;
                    charListDataGridView.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }

        /// <summary>
        /// Добавьте запись в журнал диагностики.
        /// </summary>
        /// <param name="entry">Запись в журнале</param>
        public void AddLog(string entry)
        {
            lock (logLocker)
            {
                var dir = AppSettings.Default.setAccountDir;
                if (!string.IsNullOrWhiteSpace(dir))
                {
                    try
                    {
                        var path = Path.Combine(dir, "dsam.log");
                        File.AppendAllText(path, entry + Environment.NewLine);
                    }
                    catch (Exception e)
                    {
                        string errMsg = "Write to game log failed: " + e.Message;
                        SafeInvokeUILog(errMsg);
                    }
                }
            }

            SafeInvokeUILog(entry);
        }

        private void SafeInvokeUILog(string message)
        {
            if (InvokeRequired)
            {
                logBuffer.Enqueue(message);
                return; // просто не обновляем UI из фонового потока
            }
            UpdateUILogEvent(message);
        }

        private void FlushUILogBuffer()
        {
            if (InvokeRequired)
            {
                Invoke((Action)FlushUILogBuffer);
                return;
            }

            var sb = new StringBuilder();
            while (logBuffer.TryDequeue(out var line))
                sb.AppendLine(line);

            if (sb.Length > 0)
                UpdateUILogEvent(sb.ToString());
        }

        /// <summary>
        /// Добавьте запись в экранный журнал и разверните программу, если необходимо.
        /// </summary>
        /// <param name="msg">Сообщение</param>
        private void UpdateUILogEvent(string msg)
        {
            string oldText = richTextBoxLog.Text;
            if (oldText.Length > 50000)
            {
                oldText = oldText.Substring(0, 50000);
            }

            richTextBoxLog.Text = DateTime.Now.ToShortDateString()
                + " " + DateTime.Now.ToLongTimeString()
                + ":" + msg + "\n" + oldText;
        }

        /// <summary>
        /// Проверьте соединение FLHook.
        /// </summary>
        private void timerPeriodicTasks_Tick(object sender, EventArgs e)
        {
            BeginInvoke(new Action(async () =>
            {
                TestFLHookConnectEvent.Invoke(this, new EventArgs());
            }));
        }

        /// <summary>
        /// Сохраняйте все ожидающие изменения в базе данных каждые 60 секунд.
        /// </summary>
        private void timerDBSave_Tick(object sender, EventArgs e)
        {
            BeginInvoke(new Action(async () =>
            {
                DBSaveEvent.Invoke(this, new EventArgs());
            }));
        }

        /// <summary>
        /// Проверьте файл персонажа на наличие ошибок.
        /// </summary>
        private void buttonCheckFile_Click(object sender, EventArgs e)
        {
            if (presenter.GetFLDataFileService() == null)
                updatePlayerInfoTimer.Start();
            else presenter.CheckFileCharacter();
        }

        /// <summary>
        /// Открыть окно поиска IP. Разрешить открытие нескольких экземпляров.
        /// </summary>
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            presenter.SearchForAccountsByIP();
        }

        private void searchLoginIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.SearchForAccountsByIP();
        }

        /// <summary>
        /// Открыть редактор файлов. Разрешить открытие нескольких экземпляров.
        /// </summary>
        private void fLFileEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.IniFile();
        }

        /// <summary>
        /// Добавьте полную карту к персонажу.
        /// </summary>
        private void buttonAddCompleteMap_Click(object sender, EventArgs e)
        {
            CharacterItem charRecord = presenter.GetCharRecordBySelectedItem();
            if (charRecord == null)
                return;

            if (presenter.GetFLDataFileService() == null)
            {
                updatePlayerInfoTimer.Start();
            }
            else
            {
                if (MessageBox.Show("Add complete map to player?", "Add Map", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;

                presenter.AddCompleteMap();
            }
        }

        /// <summary>
        /// Тестовое окно для декодирования всех FLS1EncodedFiles. Обычно отключено.
        /// </summary>
        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            presenter.DecodeAllFiles();
        }

        /// <summary>
        /// Если программа завершает работу, дождитесь выхода из фонового режима, 
        /// а затем закройте форму.
        /// </summary>
        private void timerShutdown_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabelStatus.Text = "Shutting down...";
            presenter.Shutdown();
        }

        /// <summary>
        /// Запустите фоновый процесс для перезагрузки игровых данных и баз данных.
        /// </summary>
        private void reloadGameDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeginInvoke(new Action(async () =>
            {
                ReloadGameDataEvent.Invoke(this, EventArgs.Empty);
            }));
        }

        private void buttonPlayerInfoSaveAdminText_Click(object sender, EventArgs e)
        {
            CharacterItem charRecord = presenter.GetCharRecordBySelectedItem();
            if (charRecord == null)
                return;

            presenter.PlayerInfoSaveAdmin(charRecord.CharPath, richTextBoxPlayerInfoAdminText.Text);
        }
    }
}
