/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 28 апреля 2026 14:26:16
 * Version: 1.0.24
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using Root.Components;
using System.Linq;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;
using Root.Components.GameData;
using System.Collections;
using System.Windows.Forms;
using System.Reflection;

namespace Root.Services
{
    public class FLGameDataService : IFLGameDataService
    {
        public DataModel Model { get; set; }
        public ConcurrentDictionary<long, HashListItem> ThreadSafeHashListModel { get; set; } = new ConcurrentDictionary<long, HashListItem>();
        public FLDataFileService flIni { get; set; } = null;
        public Dictionary<uint, uint> InfocardMap { get; set; } = new Dictionary<uint, uint>();
        /// <summary>
        /// Resource dlls containing strings.
        /// </summary>
        public List<IntPtr> VDLLs { get; set; } = new List<IntPtr>();
        public string FLDirectory { get; set; }
        public string flExePath { get; set; }
        public string flDataPath {  get; set; }
        public string GAMEDATA_AMMO { get; } = "ammo";
        public string GAMEDATA_BASES { get; } = "bases";
        public string GAMEDATA_CARGO { get; } = "cargo";
        public string GAMEDATA_CM { get; } = "countermeasures";
        public string GAMEDATA_ENGINES { get; } = "engines";
        public string GAMEDATA_FACTIONS { get; } = "factions";
        public string GAMEDATA_GUNS { get; } = "guns";
        public string GAMEDATA_LIGHTS { get; } = "lights";
        public string GAMEDATA_ARMOR { get; } = "armor";
        public string GAMEDATA_MINES { get; } = "mines";
        public string GAMEDATA_MISC { get; } = "miscequipment";
        public string GAMEDATA_FX { get; } = "fx";
        public string GAMEDATA_CLOAK { get; } = "cloak";
        public string GAMEDATA_SOUND { get; } = "sound";
        public string GAMEDATA_POWERGEN { get; } = "powergenerators";
        public string GAMEDATA_PROJECTILES { get; } = "projectiles";
        public string GAMEDATA_SCANNERS { get; } = "scanners";
        public string GAMEDATA_SHIELDS { get; } = "shields";
        public string GAMEDATA_SHIPS { get; } = "ships";
        public string GAMEDATA_SYSTEMS { get; } = "systems";
        public string GAMEDATA_THRUSTERS { get; } = "thrusters";
        public string GAMEDATA_TRACTORS { get; } = "tractorbeams";
        public string GAMEDATA_TURRETS { get; } = "turrets";
        public string GAMEDATA_GEN { get; } = "genhash";
        public string GAMEDATA_OBJECT { get; } = "object";
        public string GAMEDATA_ZONE { get; } = "zone";

        public FLGameDataService(string FLDirectory)
        {
            UpdateFLDirectory(FLDirectory);

            Model = new DataModel();
        }

        public void UpdateFLDirectory(string FLDirectory)
        {
            this.FLDirectory = FLDirectory;
            flExePath = Path.Combine(FLDirectory, "EXE");
            if (Directory.Exists(flExePath))
            {
                var pathFreelancerIni = Path.Combine(flExePath, "Freelancer.ini");
                if (File.Exists(pathFreelancerIni))
                {
                    flIni = new FLDataFileService(pathFreelancerIni, true);
                    flDataPath = Path.GetFullPath(Path.Combine(flExePath, flIni.GetSetting("Freelancer", "data path").Str(0)));
                }
            }
        }

        /// <summary>
        /// Unmanaged functions to access libraries
        /// </summary>
        static int DONT_RESOLVE_DLL_REFERENCES = 0x00000001;
        static int LOAD_LIBRARY_AS_DATAFILE = 0x00000002;
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr LoadLibraryExA(string lpLibFileName, int hFile, int dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int LoadString(IntPtr hInstance, int uID, byte[] lpBuffer, int nBufferMax);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern int LoadString(IntPtr hInstance, int uID, [Out] char[] lpBuffer, int nBufferMax);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int FreeLibrary(IntPtr hInstance);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int LockResource(int hResData);

        [DllImport("kernel32.dll")]
        static extern IntPtr FindResource(IntPtr hModule, int lpID, int lpType);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr LoadResource(IntPtr hModule, IntPtr hResInfo);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int SizeofResource(IntPtr hModule, IntPtr hResInfo);


        private ConcurrentDictionary<long, EquipInfoItem> ThreadSafeEquipInfoListModel = new ConcurrentDictionary<long, EquipInfoItem>();
        private ConcurrentBag<HardPointItem> ThreadSafeHardPointListModel = new ConcurrentBag<HardPointItem>();
        private ConcurrentDictionary<ThreadSearchHardpointItem, HardPointItem> ThreadSafeHardPointDict = new();
        private ConcurrentBag<ShipInfoItem> ThreadSafeShipListModel = new ConcurrentBag<ShipInfoItem>();


        private void LoadLibrary(string dllPath)
        {
            IntPtr hInstance = LoadLibraryExA(dllPath, 0, DONT_RESOLVE_DLL_REFERENCES | LOAD_LIBRARY_AS_DATAFILE);
            if (hInstance != null)
                VDLLs.Add(hInstance);
        }

        /// <summary>
        /// Return the string for the specified IDS. Note that this function
        /// works only for ascii strings.
        /// </summary>
        /// <param name="iIDS"></param>
        /// <returns>The string or null if it cannot be found.</returns>
        private string GetIDString(uint iIDS)
        {
            int iDLL = (int)(iIDS / 0x10000);
            int resId = (int)iIDS - (iDLL * 0x10000);

            if (VDLLs.Count > iDLL)
            {
                IntPtr hInstance = VDLLs[iDLL];
                if (hInstance != IntPtr.Zero)
                {
                    char[] buffer = new char[10000];
                    int len = LoadString(hInstance, resId, buffer, buffer.Length);
                    if (len > 0)
                        return new string(buffer, 0, len);

                    IntPtr hFindRes = FindResource(hInstance, resId, 23);
                    if (hFindRes != IntPtr.Zero)
                    {
                        IntPtr resContent = LoadResource(hInstance, hFindRes);
                        if (resContent != IntPtr.Zero)
                        {
                            int size = SizeofResource(hInstance, hFindRes);
                            byte[] bufInfo = new byte[size];
                            Marshal.Copy(resContent, bufInfo, 0, (int)size);
                            return Encoding.Unicode.GetString(bufInfo, 0, size);
                        }
                    }
                }
            }

            return null;
        }


        /// <summary>
        /// Загрузить все игровые данные.
        /// </summary>
        public async Task LoadAllAsync(CancellationToken token,
            IProgress<ProgressInfo> progress,
            ILogRecorder log)
        {
            Model.Clear();
            ThreadSafeHashListModel.Clear();

            // Load the infocard map
            var taskLoadInfocardMap = LoadInfocardMap(log, token);
            // Load the string dlls.
            var taskLoadDLLS = LoadDLLS(token);

            await Task.WhenAll(taskLoadInfocardMap, taskLoadDLLS);
            // Scan ini files and parse system and base entries
            var taskParseBaseAndSystemsEntries = ParseBaseAndSystemsEntries(progress, log, token);
            // Scan ini files and parse equipment entries.
            var taskParseEquipmentEntries = ParseEquipmentEntries(progress, log, token);
            // Scan ini files and parse faction entries
            var taskParseFactionEntries = ParseFactionEntries(progress, log, token);
            // Scan ship files and parse ship entries
            var taskParseShipEntries = ParseShipEntries(progress, log, token);
            log.AddLog($"Hashes ::{ThreadSafeHashListModel.Count}");
            await Task.WhenAll(taskParseBaseAndSystemsEntries, taskParseEquipmentEntries, taskParseFactionEntries, taskParseShipEntries);
            log.AddLog($"Hashes before ParseGoodEntries::{ThreadSafeHashListModel.Count}");
            // Scan goods.ini to retrieve "default" loadouts for the ships.
            var taskParseGoodEntries = ParseGoodEntries(progress, log, token);
            log.AddLog($"Hashes ParseGoodEntries::{ThreadSafeHashListModel.Count}");

            // Load the ion-cross game data files and override the description fields with the
            // ioncross descriptions.
            string ioncrossDir = ""; // AppSettings.Default.setIonCrossDir;
            if (ioncrossDir.Length != 0)
            {
                if (token.IsCancellationRequested)
                    return;

                progress.Report(new(100, "Scanning IFSO game data files..."));
                //LoadIonCrossNickNameDesc(ThreadSafeModel, ioncrossDir + "\\" + "GAMEDATA_bases.txt", log, true);
                //LoadIonCrossNickNameDesc(ThreadSafeModel, ioncrossDir + "\\" + "GAMEDATA_systems.txt", log, true);
                //LoadIonCrossNickNameDesc(ThreadSafeModel, ioncrossDir + "\\" + "GAMEDATA_factions.txt", log, true);
                //LoadIonCrossHashDesc(ThreadSafeModel, ioncrossDir + "\\" + "GAMEDATA_ammo.txt", log);
                //LoadIonCrossHashDesc(ThreadSafeModel, ioncrossDir + "\\" + "GAMEDATA_cargo.txt", log);
                //LoadIonCrossHashDesc(ThreadSafeModel, ioncrossDir + "\\" + "GAMEDATA_countermeasures.txt", log);
                //LoadIonCrossHashDesc(ThreadSafeModel, ioncrossDir + "\\" + "GAMEDATA_engines.txt", log);
                //LoadIonCrossHashDesc(ThreadSafeModel, ioncrossDir + "\\" + "GAMEDATA_guns.txt", log);
                //LoadIonCrossHashDesc(ThreadSafeModel, ioncrossDir + "\\" + "GAMEDATA_lights.txt", log);
                //LoadIonCrossHashDesc(ThreadSafeModel, ioncrossDir + "\\" + "GAMEDATA_mines.txt", log);
                //LoadIonCrossHashDesc(ThreadSafeModel, ioncrossDir + "\\" + "GAMEDATA_miscequipment.txt", log);
                //LoadIonCrossHashDesc(ThreadSafeModel, ioncrossDir + "\\" + "GAMEDATA_powergenerators.txt", log);
                //LoadIonCrossHashDesc(ThreadSafeModel, ioncrossDir + "\\" + "GAMEDATA_projectiles.txt", log);
                //LoadIonCrossHashDesc(ThreadSafeModel, ioncrossDir + "\\" + "GAMEDATA_scanners.txt", log);
                //LoadIonCrossHashDesc(ThreadSafeModel, ioncrossDir + "\\" + "GAMEDATA_shields.txt", log);
                //LoadIonCrossHashDesc(ThreadSafeModel, ioncrossDir + "\\" + "GAMEDATA_ships.txt", log);
                //LoadIonCrossHashDesc(ThreadSafeModel, ioncrossDir + "\\" + "GAMEDATA_thrusters.txt", log);
                //LoadIonCrossHashDesc(ThreadSafeModel, ioncrossDir + "\\" + "GAMEDATA_tractorbeams.txt", log);
                //LoadIonCrossHashDesc(ThreadSafeModel, ioncrossDir + "\\" + "GAMEDATA_turrets.txt", log);
            }

            // Большое грязное сканирование ini-файлов на предмет хэш-кодов во всех ini-файлах для создания общего списка хэш-кодов.
            // Сюда входят хэш-коды в файлах, которые могут фактически использоваться (включены из freelancer.ini).
            // Я попытался использовать информацию в разделе данных freelancer.ini, но не смог найти,
            // где упоминаются ini-файлы миссии.
            //ScanForHashCodes(DataStore, flDataPath, bgw, log);

            var taskScanForHashCodesParallel = ScanForHashCodesParallel(ThreadSafeHashListModel, token, progress, log);
           
            log.AddLog($"Hashes befor parallel task add::{ThreadSafeHashListModel.Count}");
            await Task.WhenAll(taskParseGoodEntries, taskScanForHashCodesParallel);
            log.AddLog($"Hashes after parallel task add::{ThreadSafeHashListModel.Count}");

            foreach (IntPtr hInstance in VDLLs)
                FreeLibrary(hInstance);
            VDLLs.Clear();

            Model.HashListItems.AddRange(ThreadSafeHashListModel.Values.ToList());
        }

        public async Task ParseGoodEntries(IProgress<ProgressInfo> progress, ILogRecorder log, CancellationToken token)
        {
            if (string.IsNullOrEmpty(flDataPath))
            {
                log.AddLog("ParseGoodEntries::flDataPath is null");
                return;
            }
            try
            {
                FLDataFileService flGoodsIni = new FLDataFileService(flDataPath + "\\equipment\\goods.ini", true);
                progress.Report(new(50, "Loading loadouts"));

                var shipSections = flGoodsIni.Sections
                    .Where(s => s.sectionName.Equals("good", StringComparison.OrdinalIgnoreCase)
                             && s.GetSetting("category").Str(0) == "ship")
                    .ToList();
                var counter = 0;
                var maxFiles = shipSections.ToList().Count;
                await Task.Run(() =>
                {
                    Parallel.ForEach(shipSections, new ParallelOptions 
                    { CancellationToken = token,
                    MaxDegreeOfParallelism = 2
                    }, section =>
                    {
                        AddFXFromGoods(flGoodsIni, section, log, token);
                        if (Interlocked.Increment(ref counter) % 50 == 0)
                        {
                            progress?.Report(new ProgressInfo(75, $"Read {counter}/{maxFiles} ships..."));
                        }
                    });
                }, token);

                Model.ShipInfoList = ThreadSafeShipListModel.ToList();
                Model.HardPointList = ThreadSafeHardPointDict.Values.ToList();
            }
            catch (Exception e)
            {
                log.AddLog("Error '" + e.Message + "' when parsing '" + flDataPath + "\\equipment\\goods.ini");
            }
        }

        public async Task ParseGoodEntriesOld(IProgress<ProgressInfo> progress, ILogRecorder log, CancellationToken token)
        {
            if (string.IsNullOrEmpty(flDataPath))
            {
                log.AddLog("ParseGoodEntriesOld::flDataPath is null");
                return;
            }
            try
            {
                FLDataFileService flGoodsIni = new FLDataFileService(flDataPath + "\\equipment\\goods.ini", true);
                progress.Report(new(50, "Loading loadouts"));
                for (int si = 0; si < flGoodsIni.Sections.Count; si++)
                {
                    if (token.IsCancellationRequested)
                        return;
                    FLDataFileSection section = flGoodsIni.Sections[si];
                    if (section.sectionName.ToLowerInvariant() == "good")
                    {
                        if (section.GetSetting("category").Str(0) == "ship")
                        {
                            string hullNickName = section.GetSetting("hull").Str(0);
                            AddFXFromGoodsOld(flGoodsIni, section, log);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.AddLog("Error '" + e.Message + "' when parsing '" + flDataPath + "\\equipment\\goods.ini");
            }
        }

        public async Task ParseShipEntries(IProgress<ProgressInfo> progress, ILogRecorder log, CancellationToken token)
        {
            if (flIni == null)
            {
                log.AddLog("ParseShipEntries::flIni is null");
                return;
            }
            var options = new ParallelOptions
            {
                CancellationToken = token,
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };

            Parallel.ForEach(flIni.GetSettings("Data", "ships"), options, flIniEntry =>
            {
                if (token.IsCancellationRequested)
                    return;
                progress.Report(new(30, "Loading ships: " + flIniEntry.Str(0)));
                string iniPath = flDataPath + Path.DirectorySeparatorChar + flIniEntry.Str(0);
                try
                {
                    FLDataFileService ini = new FLDataFileService(iniPath, true);

                    foreach (FLDataFileSection section in ini.Sections)
                    {
                        if (section.sectionName.ToLowerInvariant() == "ship")
                        {
                            AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_SHIPS, true);

                            string nickName = section.GetSetting("nickname").Str(0);
                            uint hash = FLUtility.CreateID(nickName.ToLowerInvariant());

                            foreach (FLDataFileSetting setting in section.settings)
                            {
                                try
                                {
                                    if (setting.settingName.ToLowerInvariant() == "da_archetype")
                                    {
                                        // UTF hardpoints are only really useful to validate the shiparch and ship default loadout.
                                        var path = Path.Combine(flDataPath, setting.Str(0));
                                        UtfFile utf = new UtfFile(path);
                                        foreach (string hp in utf.hardpoints)
                                        {
                                            if (hp.ToLowerInvariant().Contains("cloak"))
                                            {
                                                var key = new ThreadSearchHardpointItem(hash, hp);
                                                if (ThreadSafeHardPointDict.TryGetValue(key, out var existing))
                                                {
                                                    //TODO: это по идее ошибка в CMP файле так как 2+ одинаковых хардов не должно быть
                                                    log.AddLog(String.Format("Error '{nickName}' 2+ hardpoints CMP {1} в {2}", nickName, existing.HPType, path));
                                                }
                                                else
                                                {
                                                    ThreadSafeHardPointDict.TryAdd(key, new HardPointItem
                                                    {
                                                        ShipHash = hash,
                                                        HPName = hp,
                                                        HPType = GAMEDATA_CLOAK,
                                                        MountableTypes = "",
                                                        DefaultItemHash = 0
                                                    });
                                                }
                                            }
                                        }
                                    }
                                    else if (setting.settingName.ToLowerInvariant() == "hp_type")
                                    {
                                        string type = setting.Str(0);
                                        for (int i = 1; i < setting.NumValues(); i++)
                                        {
                                            string hp = setting.Str(i);
                                            var key = new ThreadSearchHardpointItem(hash, hp);
                                            ThreadSafeHardPointDict.AddOrUpdate(
                                                    key,
                                                    _ => new HardPointItem
                                                    {
                                                        ShipHash = hash,
                                                        HPName = hp,
                                                        HPType = HardpointClassToGameDataClass(type),
                                                        MountableTypes = " " + type,
                                                        DefaultItemHash = 0
                                                    },
                                                    (_, existing) =>
                                                    {
                                                        lock (existing)
                                                        {
                                                            existing.MountableTypes += " " + type;
                                                            return existing;
                                                        }
                                                    });
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    log.AddLog(String.Format("Error '{0}' when reading {1}", ex.Message, setting.desc));
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    log.AddLog("Error '" + e.Message + "' when parsing '" + iniPath);
                }
            });
            Model.HardPointList.AddRange(ThreadSafeHardPointDict.Values.ToList());
            log.AddLog($"Hashes ParseShipEntries::{ThreadSafeHashListModel.Count}");
        }

        public async Task ParseFactionEntries(IProgress<ProgressInfo> progress, ILogRecorder log, CancellationToken token)
        {
            if (flIni == null)
            {
                log.AddLog("ParseFactionEntries::flIni is null");
                return;
            }
            foreach (FLDataFileSetting flIniEntry in flIni.GetSettings("Data", "groups"))
            {
                if (token.IsCancellationRequested)
                    return;
                progress.Report(new(30, "Loading factions: " + flIniEntry.Str(0)));
                string iniPath = Path.Combine(flDataPath, flIniEntry.Str(0));
                try
                {
                    FLDataFileService ini = new FLDataFileService(iniPath, true);
                    foreach (FLDataFileSection section in ini.Sections)
                    {
                        if (token.IsCancellationRequested)
                            return;
                        if (section.sectionName.ToLowerInvariant() == "group")
                        {
                            AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_FACTIONS, true);
                        }
                    }
                }
                catch (Exception e)
                {
                    log.AddLog("Error '" + e.Message + "' when parsing '" + iniPath);
                }
            }

            log.AddLog($"Hashes ParseFactionEntries::{ThreadSafeHashListModel.Count}");
        }

        public async Task ParseEquipmentEntries(IProgress<ProgressInfo> progress, ILogRecorder log, CancellationToken token)
        {
            if (flIni == null)
            {
                log.AddLog("ParseEquipmentEntries::flIni is null");
                return;
            }
            var options = new ParallelOptions
            {
                CancellationToken = token,
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };
            Parallel.ForEach(flIni.GetSettings("Data", "equipment"), options, flIniEntry =>
            {
                if (token.IsCancellationRequested)
                    return;
                progress.Report(new(20, "Loading equipment: " + flIniEntry.Str(0)));
                string iniPath = flDataPath + Path.DirectorySeparatorChar + flIniEntry.Str(0);
                try
                {
                    FLDataFileService ini = new FLDataFileService(iniPath, true);
                    Parallel.ForEach(ini.Sections, options, section =>
                    {
                        if (token.IsCancellationRequested)
                            return;
                        try
                        {
                            ParseEquipmentSection(section);
                        }
                        catch (Exception e)
                        {
                            log.AddLog("Error '" + e.Message + "' when parsing '" + iniPath);
                        }
                    });
                }
                catch (Exception e)
                {
                    log.AddLog("Error '" + e.Message + "' when parsing '" + iniPath);
                }
            });
            log.AddLog($"Hashes ParseEquipmentEntries::{ThreadSafeHashListModel.Count}");
            Model.EquipInfoList.AddRange(ThreadSafeEquipInfoListModel.Values.ToList());
        }

        private void ParseEquipmentSection(FLDataFileSection section)
        {
            string sectionName = section.sectionName.ToLowerInvariant();
            // Внутреннее оборудование; монтируется внутри.
            switch (sectionName)
            {
                case "engine":
                    AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_ENGINES, true);
                    break;
                case "power":
                    AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_POWERGEN, true);
                    break;
                case "scanner":
                    AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_SCANNERS, true);
                    break;
                case "tractor":
                    AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_TRACTORS, true);
                    break;
                case "cloakingdevice":
                    AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_CLOAK, false);
                    break;
                case "armor":
                    AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_ARMOR, true);
                    break;
                case "internalfx":
                    if (section.SettingExists("use_sound"))
                    {
                        AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_SOUND, false);
                    }
                    break;
                // Внешние узлы подвески.
                case "attachedfx":
                    AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_FX, false);
                    break;
                case "light":
                    AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_LIGHTS, false);
                    break;
                case "gun":
                    ParseGunOrShield(section, true);
                    break;
                case "shieldgenerator":
                    ParseGunOrShield(section, false);
                    break;
                case "countermeasuredropper":
                    AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_CM, true);
                    break;
                case "thruster":
                    AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_THRUSTERS, true);
                    break;
                case "minedropper":
                    AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_MINES, true);
                    break;
                // Cargo and ammo. (Груз и боеприпасы.)
                case "munition":
                    AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_AMMO, true);
                    break;
                case "repairkit":
                    AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_CARGO, true);
                    break;
                case "countermeasure":
                    AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_AMMO, true);
                    break;
                case "shieldbattery":
                    AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_CARGO, true);
                    break;
                case "mine":
                    AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_AMMO, true);
                    break;
                case "commodity":
                    AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_CARGO, true);
                    break;
                // Случайные вещи, с которыми я не знаю, что делать. Игнорировать эти разделы
                case "shield":
                case "lootcrate":
                case "lod":
                case "tradelane":
                case "motor":
                case "explosion":
                case "cargopod":
                    break;
                default:
                    break;
            }
        }

        private void ParseGunOrShield(FLDataFileSection section, bool isGun)
        {
            string key = isGun ? "hp_gun_type" : "hp_type";
            if (section.SettingExists(key))
            {
                string hpType = section.GetSetting(key).Str(0);
                AddGameDataThreadSafe(ThreadSafeHashListModel, section, HardpointClassToGameDataClass(hpType), true);
                var hash = FLUtility.CreateID(section.GetSetting("nickname").Str(0));
                ThreadSafeEquipInfoListModel.TryAdd(hash, new EquipInfoItem()
                {
                    EquipHash = hash,
                    ItemType = HardpointClassToGameDataClass(hpType),
                    MountableType = hpType,
                });
            }
            // Вероятно, щит NPC.
            else
            {
                AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_GEN, false);
            }
        }

        public async Task ParseBaseAndSystemsEntries(IProgress<ProgressInfo> progress, ILogRecorder log, CancellationToken token)
        {
            if (flIni == null)
            {
                log.AddLog("ParseBaseAndSystemsEntries::flIni is null");
                return;
            }
            //1.Парсим все секции
            var baseSections = new ConcurrentBag<FLDataFileSection>();
            var systemSections = new ConcurrentBag<FLDataFileSection>();
            Parallel.ForEach(flIni.GetSettings("Data", "universe"), (flIniEntry, state) =>
            {
                if (token.IsCancellationRequested)
                {
                    state.Stop();
                    return;
                }

                progress.Report(new(10, "Loading universe: " + flIniEntry.Str(0)));
                string iniPath = Path.Combine(flDataPath, flIniEntry.Str(0));
                try
                {
                    FLDataFileService ini = new FLDataFileService(iniPath, true);
                    foreach (var section in ini.Sections)
                    {
                        try
                        {
                            string name = section.sectionName.ToLowerInvariant();
                            if (name == "base")
                                baseSections.Add(section);
                            else if (name == "system")
                            {
                                string file = Directory.GetParent(ini.filePath).FullName + "\\" + section.GetSetting("file").Str(0);
                                section.directoryINI = file;
                                systemSections.Add(section);
                            }
                        }
                        catch (Exception e)
                        {
                            log.AddLog("Error '" + e.Message + "' when parsing '" + iniPath);
                        }
                    }
                }
                catch (Exception e)
                {
                    log.AddLog("Error '" + e.Message + "' when parsing '" + iniPath);
                }
            });

            var opts = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount / 2 };
            // 2. Обработка баз (в параллель)
            var taskBases = Task.Run(() => Parallel.ForEach(baseSections, opts, section =>
            {
                try
                {
                    token.ThrowIfCancellationRequested();
                    AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_BASES, true); // предполагается потокобезопасность
                    string baseNick = section.GetSetting("nickname").Str(0);
                    string baseFilePath = Path.Combine(flDataPath, section.GetSetting("file").Str(0));
                    LoadRoomDataThreadSafe(ThreadSafeHashListModel, baseNick, baseFilePath); // тоже желательно потокобезопасно
                }
                catch (Exception ex)
                {
                    log.AddLog($"Error '{ex.Message}' in BASE section.");
                }
            }));

            // 3. Обработка систем (в параллель)
            var taskSystems = Task.Run(() => Parallel.ForEach(systemSections, opts, section =>
            {
                try
                {
                    token.ThrowIfCancellationRequested();
                    AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_SYSTEMS, true);
                    ParseSystemThreadSafe(section.directoryINI, log); // желательно потокобезопасно
                }
                catch (Exception ex)
                {
                    log.AddLog($"Error '{ex.Message}' in SYSTEM section.");
                }
            }));

            await Task.WhenAll(taskBases, taskSystems);

            log.AddLog($"Hashes ParseBaseAndSystemsEntries::{ThreadSafeHashListModel.Count}");
        }

        public async Task LoadDLLS(CancellationToken token)
        {
            if (flIni == null)
                return;

            LoadLibrary(flExePath + Path.DirectorySeparatorChar + "resources.dll");
            foreach (FLDataFileSetting flIniEntry in flIni.GetSettings("Resources", "DLL"))
            {
                token.ThrowIfCancellationRequested();
                LoadLibrary(flExePath + Path.DirectorySeparatorChar + flIniEntry.Str(0));
            }
        }

        public async Task LoadInfocardMap(ILogRecorder log, CancellationToken token)
        {
            if (string.IsNullOrEmpty(flDataPath))
            {
                log.AddLog("[LoadInfocardMap] Error flDataPath");
                return;
            }
            string iniInfoMapPath = Path.Combine(flDataPath, "interface\\infocardmap.ini");
            try
            {
                FLDataFileService ini = new FLDataFileService(iniInfoMapPath, true);
                foreach (FLDataFileSection section in ini.Sections)
                {
                    token.ThrowIfCancellationRequested();
                    foreach (FLDataFileSetting set in section.settings)
                    {
                        token.ThrowIfCancellationRequested();
                        InfocardMap[set.UInt(0)] = set.UInt(1);
                    }
                }
            }
            catch (Exception e)
            {
                log.AddLog("Error '" + e.Message + "' when parsing '" + iniInfoMapPath);
            }
        }

        private string GetIDSParm(FLDataFileSection section, string parmName)
        {
            string stInfo = "";
            if (section.SettingExists(parmName))
            {
                uint idsInfo = section.GetSetting(parmName).UInt(0);
                stInfo = GetIDString(idsInfo);
                if (stInfo == null)
                    throw new Exception("ids_info not found " + idsInfo);
                stInfo = stInfo.Trim();
            }
            return stInfo;
        }

        private bool AddGameDataThreadSafe(ConcurrentDictionary<long, HashListItem> items, FLDataFileSection section, string gameDataType, bool ignoreEntriesWithNoIds)
        {
            string nickName = section.GetSetting("nickname").Str(0);
            string stIDSName = GetIDSParm(section, "ids_name");
            if (stIDSName == "")
                stIDSName = GetIDSParm(section, "strid_name");

            if (ignoreEntriesWithNoIds && stIDSName == "")
                return false;

            uint hash = FLUtility.CreateID(nickName.ToLowerInvariant());
            if (gameDataType == GAMEDATA_FACTIONS)
                hash = FLUtility.CreateFactionID(nickName);

            if (items.ContainsKey(hash))
                return false;

            string stIDSInfo = GetIDSParm(section, "ids_info");

            string stIDSInfo1 = GetIDSParm(section, "ids_info1");
            if (stIDSInfo1 == "")
                stIDSInfo1 = GetIDSParm(section, "ids_short_name");

            string stIDSInfo2 = GetIDSParm(section, "ids_info2");
            string stIDSInfo3 = GetIDSParm(section, "ids_info3");

            string keys = hash.ToString() + " 0x" + hash.ToString("X");

            return items.TryAdd(hash, new HashListItem
            {
                ItemHash = hash,
                ItemNickName = nickName,
                ItemType = gameDataType,
                IDSName = stIDSName,
                IDSInfo = stIDSInfo,
                IDSInfo1 = stIDSInfo1,
                IDSInfo2 = stIDSInfo2,
                IDSInfo3 = stIDSInfo3,
                ItemKeys = keys
            });
        }

        /// <summary>
        /// Проанализируйте системный файл для извлечения базовой информации.
        /// </summary>
        /// <param name="fileName"></param>
        void ParseSystemThreadSafe(string fileName, ILogRecorder log)
        {
            FLDataFileService ini = new FLDataFileService(fileName, true);
            foreach (FLDataFileSection section in ini.Sections)
            {
                string idsName = "";
                string idsInfo = "";
                string idsInfo1 = "";
                string pos = "";

                if (section.sectionName.ToLowerInvariant() == "object")
                {
                    try
                    {

                        if (section.SettingExists("ids_name"))
                        {
                            idsName = GetIDString(section.GetSetting("ids_name").UInt(0));
                        }

                        if (section.SettingExists("ids_info"))
                        {
                            uint value = section.GetSetting("ids_info").UInt(0);
                            idsInfo = GetIDString(value);
                            if (InfocardMap.ContainsKey(value))
                                idsInfo1 = GetIDString(InfocardMap[value]);
                        }

                        if (section.SettingExists("pos"))
                        {
                            pos = section.GetSetting("pos").Str(0)
                                + "," + section.GetSetting("pos").Str(1)
                                + "," + section.GetSetting("pos").Str(2);
                        }

                        if (section.SettingExists("base"))
                        {
                            string baseNick = section.GetSetting("base").Str(0);
                            var hash = FLUtility.CreateID(baseNick.ToLowerInvariant());
                            if (ThreadSafeHashListModel.TryGetValue(hash, out var baseItem))
                            {
                                if (baseItem.IDSInfo.Length == 0)
                                    baseItem.IDSInfo = idsInfo;
                                if (baseItem.IDSInfo1.Length == 0)
                                    baseItem.IDSInfo1 = idsInfo1;
                                if (baseItem.IDSInfo2.Length == 0)
                                    baseItem.IDSInfo2 = "Postion: " + pos;
                            }
                        }

                        AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_OBJECT, false);
                    }
                    catch (Exception e)
                    {
                        log.AddLog("Error '" + e.Message + "' when parsing '" + fileName);
                    }
                }
                else if (section.sectionName.ToLowerInvariant() == "zone")
                {
                    try
                    {
                        AddGameDataThreadSafe(ThreadSafeHashListModel, section, GAMEDATA_ZONE, false);
                    }
                    catch (Exception e)
                    {
                        log.AddLog("Error '" + e.Message + "' when parsing '" + fileName);
                    }
                }
            }
        }

        /// <summary>
        /// Load room hash codes from base ini data.
        /// </summary>
        /// <param name="baseIniPath"></param>
        private void LoadRoomDataThreadSafe(ConcurrentDictionary<long, HashListItem> items, string baseNick, string baseIniPath)
        {
            FLDataFileService iniRoom = new FLDataFileService(baseIniPath, true);
            foreach (FLDataFileSection section in iniRoom.Sections)
            {
                if (section.sectionName.ToLowerInvariant() != "room")
                    continue;

                string locationNick = $"{FLUtility.CreateID(baseNick.ToLowerInvariant()):x}_{section.GetSetting("nickname").Str(0).ToLowerInvariant()}";
                uint hashU = FLUtility.CreateID(locationNick);
                string keys = $"{hashU} 0x{hashU:X}";

                items.TryAdd(hashU, new HashListItem
                {
                    ItemHash = hashU,
                    ItemNickName = locationNick,
                    ItemType = GAMEDATA_GEN,
                    IDSName = "",
                    IDSInfo = "",
                    IDSInfo1 = "",
                    IDSInfo2 = "",
                    IDSInfo3 = "",
                    ItemKeys = keys
                });
            }
        }

        /// <summary>
        /// Scan the goods files for the specifed shiphull to add the effects entries
        /// from the goods package to the ship as synthetic hardpoints.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="loadout"></param>
        void AddLoadout(FLDataFileService flIni, FLDataFileSection shipLoadoutSection, ILogRecorder log)
        {
            try
            {
                string shipNickName = shipLoadoutSection.GetSetting("archetype").Str(0);
                uint shiphash = FLUtility.CreateID(shipNickName);

                long defaultSound = 0;
                long defaultPowerPlant = 0;
                long defaultEngine = 0;

                foreach (FLDataFileSetting setting in shipLoadoutSection.settings)
                {
                    try
                    {
                        if (setting.settingName.ToLowerInvariant() != "equip")
                            continue;

                        if (setting.NumValues() != 3)
                            throw new Exception(setting.desc + "setting '" + setting.desc + "' should have 3 values");

                        string equipNickName = setting.Str(0);
                        HashListItem item = GetItemByNickName(equipNickName);
                        if (item == null)
                            throw new Exception(setting.desc + " cannot find " + equipNickName + " in game data table");

                        if (item.ItemType == GAMEDATA_LIGHTS)
                        {
                            if (setting.Str(1) == "")
                                throw new Exception(setting.desc + " invalid hardpoint for " + equipNickName);
                            Model.HardPointList.Add(new HardPointItem
                            {
                                ShipHash = shiphash,
                                HPName = setting.Str(1),
                                HPType = item.ItemType,
                                MountableTypes = "",
                                DefaultItemHash = item.ItemHash
                            });
                        }
                        else if (item.ItemType == GAMEDATA_FX)
                        {
                            if (setting.Str(1) == "")
                                throw new Exception(setting.desc + " invalid hardpoint for " + equipNickName);
                            Model.HardPointList.Add(new HardPointItem
                            {
                                ShipHash = shiphash,
                                HPName = setting.Str(1),
                                HPType = item.ItemType,
                                MountableTypes = "",
                                DefaultItemHash = item.ItemHash
                            });
                        }
                        else if (item.ItemType == GAMEDATA_SOUND)
                        {
                            defaultSound = item.ItemHash;
                        }
                        else if (item.ItemType == GAMEDATA_POWERGEN)
                        {
                            defaultPowerPlant = item.ItemHash;
                        }
                        else if (item.ItemType == GAMEDATA_ENGINES)
                        {
                            defaultEngine = item.ItemHash;
                        }
                    }
                    catch (Exception e)
                    {
                        log.AddLog("Error '" + e.Message + "'");
                    }
                }

                Model.ShipInfoList.Add(new ShipInfoItem
                {
                    ShipHash = shiphash,
                    DefaultEngine = defaultEngine,
                    DefaultSound = defaultSound,
                    DefaultPowerPlant = defaultPowerPlant
                });
            }
            catch (Exception e)
            {
                log.AddLog("Error '" + e.Message + "'");
            }
        }

        /// <summary>
        /// Отсканируйте файлы товаров для указанного корпуса корабля, 
        /// чтобы добавить записи эффектов из пакета товаров к кораблю в качестве синтетических точек подвески.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="loadout"></param>
        void AddFXFromGoods(FLDataFileService flGoodsIni, FLDataFileSection shipPackageSection, ILogRecorder log, CancellationToken token)
        {
            try
            {
                string hullNickName = shipPackageSection.GetSetting("hull").Str(0);
                var shipHulls = flGoodsIni.Sections
                    .Where(s => s.sectionName.Equals("good", StringComparison.OrdinalIgnoreCase)
                             && s.GetSetting("category").Str(0) == "shiphull")
                    .ToDictionary(s => s.GetSetting("nickname").Str(0), s => s);
                if (shipHulls.TryGetValue(hullNickName, out var section))
                {
                    // используем section
                    string shipNickName = section.GetSetting("ship").Str(0);
                    uint shiphash = FLUtility.CreateID(shipNickName);

                    long defaultSound = 0;
                    long defaultPowerPlant = 0;
                    long defaultEngine = 0;

                    // Get the loadout for the ship.
                    foreach (var setting in shipPackageSection.settings)
                    {
                        try
                        {
                            if (!setting.settingName.Equals("addon", StringComparison.OrdinalIgnoreCase))
                                continue;

                            if (setting.NumValues() != 3)
                                throw new Exception(setting.desc + "setting '" + setting.desc + "' should have 3 values");

                            string equipNickName = setting.Str(0);
                            HashListItem item = GetItemByNickName(equipNickName);
                            if (item == null)
                                throw new Exception(setting.desc + " cannot find " + equipNickName + " in game data table");

                            if (item.ItemType == GAMEDATA_LIGHTS)
                            {
                                if (setting.Str(1) == "internal")
                                    throw new Exception(setting.desc + " invalid hardpoint for " + equipNickName);
                                var added = ThreadSafeHardPointDict.TryAdd(new ThreadSearchHardpointItem(shiphash, setting.Str(1)), new HardPointItem
                                {
                                    ShipHash = shiphash,
                                    HPName = setting.Str(1),
                                    HPType = item.ItemType,
                                    MountableTypes = "",
                                    DefaultItemHash = item.ItemHash
                                });
                                if (!added)
                                {
                                    log.AddLog($"⚠️ TryAdd failed: {shiphash}|{shipNickName}, {setting.Str(1)} — дубликат?");
                                }
                            }
                            else if (item.ItemType == GAMEDATA_FX)
                            {
                                if (setting.Str(1) == "internal")
                                    throw new Exception(setting.desc + " invalid hardpoint for " + equipNickName);
                                var added = ThreadSafeHardPointDict.TryAdd(new ThreadSearchHardpointItem(shiphash, setting.Str(1)), new HardPointItem
                                {
                                    ShipHash = shiphash,
                                    HPName = setting.Str(1),
                                    HPType = item.ItemType,
                                    MountableTypes = "",
                                    DefaultItemHash = item.ItemHash
                                });
                                if (!added)
                                {
                                    log.AddLog($"⚠️ TryAdd failed: {shiphash}, {setting.Str(1)} — дубликат?");
                                }
                            }
                            else if (item.ItemType == GAMEDATA_SOUND)
                            {
                                defaultSound = item.ItemHash;
                            }
                            else if (item.ItemType == GAMEDATA_POWERGEN)
                            {
                                defaultPowerPlant = item.ItemHash;
                            }
                            else if (item.ItemType == GAMEDATA_ENGINES)
                            {
                                defaultEngine = item.ItemHash;
                            }
                        }
                        catch (Exception e)
                        {
                            log.AddLog("Error '" + e.Message + "'");
                        }
                    }
                    ThreadSafeShipListModel.Add(new ShipInfoItem
                    {
                        ShipHash = shiphash,
                        DefaultEngine = defaultEngine,
                        DefaultSound = defaultSound,
                        DefaultPowerPlant = defaultPowerPlant
                    });
                }
                else
                {
                    log.AddLog($"Не найден shiphull для hull: {hullNickName}");
                }
            }
            catch (Exception e)
            {
                log.AddLog("Error '" + e.Message + "'");
            }
        }

        void AddFXFromGoodsOld(FLDataFileService flGoodsIni, FLDataFileSection shipPackageSection, ILogRecorder log)
        {
            try
            {
                string hullNickName = shipPackageSection.GetSetting("hull").Str(0);

                foreach (FLDataFileSection section in flGoodsIni.Sections)
                {
                    if (section.sectionName.ToLowerInvariant() != "good")
                        continue;

                    if (section.GetSetting("category").Str(0) != "shiphull")
                        continue;

                    if (section.GetSetting("nickname").Str(0) != hullNickName)
                        continue;

                    string shipNickName = section.GetSetting("ship").Str(0);
                    uint shiphash = FLUtility.CreateID(shipNickName);

                    long defaultSound = 0;
                    long defaultPowerPlant = 0;
                    long defaultEngine = 0;

                    // Get the loadout for the ship.
                    foreach (FLDataFileSetting setting in shipPackageSection.settings)
                    {
                        try
                        {
                            if (setting.settingName.ToLowerInvariant() != "addon")
                                continue;

                            if (setting.NumValues() != 3)
                                throw new Exception(setting.desc + "setting '" + setting.desc + "' should have 3 values");

                            string equipNickName = setting.Str(0);
                            HashListItem item = GetItemByNickName(equipNickName);
                            if (item == null)
                                throw new Exception(setting.desc + " cannot find " + equipNickName + " in game data table");

                            if (item.ItemType == GAMEDATA_LIGHTS)
                            {
                                if (setting.Str(1) == "internal")
                                    throw new Exception(setting.desc + " invalid hardpoint for " + equipNickName);
                                Model.HardPointList.Add(new HardPointItem
                                {
                                    ShipHash = shiphash,
                                    HPName = setting.Str(1),
                                    HPType = item.ItemType,
                                    MountableTypes = "",
                                    DefaultItemHash = item.ItemHash
                                });
                            }
                            else if (item.ItemType == GAMEDATA_FX)
                            {
                                if (setting.Str(1) == "internal")
                                    throw new Exception(setting.desc + " invalid hardpoint for " + equipNickName);
                                Model.HardPointList.Add(new HardPointItem
                                {
                                    ShipHash = shiphash,
                                    HPName = setting.Str(1),
                                    HPType = item.ItemType,
                                    MountableTypes = "",
                                    DefaultItemHash = item.ItemHash
                                });
                            }
                            else if (item.ItemType == GAMEDATA_SOUND)
                            {
                                defaultSound = item.ItemHash;
                            }
                            else if (item.ItemType == GAMEDATA_POWERGEN)
                            {
                                defaultPowerPlant = item.ItemHash;
                            }
                            else if (item.ItemType == GAMEDATA_ENGINES)
                            {
                                defaultEngine = item.ItemHash;
                            }
                        }
                        catch (Exception e)
                        {
                            log.AddLog("Error '" + e.Message + "'");
                        }
                    }
                    Model.ShipInfoList.Add(new ShipInfoItem
                    {
                        ShipHash = shiphash,
                        DefaultEngine = defaultEngine,
                        DefaultSound = defaultSound,
                        DefaultPowerPlant = defaultPowerPlant
                    });
                }
            }
            catch (Exception e)
            {
                log.AddLog("Error '" + e.Message + "'");
            }
        }

        private async Task ScanForHashCodesParallel(ConcurrentDictionary<long, HashListItem> dataStore, 
            CancellationToken cancelToken, 
            IProgress<ProgressInfo> progress, ILogRecorder log)
        {
            var allIniFiles = Directory.GetFiles(flDataPath, "*.ini", SearchOption.AllDirectories);

            var hashListItems = new ConcurrentBag<HashListItem>();
            var deduplicationSet = new ConcurrentDictionary<uint, byte>(); // dummy value
            var keyStats = new ConcurrentDictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            int counter = 0;
            var options = new ParallelOptions
            {
                CancellationToken = cancelToken,
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };
            Parallel.ForEach(allIniFiles, options, (filePath, state, index) =>
            {
                try
                {
                    if (Interlocked.Increment(ref counter) % 50 == 0)
                    {
                        progress?.Report(new ProgressInfo(65, $"Scanning {counter} files..."));
                    }

                    FLDataFileService ini = new FLDataFileService(filePath, true);
                    foreach (var section in ini.Sections)
                    {
                        foreach (var setting in section.settings)
                        {
                            if (setting.NumValues() == 0)
                                continue;

                            string key = setting.settingName.ToLowerInvariant();
                            if (key is "nickname" or "archetype" or "loadout" or "explosion_arch" or "fuse" or "zone"
                                or "name" or "room" or "prop" or "msg_id_prefix" or "npc")
                            {
                                // подсчёт количества каждого ключа
                                keyStats.AddOrUpdate(key, 1, (_, oldValue) => oldValue + 1);

                                string nickName = setting.Str(0);
                                uint hash = FLUtility.CreateID(nickName.Trim().ToLowerInvariant());

                                if (deduplicationSet.TryAdd(hash, 0))
                                {
                                   string keys = $"{hash} 0x{hash:X}";
                                    if (dataStore.TryAdd(hash, new HashListItem
                                        {
                                            ItemHash = hash,
                                            ItemNickName = nickName,
                                            ItemType = GAMEDATA_GEN,
                                            IDSName = "",
                                            IDSInfo = "",
                                            IDSInfo1 = "",
                                            IDSInfo2 = "",
                                            IDSInfo3 = "",
                                            ItemKeys = keys
                                        }))
                                    {
                                        //ok
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.AddLog($"Error '{ex.Message}' when scanning {filePath}");
                }
            });


            // ВЫВОД СТАТИСТИКИ ПО КЛЮЧАМ
            log.AddLog("📊 Статистика по ключам:");
            foreach (var pair in keyStats.OrderByDescending(p => p.Value))
            {
                log.AddLog($"🔹 {pair.Key} — {pair.Value} совпадений");
            }
            log.AddLog($"✅ Всего подходящих ключей: {keyStats.Values.Sum()}");
            log.AddLog($"✅ Всего уникальных хешей добавлено: {dataStore.Count}");
            log.AddLog($"⚠️ Пропущено по причине коллизий/дубликатов: {keyStats.Values.Sum() - dataStore.Count}");
        }


        /// <summary>
        /// Return the item for the specified faction nickname.
        /// </summary>
        /// <param name="itemHash">The nickname.</param>
        /// <returns>The item or null if it cannot be found.</returns>
        public HashListItem GetItemByFactionNickName(string nick)
        {
            return GetItemByHash(FLUtility.CreateFactionID(nick.ToLowerInvariant()));
        }

        /// <summary>
        /// Return the item for the specified nickname.
        /// </summary>
        /// <param name="itemHash">The nickname.</param>
        /// <returns>The item or null if it cannot be found.</returns>
        public HashListItem GetItemByNickName(string nick)
        {
            return GetItemByHash(FLUtility.CreateID(nick.ToLowerInvariant()));
        }

        /// <summary>
        /// Return a human readable description for a nickname.
        /// </summary>
        /// <param name="itemHash">The nickname</param>
        /// <returns>A description string.</returns>
        public string GetItemDescByFactionNickName(string nick)
        {
            HashListItem item = GetItemByFactionNickName(nick);
            if (item == null)
                return "Unknown[" + nick + "]";
            return item.IDSName;
        }

        /// <summary>
        /// Return a human readable description for a nickname.
        /// </summary>
        /// <param name="itemHash">The nickname</param>
        /// <returns>A description string.</returns>
        public string GetItemDescByNickNameX(string nick)
        {
            HashListItem item = GetItemByNickName(nick);
            if (item == null)
                return "Unknown[" + nick + "]";
            return item.IDSName;
        }


        /// <summary>
        /// Return the item for the specified key or hashcode.
        /// </summary>
        /// <param name="itemHash">The hashcode or key.</param>
        /// <returns>The item or null if it cannot be found.</returns>
        public HashListItem GetItemByHash(long itemHash)
        {
            var item = ThreadSafeHashListModel.FirstOrDefault((it) => it.Key == itemHash).Value;
            if(item == null)
                item = Model.HashListItems.FirstOrDefault((it) => it.ItemHash == itemHash);
            return item;
        }

        /// <summary>
        /// Return a human readable description for a key from a character file.
        /// </summary>
        /// <param name="itemHash">The hashcode or key.</param>
        /// <returns>A description string.</returns>
        public string GetItemDescByHash(long itemHash)
        {
            HashListItem item = GetItemByHash(itemHash);
            if (item == null)
                return "Unknown[" + itemHash + "]";
            if (string.IsNullOrEmpty(item.IDSName) || item.IDSName.Length == 0)
                return item.ItemNickName;
            return item.IDSName;
        }

        /// <summary>
        /// Return the nickName for the hashcode from a character file.
        /// </summary>
        /// <param name="itemHash">The key or hashcode</param>
        /// <returns>The nickname string.</returns>
        public string GetItemNickByHash(long itemHash)
        {
            HashListItem item = GetItemByHash(itemHash);
            if (item == null)
                return "Unknown[" + itemHash + "]";
            return item.ItemNickName;
        }

        /// <summary>
        /// Load an ion cross game data file that has a nickname key
        /// </summary>
        /// <param name="items">The database to copy data into</param>
        /// <param name="filePath">The file </param>
        /// <param name="log"></param>
        private void LoadIonCrossNickNameDesc(List<HashListItem> items, string filePath, ILogRecorder log, bool isFaction)
        {
            try
            {
                FLDataFileService ini = new FLDataFileService(filePath, false);
                foreach (FLDataFileSection s in ini.Sections)
                {
                    foreach (FLDataFileSetting setting in s.settings)
                    {
                        try
                        {
                            HashListItem itemRecord = GetItemByNickName(setting.settingName.ToLowerInvariant());
                            if (isFaction)
                                itemRecord = GetItemByFactionNickName(setting.settingName.ToLowerInvariant());

                            if (itemRecord != null)
                            {
                                if (setting.NumValues() > 0)
                                    itemRecord.IDSName = setting.Str(0);
                            }
                        }
                        catch (Exception ex)
                        {
                            log.AddLog(String.Format("Error '{0}' when reading {1}", ex.Message, setting.desc));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.AddLog(String.Format("Error '{0}' when reading {1}", ex.Message, filePath));
            }
        }

        /// <summary>
        /// Load a ion cross game data file that has a hash key
        /// </summary>
        /// <param name="items">The database to copy data into</param>
        /// <param name="filePath">The file </param>
        /// <param name="log"></param>
        private void LoadIonCrossHashDesc(List<HashListItem> items, string filePath, ILogRecorder log)
        {
            try
            {
                FLDataFileService ini = new FLDataFileService(filePath, true);
                foreach (FLDataFileSection s in ini.Sections)
                {
                    foreach (FLDataFileSetting setting in s.settings)
                    {
                        try
                        {
                            HashListItem itemRecord = GetItemByNickName(setting.settingName);
                            if (itemRecord == null)
                                itemRecord = GetItemByHash(Convert.ToUInt32(setting.settingName));
                            if (itemRecord != null)
                            {
                                if (setting.NumValues() > 1)
                                    itemRecord.IDSName = setting.Str(1);
                                else if (setting.NumValues() > 0)
                                    itemRecord.IDSName = setting.Str(0);
                            }
                        }
                        catch (Exception ex)
                        {
                            log.AddLog(String.Format("Error '{0}' when reading {1}", ex.Message, setting.desc));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.AddLog(String.Format("Error '{0}' when reading {1}", ex.Message, filePath));
            }
        }

        private string HardpointClassToGameDataClass(string hpClass)
        {
            if (hpClass == "hp_cargo_pod")
                return GAMEDATA_MISC;

            if (hpClass == "hp_countermeasure_dropper")
                return GAMEDATA_CM;

            if (hpClass == "hp_turret")
                return GAMEDATA_TURRETS;

            if (hpClass == "hp_mine_dropper")
                return GAMEDATA_MINES;

            if (hpClass == "hp_torpedo")
                return GAMEDATA_PROJECTILES;

            if (hpClass == "hp_thruster")
                return GAMEDATA_THRUSTERS;

            if (hpClass == "hp_freighter_shield_generator")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_elite_shield_generator")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_fighter_shield_generator")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_shield_generator")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_gun")
                return GAMEDATA_GUNS;

            if (hpClass == "hp_child")
                return GAMEDATA_MISC;

            if (hpClass == "hp_tractor_source")
                return GAMEDATA_MISC;

            if (hpClass == "hp_bay_external")
                return GAMEDATA_MISC;

            if (hpClass == "hp_bay_surface")
                return GAMEDATA_MISC;

            if (hpClass == "hp_gun_special_1")
                return GAMEDATA_GUNS;

            if (hpClass == "hp_gun_special_2")
                return GAMEDATA_GUNS;

            if (hpClass == "hp_gun_special_3")
                return GAMEDATA_GUNS;

            if (hpClass == "hp_gun_special_4")
                return GAMEDATA_GUNS;

            if (hpClass == "hp_gun_special_5")
                return GAMEDATA_GUNS;

            if (hpClass == "hp_gun_special_6")
                return GAMEDATA_GUNS;

            if (hpClass == "hp_gun_special_7")
                return GAMEDATA_GUNS;

            if (hpClass == "hp_gun_special_8")
                return GAMEDATA_GUNS;

            if (hpClass == "hp_gun_special_9")
                return GAMEDATA_GUNS;

            if (hpClass == "hp_gun_special_10")
                return GAMEDATA_GUNS;

            if (hpClass == "hp_turret_special_1")
                return GAMEDATA_TURRETS;

            if (hpClass == "hp_turret_special_2")
                return GAMEDATA_TURRETS;

            if (hpClass == "hp_turret_special_3")
                return GAMEDATA_TURRETS;

            if (hpClass == "hp_turret_special_4")
                return GAMEDATA_TURRETS;

            if (hpClass == "hp_turret_special_5")
                return GAMEDATA_TURRETS;

            if (hpClass == "hp_turret_special_6")
                return GAMEDATA_TURRETS;

            if (hpClass == "hp_turret_special_7")
                return GAMEDATA_TURRETS;

            if (hpClass == "hp_turret_special_8")
                return GAMEDATA_TURRETS;

            if (hpClass == "hp_turret_special_9")
                return GAMEDATA_TURRETS;

            if (hpClass == "hp_turret_special_10")
                return GAMEDATA_TURRETS;

            if (hpClass == "hp_torpedo_special_1")
                return GAMEDATA_PROJECTILES;

            if (hpClass == "hp_torpedo_special_2")
                return GAMEDATA_PROJECTILES;

            if (hpClass == "hp_fighter_shield_special_1")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_fighter_shield_special_2")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_fighter_shield_special_3")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_fighter_shield_special_4")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_fighter_shield_special_5")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_fighter_shield_special_6")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_fighter_shield_special_7")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_fighter_shield_special_8")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_fighter_shield_special_9")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_fighter_shield_special_10")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_elite_shield_special_1")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_elite_shield_special_2")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_elite_shield_special_3")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_elite_shield_special_4")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_elite_shield_special_5")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_elite_shield_special_6")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_elite_shield_special_7")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_elite_shield_special_8")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_elite_shield_special_9")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_elite_shield_special_10")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_freighter_shield_special_1")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_freighter_shield_special_2")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_freighter_shield_special_3")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_freighter_shield_special_4")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_freighter_shield_special_5")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_freighter_shield_special_6")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_freighter_shield_special_7")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_freighter_shield_special_8")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_freighter_shield_special_9")
                return GAMEDATA_SHIELDS;

            if (hpClass == "hp_freighter_shield_special_10")
                return GAMEDATA_SHIELDS;

            throw new Exception("unknown hardpoint class " + hpClass);
        }

        public ShipInfoItem GetShipInfo(uint hash)
        {
            return Model.ShipInfoList.FirstOrDefault((it) => it.ShipHash == hash);
        }

        public EquipInfoItem GetEquipInfo(uint hash)
        {
            return Model.EquipInfoList.FirstOrDefault((it) => it.EquipHash == hash);
        }

        public HardPointItem[] GetHardPointListByShip(uint shipHash)
        {
            return Model.HardPointList
                .Where(hp => hp.ShipHash == shipHash)
                .ToArray();
        }

        public HardPointItem GetHardPointByShipAndHPName(uint shipHash, string hpName)
        {
            var matches = Model.HardPointList
                .Where(hp => hp.ShipHash == shipHash && hp.HPName == hpName)
                .ToArray();

            if (matches.Length > 1)
                throw new Exception($"Invalid hardpoint configuration ship={shipHash} hp={hpName}");
            if (matches.Length == 0)
                return null;

            return matches[0];
        }
    }
}
