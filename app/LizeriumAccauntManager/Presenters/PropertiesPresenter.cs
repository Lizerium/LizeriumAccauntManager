/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 30 апреля 2026 09:20:20
 * Version: 1.0.26
 */

using System;
using System.Windows.Forms;

using Root.Components;
using Root.Views;

namespace Root.Presenters
{
    public class PropertiesPresenter
    {
        private readonly IPropertiesView view;

        public event EventHandler UpdateSettings;

        public PropertiesPresenter(IPropertiesView view)
        {
            this.view = view;

            view.SavePropsEvent += View_SavePropsEvent;
            view.ResetSettingsEvent += View_ResetSettingsEvent;
            view.LoadPropsEvent += View_LoadPropsEvent;
        }

        private void View_LoadPropsEvent()
        {
            var req = new PropsSaveRequest()
            {
                setAccountDir = AppSettings.Default.setAccountDir,
                setIonCrossDir = AppSettings.Default.setIonCrossDir,
                setFLDir = AppSettings.Default.setFLDir,
                setCheckChangedOnly = AppSettings.Default.setCheckChangedOnly,
                setWriteEncryptedFiles = AppSettings.Default.setWriteEncryptedFiles,
                setFLHookPort = AppSettings.Default.setFLHookPort,
                setFLHookPassword = AppSettings.Default.setFLHookPassword,
                setAutomaticCharClean = AppSettings.Default.setAutomaticCharClean,
                setAutomaticCharWipe = AppSettings.Default.setAutomaticCharWipe,
                setDaysToDeleteInactiveChars = AppSettings.Default.setDaysToDeleteInactiveChars,
                setSecsToDeleteUninterestedChars = AppSettings.Default.setSecsToDeleteUninterestedChars,
                setFLHookUnicode = AppSettings.Default.setFLHookUnicode,

                setStatisticsDir = AppSettings.Default.setStatisticsDir,
                setStatsFactions = AppSettings.Default.setStatsFactions,

                setAutomaticFixErrors = AppSettings.Default.setAutomaticFixErrors,
                setCheckDefaultEngine = AppSettings.Default.setCheckDefaultEngine,
                setCheckDefaultPowerPlant = AppSettings.Default.setCheckDefaultPowerPlant,
                setReportVisitErrors = AppSettings.Default.setReportVisitErrors,
                setCheckDefaultLights = AppSettings.Default.setCheckDefaultLights
            };
            view.InstallLoadData(req);
        }

        private void View_ResetSettingsEvent(object sender, EventArgs e)
        {
            AppSettings.Default.Upgrade();
            AppSettings.Default.Reset();
            AppSettings.Default.Save();
        }

        private void View_SavePropsEvent(object sender, PropsSaveRequest e)
        {
            var response = e;
            lock (AppSettings.Default)
            {
                AppSettings.Default.setAccountDir = e.setAccountDir;
                AppSettings.Default.setIonCrossDir = e.setIonCrossDir;
                AppSettings.Default.setFLDir = e.setFLDir;
                AppSettings.Default.setCheckChangedOnly = e.setCheckChangedOnly;
                AppSettings.Default.setWriteEncryptedFiles = e.setWriteEncryptedFiles;
                AppSettings.Default.setFLHookPort = e.setFLHookPort;
                AppSettings.Default.setFLHookPassword = e.setFLHookPassword;
                AppSettings.Default.setFLHookUnicode = e.setFLHookUnicode;
                AppSettings.Default.setAutomaticCharClean = e.setAutomaticCharClean;
                AppSettings.Default.setAutomaticCharWipe = e.setAutomaticCharWipe;
                AppSettings.Default.setDaysToDeleteInactiveChars = e.setDaysToDeleteInactiveChars;
                AppSettings.Default.setSecsToDeleteUninterestedChars = e.setSecsToDeleteUninterestedChars;
                AppSettings.Default.setStatisticsDir = e.setStatisticsDir;
                AppSettings.Default.setStatsFactions = e.setStatsFactions;

                AppSettings.Default.setAutomaticFixErrors = e.setAutomaticFixErrors;
                AppSettings.Default.setCheckDefaultEngine = e.setCheckDefaultEngine;
                AppSettings.Default.setCheckDefaultPowerPlant = e.setCheckDefaultPowerPlant;
                AppSettings.Default.setReportVisitErrors = e.setReportVisitErrors;
                AppSettings.Default.setCheckDefaultLights = e.setCheckDefaultLights;
                AppSettings.Default.Save();
                UpdateSettings.Invoke(sender, EventArgs.Empty);
            }
        }
     
        public void Show() => view.Show();
        public void BringToFront() => view.BringToFront();
        public bool IsDisposed => (view as Form)?.IsDisposed ?? false;
    }
}
