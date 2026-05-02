/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 02 мая 2026 19:17:21
 * Version: 1.0.28
 */

using Root.Components;

namespace Root
{
    partial class AddCargoWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddCargoWindow));
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.itemGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDSNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDSInfoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDSInfo1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDSInfo2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDSInfo3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hashListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBoxInfo = new System.Windows.Forms.RichTextBox();
            this.checkBoxShowAllTypes = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemGrid)).BeginInit();
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
            // numericUpDown1
            // 
            resources.ApplyResources(this.numericUpDown1, "numericUpDown1");
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // itemGrid
            // 
            this.itemGrid.AllowUserToAddRows = false;
            this.itemGrid.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.itemGrid, "itemGrid");
            this.itemGrid.AutoGenerateColumns = false;
            this.itemGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.itemGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.iDSNameDataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn2,
            this.itemTypeDataGridViewTextBoxColumn,
            this.iDSInfoDataGridViewTextBoxColumn,
            this.iDSInfo1DataGridViewTextBoxColumn,
            this.iDSInfo2DataGridViewTextBoxColumn,
            this.iDSInfo3DataGridViewTextBoxColumn});
            this.itemGrid.DataSource = this.hashListBindingSource;
            this.itemGrid.MultiSelect = false;
            this.itemGrid.Name = "itemGrid";
            this.itemGrid.ReadOnly = true;
            this.itemGrid.RowHeadersVisible = false;
            this.itemGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.itemGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.acItemGrid_CellDoubleClick);
            this.itemGrid.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ItemHash";
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // iDSNameDataGridViewTextBoxColumn
            // 
            this.iDSNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.iDSNameDataGridViewTextBoxColumn.DataPropertyName = "IDSName";
            resources.ApplyResources(this.iDSNameDataGridViewTextBoxColumn, "iDSNameDataGridViewTextBoxColumn");
            this.iDSNameDataGridViewTextBoxColumn.Name = "iDSNameDataGridViewTextBoxColumn";
            this.iDSNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ItemNickName";
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
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
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // richTextBoxInfo
            // 
            resources.ApplyResources(this.richTextBoxInfo, "richTextBoxInfo");
            this.richTextBoxInfo.Name = "richTextBoxInfo";
            // 
            // checkBoxShowAllTypes
            // 
            resources.ApplyResources(this.checkBoxShowAllTypes, "checkBoxShowAllTypes");
            this.checkBoxShowAllTypes.Name = "checkBoxShowAllTypes";
            this.checkBoxShowAllTypes.UseVisualStyleBackColor = true;
            this.checkBoxShowAllTypes.CheckedChanged += new System.EventHandler(this.checkBoxShowAllTypes_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // AddCargoWindow
            // 
            this.AcceptButton = this.okButton;
            this.CancelButton = this.cancelButton;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.checkBoxShowAllTypes);
            this.Controls.Add(this.richTextBoxInfo);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.itemGrid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Name = "AddCargoWindow";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.AddCargoWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hashListBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView itemGrid;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.BindingSource hashListBindingSource;
        private System.Windows.Forms.RichTextBox richTextBoxInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDSNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDSInfoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDSInfo1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDSInfo2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDSInfo3DataGridViewTextBoxColumn;
        private System.Windows.Forms.CheckBox checkBoxShowAllTypes;
        private System.Windows.Forms.Timer timer1;
    }
}