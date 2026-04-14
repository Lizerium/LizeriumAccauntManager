/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 14 апреля 2026 12:25:10
 * Version: 1.0.8
 */

using Root.Components;

namespace Root
{
    partial class BannedPlayers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BannedPlayers));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.accDirDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.banReasonDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.banStartDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.banEndDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.banListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.textBoxCharacters = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBoxBanReason = new System.Windows.Forms.RichTextBox();
            this.timerFilter = new System.Windows.Forms.Timer(this.components);
            this.checkBoxShowExpiredBans = new System.Windows.Forms.CheckBox();
            this.buttonUnban = new System.Windows.Forms.Button();
            this.buttonEditBan = new System.Windows.Forms.Button();
            this.buttonShowAccount = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.banListBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.accDirDataGridViewTextBoxColumn,
            this.accIDDataGridViewTextBoxColumn,
            this.banReasonDataGridViewTextBoxColumn,
            this.banStartDataGridViewTextBoxColumn,
            this.banEndDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.banListBindingSource;
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // accDirDataGridViewTextBoxColumn
            // 
            this.accDirDataGridViewTextBoxColumn.DataPropertyName = "AccDir";
            resources.ApplyResources(this.accDirDataGridViewTextBoxColumn, "accDirDataGridViewTextBoxColumn");
            this.accDirDataGridViewTextBoxColumn.Name = "accDirDataGridViewTextBoxColumn";
            this.accDirDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // accIDDataGridViewTextBoxColumn
            // 
            this.accIDDataGridViewTextBoxColumn.DataPropertyName = "AccID";
            resources.ApplyResources(this.accIDDataGridViewTextBoxColumn, "accIDDataGridViewTextBoxColumn");
            this.accIDDataGridViewTextBoxColumn.Name = "accIDDataGridViewTextBoxColumn";
            this.accIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // banReasonDataGridViewTextBoxColumn
            // 
            this.banReasonDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.banReasonDataGridViewTextBoxColumn.DataPropertyName = "BanReason";
            resources.ApplyResources(this.banReasonDataGridViewTextBoxColumn, "banReasonDataGridViewTextBoxColumn");
            this.banReasonDataGridViewTextBoxColumn.Name = "banReasonDataGridViewTextBoxColumn";
            this.banReasonDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // banStartDataGridViewTextBoxColumn
            // 
            this.banStartDataGridViewTextBoxColumn.DataPropertyName = "BanStart";
            resources.ApplyResources(this.banStartDataGridViewTextBoxColumn, "banStartDataGridViewTextBoxColumn");
            this.banStartDataGridViewTextBoxColumn.Name = "banStartDataGridViewTextBoxColumn";
            this.banStartDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // banEndDataGridViewTextBoxColumn
            // 
            this.banEndDataGridViewTextBoxColumn.DataPropertyName = "BanEnd";
            resources.ApplyResources(this.banEndDataGridViewTextBoxColumn, "banEndDataGridViewTextBoxColumn");
            this.banEndDataGridViewTextBoxColumn.Name = "banEndDataGridViewTextBoxColumn";
            this.banEndDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // banListBindingSource
            // 
            this.banListBindingSource.DataSource = typeof(Root.Components.BanItem);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxFilter
            // 
            resources.ApplyResources(this.textBoxFilter, "textBoxFilter");
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // textBoxCharacters
            // 
            resources.ApplyResources(this.textBoxCharacters, "textBoxCharacters");
            this.textBoxCharacters.Name = "textBoxCharacters";
            this.textBoxCharacters.ReadOnly = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // richTextBoxBanReason
            // 
            resources.ApplyResources(this.richTextBoxBanReason, "richTextBoxBanReason");
            this.richTextBoxBanReason.Name = "richTextBoxBanReason";
            this.richTextBoxBanReason.ReadOnly = true;
            // 
            // timerFilter
            // 
            this.timerFilter.Tick += new System.EventHandler(this.TimerFilterUpdate);
            // 
            // checkBoxShowExpiredBans
            // 
            resources.ApplyResources(this.checkBoxShowExpiredBans, "checkBoxShowExpiredBans");
            this.checkBoxShowExpiredBans.Name = "checkBoxShowExpiredBans";
            this.checkBoxShowExpiredBans.UseVisualStyleBackColor = true;
            this.checkBoxShowExpiredBans.CheckedChanged += new System.EventHandler(this.checkBoxShowExpiredBans_CheckedChanged);
            // 
            // buttonUnban
            // 
            resources.ApplyResources(this.buttonUnban, "buttonUnban");
            this.buttonUnban.Name = "buttonUnban";
            this.buttonUnban.UseVisualStyleBackColor = true;
            this.buttonUnban.Click += new System.EventHandler(this.buttonUnban_Click);
            // 
            // buttonEditBan
            // 
            resources.ApplyResources(this.buttonEditBan, "buttonEditBan");
            this.buttonEditBan.Name = "buttonEditBan";
            this.buttonEditBan.UseVisualStyleBackColor = true;
            this.buttonEditBan.Click += new System.EventHandler(this.buttonEditBan_Click);
            // 
            // buttonShowAccount
            // 
            resources.ApplyResources(this.buttonShowAccount, "buttonShowAccount");
            this.buttonShowAccount.Name = "buttonShowAccount";
            this.buttonShowAccount.UseVisualStyleBackColor = true;
            this.buttonShowAccount.Click += new System.EventHandler(this.buttonShowAccount_Click);
            // 
            // BannedPlayers
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonShowAccount);
            this.Controls.Add(this.buttonEditBan);
            this.Controls.Add(this.buttonUnban);
            this.Controls.Add(this.checkBoxShowExpiredBans);
            this.Controls.Add(this.richTextBoxBanReason);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxCharacters);
            this.Controls.Add(this.textBoxFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "BannedPlayers";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.banListBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.TextBox textBoxCharacters;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBoxBanReason;
        private System.Windows.Forms.Timer timerFilter;
        private System.Windows.Forms.CheckBox checkBoxShowExpiredBans;
        private System.Windows.Forms.Button buttonUnban;
        private System.Windows.Forms.Button buttonEditBan;
        private System.Windows.Forms.DataGridViewTextBoxColumn accDirDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn banReasonDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn banStartDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn banEndDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource banListBindingSource;
        private System.Windows.Forms.Button buttonShowAccount;
    }
}