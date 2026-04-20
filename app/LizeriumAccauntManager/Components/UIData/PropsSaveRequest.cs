/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 20 апреля 2026 16:22:14
 * Version: 1.0.16
 */

namespace Root.Components
{
    public class PropsSaveRequest
    {
        public string setAccountDir { get; set; }
        public string setIonCrossDir { get; set; }
        public string setFLDir { get; set; }
        public bool setCheckChangedOnly { get; set; }
        public bool setWriteEncryptedFiles { get; set; }
        public decimal setFLHookPort { get; set; }
        public string setFLHookPassword { get; set; }
        public bool setFLHookUnicode { get; set; }
        public bool setAutomaticCharClean { get; set; }
        public bool setAutomaticCharWipe { get; set; }
        public decimal setDaysToDeleteInactiveChars { get; set; }
        public decimal setSecsToDeleteUninterestedChars { get; set; }
        public string setStatisticsDir { get; set; }
        public string setStatsFactions { get; set; }

        public bool setAutomaticFixErrors { get; set; }
        public bool setCheckDefaultEngine { get; set; }
        public bool setCheckDefaultPowerPlant { get; set; }
        public bool setReportVisitErrors { get; set; }
        public bool setCheckDefaultLights { get; set; }
    }
}
