/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 09 апреля 2026 10:58:05
 * Version: 1.0.3
 */

using System;
using System.Linq;
using System.Windows.Forms;

using Root.Components;
using Root.Services;

namespace Root
{
    public partial class ChangeShipWindow : Form
    {
        private ICharService charService;
        private IFLDataFileService charFile;
        public IFLGameDataService FLGameData;

        public ChangeShipWindow(ICharService appServices, IFLGameDataService gameData, 
            IFLDataFileService charFile)
        {
            this.charService = appServices;
            this.charFile = charFile;
            InitializeComponent();
            SetupDataSourceBase(gameData);
            FLGameData = gameData;

            FilterUpdate();
        }

        private void SetupDataSourceBase(IFLGameDataService gameData)
        {
            hashListBindingSource.DataSource = new SortableBindingList<HashListItem>(gameData.Model.HashListItems.
                Where(it => it.ItemType == "ships"));
        }

        /// <summary>
        /// Setup list of ships.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeShipWindow_Load(object sender, EventArgs e)
        {
            // Select the row that the player is currently 
            if (charFile.SettingExists("Player", "ship_archetype"))
            {
                uint currentShipHash = charFile.GetSetting("Player", "ship_archetype").UInt(0);
                foreach (DataGridViewRow row in csItemGrid.Rows)
                {
                    var dataRow = (HashListItem)row.DataBoundItem;
                    if (dataRow.ItemHash == currentShipHash)
                    {
                        row.Selected = true;
                        csItemGrid.FirstDisplayedScrollingRowIndex = row.Index;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Update the filter applied to the character list data grid view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterUpdate()
        {
            if (textBox1.Text.Length > 0)
            {
                var search = FLGameData.Model.HashListItems.
                Where(it => it.ItemType == "ships" && (it.IDSName.Contains(textBox1.Text)
                || it.ItemNickName.Contains(textBox1.Text)));

                hashListBindingSource.DataSource = new SortableBindingList<HashListItem>(search);
            }
            else SetupDataSourceBase(FLGameData);
        }


        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (csItemGrid.SelectedRows.Count != 1)
               return;

            var dataRow = (HashListItem)csItemGrid.SelectedRows[0].DataBoundItem;
            charFile.AddSetting("Player", "ship_archetype", new object[] { dataRow.ItemHash });
            while (charFile.DeleteSetting("Player", "equip"));
            charService.SaveCharFile(charFile);
            this.Close();
        }

        // <summary>
        /// Show details when row is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in csItemGrid.SelectedRows)
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
                else
                {
                    string xml = (string)row.Cells[iDSInfoDataGridViewTextBoxColumn.Index].Value;
                    if (string.IsNullOrEmpty(xml) || xml.Length == 0)
                        xml = "No information available";
                    rtf += FLUtility.FLXmlToRtf(xml);
                }
                richTextBoxInfo.Rtf = richTextBoxInfo.Rtf.Replace("@@@INSERTED_RTF_CODE_HACK@@@", rtf);
                break;
            }
        }

        /// <summary>
        /// A double click is treated like the OK button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void csItemGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            saveButton_Click(null, null);
        }

        /// <summary>
        /// Update the filter on a timer.
        /// </summary>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            timer1.Start();
        }

        /// <summary>
        /// Update the filter on a timer.
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            FilterUpdate();
        }
    }
}
