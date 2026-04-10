/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 10 апреля 2026 12:32:19
 * Version: 1.0.4
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

using Root.Components;
using Root.Services;

namespace Root
{
    /// <summary>
    /// Изменить местоположение игрока.
    /// </summary>
    public partial class ChangeLocationWindow : Form
    {
        ICharService charService;
        IFLDataFileService charFile;
        public IFLGameDataService FLGameData;
        private bool IsLoaded { get; set; } = false;

        public ChangeLocationWindow(ICharService appServices, 
            IFLGameDataService gameData, IFLDataFileService charFile)
        {
            this.charService = appServices;
            this.charFile = charFile;
            FLGameData = gameData;

            InitializeComponent();
            ReloadDataSources(gameData);
            FilterUpdate();
        }

        private void ReloadDataSources(IFLGameDataService gameData)
        {
            hashListBindingSource.DataSource = new SortableBindingList<HashListItem>(gameData.Model.HashListItems.Where(it => it.ItemType == "bases").ToList());
            hashListBindingSource1.DataSource = new SortableBindingList<HashListItem>(gameData.Model.HashListItems.Where(it => it.ItemType == "systems").ToList());
        }

        private void ChangeLocationWindow_Load(object sender, EventArgs e)
        {
            IsLoaded = false;
            // Select the row that the player is currently at
            if (charFile.SettingExists("Player", "base")
                && charFile.SettingExists("Player", "last_base"))
            {
                checkBox2.Checked = false;

                string currentBaseNick = charFile.GetSetting("Player", "last_base").Str(0);
                string currentSystemNick = charFile.GetSetting("Player", "system").Str(0);

                foreach (DataGridViewRow row in dataGridViewBase.Rows)
                {
                    var dataRow = (HashListItem)row.DataBoundItem;
                    if (dataRow.ItemNickName == currentBaseNick)
                    {
                        row.Selected = true;
                        dataGridViewBase.FirstDisplayedScrollingRowIndex = row.Index;
                        break;
                    }
                }
                foreach (DataGridViewRow row in dataGridViewSystem.Rows)
                {
                    var dataRow = (HashListItem)row.DataBoundItem;
                    if (dataRow.ItemNickName == currentSystemNick)
                    {
                        row.Selected = true;
                        dataGridViewSystem.FirstDisplayedScrollingRowIndex = row.Index;
                        break;
                    }
                }
            }

            if (charFile.SettingExists("Player", "system")
                && charFile.SettingExists("Player", "pos"))
            {
                checkBox2.Checked = true;

                string currentSystemNick = charFile.GetSetting("Player", "system").Str(0);
                foreach (DataGridViewRow row in dataGridViewSystem.Rows)
                {
                    var dataRow = (HashListItem)row.DataBoundItem;
                    if (dataRow.ItemNickName == currentSystemNick)
                    {
                        row.Selected = true;
                        dataGridViewSystem.FirstDisplayedScrollingRowIndex = row.Index;
                        break;
                    }
                }

                textBoxPosX.Text = charFile.GetSetting("Player", "pos").Str(0);
                textBoxPosY.Text = charFile.GetSetting("Player", "pos").Str(1);
                textBoxPosZ.Text = charFile.GetSetting("Player", "pos").Str(2);
            }
            else
            {
                textBoxPosX.Text = "0";
                textBoxPosY.Text = "100000";
                textBoxPosZ.Text = "100000";
            }

            IsLoaded = true;
        }

        /// <summary>
        /// Save the new location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewBase.SelectedRows.Count != 1)
                return;

            foreach (DataGridViewRow row in dataGridViewBase.SelectedRows)
            {
                var dataRow = (HashListItem)row.DataBoundItem;
                string baseNick = dataRow.ItemNickName;
                string systemNick = baseNick.Substring(0, 4);

                charFile.AddSetting("Player", "base", new object[] { baseNick });
                charFile.AddSetting("Player", "last_base", new object[] { baseNick });
                charFile.AddSetting("Player", "system", new object[] { systemNick });
                break;
            }

            if (checkBox2.Checked)
            {
                foreach (DataGridViewRow row in dataGridViewSystem.SelectedRows)
                {
                    var dataRow = (HashListItem)row.DataBoundItem;
                    string systemNick = dataRow.ItemNickName;
                    charFile.DeleteSetting("Player", "base");
                    charFile.AddSetting("Player", "pos", new object[] { textBoxPosX.Text, textBoxPosY.Text, textBoxPosZ.Text });
                    charFile.AddSetting("Player", "rotation", new object[] { 0, 0, 0 });
                    charFile.AddSetting("Player", "system", new object[] { systemNick });
                    break;
                }
            }
            else
            {
                charFile.DeleteSetting("Player", "pos");
                charFile.DeleteSetting("Player", "rotation");
            }

            charService.SaveCharFile(charFile);
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FilterUpdate();
        }

        /// <summary>
        /// Update the filter applied to the character list data grid view.
        /// </summary>
        private void FilterUpdate()
        {
            if (textBox1.Text.Length > 0)
            {
                var SystemFilter = FLGameData.Model.HashListItems.Where(it => it.ItemType == "systems"
                      && (it.IDSName.IndexOf(textBox1.Text, StringComparison.OrdinalIgnoreCase) >= 0
                      || it.IDSInfo.IndexOf(textBox1.Text, StringComparison.OrdinalIgnoreCase) >= 0
                      || it.ItemNickName.IndexOf(textBox1.Text, StringComparison.OrdinalIgnoreCase) >= 0));
                hashListBindingSource1.DataSource = new SortableBindingList<HashListItem>(SystemFilter);
            }
        }

        /// <summary>
        /// Show details when row is selected base
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewBase_SelectionChanged(object sender, EventArgs e)
        {
            if (itemTypeDataGridViewTextBoxColumn == null || !IsLoaded) return;
            foreach (DataGridViewRow row in dataGridViewBase.SelectedRows)
            {
                richTextBoxInfo.Clear();
                richTextBoxInfo.AppendText("@@@INSERTED_RTF_CODE_HACK@@@");
                string rtf = "";

                if ((string)row.Cells[itemTypeDataGridViewTextBoxColumn.Index].Value == "ships")
                {
                    rtf += FLUtility.FLXmlToRtf((string)row.Cells[iDSInfo1DataGridViewTextBoxColumn.Index].Value);
                    rtf += "\\pard \\par ";
                    rtf += FLUtility.FLXmlToRtf((string)row.Cells[iDSInfoDataGridViewTextBoxColumn.Index].Value);
                }
                else if ((string)row.Cells[itemTypeDataGridViewTextBoxColumn.Index].Value == FLGameData.GAMEDATA_BASES)
                {
                    rtf += FLUtility.FLXmlToRtf((string)row.Cells[iDSInfoDataGridViewTextBoxColumn.Index].Value);
                    rtf += "\\pard \\par ";
                    rtf += FLUtility.FLXmlToRtf((string)row.Cells[iDSInfo1DataGridViewTextBoxColumn.Index].Value);
                    rtf += "\\pard \\par ";
                    rtf += FLUtility.FLXmlToRtf((string)row.Cells[iDSInfo2DataGridViewTextBoxColumn.Index].Value);
                    rtf += "\\pard \\par ";
                    rtf += FLUtility.FLXmlToRtf((string)row.Cells[iDSInfo3DataGridViewTextBoxColumn.Index].Value);
                }
                else
                {
                    string xml = (string)row.Cells[iDSInfoDataGridViewTextBoxColumn.Index].Value;
                    if (xml.Length == 0)
                        xml = "No information available";
                    rtf += FLUtility.FLXmlToRtf(xml);
                }
                richTextBoxInfo.Rtf = richTextBoxInfo.Rtf.Replace("@@@INSERTED_RTF_CODE_HACK@@@", rtf);
                break;
            }
        }

        // <summary>
        /// Show details when row is selected system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewSystem_SelectionChanged(object sender, EventArgs e)
        {
            if (itemTypeDataGridViewTextBoxColumn == null || !IsLoaded) return;
            ReloadViewBases();
            foreach (DataGridViewRow row in dataGridViewSystem.SelectedRows)
            {
                richTextBoxInfo.Clear();
                richTextBoxInfo.AppendText("@@@INSERTED_RTF_CODE_HACK@@@");
                string rtf = "";

                if ((string)row.Cells[itemTypeDataGridViewTextBoxColumn.Index].Value == "ships")
                {
                    rtf += FLUtility.FLXmlToRtf((string)row.Cells[iDSInfo1DataGridViewTextBoxColumn.Index].Value);
                    rtf += "\\pard \\par ";
                    rtf += FLUtility.FLXmlToRtf((string)row.Cells[iDSInfoDataGridViewTextBoxColumn.Index].Value);
                }
                else if ((string)row.Cells[itemTypeDataGridViewTextBoxColumn.Index].Value == FLGameData.GAMEDATA_BASES)
                {
                    rtf += FLUtility.FLXmlToRtf((string)row.Cells[iDSInfoDataGridViewTextBoxColumn.Index].Value);
                    rtf += "\\pard \\par ";
                    rtf += FLUtility.FLXmlToRtf((string)row.Cells[iDSInfo1DataGridViewTextBoxColumn.Index].Value);
                    rtf += "\\pard \\par ";
                    rtf += FLUtility.FLXmlToRtf((string)row.Cells[iDSInfo2DataGridViewTextBoxColumn.Index].Value);
                    rtf += "\\pard \\par ";
                    rtf += FLUtility.FLXmlToRtf((string)row.Cells[iDSInfo3DataGridViewTextBoxColumn.Index].Value);
                }
                else
                {
                    string xml = row.Cells[iDSInfo1DataGridViewTextBoxColumn.Index].Value.ToString();
                    if (xml.Length == 0)
                        xml = "No information available";
                    rtf += FLUtility.FLXmlToRtf(xml);
                }
                richTextBoxInfo.Rtf = richTextBoxInfo.Rtf.Replace("@@@INSERTED_RTF_CODE_HACK@@@", rtf);
                break;
            }
        }

        private void ReloadViewBases()
        {
            var bases = new List<HashListItem>();
            foreach (DataGridViewRow row in dataGridViewSystem.SelectedRows)
            {
                var rowValSystem = (string)row.Cells[dataGridViewTextBoxColumn3.Index].Value;
                if (!string.IsNullOrEmpty((string)row.Cells[dataGridViewTextBoxColumn3.Index].Value))
                {
                    bases.AddRange(FLGameData.Model.HashListItems.Where(it => it.ItemType == "bases"
                        && it.ItemNickName.StartsWith(rowValSystem, StringComparison.OrdinalIgnoreCase)));
                }
            }

            hashListBindingSource.DataSource = new SortableBindingList<HashListItem>(bases);
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                dataGridViewSystem.Enabled = true;
                textBoxPosX.Enabled = true;
                textBoxPosY.Enabled = true;
                textBoxPosZ.Enabled = true;
            }
            else
            {
                dataGridViewSystem.Enabled = false;
                textBoxPosX.Enabled = false;
                textBoxPosY.Enabled = false;
                textBoxPosZ.Enabled = false;

            }
        }

        private void dataGridViewBase_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            checkBox2.Checked = false;
            saveButton_Click(null, null);
        }

        private void dataGridViewSystem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            checkBox2.Checked = true;
            saveButton_Click(null, null);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FilterUpdate();
        }
    }
}
