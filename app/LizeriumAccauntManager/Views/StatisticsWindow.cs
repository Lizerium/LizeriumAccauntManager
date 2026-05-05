/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 05 мая 2026 07:02:03
 * Version: 1.0.31
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

using Root.Components;
using Root.Views;

namespace Root.Tool_UI
{
    public partial class StatisticsWindow : Form, IStatisticsView
    {
        public event EventHandler OnRefreshRequested;

        public StatisticsWindow()
        {
            InitializeComponent();
        }

        private void StatisticsWindow_Load(object sender, EventArgs e)
        {
            buttonRefresh_Click(null, null);          
        }

        public void SetLoadingState(bool isLoading)
        {
            Cursor = isLoading ? Cursors.WaitCursor : Cursors.Default;
            label1.Text = isLoading ? "General Statistics - Loading..." : "General Statistics";
        }

        public void SetStatistics(List<GeneralStatistics> stats)
        {
            generalStatisticsTableBindingSource.DataSource = 
                new SortableBindingList<GeneralStatistics>(stats.ToList());
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            OnRefreshRequested.Invoke(this, EventArgs.Empty);
        }

        public void ShowMessage(string text, string title)
        {
            MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string filterText = FLUtility.EscapeLikeExpressionString(textBox1.Text);
            generalStatisticsTableBindingSource.Filter = string.IsNullOrWhiteSpace(filterText)
                ? null
                : $"(Description LIKE '%{filterText}%')";
        }
    }
}
