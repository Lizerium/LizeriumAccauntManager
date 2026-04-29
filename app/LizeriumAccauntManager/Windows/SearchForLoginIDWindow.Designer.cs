/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 29 апреля 2026 06:52:59
 * Version: 1.0.25
 */

using Root.Components;

namespace Root.Tool_UI
{
    partial class SearchForAccountsByLoginIDWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchForAccountsByLoginIDWindow));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.accDirDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accessTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loginIDListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.buttonShowAccount = new System.Windows.Forms.Button();
            this.iPListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginIDListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iPListBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // buttonSearch
            // 
            resources.ApplyResources(this.buttonSearch, "buttonSearch");
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonClose
            // 
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.accDirDataGridViewTextBoxColumn,
            this.accessTimeDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.loginIDListBindingSource;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // accDirDataGridViewTextBoxColumn
            // 
            this.accDirDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.accDirDataGridViewTextBoxColumn.DataPropertyName = "AccDir";
            resources.ApplyResources(this.accDirDataGridViewTextBoxColumn, "accDirDataGridViewTextBoxColumn");
            this.accDirDataGridViewTextBoxColumn.Name = "accDirDataGridViewTextBoxColumn";
            // 
            // accessTimeDataGridViewTextBoxColumn
            // 
            this.accessTimeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.accessTimeDataGridViewTextBoxColumn.DataPropertyName = "AccessTime";
            resources.ApplyResources(this.accessTimeDataGridViewTextBoxColumn, "accessTimeDataGridViewTextBoxColumn");
            this.accessTimeDataGridViewTextBoxColumn.Name = "accessTimeDataGridViewTextBoxColumn";
            // 
            // loginIDListBindingSource
            // 
            this.loginIDListBindingSource.DataSource = typeof(Root.Components.LoginIDItem);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // buttonShowAccount
            // 
            resources.ApplyResources(this.buttonShowAccount, "buttonShowAccount");
            this.buttonShowAccount.Name = "buttonShowAccount";
            this.buttonShowAccount.UseVisualStyleBackColor = true;
            this.buttonShowAccount.Click += new System.EventHandler(this.buttonShowAccount_Click);
            // 
            // iPListBindingSource
            // 
            this.iPListBindingSource.DataSource = typeof(Root.Components.IPItem);
            // 
            // SearchForAccountsByLoginIDWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonShowAccount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "SearchForAccountsByLoginIDWindow";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginIDListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iPListBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource iPListBindingSource;
        private System.Windows.Forms.Button buttonShowAccount;
        private System.Windows.Forms.BindingSource loginIDListBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn accDirDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accessTimeDataGridViewTextBoxColumn;
    }
}