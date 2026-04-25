/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 25 апреля 2026 08:11:28
 * Version: 1.0.21
 */

using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using Root.Components;

namespace Root.Services
{
    public class BanService : IBanService
    {
        private IDataBaseService DataBaseService { get; set; }
        private IFLHookSocketService FLHookSocketService { get; set; }
        public Action<string> SendMessageAction { get; set; }

        public BanService(IDataBaseService databaseService, 
            IFLHookSocketService fLHookSocketService)
        {
            DataBaseService = databaseService;
            FLHookSocketService = fLHookSocketService;
        }

        public void BanAccount(BanRequest request)
        {
            // Добавить/изменить запись о бане.
            BanItem banRecord = DataBaseService.Model.DataSetPlayerInfo.FindBanByAccDir(request.AccDir);
            if (banRecord == null)
            {
                banRecord = new BanItem();
                banRecord.AccDir = request.AccDir;
                banRecord.AccID = request.AaccID;
                banRecord.BanReason = request.BanReason;
                banRecord.BanStart = request.BanStart;
                banRecord.BanEnd = request.BanEnd;
                DataBaseService.Model.DataSetPlayerInfo.BanList.Add(banRecord);
            }
            else
            {
                banRecord.BanReason = request.BanReason;
                banRecord.BanStart = request.BanStart;
                banRecord.BanEnd = request.BanEnd;
            }

            // Запретить использование FLHook, чтобы список учетных записей FLServer оставался точным.
            var charRecords = DataBaseService.Model.DataSetPlayerInfo.CharacterList
                .Where(c => c.AccDir == request.AccDir && !c.IsDeleted)
                .ToList();
            foreach (CharacterItem charRecord in charRecords)
            {
                if (FLHookSocketService.CmdIsOnServer(charRecord.CharName))
                    FLHookSocketService.CmdKick(charRecord.CharName);
            }

            foreach (CharacterItem charRecord in charRecords)
            {
                if (!FLHookSocketService.CmdBan(charRecord.CharName))
                    SendMessageAction.Invoke("Warning flhook command failed \"" + FLHookSocketService.LastCmdError + "\". Creating ban file in account anyway.");
                break;
            }

            // Запишите файл бана вручную в качестве резервной копии на flhook.
            string banFilePath = AppSettings.Default.setAccountDir + "\\" + request.AccDir + Path.DirectorySeparatorChar + "banned";
            try
            {
                File.Create(banFilePath).Close();
            }
            catch (Exception ex)
            {
                SendMessageAction.Invoke("Create ban file failed '" + ex.Message + "'");
            }
        }

        public void UnbanAccount(string accDir)
        {
            // Удалить запись о бане.
            BanItem banRecord = DataBaseService.Model.DataSetPlayerInfo.FindBanByAccDir(accDir);
            if (banRecord != null)
                DataBaseService.DeleteBanRecordFromDatabase(accDir);

            // Разблокируйте с помощью FLHook, чтобы список учетных записей FLServer оставался точным.
            var charRecords = DataBaseService.Model.DataSetPlayerInfo.CharacterList
                .Where(c => c.AccDir == accDir && !c.IsDeleted)
                .ToList();
            foreach (CharacterItem charRecord in charRecords)
            {
                if (!FLHookSocketService.CmdUnban(charRecord.CharName))
                {
                    SendMessageAction.Invoke("Warning flhook command failed \"" + FLHookSocketService.LastCmdError + "\". Removing ban file from account anyway.");
                }
                break;
            }

            // Удалите файл бана вручную в качестве резервной копии для flhook.
            string banFilePath = AppSettings.Default.setAccountDir + "\\" + accDir + Path.DirectorySeparatorChar + "banned";
            try
            {
                File.Delete(banFilePath);
            }
            catch { }
        }
    }
}
