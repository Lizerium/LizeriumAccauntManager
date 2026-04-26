/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 26 апреля 2026 09:56:57
 * Version: 1.0.22
 */

using System;
using System.Windows.Forms;
using Root.Components;
using Root.Services;

namespace Root
{
    public partial class CreateBanWindow : Form, ILogRecorder
    {
        private IBanService BanService;
        private string accDir;
        private string accID;
        private DataModel dataSet;
        private BanItem banRecord = null;

        public Action<string> LogAction { get; set; }

        public CreateBanWindow(IBanService banService, string accDir, string accID, 
            DataModel model, BanItem banRecord)
        { 
            this.BanService = banService;
            this.accDir = accDir;
            this.accID = accID;
            this.dataSet = model;
            this.banRecord = banRecord;

            InitializeComponent();

            if (banRecord != null)
            {
                richTextBox1.Text = banRecord.BanReason;
                dateTimePickerStartDate.Value = banRecord.BanStart.ToUniversalTime();
                numericUpDownDuration.Value = (decimal)(banRecord.BanEnd - banRecord.BanEnd).TotalDays;
            }
            else
            {
                dateTimePickerStartDate.Value = DateTime.Now;
                numericUpDownDuration.Value = 0;
            }
        }

        private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            textBoxEndDate.Text = calcEndDate().ToLongDateString();
        }

        private void numericUpDownDuration_ValueChanged(object sender, EventArgs e)
        {
            textBoxEndDate.Text = calcEndDate().ToLongDateString();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            var request = new BanRequest()
            {
                AaccID = this.accID,
                AccDir = this.accDir,
                BanReason = richTextBox1.Text,
                BanStart = dateTimePickerStartDate.Value,
                BanEnd = calcEndDate()
            };
            BanService.SendMessageAction += AddLog;
            BanService.BanAccount(request);
            this.Close();
        }

        private DateTime calcEndDate()
        {
            return dateTimePickerStartDate.Value.AddDays((int)numericUpDownDuration.Value);
        }

        public void AddLog(string entry)
        {
            LogAction?.Invoke(entry);
        }
    }
}
