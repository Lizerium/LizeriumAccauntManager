/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 21 апреля 2026 06:52:36
 * Version: 1.0.17
 */

using Root.Components;

namespace Root
{
    partial class AddEquipmentWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEquipmentWindow));
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.ceItemGrid = new System.Windows.Forms.DataGridView();
            this.itemHashDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDSNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemNickNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDSInfoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDSInfo1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDSInfo2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDSInfo3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hashListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.richTextBoxInfo = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBoxShowAllTypes = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ceItemGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hashListBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // ceItemGrid
            // 
            this.ceItemGrid.AllowUserToAddRows = false;
            this.ceItemGrid.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.ceItemGrid, "ceItemGrid");
            this.ceItemGrid.AutoGenerateColumns = false;
            this.ceItemGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ceItemGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.itemHashDataGridViewTextBoxColumn,
            this.iDSNameDataGridViewTextBoxColumn,
            this.itemNickNameDataGridViewTextBoxColumn,
            this.itemTypeDataGridViewTextBoxColumn,
            this.iDSInfoDataGridViewTextBoxColumn,
            this.iDSInfo1DataGridViewTextBoxColumn,
            this.iDSInfo2DataGridViewTextBoxColumn,
            this.iDSInfo3DataGridViewTextBoxColumn});
            this.ceItemGrid.DataSource = this.hashListBindingSource;
            this.ceItemGrid.MultiSelect = false;
            this.ceItemGrid.Name = "ceItemGrid";
            this.ceItemGrid.ReadOnly = true;
            this.ceItemGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ceItemGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_DoubleClick);
            this.ceItemGrid.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
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
            this.itemTypeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.itemTypeDataGridViewTextBoxColumn.DataPropertyName = "ItemType";
            this.itemTypeDataGridViewTextBoxColumn.FillWeight = 60F;
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // richTextBoxInfo
            // 
            resources.ApplyResources(this.richTextBoxInfo, "richTextBoxInfo");
            this.richTextBoxInfo.Name = "richTextBoxInfo";
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBoxShowAllTypes
            // 
            resources.ApplyResources(this.checkBoxShowAllTypes, "checkBoxShowAllTypes");
            this.checkBoxShowAllTypes.Name = "checkBoxShowAllTypes";
            this.checkBoxShowAllTypes.UseVisualStyleBackColor = true;
            this.checkBoxShowAllTypes.CheckedChanged += new System.EventHandler(this.checkBoxShowAllTypes_CheckedChanged);
            // 
            // AddEquipmentWindow
            // 
            this.AcceptButton = this.okButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.Controls.Add(this.checkBoxShowAllTypes);
            this.Controls.Add(this.richTextBoxInfo);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ceItemGrid);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Name = "AddEquipmentWindow";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.AddEquipmentWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ceItemGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hashListBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.DataGridView ceItemGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
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
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox checkBoxShowAllTypes;

    }
}