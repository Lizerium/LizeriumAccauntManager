/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 13 апреля 2026 12:59:47
 * Version: 1.0.7
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Root.Components;
using Root.Services;

namespace Root
{
    public partial class AddEquipmentWindow : Form
    {
        IFLGameDataService gameData;
        List<PIEquipment> equipTable;
        PIEquipment rowToEdit;
        string defaultGameDataType = "";

        /// <summary>
        /// Edit the specified row.
        /// </summary>
        /// <param name="parent">The main window parent.</param>
        /// <param name="rowToEdit">The row to edit.</param>
        public AddEquipmentWindow(IFLGameDataService gameData, List<PIEquipment> equipTable, PIEquipment rowToEdit)
        {
            this.gameData = gameData;
            this.equipTable = equipTable;
            this.rowToEdit = rowToEdit;

            if (rowToEdit != null)
            {
                defaultGameDataType = rowToEdit.itemGameDataType;
            }

            InitializeComponent();
            SetupDataSource(gameData);
            FilterUpdate();
        }

        private void SetupDataSource(IFLGameDataService gameData)
        {
            this.hashListBindingSource.DataSource = new SortableBindingList<HashListItem>(gameData.Model.HashListItems.
                            Where(it => it.ItemType == gameData.GAMEDATA_GUNS
                            || it.ItemType == gameData.GAMEDATA_TURRETS
                             || it.ItemType == gameData.GAMEDATA_MINES
                             || it.ItemType == gameData.GAMEDATA_PROJECTILES
                             || it.ItemType == gameData.GAMEDATA_SHIELDS
                             || it.ItemType == gameData.GAMEDATA_THRUSTERS
                             || it.ItemType == gameData.GAMEDATA_CM
                             || it.ItemType == gameData.GAMEDATA_CLOAK
                             || it.ItemType == gameData.GAMEDATA_LIGHTS
                             || it.ItemType == gameData.GAMEDATA_MISC
                             || it.ItemType == gameData.GAMEDATA_SCANNERS
                             || it.ItemType == gameData.GAMEDATA_TRACTORS
                             || it.ItemType == gameData.GAMEDATA_ENGINES
                             || it.ItemType == gameData.GAMEDATA_ARMOR
                             || it.ItemType == gameData.GAMEDATA_FX
                             || it.ItemType == gameData.GAMEDATA_POWERGEN
                             ));
        }

        private void AddEquipmentWindow_Load(object sender, EventArgs e)
        {
            if (rowToEdit != null)
            {
                foreach (DataGridViewRow row in ceItemGrid.Rows)
                {
                    HashListItem dataRow = (HashListItem)row.DataBoundItem;
                    if (dataRow.ItemHash == rowToEdit.itemHash)
                    {
                        row.Selected = true;
                        ceItemGrid.FirstDisplayedScrollingRowIndex = row.Index;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Do nothing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Add the selected item to the parent's cargo table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            if (ceItemGrid.SelectedRows.Count != 1)
                return;

            foreach (DataGridViewRow row in ceItemGrid.SelectedRows)
            {
                HashListItem dataRow = (HashListItem)row.DataBoundItem;
                if (rowToEdit != null)
                {
                    rowToEdit.itemDescription = gameData.GetItemDescByHash(dataRow.ItemHash);
                    rowToEdit.itemHash = dataRow.ItemHash;
                }
                else
                {
                    equipTable.Add(new PIEquipment()
                    {
                        itemHash = dataRow.ItemHash,
                        itemDescription = gameData.GetItemDescByHash(dataRow.ItemHash),
                        itemHardpoint = "",
                        itemAllowedTypes = "*",
                        itemGameDataType = ""
                    });
                }
            }

            this.Close();
        }


        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            //timer1.Start();
            FilterUpdate();
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
                string filterText = textBox1.Text;
                var data = gameData.Model.HashListItems.
                    Where(it => it.IDSName.Contains(FLUtility.EscapeLikeExpressionString(filterText))
                    ).ToList();
                this.hashListBindingSource.DataSource = new SortableBindingList<HashListItem>(data);
            }
            else SetupDataSource(gameData);
        }

        /// <summary>
        /// A double click is treated like the OK button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            okButton_Click(sender, e);
        }

        // <summary>
        /// Show details when row is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in ceItemGrid.SelectedRows)
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

        private void checkBoxShowAllTypes_CheckedChanged(object sender, EventArgs e)
        {
            FilterUpdate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //FilterUpdate();
        }
    }
}
