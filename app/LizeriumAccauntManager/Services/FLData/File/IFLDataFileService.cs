/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 20 апреля 2026 03:07:02
 * Version: 1.0.15
 */

using System.Collections.Generic;

namespace Root.Services
{
    public interface IFLDataFileService
    {
        string filePath { get; set; }
        List<FLDataFileSection> Sections { get; set; }
        List<FLDataFileSetting> GetSettings(string sectionName, string settingName);
        List<FLDataFileSetting> GetSettings(string sectionName);
        void SaveSettings(string filePath, bool encrypt);
        FLDataFileSetting GetSetting(string sectionName, string settingName);
        bool AddSetting(string sectionName, string settingName, object[] settingValues);
        bool SettingExists(string sectionName, string settingName);
        bool DeleteSetting(string sectionName, string settingName);
        bool DeleteSetting(FLDataFileSetting setting);
        void AddSettingNotUnique(string sectionName, string settingName, object[] settingValues);
        string GetIniFileContents();
    }
}
