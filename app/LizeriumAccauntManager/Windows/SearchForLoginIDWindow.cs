/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 21 апреля 2026 06:52:36
 * Version: 1.0.17
 */

using System;
using System.Windows.Forms;

using Root.App;
using Root.Services;

namespace Root.Tool_UI
{
    /// <summary>
    /// TODO: ПРИВЯЗАТЬ ФОРМУ
    /// </summary>
    public partial class SearchForAccountsByLoginIDWindow : Form
    {
        IMainView view;
        IDataBaseService dataBase = null;

        public SearchForAccountsByLoginIDWindow(IMainView view, IDataBaseService dataBase)
        {
            this.view = view;
            this.dataBase = dataBase;
            InitializeComponent();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            label2.Text = "Matching Accounts - Searching";

            string[] ids = new string[1];
            ids[0] = FLUtility.EscapeLikeExpressionString(textBox1.Text);
            dataBase.GetLoginIDListByLoginID(ids);
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
