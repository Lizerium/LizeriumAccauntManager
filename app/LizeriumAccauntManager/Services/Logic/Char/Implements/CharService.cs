/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 29 апреля 2026 06:52:59
 * Version: 1.0.25
 */

using Root.Components;
using System;
using System.IO;
using Root.App;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Root.Services
{
    public class CharService : ICharService
    {
        public readonly IMainView view;

        /// <summary>
        /// Служба доступа к базе данных.
        /// </summary>
        private IDataBaseService DataBaseService { get; set; }
        /// <summary>
        /// Коммуникационный сокет для выполнения команд flhook.
        /// </summary>
        private IFLHookSocketService FLHookSocketService { get; set; }
        /// <summary>
        /// Текущий загруженный файл символов
        /// </summary>
        private IFLDataFileService FLDataFileService { get; set; }
        /// <summary>
        /// Игровые данные, включая инфокарты.
        /// </summary>
        private IFLGameDataService FLGameDataService { get; set; }
        /// <summary>
        /// Обновлять область информации об игроке при выборе строки в сетке данных списка персонажей.
        /// </summary>
        private CharacterItem SelectedCharRecord { get; set; } = null;

        public CharService(IMainView view, IDataBaseService dataBaseService, 
            IFLHookSocketService fLHookSocketService, IFLDataFileService fLDataFileService, 
            IFLGameDataService fLGameDataService)
        {
            this.view = view;
            DataBaseService = dataBaseService;
            FLHookSocketService = fLHookSocketService;
            FLDataFileService = fLDataFileService;
            FLGameDataService = fLGameDataService;
        }

        /// <summary>
        /// Сохраните текущий файл ini игрока на диск. 
        /// Отображает диалоговое окно при неудаче. 
        /// Если подключен к flhook, то игрок будет выгнан и забанен при внесении изменений.
        /// </summary>
        public void SaveCharFile(IFLDataFileService charFile)
        {
            view.SetupWaitCursor();

            try
            {
                string charName = charFile.GetSetting("Player", "Name").UniStr(0);

                // Если игрок залогинился на другом персонаже,
                // то мы не можем выгнать его с помощью этого файла персонажа.
                // У нас есть кик того, на котором он залогинился.
                string charPath = charFile.filePath.Substring(AppSettings.Default.setAccountDir.Length + 1);
                CharacterItem charRecord = DataBaseService.Model.DataSetPlayerInfo.FindByCharPath(charPath);
                if (charRecord != null)
                {
                    var charRecords = DataBaseService.Model.DataSetPlayerInfo.CharacterList
                          .Where(c => c.AccDir == charRecord.AccDir && !c.IsDeleted)
                          .ToList();
                    foreach (CharacterItem accCharRecord in charRecords)
                    {
                        if (FLHookSocketService.CmdIsOnServer(accCharRecord.CharName))
                            FLHookSocketService.CmdKick(accCharRecord.CharName);
                    }

                    foreach (CharacterItem accCharRecord in charRecords)
                    {
                        if (!FLHookSocketService.CmdBan(accCharRecord.CharName))
                            view.ShowMessage("Warning flhook command failed \"" + FLHookSocketService.LastCmdError + "\". Creating ban file in account anyway.");
                        break;
                    }
                }

                FLHookSocketService.CmdBan(charName);

                charFile.SaveSettings(charFile.filePath, AppSettings.Default.setWriteEncryptedFiles);

                FLHookSocketService.CmdUnban(charName);
            }
            catch (Exception ex)
            {
                view.ShowMessage("Error '" + ex.Message + "' when saving " + FLDataFileService.filePath);
            }

            view.RestoreCursor();
            view.SetupUpdatePlayerInfoTimerState(true);
        }

        public void RenameCharacter(string newName)
        {
            string currentName = FLDataFileService.GetSetting("Player", "name").UniStr(0);

            if (!FLHookSocketService.CmdRename(currentName, newName))
            {
                view.ShowMessage("Error flhook command failed '" + FLHookSocketService.LastCmdError + "'");
            }
        }

        public void ChangeMoneyCharacter(uint newMoney)
        {
            FLDataFileService.AddSetting("Player", "money", new object[] { newMoney });
            SaveCharFile(FLDataFileService);
        }

        public async Task<string> PrettyPrintCharFile(IFLGameDataService fLGameDataService, IFLDataFileService fLDataFileService)
        {
            return FLUtility.PrettyPrintCharFile(fLGameDataService, fLDataFileService);
        }

        public CharacterItem GetSelectedCharRecord()
        {
            return SelectedCharRecord;
        }

        public void SetSelectedCharRecord(CharacterItem item)
        {
            SelectedCharRecord = item;
        }

        /// <summary>
        /// Удалить указанный символ
        /// </summary>
        /// <param name="charName">string</param>
        /// <param name="charPath">string</param>
        public void DeleteChar(string charName, string charPath)
        {
            if (!FLHookSocketService.CmdDeleteChar(charName))
                view.ShowMessage("Warning flhook command failed '" + FLHookSocketService.LastCmdError + "'. Deleting player file from account anyway.");
            try
            {
                File.Delete(Path.Combine(AppSettings.Default.setAccountDir, charPath));
            }
            catch (Exception ex)
            {
                view.AddLog(ex.Message);
            }
        }

        public void CheckFileCharacter()
        {
            if (FLUtility.CheckCharFile(DataBaseService.Model, view, FLGameDataService, 
                FLDataFileService, false, false))
            {
                if (MessageBox.Show("See the log tab for details. Fix errors?", "Errors Detected",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FLUtility.CheckCharFile(DataBaseService.Model, view, FLGameDataService, 
                        FLDataFileService, false, true);
                    SaveCharFile(FLDataFileService);
                }
            }
        }
    }
}
