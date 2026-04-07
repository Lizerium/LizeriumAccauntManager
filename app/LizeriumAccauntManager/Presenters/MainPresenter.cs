/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 07 апреля 2026 10:56:41
 * Version: 1.0.1
 */

using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

using Root.App;
using Root.Components;
using Root.Services;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Text;
using Root.Tool_UI;
using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Root.Presenters
{
    public class MainPresenter
    {
        private readonly IMainView view;
        /// <summary>
        /// Если true, пользователю не будет предложено выйти.
        /// </summary>
        private bool ForceExit { get; set; } = false;
        /// <summary>
        /// Коммуникационный сокет для выполнения команд flhook.
        /// </summary>
        private IFLHookSocketService FLHookSocketService { get; set; }
        /// <summary>
        /// Монитор событий подключения FLhook
        /// </summary>
        private IFLHookEventSocketService FLHookEventSocketService { get; set; }
        /// <summary>
        /// Слушатель сообщений FLHook
        /// </summary>
        private IFLHookListener FLHookListener { get; set; }
        /// <summary>
        /// Служба доступа к базе данных.
        /// </summary>
        private IDataBaseService DataBaseService { get; set; }
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
        /// Токен отмены загрузки информации об игре
        /// </summary>
        private CancellationTokenSource LoadCancelTokenSource { get; set; }
        /// <summary>
        /// Токен отмены сохранения в бд
        /// </summary>
        private CancellationTokenSource DbSaveCancelTokenSource { get; set; }
        /// <summary>
        /// Окно показа забаненных пользователей
        /// </summary>
        private BannedPlayersPresenter BannedPlayersPresenter;
        /// <summary>
        /// Окно показа статистики
        /// </summary>
        private StatisticsPresenter StatisticsPresenter;
        /// <summary>
        /// Окно показа хэш-кода
        /// </summary>
        private HashPresenter HashPresenter;
        /// <summary>
        /// Количество ожидающих изменений в наборе данных информации об игроке
        /// </summary>
        private int DBUpdatesPending = 0;

        public MainPresenter(IMainView view, 
            IFLHookSocketService fLHookSocketService,
            IFLHookEventSocketService fLHookEventSocketService,
            IFLHookListener fLHookListener,
            IDataBaseService dataBaseService,
            IFLDataFileService flDataFileService,
            IFLGameDataService fLGameDataService,
            IBanService banService,
            ICharService charService)
        {
            this.view = view;
            FLHookSocketService = fLHookSocketService;
            FLHookEventSocketService = fLHookEventSocketService;
            FLHookListener = fLHookListener;
            DataBaseService = dataBaseService;
            FLDataFileService = flDataFileService;
            FLGameDataService = fLGameDataService;
            BanService = banService;

            // подписываемся на события
            FLHookListener.HookEventProcessed += FLHookListener_HookEventProcessed;
            view.ViewLoadEvent += (s, e) => _ = View_ViewLoad();
            view.RescanAccountFilesEvent += (s, e) => _ = RescanAccountFilesAsync();
            view.UpdatePlayerInfoEvent += (s, e) => _ = UpdatePlayerInfoAsync();
            view.TestFLHookConnectEvent += (s, e) => _ = TestFLHookConnectAsync();
            view.DBSaveEvent += (s, e) => _ = DBSaveAsync();
            view.ReloadGameDataEvent += (s, e) => _ = ReloadGameDataAsync();
            CharService = charService;
        }

        private void FLHookListener_HookEventProcessed(string type, string[] keys, string[] values, string line)
        {
            //TODO:FLHookListener_HookEventProcessed
        }

        /// <summary>
        /// Процесс закрытия формы
        /// </summary>
        public bool ShouldClose()
        {
            if (!ForceExit && view.ConfirmClose("Are you sure you want to quit?", "Close Application?") != DialogResult.Yes)
                return false;

            ShutdownServices();
            return true;
        }

        /// <summary>
        /// Процесс завершения всех сервисов
        /// </summary>
        private void ShutdownServices()
        {
            Task.Run(() =>
            {
                if (FLHookSocketService != null)
                {
                    FLHookSocketService.Shutdown();
                    FLHookSocketService = null;
                }
            });

            // Set a timer to wait for thread shutdown.
            if (LoadCancelTokenSource != null)
            {
                LoadCancelTokenSource.Cancel();
                view.SetupTimerShutdownState(true);
            }
        }

        public void Shutdown()
        {
            if(LoadCancelTokenSource == null
                && DbSaveCancelTokenSource == null)
            {
                ForceExit = true;
                view.Close();
            }
        }

        /// <summary>
        /// Событие загрузки формы
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private async Task View_ViewLoad()
        {
            // Если каталог учетных записей по умолчанию не настроен, создайте его.
            if (AppSettings.Default.setAccountDir == "")
            {
                AppSettings.Default.setAccountDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                 + Path.DirectorySeparatorChar + "My Games"
                 + Path.DirectorySeparatorChar + "Freelancer"
                 + Path.DirectorySeparatorChar + "Accts"
                 + Path.DirectorySeparatorChar + "MultiPlayer";
                AppSettings.Default.Save();
            }

            if (AppSettings.Default.setFLDir == "")
            {
                string path = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Microsoft Games\\Freelancer\\1.0", "AppPath", "C:\\Program Files\\Microsoft Games\\Freelancer");
                if (path == null)
                    path = "C:\\Program Files\\Microsoft Games\\Freelancer";
                AppSettings.Default.setFLDir = path;
                AppSettings.Default.Save();
            }

            if (AppSettings.Default.setIonCrossDir == "")
            {
                string path = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Microsoft Games\\Freelancer\\1.0", "AppPath", "C:\\Program Files\\Microsoft Games\\Freelancer");
                if (path == null)
                    path = "C:\\Program Files\\Microsoft Games\\Freelancer\\IONCROSS";
                AppSettings.Default.setIonCrossDir = path;
                AppSettings.Default.Save();
            }

            // Если ключевые каталоги / файлы не существуют,
            // откройте окно свойств, чтобы создать их.
            if (!Directory.Exists(AppSettings.Default.setAccountDir)
                || !Directory.Exists(AppSettings.Default.setFLDir)
                || !File.Exists(AppSettings.Default.setFLDir + "\\EXE\\Freelancer.ini"))
            {
                view.ShowProperties();
            }

            if (string.IsNullOrEmpty(AppSettings.Default.setFLDir) || !Directory.Exists(AppSettings.Default.setFLDir))
            {
                view.ReportProgress(100, "Wait setup correct Freelancer directory...");
                return;
            }

            // Сбросить кнопки информации об игроке
            await UpdatePlayerInfoAsync();
            FilterUpdate();

            // Отключить меню во время загрузки данных.
            view.SetupMenuState(false);

            await LoadGameDataAsync();
        }

        private async Task LoadGameDataAsync()
        {
            // Загрузите игровые данные.
            LoadCancelTokenSource = new CancellationTokenSource();
            var progress = new Progress<ProgressInfo>(tuple =>
            {
                view.ReportProgress(tuple.Percent, tuple.Message);
            });

            // Получаю данные игры
            var data = await LoadItAsync(progress, LoadCancelTokenSource.Token);

            // Загрузка завершена
            LoadItCompleted(data, progress);
        }

        private async void LoadItCompleted(DataModel data, IProgress<ProgressInfo> progress)
        {
            if (LoadCancelTokenSource.IsCancellationRequested)
                return;

            if (data == null)
            {
                if (view.ConfirmClose("Cannot access database. Check your 'Player account directory'. Try again?",
                    "Error") == DialogResult.No)
                {
                    ForceExit = true;
                    Application.Exit();
                    return;
                }
                return;
            }
            LoadCancelTokenSource = null;

            progress.Report(new(90, "Updating tables"));
            FLGameDataService.Model = data;
            DataBaseService.Model.DataSetPlayerInfo.BanList.Clear();
            DataBaseService.Model.DataSetPlayerInfo.MergeBanList(data.DataSetPlayerInfo.BanList);
            DataBaseService.Model.DataSetPlayerInfo.CharacterList.Clear();
            DataBaseService.Model.DataSetPlayerInfo.MergeCharacterList(data.DataSetPlayerInfo.CharacterList);

            progress.Report(new(0, "OK"));
            view.AddLog("OK");

            // Включите меню теперь, когда загружены ключевые данные.
            view.SetupMenuState(true);
            view.SetupTimerPeriodicTasksState(true);
            view.SetupTimerDBSave(true);
            view.CompleteLoadUIEvent();

            // Запустите фоновый обработчик загрузки данных.
            await RescanAccountFilesAsync();
        }

        public async Task RescanAccountFilesAsync()
        {
            if (LoadCancelTokenSource == null)
            {
                LoadCancelTokenSource = new CancellationTokenSource();
                view.SetupRescanAccountFilesToolStripState(false);
                var progress = new Progress<ProgressInfo>(tuple =>
                {
                    view.ReportProgress(tuple.Percent, tuple.Message);
                });

                DataModel data = null;
                await Task.Run(async () =>
                {
                    // Сканирование каталогов учетных записей поток фоновой обработки
                    data = await CheckItAsync(progress, LoadCancelTokenSource.Token);
                });

                // Загрузка завершена
                CheckItCompleted(data, progress);
            }
        }

        /// <summary>
        /// Вызывается после завершения фонового потока обработки каталога учетных записей.
        /// </summary>
        private void CheckItCompleted(DataModel data, IProgress<ProgressInfo> progress)
        {
            if (LoadCancelTokenSource.IsCancellationRequested)
                return;

            view.SetupRescanAccountFilesToolStripState(true);
            LoadCancelTokenSource = null;

            DataBaseService.Model.DataSetPlayerInfo.BanList.Clear();
            DataBaseService.Model.DataSetPlayerInfo.MergeBanList(data.DataSetPlayerInfo.BanList);
            DataBaseService.Model.DataSetPlayerInfo.CharacterList.Clear();
            DataBaseService.Model.DataSetPlayerInfo.MergeCharacterList(data.DataSetPlayerInfo.CharacterList);
            progress.Report(new(0, "OK"));

            view.CompleteLoadUIEvent();
        }

        /// <summary>
        /// Сканирование каталогов учетных записей поток фоновой обработки
        /// </summary>
        /// <param name="progress">Прогресс сканирования</param>
        /// <param name="token">Токен отмены операции</param>
        private async Task<DataModel> CheckItAsync(Progress<ProgressInfo> progress, CancellationToken token)
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.BelowNormal;
            var data = await FLUtility.CheckAccounts(progress, token, view, FLGameDataService);
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Normal;
            return data;
        }

        /// <summary>
        /// Загрузить файл персонажа и отобразить его на вкладках информации об игроке
        /// </summary>
        public async Task UpdatePlayerInfoAsync()
        {
            if (DbSaveCancelTokenSource != null) return;

            view.ResetPlayerInfoUI();

            FLDataFileService = null;
            DataBaseService.Model.DataSetUI.PIFactionList.Clear();
            DataBaseService.Model.DataSetUI.PICargoList.Clear();
            DataBaseService.Model.DataSetUI.PIEquipmentList.Clear();

            CharacterItem charRecord = GetCharRecordBySelectedItem();
            if (charRecord == null)
                return;

            // Rescan the account to ensure the database is in an accurate state.
            DBUpdatesPending += FLUtility.ScanCharAccount(DataBaseService.Model,
                DataBaseService, view, FLGameDataService, charRecord.AccDir);
            DBUpdatesPending += FLUtility.CheckForDeletedChars(DataBaseService.Model.DataSetPlayerInfo,
                DataBaseService, charRecord.AccDir);

            view.EnableSimpleElements(true);

            if (!charRecord.IsDeleted)
                view.EnableCharIsNotDeleteElements();

            // Чтение специальных файлов учетных записей, администратора, заблокированных
            // и аутентифицированных (используется плагином управления проигрывателем).
            string accDirPath = AppSettings.Default.setAccountDir + "\\" + charRecord.AccDir;
            bool isAdmin = File.Exists(accDirPath + Path.DirectorySeparatorChar + "flhookadmin.ini");
            isAdmin |= File.Exists(accDirPath + Path.DirectorySeparatorChar + "admin");
            bool isBanned = File.Exists(accDirPath + Path.DirectorySeparatorChar + "banned");
            bool isAuthenticated = File.Exists(accDirPath + Path.DirectorySeparatorChar + "authenticated");

            if (isBanned)
                view.SetupBanStates();

            var uiCharInfo = new CharUIInfo()
            {
                AccID = charRecord.AccID,
                CharName = charRecord.CharName,
                Created = $"{charRecord.Created:f}",
                Rank = charRecord.Rank.ToString(NumberFormatInfo.InvariantInfo),
                Money = charRecord.Money.ToString(NumberFormatInfo.InvariantInfo),
                Location = charRecord.Location,
                Ship = charRecord.Ship,
                TimePlayed = new TimeSpan(0, 0, (int)charRecord.OnLineSecs).ToString(),
                LastOnline = $"{charRecord.LastOnLine:f}",
                Path = Path.Combine(AppSettings.Default.setAccountDir, charRecord.CharPath),
                CharPath = charRecord.CharPath,
                IsBanned = isBanned,
                IsAdmin = isAdmin,
                IsAuthenticated = isAuthenticated,
                IsDeleted = charRecord.IsDeleted
            };

            try
            {
                // Сначала укажите идентификатор учетной записи и путь.
                // После этого мы можем столкнуться с исключением и остановить обновление.
                view.SetPlayerInfoBasic(uiCharInfo);

                var ipListTask = DataBaseService.GetIPListByAccDir(charRecord.AccDir);
                var loginIDListTask = DataBaseService.GetLoginIDListByAccDir(charRecord.AccDir);

                await Task.Run(async () =>
                {
                    await Task.WhenAll(ipListTask, loginIDListTask);
                });

                var ipList = ipListTask.Result;
                var loginIDList = loginIDListTask.Result;

                view.SetIPInfo(string.Join("\r\n", 
                    ipList.Select(x => $"{x.IP} {x.AccessTime}")
                    .Concat(loginIDList.Select(x => $"{x.LoginID} {x.AccessTime}"))));

                if(!File.Exists(uiCharInfo.Path))
                    return;

                var path = Path.Combine(AppSettings.Default.setAccountDir, charRecord.CharPath);
                FLDataFileService = new FLDataFileService(path, true);
                view.SetupStateCheckFile(true);

                uint kills = 0;
                foreach (FLDataFileSetting e in FLDataFileService.GetSettings("mPlayer", "ship_type_killed"))
                    kills += e.UInt(1);
                view.SetupKills(kills.ToString());

                long shipArchType = 0;
                view.SetupShipName(FLUtility.GetShip(FLGameDataService, FLDataFileService, 
                    out shipArchType));

                view.SetupOnline("No");
                await Task.Run(() =>
                {
                    if (FLHookSocketService.CmdIsOnServer(charRecord.CharName))
                    {
                        view.SetupOnline("Yes");
                        view.SetupKickPlayerState(true);
                    }
                });

                // Поля вкладки фракций
                if (FLDataFileService.SettingExists("Player", "rep_group"))
                    view.SetupAffiliation(FLGameDataService.GetItemDescByFactionNickName(FLDataFileService.GetSetting("Player", "rep_group").Str(0)));

                foreach (FLDataFileSetting e in FLDataFileService.GetSettings("Player", "house"))
                    DataBaseService.Model.DataSetUI.PIFactionList.Add(new PIFaction()
                    {
                        itemDescription = FLGameDataService
                        .GetItemDescByFactionNickName(e.Str(1)),
                        itemNickname = e.Str(1),
                        itemRep = e.Float(0)
                    });
                // Заполним таблицу грузов.
                foreach (FLDataFileSetting e in FLDataFileService.GetSettings("Player", "cargo"))
                {
                    // Add the row to the table
                    PICargo pICargo = new PICargo();
                    pICargo.itemHash = e.UInt(0);
                    pICargo.itemCount = e.UInt(1);
                    pICargo.itemDescription = FLGameDataService.GetItemDescByHash(pICargo.itemHash);
                    DataBaseService.Model.DataSetUI.AddPICargoTableRow(pICargo);
                }

                // Заполните таблицу оборудования разрешенными типами точек подвески.
                foreach (HardPointItem hp in FLGameDataService.Model.HardPointList)
                {
                    if (hp.ShipHash == shipArchType)
                    {
                        bool exists = false;
                        foreach (PIEquipment eq in DataBaseService.Model.DataSetUI.PIEquipmentList)
                        {
                            if (eq.itemHardpoint == hp.HPName)
                            {
                                eq.itemAllowedTypes += " " + hp.HPType;
                                exists = true;
                                break;
                            }
                        }
                        if (!exists)
                            DataBaseService.Model.DataSetUI.AddPIEquipmentTableRow(hp.HPName, "", 0, "", hp.HPType);
                    }
                }

                // Заполните таблицу оборудования содержимым файла char, обновив точки крепления.
                foreach (FLDataFileSetting e in FLDataFileService.GetSettings("Player", "base_equip"))
                {
                    uint itemHash = e.UInt(0);
                    string hpName = e.Str(1);

                    bool internalHardPoint = true;
                    if (hpName.Length > 0)
                    {
                        foreach (PIEquipment row in DataBaseService.Model.DataSetUI.PIEquipmentList)
                        {
                            // TODO: Чувствительны ли к регистру имена точек крепления?
                            if (row.itemHardpoint.ToLowerInvariant() == hpName.ToLowerInvariant())
                            {
                                row.itemHash = itemHash;
                                row.itemDescription = FLGameDataService.GetItemDescByHash(itemHash);
                                if (FLGameDataService.GetItemByHash(itemHash) != null)
                                    row.itemGameDataType = FLGameDataService.GetItemByHash(itemHash).ItemType;
                                internalHardPoint = false;
                                break;
                            }
                        }
                    }

                    if (internalHardPoint)
                    {
                        PIEquipment row = new PIEquipment();
                        row.itemHash = itemHash;
                        row.itemDescription = FLGameDataService.GetItemDescByHash(itemHash);
                        row.itemHardpoint = "*" + hpName;
                        if (FLGameDataService.GetItemByHash(itemHash) != null)
                            row.itemGameDataType = FLGameDataService.GetItemByHash(itemHash).ItemType;
                        DataBaseService.Model.DataSetUI.AddPIEquipmentTableRow(row);
                    }
                }

                view.SortFactions();
                view.SortCargo();
                view.SortEquipment();
            }
            catch (Exception ex)
            {
                view.ShowMessage("Error '" + ex.Message + "' when loading " + charRecord.CharPath);
            }

            // Загрузить вкладку просмотра файла.
            // Не загружать текст в текстовую область, если вкладка не выбрана
            view.SelectedTabControlChanged();
        }

        /// <summary>
        /// Обновите фильтр, применяемый к представлению сетки данных списка персонажей.
        /// </summary>
        public void FilterUpdate()
        {
            view.FilterUpdateState(false);

            string filter = null;

            DateTime startTime = DateTime.Now;

            Cursor.Current = Cursors.WaitCursor;

            // Запишите текущие выбранные строки.
            CharacterItem selectedCharRecord = GetCharRecordBySelectedItem();

            var allItems = DataBaseService.Model.DataSetPlayerInfo.CharacterList;
            IEnumerable<CharacterItem> filtered = allItems;

            // удалить символы
            if (!view.GetStateCheckBoxFilterDeleted())
            {
                filter += "(IsDeleted = 'false')";
            }
            // поиск
            if (view.GetTextBoxFilterText().Length > 0)
            {
                string filterText = view.GetTextBoxFilterText().ToLowerInvariant();
                filtered = filtered.Where(c =>
                    (c.CharName ?? "").ToLowerInvariant().Contains(filterText) ||
                    (c.AccID ?? "").ToLowerInvariant().Contains(filterText) ||
                    (c.Location ?? "").ToLowerInvariant().Contains(filterText) ||
                    (c.Ship ?? "").ToLowerInvariant().Contains(filterText) ||
                    (c.CharPath ?? "").ToLowerInvariant().Contains(filterText));
            }// выберите AccDir
            else if (view.GetStateCheckBoxFilterSameAccount())
            {
                filtered = filtered.Where(c => c.AccDir == selectedCharRecord.AccDir);
            }// выберите IP
            else if (view.GetStateCheckBoxFilterSameIP() && selectedCharRecord != null)
            {
                var selectedIPs = DataBaseService.Model.IPList
                    .Where(ip => ip.AccDir == selectedCharRecord.AccDir)
                    .Select(ip => ip.IP)
                    .Distinct()
                    .ToList();

                var accDirs = DataBaseService.Model.IPList
                    .Where(ip => selectedIPs.Contains(ip.IP))
                    .Select(ip => ip.AccDir)
                    .Distinct()
                    .ToList();

                if (!accDirs.Contains(selectedCharRecord.AccDir))
                    accDirs.Add(selectedCharRecord.AccDir);

                filtered = filtered.Where(c => accDirs.Contains(c.AccDir));
            }// select LoginID
            else if (view.GetStateCheckBoxFilterSameLoginID() && selectedCharRecord != null)
            {
                var selectedLoginIDs = DataBaseService.Model.LoginIDList
                 .Where(l => l.AccDir == selectedCharRecord.AccDir)
                 .Select(l => l.LoginID)
                 .Distinct()
                 .ToList();

                var accDirs = DataBaseService.Model.LoginIDList
                    .Where(l => selectedLoginIDs.Contains(l.LoginID))
                    .Select(l => l.AccDir)
                    .Distinct()
                    .ToList();

                if (!accDirs.Contains(selectedCharRecord.AccDir))
                    accDirs.Add(selectedCharRecord.AccDir);

                filtered = filtered.Where(c => accDirs.Contains(c.AccDir));
            }

            // Установка нового списка
            view.SetupCharacterListBindingSource(filtered.ToList());

            // Теперь найдите ранее выбранный символ и выберите его повторно, если это возможно.
            view.SetupOldCharacterItem();

            Cursor.Current = Cursors.Default;

            double opTime = DateTime.Now.Subtract(startTime).TotalMilliseconds;
            if (opTime > 200)
                view.AddLog(String.Format("Warning: filter operation took a {0} ms to complete.", opTime));
        }

        /// <summary>
        /// Фоновый обработчик для загрузки баз данных и игровых данных.
        /// </summary>
        private async Task<DataModel> LoadItAsync(IProgress<ProgressInfo> progress, CancellationToken token)
        {
            try
            {
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.BelowNormal;

                // Загрузите список заблокированных и списки персонажей.
                progress.Report(new(20, "Opening database..."));
                view.AddLog("Opening database...");
                DataModel tempCharsAndBansDataStore = new DataModel();

                using (DataBaseService dataBaseService = new DataBaseService())
                {
                    // При необходимости загрузите данные игры.
                    progress.Report(new(40, "Loading game data..."));
                    view.AddLog("Loading game data...");
                    await Task.Run(async () =>
                    {
                        await dataBaseService.GetGameData(view, tempCharsAndBansDataStore);
                    });
                    if (tempCharsAndBansDataStore.HashListItems.Count == 0)
                    {
                        await Task.Run(async () =>
                        {
                            await FLGameDataService.LoadAllAsync(token, progress, view);
                            await dataBaseService.InsertOrUpdateGameInfoChanges(view, FLGameDataService.Model);
                        });
                        tempCharsAndBansDataStore = FLGameDataService.Model;
                    }

                    token.ThrowIfCancellationRequested();

                    progress.Report(new(60, "Loading player information..."));
                    view.AddLog("Loading player information...");
                    await Task.Run(async () =>
                    {
                        var t1 = dataBaseService.GetBanList(tempCharsAndBansDataStore);
                        var t2 = dataBaseService.GetCharacterList(tempCharsAndBansDataStore);

                        await Task.WhenAll(t1, t2);
                    });
                    view.AddLog("Load complete "
                        + tempCharsAndBansDataStore.DataSetPlayerInfo.CharacterList.Count + " characters, "
                        + tempCharsAndBansDataStore.DataSetPlayerInfo.BanList.Count + " bans");

                    string cmdFile = AppSettings.Default.setAccountDir + "\\cmdfile.sql";
                    if (File.Exists(cmdFile))
                    {
                        try
                        {
                            using (StreamReader sr = new StreamReader(cmdFile))
                            {
                                progress.Report(new(70, "Executing SQL command file..."));
                                view.AddLog("Executing SQL command file...");
                                while (true)
                                {
                                    string st = sr.ReadLine();
                                    if (st == null)
                                        break;
                                    await Task.Run(async () =>
                                    {
                                        await dataBaseService.ExecuteSimpleSQL(st);
                                    });
                                }
                                sr.Close();
                                view.AddLog("Executed SQL command file.");
                            }
                        }
                        catch (Exception ex)
                        {
                            view.AddLog(String.Format("Error '{0}' when executing {1}", ex.Message, cmdFile));
                        }
                    }

                    // For each system add a count of the number of players in it (both docked and in space)
                    progress.Report(new(80, "Updating statistics..."));
                    view.AddLog("Loading statistics...");

                    await Task.Run(async () =>
                    {
                        var t1 = dataBaseService.UpdateGeneralTableStatistics();
                        var t2 = dataBaseService.UpdateCharactersStatistic(FLGameDataService);

                        await Task.WhenAll(t1, t2);
                    });
                }

                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Normal;
                return tempCharsAndBansDataStore;
            }
            catch (Exception ex)
            {
                view.AddLog(String.Format("Error '{0}' at {1}", ex.Message, ex.StackTrace));
                return null;
            }
        }

        /// <summary>
        /// Сохраняйте все ожидающие изменения в базе данных
        /// </summary>
        private async Task DBSaveAsync()
        {
            DbSaveCancelTokenSource = new CancellationTokenSource();
            // Сохраните изменения в базе данных.
            // Поскольку эти изменения невелики, сделайте это из потока gui,
            // но только в том случае, если не запущена фоновая операция.
            view.ReportProgress(50, "Saving database...");
            await Task.Run(async () =>
            {
                await DataBaseService.InsertOrUpdatePlayerInfoChanges(DataBaseService.Model, view);
            });
            view.ReportProgress(0, "OK");

            view.ResetCharListEvent();
            DBUpdatesPending = 0;
            view.SetupDisplayDBText(String.Format("{0}", DBUpdatesPending));

            // Общая статистическая информация. Это занимает некоторое время и выполняется в фоновом режиме
            var progress = new Progress<ProgressInfo>(tuple =>
            {
                view.ReportProgress(tuple.Percent, tuple.Message);
            });
            await Task.Run(async () =>
            {
                await StatisticsUpdateAsync(progress, DbSaveCancelTokenSource.Token);
            });
            view.ReportProgress(0, "Finaly");
            DbSaveCancelTokenSource = null;
        }

        /// <summary>
        /// Создайте статистику.
        /// </summary>
        private async Task StatisticsUpdateAsync(IProgress<ProgressInfo> progress, CancellationToken token)
        {
            try
            {
                await Task.Run(async () =>
                {
                    using (DataBaseService dataBaseService = new DataBaseService())
                    {
                        if (await dataBaseService.ActiveCharacterHistoryExist())
                        {
                            progress.Report(new(30, "Cleaning up history database..."));
                            //TODO: горизонт истории может быть помещен в редактируемое поле gui
                            await dataBaseService.CleanUpCharacterHistory(7);
                            progress.Report(new(30, "Saving history database..."));
                            await dataBaseService.UpdateActiveCharacterHistory();
                            if (!token.IsCancellationRequested)
                                return;
                        }
                    }

                    using (StatisticsGenerator gen = new StatisticsGenerator(FLGameDataService, FLHookSocketService))
                    {
                        progress.Report(new(50, "Generate statistics..."));
                        gen.GenerateGeneralStatistics(progress, view);
                        if (token.IsCancellationRequested)
                            return;

                        gen.GeneratePlayerStats(progress, view);
                        if (token.IsCancellationRequested)
                            return;

                        gen.GenerateFactionActivity(progress, token, view);
                    }
                });
            }
            catch (Exception ex)
            {
                view.AddLog(String.Format("Error '{0}' when saving database", ex.Message));
            }
        }

        public async Task ReloadGameDataAsync()
        {
            if (LoadCancelTokenSource == null)
            {
                await DataBaseService.ClearGameData(view);
                await LoadGameDataAsync();
            }
        }

        /// <summary>
        /// Получить запись списка символов текущей выбранной строки в таблице.
        /// </summary>
        /// <returns>Запись или ноль, если запись не была выбрана</returns>
        public CharacterItem GetCharRecordBySelectedItem()
        {
            var row = view.GetCharRecordBySelectedRow();
            if (string.IsNullOrEmpty(row)) return null;
            return DataBaseService.Model.DataSetPlayerInfo.FindByCharPath(view.GetCharRecordBySelectedRow());
        }

        /// <summary>
        /// Обновить информацию об игроке на сервере если он онлайн
        /// </summary>
        public void UpdateCharacterToServer()
        {
            CharacterItem charRecord = GetCharRecordBySelectedItem();
            Task.Run(() =>
            {
                if (charRecord != null && !charRecord.IsDeleted
                    && FLHookSocketService.CmdIsOnServer(charRecord.CharName))
                {
                    FLHookSocketService.CmdSaveChar(charRecord.CharName);
                }
            });
        }

        public string GetIniFileContents()
        {
            return FLDataFileService.GetIniFileContents();
        }

        public IFLDataFileService GetFLDataFileService()
        {
            return FLDataFileService;
        }

        public bool FLDataFileServiceIsNull()
        {
            return FLDataFileService != null;
        }

        public void BanPlayer(CharacterItem charRecord)
        {
            var banWindow = new CreateBanWindow(BanService, charRecord.AccDir, charRecord.AccID, 
                DataBaseService.Model, null);
            banWindow.ShowDialog();
            banWindow.LogAction += view.AddLog;
        }

        public void UnBanPlayer(CharacterItem charRecord)
        {
            BanService.UnbanAccount(charRecord.AccDir);
        }

        public void KickPlayer(string CharName)
        {
            Task.Run(() =>
            {
                if (!FLHookSocketService.CmdKick(CharName))
                    view.ShowMessage("Warning flhook command failed '" + FLHookSocketService.LastCmdError + "'.");
            });
        }

        public void UpdateLastPlayerTime()
        {
            DateTime now = DateTime.Now;
            long high = (now.ToFileTime() >> 32) & 0xFFFFFFFF;
            long low = now.ToFileTime() & 0xFFFFFFFF;
            FLDataFileService.AddSetting("Player", "tstamp", new object[] { high, low });
            CharService.SaveCharFile(FLDataFileService);
        }

        public void OpenBannedPlayers()
        {
            if (BannedPlayersPresenter == null || BannedPlayersPresenter.IsDisposed)
            {
                var form = new BannedPlayers(); // чистая View, без логики
                BannedPlayersPresenter = new BannedPlayersPresenter(form, BanService, 
                    DataBaseService.Model, this);
            }

            BannedPlayersPresenter.Show();
            BannedPlayersPresenter.BringToFront();
        }

        public void BannedPlayersHighlightRecord(string AccID)
        {
            BannedPlayersPresenter?.HighlightRecord(AccID);
        }

        public void SaveFactionPlayer()
        {
            while (FLDataFileService.DeleteSetting("Player", "house")) ;
            foreach (PIFaction r in DataBaseService.Model.DataSetUI.PIFactionList)
                FLDataFileService.AddSettingNotUnique("Player", "house", new object[] { r.itemRep, r.itemNickname });

            CharService.SaveCharFile(FLDataFileService);
        }

        public void FilterOnAccDir(string dir)
        {
            view.FilterOnAccDir(dir);
        }

        public async Task TestFLHookConnectAsync()
        {
            int load = 0;
            bool npcSpawnEnabled = false;
            string upTime = "";

            // Проверьте командное соединение.
            bool serverUp = false;
            await Task.Run(() =>
            {
                if (FLHookSocketService != null && FLHookSocketService.CmdServerInfo(out load, out npcSpawnEnabled, upTime))
                {
                    serverUp = true;
                }
                if (serverUp && (FLHookSocketService != null))
                {
                    FLHookSocketService.CmdGetPlayers();
                }
            });

            view.SetupHookStateText(serverUp ? "OK " + upTime : "-");

            // Проверьте все счета, которые необходимо проверить.
            List<string> processedAccDirs = new List<string>();
            foreach (KeyValuePair<string, DateTime> value in FLHookListener.PendingAccDirsToCheck)
            {
                if (value.Value < DateTime.Now)
                {
                    string accDir = value.Key;
                    DBUpdatesPending += FLUtility.ScanCharAccount(DataBaseService.Model, DataBaseService,
                        view, FLGameDataService, accDir);
                    DBUpdatesPending += FLUtility.CheckForDeletedChars(DataBaseService.Model.DataSetPlayerInfo,
                        DataBaseService, accDir);
                    processedAccDirs.Add(accDir);
                }
            }
            foreach (string accDir in processedAccDirs)
            {
                FLHookListener.PendingAccDirsToCheck.Remove(accDir);
            }

            // Проверьте, не запрашивает ли другое приложение проверку счетов игроков
            RegistryKey sk1 = Registry.CurrentUser.OpenSubKey("Software\\" + Application.ProductName, true);
            if (sk1 != null)
            {
                string value = sk1.GetValue("AutocleanPending", 0).ToString();
                if (value == "1")
                {
                    view.AddLog("Executing registry triggered account scan");
                    sk1.SetValue("AutocleanPending", 0);
                    await RescanAccountFilesAsync();
                }
            }

            // Обновление отображения обновления БД
            view.SetupDisplayDBText(String.Format("{0}", DBUpdatesPending));
        }

        public void OpenChangeLocation()
        {
            new ChangeLocationWindow(CharService, FLGameDataService, FLDataFileService).ShowDialog();
        }

        public void OpenChangeShip()
        {
            new ChangeShipWindow(CharService, FLGameDataService, FLDataFileService).ShowDialog();
        }

        public void OpenAddCargoPlayer(PICargo pICargo)
        {
            new AddCargoWindow(FLGameDataService, DataBaseService.Model.DataSetUI.PICargoList, pICargo).ShowDialog();
        }

        public void OpenAddEquipment(PIEquipment item)
        {
            new AddEquipmentWindow(FLGameDataService, DataBaseService.Model.DataSetUI.PIEquipmentList, item).ShowDialog();
        }

        public void OpenAbout()
        {
            new AboutWindow().ShowDialog();
        }

        public void SearchForAccountsByIP()
        {
            new SearchForAccountsByIPWindow(view, DataBaseService).Show();
        }

        public void IniFile()
        {
            new IniFileWindow().Show();
        }

        public void DecodeAllFiles()
        {
            new DecodeAllFilesWindow().Show();
        }

        public void AddCompleteMap()
        {
            while (FLDataFileService.DeleteSetting("Player", "visit")) ;
            foreach (HashListItem r in FLGameDataService.Model.HashListItems)
            {
                if (r.ItemType == FLGameDataService.GAMEDATA_OBJECT)
                {
                    FLDataFileService.AddSettingNotUnique("Player", "visit", new object[] { r.ItemHash, 63 });
                }
                else if (r.ItemType == FLGameDataService.GAMEDATA_ZONE)
                {
                    FLDataFileService.AddSettingNotUnique("Player", "visit", new object[] { r.ItemHash, 41 });
                }
                else if (r.ItemType == FLGameDataService.GAMEDATA_BASES)
                {
                    FLDataFileService.AddSettingNotUnique("Player", "visit", new object[] { r.ItemHash, 41 });
                }
                else if (r.ItemType == FLGameDataService.GAMEDATA_SYSTEMS)
                {
                    FLDataFileService.AddSettingNotUnique("Player", "visit", new object[] { r.ItemHash, 41 });
                }
            }

            while (FLDataFileService.DeleteSetting("mPlayer", "sys_visited")) ;
            foreach (HashListItem r in FLGameDataService.Model.HashListItems)
            {
                if (r.ItemType == FLGameDataService.GAMEDATA_SYSTEMS)
                {
                    FLDataFileService.AddSettingNotUnique("mPlayer", "sys_visited", new object[] { r.ItemHash });
                }
            }

            while (FLDataFileService.DeleteSetting("mPlayer", "base_visited")) ;
            foreach (HashListItem r in FLGameDataService.Model.HashListItems)
            {
                if (r.ItemType == FLGameDataService.GAMEDATA_BASES)
                {
                    FLDataFileService.AddSettingNotUnique("mPlayer", "base_visited", new object[] { r.ItemHash });
                }
            }

            CharService.SaveCharFile(FLDataFileService);
        }

        public void OpenStatistics()
        {
            if (StatisticsPresenter == null || StatisticsPresenter.IsDisposed)
            {
                var form = new StatisticsWindow(); // чистая View, без логики
                StatisticsPresenter = new StatisticsPresenter(form, DataBaseService);
            }

            StatisticsPresenter.Show();
            StatisticsPresenter.BringToFront();
        }

        public void OpenHash()
        {
            if (HashPresenter == null || HashPresenter.IsDisposed)
            {
                var form = new HashWindow();
                HashPresenter = new HashPresenter(form, FLGameDataService);
            }

            HashPresenter.Show();
            HashPresenter.BringToFront();
        }

        public void RemoveCargoPlayer(PICargo item)
        {
            DataBaseService.Model.DataSetUI.PICargoList.Remove(item);
        }

        public void RemoveEquipmentPlayer(PIEquipment item)
        {
            if (item.itemHardpoint.StartsWith("*"))
            {
                DataBaseService.Model.DataSetUI.PIEquipmentList.Remove(item);
            }
            else
            {
                item.itemHash = 0;
                item.itemDescription = "";
            }
        }

        public void SaveCargoPlayer()
        {
            // Удалить все строки грузов и базовых грузов
            while (FLDataFileService.DeleteSetting("Player", "base_cargo")) ;
            while (FLDataFileService.DeleteSetting("Player", "cargo")) ;

            //Замените их содержимым таблицы.
            foreach (PICargo r in DataBaseService.Model.DataSetUI.PICargoList)
                FLDataFileService.AddSettingNotUnique("Player", "cargo",
                    new object[] { r.itemHash, r.itemCount, "", 1, 0 });
            foreach (PICargo r in DataBaseService.Model.DataSetUI.PICargoList)
                FLDataFileService.AddSettingNotUnique("Player", "base_cargo", 
                    new object[] { r.itemHash, r.itemCount, "", 1, 0 });

            CharService.SaveCharFile(FLDataFileService);
        }

        public void SaveEquipmentPlayer()
        {
            // Удалить все строки грузов и базовых грузов
            while (FLDataFileService.DeleteSetting("Player", "base_equip")) ;
            while (FLDataFileService.DeleteSetting("Player", "equip")) ;

            // Замените их содержимым таблицы.
            // Обратите внимание, что если hardpoint является "специальным",
            // начинающимся с '*', мы не записываем пустую строку обратно в charfile.
            foreach (PIEquipment r in DataBaseService.Model.DataSetUI.PIEquipmentList)
            {
                if (r.itemHash != 0)
                    FLDataFileService.AddSettingNotUnique("Player", "equip", new object[] { r.itemHash, (r.itemHardpoint.StartsWith("*") ? "" : r.itemHardpoint), 1 });
            }
            foreach (PIEquipment r in DataBaseService.Model.DataSetUI.PIEquipmentList)
            {
                if (r.itemHash != 0)
                    FLDataFileService.AddSettingNotUnique("Player", "base_equip", new object[] { r.itemHash, (r.itemHardpoint.StartsWith("*") ? "" : r.itemHardpoint), 1 });
            }

            CharService.SaveCharFile(FLDataFileService);
        }

        public void SaveDataFile(string text)
        {
            IFLDataFileService editedCharFile = new FLDataFileService(Encoding.Default.GetBytes(text), FLDataFileService.filePath, true);
            editedCharFile.SaveSettings(FLDataFileService.filePath, AppSettings.Default.setWriteEncryptedFiles);
        }

        public void PlayerInfoSaveAdmin(string CharPath, string note)
        {
            FLUtility.SetPlayerInfoAdminNote(AppSettings.Default.setAccountDir, CharPath,
              note);
        }

        #region CharService

        public void SetSelectedCharRecord(CharacterItem newSelectedCharRecord)
        {
            CharService.SetSelectedCharRecord(newSelectedCharRecord);
        }

        public CharacterItem GetSelectedCharRecord()
        {
            return CharService.GetSelectedCharRecord();
        }

        public void RenameCharacter(string newName)
        {
            CharService.RenameCharacter(newName);
        }

        public void ChangeMoneyCharacter(uint newMoney)
        {
            CharService.ChangeMoneyCharacter(newMoney);
        }

        public void DeleteChar(string charName, string charPath)
        {
            CharService.DeleteChar(charName, charPath);
        }

        public async Task<string> PrettyPrintCharFile()
        {
            var data = "";
            await Task.Run(async () =>
            {
                data = await CharService.PrettyPrintCharFile(FLGameDataService, FLDataFileService);
            });
            return data;
        }

        public void CheckFileCharacter()
        {
            CharService.CheckFileCharacter();
        }

        public void PropertiesUpdateSettings()
        {
            FLGameDataService.UpdateFLDirectory(AppSettings.Default.setFLDir);
        }

        #endregion
    }
}
