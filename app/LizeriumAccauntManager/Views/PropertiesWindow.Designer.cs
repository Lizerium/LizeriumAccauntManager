/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 17 апреля 2026 06:52:03
 * Version: 1.0.13
 */

namespace Root
{
    partial class PropertiesWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertiesWindow));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxCheckDefaultLights = new System.Windows.Forms.CheckBox();
            this.checkBoxCheckDefaultEngine = new System.Windows.Forms.CheckBox();
            this.checkBoxCheckDefaultPowerPlant = new System.Windows.Forms.CheckBox();
            this.checkBoxReportVisitError = new System.Windows.Forms.CheckBox();
            this.checkBoxAutomaticFixCharFiles = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxAutomaticCharClean = new System.Windows.Forms.CheckBox();
            this.checkBoxAutomaticCharWipe = new System.Windows.Forms.CheckBox();
            this.checkBoxChangedOnly = new System.Windows.Forms.CheckBox();
            this.flDirButton = new System.Windows.Forms.Button();
            this.textBoxFLDir = new System.Windows.Forms.TextBox();
            this.checkBoxWriteEncryptedFiles = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ionCrossDirButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxIoncrossDir = new System.Windows.Forms.TextBox();
            this.textBoxAccDir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.accountDirButton = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxUnicode = new System.Windows.Forms.CheckBox();
            this.textBoxFLHookPort = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxFLHookLogin = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxStatsFactions = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.textBoxStatisticsDir = new System.Windows.Forms.TextBox();
            this.buttonResetProps = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxFLHookPort)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxCheckDefaultLights);
            this.groupBox2.Controls.Add(this.checkBoxCheckDefaultEngine);
            this.groupBox2.Controls.Add(this.checkBoxCheckDefaultPowerPlant);
            this.groupBox2.Controls.Add(this.checkBoxReportVisitError);
            this.groupBox2.Controls.Add(this.checkBoxAutomaticFixCharFiles);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.numericUpDown2);
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.checkBoxAutomaticCharClean);
            this.groupBox2.Controls.Add(this.checkBoxAutomaticCharWipe);
            this.groupBox2.Controls.Add(this.checkBoxChangedOnly);
            this.groupBox2.Controls.Add(this.flDirButton);
            this.groupBox2.Controls.Add(this.textBoxFLDir);
            this.groupBox2.Controls.Add(this.checkBoxWriteEncryptedFiles);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.ionCrossDirButton);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBoxIoncrossDir);
            this.groupBox2.Controls.Add(this.textBoxAccDir);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.accountDirButton);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // checkBoxCheckDefaultLights
            // 
            resources.ApplyResources(this.checkBoxCheckDefaultLights, "checkBoxCheckDefaultLights");
            this.checkBoxCheckDefaultLights.Name = "checkBoxCheckDefaultLights";
            this.checkBoxCheckDefaultLights.UseVisualStyleBackColor = true;
            // 
            // checkBoxCheckDefaultEngine
            // 
            resources.ApplyResources(this.checkBoxCheckDefaultEngine, "checkBoxCheckDefaultEngine");
            this.checkBoxCheckDefaultEngine.Name = "checkBoxCheckDefaultEngine";
            this.checkBoxCheckDefaultEngine.UseVisualStyleBackColor = true;
            // 
            // checkBoxCheckDefaultPowerPlant
            // 
            resources.ApplyResources(this.checkBoxCheckDefaultPowerPlant, "checkBoxCheckDefaultPowerPlant");
            this.checkBoxCheckDefaultPowerPlant.Name = "checkBoxCheckDefaultPowerPlant";
            this.checkBoxCheckDefaultPowerPlant.UseVisualStyleBackColor = true;
            // 
            // checkBoxReportVisitError
            // 
            resources.ApplyResources(this.checkBoxReportVisitError, "checkBoxReportVisitError");
            this.checkBoxReportVisitError.Name = "checkBoxReportVisitError";
            this.checkBoxReportVisitError.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutomaticFixCharFiles
            // 
            resources.ApplyResources(this.checkBoxAutomaticFixCharFiles, "checkBoxAutomaticFixCharFiles");
            this.checkBoxAutomaticFixCharFiles.Name = "checkBoxAutomaticFixCharFiles";
            this.checkBoxAutomaticFixCharFiles.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // numericUpDown2
            // 
            resources.ApplyResources(this.numericUpDown2, "numericUpDown2");
            this.numericUpDown2.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            // 
            // numericUpDown1
            // 
            resources.ApplyResources(this.numericUpDown1, "numericUpDown1");
            this.numericUpDown1.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // checkBoxAutomaticCharClean
            // 
            resources.ApplyResources(this.checkBoxAutomaticCharClean, "checkBoxAutomaticCharClean");
            this.checkBoxAutomaticCharClean.Name = "checkBoxAutomaticCharClean";
            this.checkBoxAutomaticCharClean.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutomaticCharWipe
            // 
            resources.ApplyResources(this.checkBoxAutomaticCharWipe, "checkBoxAutomaticCharWipe");
            this.checkBoxAutomaticCharWipe.Name = "checkBoxAutomaticCharWipe";
            this.checkBoxAutomaticCharWipe.UseVisualStyleBackColor = true;
            // 
            // checkBoxChangedOnly
            // 
            this.checkBoxChangedOnly.Checked = true;
            this.checkBoxChangedOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.checkBoxChangedOnly, "checkBoxChangedOnly");
            this.checkBoxChangedOnly.Name = "checkBoxChangedOnly";
            // 
            // flDirButton
            // 
            resources.ApplyResources(this.flDirButton, "flDirButton");
            this.flDirButton.Name = "flDirButton";
            this.flDirButton.UseVisualStyleBackColor = true;
            this.flDirButton.Click += new System.EventHandler(this.flDirButton_Click);
            // 
            // textBoxFLDir
            // 
            resources.ApplyResources(this.textBoxFLDir, "textBoxFLDir");
            this.textBoxFLDir.Name = "textBoxFLDir";
            // 
            // checkBoxWriteEncryptedFiles
            // 
            resources.ApplyResources(this.checkBoxWriteEncryptedFiles, "checkBoxWriteEncryptedFiles");
            this.checkBoxWriteEncryptedFiles.Name = "checkBoxWriteEncryptedFiles";
            this.checkBoxWriteEncryptedFiles.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // ionCrossDirButton
            // 
            resources.ApplyResources(this.ionCrossDirButton, "ionCrossDirButton");
            this.ionCrossDirButton.Name = "ionCrossDirButton";
            this.ionCrossDirButton.UseVisualStyleBackColor = true;
            this.ionCrossDirButton.Click += new System.EventHandler(this.ioncrossDirButton_Click);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // textBoxIoncrossDir
            // 
            resources.ApplyResources(this.textBoxIoncrossDir, "textBoxIoncrossDir");
            this.textBoxIoncrossDir.Name = "textBoxIoncrossDir";
            // 
            // textBoxAccDir
            // 
            resources.ApplyResources(this.textBoxAccDir, "textBoxAccDir");
            this.textBoxAccDir.Name = "textBoxAccDir";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // accountDirButton
            // 
            resources.ApplyResources(this.accountDirButton, "accountDirButton");
            this.accountDirButton.Name = "accountDirButton";
            this.accountDirButton.UseVisualStyleBackColor = true;
            this.accountDirButton.Click += new System.EventHandler(this.accountDirButton_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.groupBox3);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxUnicode);
            this.groupBox1.Controls.Add(this.textBoxFLHookPort);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.textBoxFLHookLogin);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // checkBoxUnicode
            // 
            resources.ApplyResources(this.checkBoxUnicode, "checkBoxUnicode");
            this.checkBoxUnicode.Name = "checkBoxUnicode";
            this.checkBoxUnicode.UseVisualStyleBackColor = true;
            // 
            // textBoxFLHookPort
            // 
            resources.ApplyResources(this.textBoxFLHookPort, "textBoxFLHookPort");
            this.textBoxFLHookPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.textBoxFLHookPort.Name = "textBoxFLHookPort";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // textBoxFLHookLogin
            // 
            resources.ApplyResources(this.textBoxFLHookLogin, "textBoxFLHookLogin");
            this.textBoxFLHookLogin.Name = "textBoxFLHookLogin";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxStatsFactions);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.textBoxStatisticsDir);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // textBoxStatsFactions
            // 
            resources.ApplyResources(this.textBoxStatsFactions, "textBoxStatsFactions");
            this.textBoxStatsFactions.Name = "textBoxStatsFactions";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBoxStatisticsDir
            // 
            resources.ApplyResources(this.textBoxStatisticsDir, "textBoxStatisticsDir");
            this.textBoxStatisticsDir.Name = "textBoxStatisticsDir";
            // 
            // buttonResetProps
            // 
            resources.ApplyResources(this.buttonResetProps, "buttonResetProps");
            this.buttonResetProps.Name = "buttonResetProps";
            this.buttonResetProps.UseVisualStyleBackColor = true;
            this.buttonResetProps.Click += new System.EventHandler(this.buttonResetProps_Click);
            // 
            // PropertiesWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonResetProps);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "PropertiesWindow";
            this.Load += new System.EventHandler(this.PropertiesWindow_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxFLHookPort)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxAutomaticCharClean;
        private System.Windows.Forms.CheckBox checkBoxAutomaticCharWipe;
        private System.Windows.Forms.CheckBox checkBoxChangedOnly;
        private System.Windows.Forms.Button flDirButton;
        private System.Windows.Forms.TextBox textBoxFLDir;
        private System.Windows.Forms.CheckBox checkBoxWriteEncryptedFiles;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ionCrossDirButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxIoncrossDir;
        private System.Windows.Forms.TextBox textBoxAccDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button accountDirButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxStatsFactions;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBoxStatisticsDir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxUnicode;
        private System.Windows.Forms.NumericUpDown textBoxFLHookPort;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxFLHookLogin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxCheckDefaultEngine;
        private System.Windows.Forms.CheckBox checkBoxCheckDefaultPowerPlant;
        private System.Windows.Forms.CheckBox checkBoxReportVisitError;
        private System.Windows.Forms.CheckBox checkBoxAutomaticFixCharFiles;
        private System.Windows.Forms.CheckBox checkBoxCheckDefaultLights;
        private System.Windows.Forms.Button buttonResetProps;
    }
}