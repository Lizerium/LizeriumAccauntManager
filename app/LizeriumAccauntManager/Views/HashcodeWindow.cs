/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 апреля 2026 14:16:02
 * Version: 1.0.6
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Root.Components;
using Root.Views;

namespace Root
{
    public partial class HashWindow : Form, IHashView
    {
        public event Action<SelectedStatData> OnRowSelected;
        public event EventHandler<string> OnFilterChanged;

        public HashWindow()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Start();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SetHashList(DataModel model)
        {
            hashListBindingSource.DataSource = new SortableBindingList<HashListItem>(model.HashListItems);
        }

        /// <summary>
        /// Показывать подробности, когда выбрана строка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                var selectedData = new SelectedStatData
                {
                    ItemType = row.Cells[itemTypeDataGridViewTextBoxColumn.Index].Value?.ToString(),
                    IDSInfo = row.Cells[iDSInfoDataGridViewTextBoxColumn.Index].Value?.ToString(),
                    IDSInfo1 = row.Cells[iDSInfo1DataGridViewTextBoxColumn.Index].Value?.ToString(),
                    IDSInfo2 = row.Cells[iDSInfo2DataGridViewTextBoxColumn.Index].Value?.ToString(),
                    IDSInfo3 = row.Cells[iDSInfo3DataGridViewTextBoxColumn.Index].Value?.ToString()
                };

                richTextBoxInfo.Clear();
                richTextBoxInfo.AppendText("@@@INSERTED_RTF_CODE_HACK@@@");
                OnRowSelected?.Invoke(selectedData);
                break;
            }
        }

        public void SetInfoRtf(string rtf)
        {
            richTextBoxInfo.Rtf = richTextBoxInfo.Rtf.Replace("@@@INSERTED_RTF_CODE_HACK@@@", rtf);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            OnFilterChanged.Invoke(this, textBox1.Text);
        }

        public void ApplyFilter(List<HashListItem> expression)
        {
            if (expression == null || expression.Count == 0) return;
             hashListBindingSource.DataSource = new SortableBindingList<HashListItem>(expression);
        }
    }
}
