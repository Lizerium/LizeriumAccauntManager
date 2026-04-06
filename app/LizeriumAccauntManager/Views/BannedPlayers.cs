using System;
using System.Windows.Forms;
using System.Linq;
using Root.Components;
using System.Collections.Generic;

namespace Root
{
    public partial class BannedPlayers : Form, IBannedPlayersView
    {
        public event Action<string> OnAccountSelected;
        public event Action<string> OnAccountSelectedForDetails;
        public event Action<string> UnbanAction;
        public event Action<string> BanAction;

        public BannedPlayers()
        {
            InitializeComponent();

            TimerFilterUpdate(null, null);
            dataGridView1_SelectionChanged(null, null);
        }
        public void SetData(IEnumerable<BanItem> banList)
        {
            banListBindingSource.DataSource = new SortableBindingList<BanItem>(banList.ToList());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
        /// <summary>
        /// Update the filter applied to the character list data grid view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerFilterUpdate(object sender, EventArgs e)
        {
            timerFilter.Stop();
            string filter = "";
            
            if (textBoxFilter.Text.Length > 2)
            {
                string filterText = textBoxFilter.Text;
                if (filter != "")
                    filter += " AND ";
                filter += "((AccID = '" + FLUtility.EscapeEqualsExpressionString(filterText) + "') " +
                    " OR (AccDir = '"+ FLUtility.EscapeLikeExpressionString(filterText) + "') " +
                    " OR (BanReason LIKE '%" + FLUtility.EscapeLikeExpressionString(filterText) + "%'))";
            }

            if (checkBoxShowExpiredBans.Checked)
            {
                if (filter != "")
                    filter += " AND ";
                filter += "(BanEnd < #"+String.Format("{0:s}",DateTime.Now.ToUniversalTime())+"#)";
            }

            banListBindingSource.Filter = filter;
        }

        /// <summary>
        /// Update the account information area.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            UpdateAccInfo();
        }

        public void UpdateAccInfo()
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                textBoxCharacters.Text = "";
                richTextBoxBanReason.Text = "";
                return;
            }

            string accDir = dataGridView1.SelectedRows[0].Cells[accDirDataGridViewTextBoxColumn.Index].Value?.ToString();

            if (string.IsNullOrEmpty(accDir))
            {
                textBoxCharacters.Text = "";
                richTextBoxBanReason.Text = "";
                return;
            }

            OnAccountSelectedForDetails?.Invoke(accDir);
        }

        public void UpdateOnAccauntInfoFinaly(string charStr)
        {
            textBoxCharacters.Text = charStr;
            // Для BanReason - допустим, берём из той же строки, что выбрана
            richTextBoxBanReason.Text = dataGridView1.SelectedRows[0].Cells[banReasonDataGridViewTextBoxColumn.Index].Value?.ToString() ?? "";
        }

        /// <summary>
        /// Update the list filter to show the specified record.
        /// </summary>
        /// <param name="accountID">The account ID to select and show.</param>
        public void HighlightRecord(string accountID)
        {
            textBoxFilter.Text = accountID;
            timerFilter.Start();
        }

        private void checkBoxShowExpiredBans_CheckedChanged(object sender, EventArgs e)
        {
            timerFilter.Start();
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            timerFilter.Start();
        }

        private void buttonUnban_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string accDir = (string)dataGridView1.SelectedRows[0].Cells[accDirDataGridViewTextBoxColumn.Index].Value;
                UnbanAction.Invoke(accDir);
            }
        }

        private void buttonEditBan_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string accDir = dataGridView1.SelectedRows[0].Cells[accDirDataGridViewTextBoxColumn.Index].Value?.ToString();

                if (!string.IsNullOrEmpty(accDir))
                    BanAction.Invoke(accDir);
            }
        }

        private void buttonShowAccount_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string accDir = (string)dataGridView1.SelectedRows[0].Cells[accDirDataGridViewTextBoxColumn.Index].Value;
                OnAccountSelected?.Invoke(accDir);
            }
        }
    }
}
