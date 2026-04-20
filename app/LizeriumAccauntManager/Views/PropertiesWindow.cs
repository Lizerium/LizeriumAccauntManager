/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 20 апреля 2026 16:22:14
 * Version: 1.0.16
 */

using System;
using System.Windows.Forms;

using Root.Components;
using Root.Views;

namespace Root
{
    public partial class PropertiesWindow : Form, IPropertiesView
    {
        public event EventHandler<PropsSaveRequest> SavePropsEvent;
        public event EventHandler ResetSettingsEvent;
        public event Action LoadPropsEvent;

        public PropertiesWindow()
        {
            InitializeComponent();
        }

        private void PropertiesWindow_Load(object sender, EventArgs e)
        {
            LoadPropsEvent.Invoke();
        }

        public void InstallLoadData(PropsSaveRequest request)
        {
            textBoxAccDir.Text = request.setAccountDir;
            textBoxIoncrossDir.Text = request.setIonCrossDir;
            textBoxFLDir.Text = request.setFLDir;
            checkBoxChangedOnly.Checked = request.setCheckChangedOnly;
            checkBoxWriteEncryptedFiles.Checked = request.setWriteEncryptedFiles;
            textBoxFLHookPort.Value = request.setFLHookPort;
            textBoxFLHookLogin.Text = request.setFLHookPassword;
            checkBoxAutomaticCharClean.Checked = request.setAutomaticCharClean;
            checkBoxAutomaticCharWipe.Checked = request.setAutomaticCharWipe;
            numericUpDown1.Value = request.setDaysToDeleteInactiveChars;
            numericUpDown2.Value = request.setSecsToDeleteUninterestedChars / 60;
            checkBoxUnicode.Checked = request.setFLHookUnicode;

            textBoxStatisticsDir.Text = request.setStatisticsDir;
            textBoxStatsFactions.Text = request.setStatsFactions;

            checkBoxAutomaticFixCharFiles.Checked = request.setAutomaticFixErrors;
            checkBoxCheckDefaultEngine.Checked = request.setCheckDefaultEngine;
            checkBoxCheckDefaultPowerPlant.Checked = request.setCheckDefaultPowerPlant;
            checkBoxReportVisitError.Checked = request.setReportVisitErrors;
            checkBoxCheckDefaultLights.Checked = request.setCheckDefaultLights;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SavePropsEvent.Invoke(sender, new PropsSaveRequest()
            {
                setAccountDir = textBoxAccDir.Text,
                setIonCrossDir = textBoxIoncrossDir.Text,
                setFLDir = textBoxFLDir.Text,
                setCheckChangedOnly = checkBoxChangedOnly.Checked,
                setWriteEncryptedFiles = checkBoxWriteEncryptedFiles.Checked,
                setFLHookPort = textBoxFLHookPort.Value,
                setFLHookPassword = textBoxFLHookLogin.Text,
                setFLHookUnicode = checkBoxUnicode.Checked,
                setAutomaticCharClean = checkBoxAutomaticCharClean.Checked,
                setAutomaticCharWipe = checkBoxAutomaticCharWipe.Checked,
                setDaysToDeleteInactiveChars = numericUpDown1.Value,
                setSecsToDeleteUninterestedChars = numericUpDown2.Value * 60,
                setStatisticsDir = textBoxStatisticsDir.Text,
                setStatsFactions = textBoxStatsFactions.Text,
                setAutomaticFixErrors = checkBoxAutomaticFixCharFiles.Checked,
                setCheckDefaultEngine = checkBoxCheckDefaultEngine.Checked,
                setCheckDefaultPowerPlant = checkBoxCheckDefaultPowerPlant.Checked,
                setReportVisitErrors = checkBoxReportVisitError.Checked,
                setCheckDefaultLights = checkBoxCheckDefaultLights.Checked
            });
            this.Close();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void accountDirButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textBoxAccDir.Text;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxAccDir.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void ioncrossDirButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textBoxIoncrossDir.Text;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxIoncrossDir.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void flDirButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textBoxFLDir.Text;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxFLDir.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textBoxStatisticsDir.Text;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxStatisticsDir.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void buttonResetProps_Click(object sender, EventArgs e)
        {
            ResetSettingsEvent.Invoke(this, EventArgs.Empty);
            Application.Exit();
        }
    }
}
