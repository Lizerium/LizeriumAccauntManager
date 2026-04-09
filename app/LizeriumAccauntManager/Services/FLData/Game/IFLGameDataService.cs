/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 09 апреля 2026 10:58:05
 * Version: 1.0.3
 */

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Root.Components;

namespace Root.Services
{
    public interface IFLGameDataService
    {
        DataModel Model { get; set; }
        Dictionary<uint, uint> InfocardMap { get; set; }
        List<IntPtr> VDLLs { get; set; }
        string FLDirectory { get; set; }
        string GAMEDATA_BASES { get; }
        string GAMEDATA_SYSTEMS { get; }
        string GAMEDATA_SHIPS { get; }
        string GAMEDATA_CARGO { get; }
        string GAMEDATA_GUNS { get; }
        string GAMEDATA_MINES { get; }
        string GAMEDATA_TURRETS { get; }
        string GAMEDATA_PROJECTILES { get; }
        string GAMEDATA_SHIELDS { get; }
        string GAMEDATA_THRUSTERS { get; }
        string GAMEDATA_CM { get; }
        string GAMEDATA_LIGHTS { get; }
        string GAMEDATA_MISC { get; }
        string GAMEDATA_SCANNERS { get; }
        string GAMEDATA_TRACTORS { get; }
        string GAMEDATA_ENGINES { get; }
        string GAMEDATA_ARMOR { get; }
        string GAMEDATA_POWERGEN { get; }
        string GAMEDATA_CLOAK { get; }
        string GAMEDATA_FX { get; }
        string GAMEDATA_SOUND { get; }
        string GAMEDATA_ZONE { get; }
        string GAMEDATA_OBJECT { get; }
        ConcurrentDictionary<long, HashListItem> ThreadSafeHashListModel { get; set; }

        void UpdateFLDirectory(string FLDirectory);
        string GetItemDescByFactionNickName(string nick);
        string GetItemDescByNickNameX(string nick);
        string GetItemDescByHash(long itemHash);
        HashListItem GetItemByHash(long itemHash);
        HashListItem GetItemByNickName(string nick);
        EquipInfoItem GetEquipInfo(uint hash);
        ShipInfoItem GetShipInfo(uint hash);
        HardPointItem[] GetHardPointListByShip(uint shipHash);
        Task LoadInfocardMap(ILogRecorder log, CancellationToken token);
        Task LoadDLLS(CancellationToken token);
        Task ParseBaseAndSystemsEntries(IProgress<ProgressInfo> progress, ILogRecorder log, CancellationToken token);
        Task ParseEquipmentEntries(IProgress<ProgressInfo> progress, ILogRecorder log, CancellationToken token);
        Task ParseFactionEntries(IProgress<ProgressInfo> progress, ILogRecorder log, CancellationToken token);
        Task ParseShipEntries(IProgress<ProgressInfo> progress, ILogRecorder log, CancellationToken token);
        Task ParseGoodEntries(IProgress<ProgressInfo> progress, ILogRecorder log, CancellationToken token);
        Task LoadAllAsync(CancellationToken token, IProgress<ProgressInfo> progress,ILogRecorder log);
    }
}
