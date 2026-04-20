/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 20 апреля 2026 03:07:02
 * Version: 1.0.15
 */

using static Root.Services.FLHookSocketService;
using System.Collections.Generic;

namespace Root.Services
{
    public interface IFLHookSocketService
    {
        string LastCmdError { get; set; }
        Dictionary<int, PlayerInfo> PlayerInfoList { get; set; }
        bool CmdBan(string charName);
        bool CmdDeleteChar(string charName);
        bool CmdGetPlayers();
        bool CmdIsOnServer(string charName);
        bool CmdKick(string charName);
        bool CmdRename(string currentName, string newName);
        bool CmdSaveChar(string charName);
        bool CmdServerInfo(out int load, out bool npcSpawnEnabled, string upTime);
        bool CmdUnban(string charName);
        void Shutdown();
    }
}
