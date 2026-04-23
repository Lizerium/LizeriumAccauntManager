/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 23 апреля 2026 06:53:22
 * Version: 1.0.19
 */

using System;
using System.ComponentModel;
using System.Windows.Forms;

using Root.App;
using Root.Components;
using Root.Services;

namespace Root.Tool_UI
{
    public partial class SearchForAccountsByIPWindow : Form
    {
        IMainView view;
        IDataBaseService dataBase = null;

        public SearchForAccountsByIPWindow(IMainView view, IDataBaseService dataBase)
        {
            this.view = view;
            this.dataBase = dataBase;
            InitializeComponent();
        }

        private async void buttonSearch_Click(object sender, EventArgs e)
        {
            label2.Text = "Matching Accounts - Searching";
            var ipItems = await dataBase.GetIPListByIP(FLUtility.EscapeLikeExpressionString(textBox1.Text));
            // Обновляем биндинг
            iPListBindingSource.DataSource = new BindingList<IPItem>(ipItems);

            label2.Text = "Matching Accounts";
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonShowAccount_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                string accDir = (string)dataGridView1.SelectedCells[0].OwningRow.Cells[accDirDataGridViewTextBoxColumn.Index].Value;
                view.FilterOnAccDir(accDir);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                string accDir = (string)dataGridView1.SelectedCells[0].OwningRow.Cells[accDirDataGridViewTextBoxColumn.Index].Value;
                view.FilterOnAccDir(accDir);
            }
        }
    }
}
