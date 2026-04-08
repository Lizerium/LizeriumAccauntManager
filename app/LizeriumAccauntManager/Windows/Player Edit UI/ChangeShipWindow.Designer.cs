/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 08 апреля 2026 14:27:40
 * Version: 1.0.2
 */

using Root.Components;

namespace Root
{
    partial class ChangeShipWindow
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
            System.Windows.Forms.Button cancelButton;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeShipWindow));
            this.csItemGrid = new System.Windows.Forms.DataGridView();
            this.itemHashDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDSNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemNickNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDSInfoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDSInfo1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDSInfo2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDSInfo3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hashListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.saveButton = new System.Windows.Forms.Button();
            this.richTextBoxInfo = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.csItemGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hashListBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            resources.ApplyResources(cancelButton, "cancelButton");
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // csItemGrid
            // 
            this.csItemGrid.AllowUserToAddRows = false;
            this.csItemGrid.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.csItemGrid, "csItemGrid");
            this.csItemGrid.AutoGenerateColumns = false;
            this.csItemGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.csItemGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.itemHashDataGridViewTextBoxColumn,
            this.iDSNameDataGridViewTextBoxColumn,
            this.itemNickNameDataGridViewTextBoxColumn,
            this.itemTypeDataGridViewTextBoxColumn,
            this.iDSInfoDataGridViewTextBoxColumn,
            this.iDSInfo1DataGridViewTextBoxColumn,
            this.iDSInfo2DataGridViewTextBoxColumn,
            this.iDSInfo3DataGridViewTextBoxColumn});
            this.csItemGrid.DataSource = this.hashListBindingSource;
            this.csItemGrid.MultiSelect = false;
            this.csItemGrid.Name = "csItemGrid";
            this.csItemGrid.ReadOnly = true;
            this.csItemGrid.RowHeadersVisible = false;
            this.csItemGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.csItemGrid.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.csItemGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.csItemGrid_MouseDoubleClick);
            // 
            // itemHashDataGridViewTextBoxColumn
            // 
            this.itemHashDataGridViewTextBoxColumn.DataPropertyName = "ItemHash";
            resources.ApplyResources(this.itemHashDataGridViewTextBoxColumn, "itemHashDataGridViewTextBoxColumn");
            this.itemHashDataGridViewTextBoxColumn.Name = "itemHashDataGridViewTextBoxColumn";
            this.itemHashDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iDSNameDataGridViewTextBoxColumn
            // 
            this.iDSNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.iDSNameDataGridViewTextBoxColumn.DataPropertyName = "IDSName";
            resources.ApplyResources(this.iDSNameDataGridViewTextBoxColumn, "iDSNameDataGridViewTextBoxColumn");
            this.iDSNameDataGridViewTextBoxColumn.Name = "iDSNameDataGridViewTextBoxColumn";
            this.iDSNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // itemNickNameDataGridViewTextBoxColumn
            // 
            this.itemNickNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.itemNickNameDataGridViewTextBoxColumn.DataPropertyName = "ItemNickName";
            resources.ApplyResources(this.itemNickNameDataGridViewTextBoxColumn, "itemNickNameDataGridViewTextBoxColumn");
            this.itemNickNameDataGridViewTextBoxColumn.Name = "itemNickNameDataGridViewTextBoxColumn";
            this.itemNickNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // itemTypeDataGridViewTextBoxColumn
            // 
            this.itemTypeDataGridViewTextBoxColumn.DataPropertyName = "ItemType";
            resources.ApplyResources(this.itemTypeDataGridViewTextBoxColumn, "itemTypeDataGridViewTextBoxColumn");
            this.itemTypeDataGridViewTextBoxColumn.Name = "itemTypeDataGridViewTextBoxColumn";
            this.itemTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iDSInfoDataGridViewTextBoxColumn
            // 
            this.iDSInfoDataGridViewTextBoxColumn.DataPropertyName = "IDSInfo";
            resources.ApplyResources(this.iDSInfoDataGridViewTextBoxColumn, "iDSInfoDataGridViewTextBoxColumn");
            this.iDSInfoDataGridViewTextBoxColumn.Name = "iDSInfoDataGridViewTextBoxColumn";
            this.iDSInfoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iDSInfo1DataGridViewTextBoxColumn
            // 
            this.iDSInfo1DataGridViewTextBoxColumn.DataPropertyName = "IDSInfo1";
            resources.ApplyResources(this.iDSInfo1DataGridViewTextBoxColumn, "iDSInfo1DataGridViewTextBoxColumn");
            this.iDSInfo1DataGridViewTextBoxColumn.Name = "iDSInfo1DataGridViewTextBoxColumn";
            this.iDSInfo1DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iDSInfo2DataGridViewTextBoxColumn
            // 
            this.iDSInfo2DataGridViewTextBoxColumn.DataPropertyName = "IDSInfo2";
            resources.ApplyResources(this.iDSInfo2DataGridViewTextBoxColumn, "iDSInfo2DataGridViewTextBoxColumn");
            this.iDSInfo2DataGridViewTextBoxColumn.Name = "iDSInfo2DataGridViewTextBoxColumn";
            this.iDSInfo2DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iDSInfo3DataGridViewTextBoxColumn
            // 
            this.iDSInfo3DataGridViewTextBoxColumn.DataPropertyName = "IDSInfo3";
            resources.ApplyResources(this.iDSInfo3DataGridViewTextBoxColumn, "iDSInfo3DataGridViewTextBoxColumn");
            this.iDSInfo3DataGridViewTextBoxColumn.Name = "iDSInfo3DataGridViewTextBoxColumn";
            this.iDSInfo3DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // hashListBindingSource
            // 
            this.hashListBindingSource.DataSource = typeof(Root.Components.HashListItem);
            // 
            // saveButton
            // 
            resources.ApplyResources(this.saveButton, "saveButton");
            this.saveButton.Name = "saveButton";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // richTextBoxInfo
            // 
            resources.ApplyResources(this.richTextBoxInfo, "richTextBoxInfo");
            this.richTextBoxInfo.Name = "richTextBoxInfo";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ChangeShipWindow
            // 
            this.AcceptButton = this.saveButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = cancelButton;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBoxInfo);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(cancelButton);
            this.Controls.Add(this.csItemGrid);
            this.Name = "ChangeShipWindow";
            this.Load += new System.EventHandler(this.ChangeShipWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.csItemGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hashListBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView csItemGrid;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.BindingSource hashListBindingSource;
        private System.Windows.Forms.RichTextBox richTextBoxInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemHashDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDSNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemNickNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDSInfoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDSInfo1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDSInfo2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDSInfo3DataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;

    }
}