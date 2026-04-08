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
    partial class HashWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HashWindow));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ItemKeys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemNickNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDSNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDSInfoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDSInfo1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDSInfo2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDSInfo3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hashListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.closeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.richTextBoxInfo = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hashListBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemKeys,
            this.itemNickNameDataGridViewTextBoxColumn,
            this.itemTypeDataGridViewTextBoxColumn,
            this.iDSNameDataGridViewTextBoxColumn,
            this.iDSInfoDataGridViewTextBoxColumn,
            this.iDSInfo1DataGridViewTextBoxColumn,
            this.iDSInfo2DataGridViewTextBoxColumn,
            this.iDSInfo3DataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.hashListBindingSource;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // ItemKeys
            // 
            this.ItemKeys.DataPropertyName = "ItemKeys";
            resources.ApplyResources(this.ItemKeys, "ItemKeys");
            this.ItemKeys.Name = "ItemKeys";
            this.ItemKeys.ReadOnly = true;
            // 
            // itemNickNameDataGridViewTextBoxColumn
            // 
            this.itemNickNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.itemNickNameDataGridViewTextBoxColumn.DataPropertyName = "ItemNickName";
            this.itemNickNameDataGridViewTextBoxColumn.FillWeight = 15F;
            resources.ApplyResources(this.itemNickNameDataGridViewTextBoxColumn, "itemNickNameDataGridViewTextBoxColumn");
            this.itemNickNameDataGridViewTextBoxColumn.Name = "itemNickNameDataGridViewTextBoxColumn";
            this.itemNickNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // itemTypeDataGridViewTextBoxColumn
            // 
            this.itemTypeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.itemTypeDataGridViewTextBoxColumn.DataPropertyName = "ItemType";
            this.itemTypeDataGridViewTextBoxColumn.FillWeight = 15F;
            resources.ApplyResources(this.itemTypeDataGridViewTextBoxColumn, "itemTypeDataGridViewTextBoxColumn");
            this.itemTypeDataGridViewTextBoxColumn.Name = "itemTypeDataGridViewTextBoxColumn";
            this.itemTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iDSNameDataGridViewTextBoxColumn
            // 
            this.iDSNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.iDSNameDataGridViewTextBoxColumn.DataPropertyName = "IDSName";
            this.iDSNameDataGridViewTextBoxColumn.FillWeight = 40F;
            resources.ApplyResources(this.iDSNameDataGridViewTextBoxColumn, "iDSNameDataGridViewTextBoxColumn");
            this.iDSNameDataGridViewTextBoxColumn.Name = "iDSNameDataGridViewTextBoxColumn";
            this.iDSNameDataGridViewTextBoxColumn.ReadOnly = true;
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
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            this.closeButton.Name = "closeButton";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
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
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // richTextBoxInfo
            // 
            resources.ApplyResources(this.richTextBoxInfo, "richTextBoxInfo");
            this.richTextBoxInfo.Name = "richTextBoxInfo";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // HashWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.richTextBoxInfo);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.dataGridView1);
            this.Name = "HashWindow";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hashListBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource hashListBindingSource;
        private System.Windows.Forms.RichTextBox richTextBoxInfo;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemKeys;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemNickNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDSNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDSInfoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDSInfo1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDSInfo2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDSInfo3DataGridViewTextBoxColumn;
    }
}