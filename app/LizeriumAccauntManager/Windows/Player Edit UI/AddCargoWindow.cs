/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 28 апреля 2026 14:26:16
 * Version: 1.0.24
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Root.Components;
using Root.Services;

namespace Root
{
    public partial class AddCargoWindow : Form
    {
        IFLGameDataService gameData;
        List<PICargo> cargoTable;
        PICargo rowToEdit;

        public AddCargoWindow(IFLGameDataService gameData, 
            List<PICargo> cargoTable, PICargo rowToEdit)
        {
            this.gameData = gameData;
            this.cargoTable = cargoTable;
            this.rowToEdit = rowToEdit;

            InitializeComponent();
            SetupDataSource(gameData);
            FilterUpdate();
        }

        private void SetupDataSource(IFLGameDataService gameData)
        {
            hashListBindingSource.DataSource = new SortableBindingList<HashListItem>(
                gameData.Model.HashListItems.Where(it => it.ItemType == gameData.GAMEDATA_GUNS
                || it.ItemType == gameData.GAMEDATA_TURRETS
                || it.ItemType == gameData.GAMEDATA_MINES
                || it.ItemType == gameData.GAMEDATA_PROJECTILES
                || it.ItemType == gameData.GAMEDATA_SHIELDS
                || it.ItemType == gameData.GAMEDATA_THRUSTERS
                || it.ItemType == gameData.GAMEDATA_CM
                || it.ItemType == gameData.GAMEDATA_LIGHTS
                || it.ItemType == gameData.GAMEDATA_MISC
                || it.ItemType == gameData.GAMEDATA_SCANNERS
                || it.ItemType == gameData.GAMEDATA_TRACTORS
                || it.ItemType == gameData.GAMEDATA_ENGINES
                || it.ItemType == gameData.GAMEDATA_ARMOR
                || it.ItemType == gameData.GAMEDATA_POWERGEN
                ));
        }
       
        private void AddCargoWindow_Load(object sender, EventArgs e)
        {
            if (rowToEdit != null)
            {
                foreach (DataGridViewRow row in itemGrid.Rows)
                {
                    HashListItem dataRow = (HashListItem)row.DataBoundItem;
                    if (dataRow.ItemHash == rowToEdit.itemHash)
                    {
                        numericUpDown1.Value = rowToEdit.itemCount;
                        row.Selected = true;
                        itemGrid.FirstDisplayedScrollingRowIndex = row.Index;
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
            foreach (DataGridViewRow row in itemGrid.SelectedRows)
            {
                HashListItem dataRow = (HashListItem)row.DataBoundItem;
                if (rowToEdit != null)
                {
                    rowToEdit.itemDescription = gameData.GetItemDescByHash(dataRow.ItemHash);
                    rowToEdit.itemCount = (uint)numericUpDown1.Value;
                    rowToEdit.itemHash = dataRow.ItemHash;
                }
                else
                {
                    cargoTable.Add(new PICargo()
                    {
                        itemHash = dataRow.ItemHash,
                        itemCount = (uint)numericUpDown1.Value,
                        itemDescription = gameData.GetItemDescByHash(dataRow.ItemHash)
                    });
                }
            }

            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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
           string filterText = textBox1.Text;
            if (checkBoxShowAllTypes.Checked)
            {
                hashListBindingSource.DataSource = new SortableBindingList<HashListItem>(
                      gameData.Model.HashListItems.Where(it => it.ItemType == gameData.GAMEDATA_GUNS
                      || it.ItemType == gameData.GAMEDATA_TURRETS
                      || it.ItemType == gameData.GAMEDATA_MINES
                      || it.ItemType == gameData.GAMEDATA_PROJECTILES
                      || it.ItemType == gameData.GAMEDATA_SHIELDS
                      || it.ItemType == gameData.GAMEDATA_THRUSTERS
                      || it.ItemType == gameData.GAMEDATA_CM
                      || it.ItemType == gameData.GAMEDATA_LIGHTS
                      || it.ItemType == gameData.GAMEDATA_MISC
                      || it.ItemType == gameData.GAMEDATA_SCANNERS
                      || it.ItemType == gameData.GAMEDATA_TRACTORS
                      || it.ItemType == gameData.GAMEDATA_ENGINES
                      || it.ItemType == gameData.GAMEDATA_ARMOR
                      || it.ItemType == gameData.GAMEDATA_POWERGEN 
                      && (it.IDSName.Contains(FLUtility.EscapeLikeExpressionString(filterText))
                      || it.ItemNickName.Contains(FLUtility.EscapeLikeExpressionString(filterText))
                      || it.ItemType.Contains(FLUtility.EscapeLikeExpressionString(filterText)))));
            }

            if (textBox1.Text.Length > 0)
            {
                hashListBindingSource.DataSource = new SortableBindingList<HashListItem>(
                      gameData.Model.HashListItems.Where(it =>it.IDSName.Contains(FLUtility.EscapeLikeExpressionString(filterText))
                      || it.ItemNickName.Contains(FLUtility.EscapeLikeExpressionString(filterText))
                      || it.ItemType.Contains(FLUtility.EscapeLikeExpressionString(filterText))));
            }
        }


        /// <summary>
        /// A double click is treat like the OK button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void acItemGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
            foreach (DataGridViewRow row in itemGrid.SelectedRows)
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
                    if (xml.Length == 0)
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
            FilterUpdate();
        }
    }
}
