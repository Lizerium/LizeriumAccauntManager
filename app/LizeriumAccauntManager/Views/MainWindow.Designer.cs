using Root.Components;
using Root.Components;
using Root.Components;

using System.ComponentModel;

namespace Root
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.changeShipButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonAddCompleteMap = new System.Windows.Forms.Button();
            this.buttonCheckFile = new System.Windows.Forms.Button();
            this.buttonResetLastOnline = new System.Windows.Forms.Button();
            this.buttonBanInfo = new System.Windows.Forms.Button();
            this.reloadFileButton = new System.Windows.Forms.Button();
            this.piOnline = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.kickPlayerButton = new System.Windows.Forms.Button();
            this.deletePlayerButton = new System.Windows.Forms.Button();
            this.piIPAddresses = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.piPath = new System.Windows.Forms.TextBox();
            this.openDirButton = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.changeMoneyButton = new System.Windows.Forms.Button();
            this.changeBanButton = new System.Windows.Forms.Button();
            this.changeNameButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.changeLocationButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.piName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.piAccountID = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.piCreated = new System.Windows.Forms.TextBox();
            this.piMoney = new System.Windows.Forms.TextBox();
            this.piLastOnline = new System.Windows.Forms.TextBox();
            this.piTimePlayed = new System.Windows.Forms.TextBox();
            this.piKills = new System.Windows.Forms.TextBox();
            this.piRank = new System.Windows.Forms.TextBox();
            this.piLocation = new System.Windows.Forms.TextBox();
            this.piShip = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.discardFactionButton = new System.Windows.Forms.Button();
            this.piReputationEdit = new System.Windows.Forms.NumericUpDown();
            this.saveFactionButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.piAffiliation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.piFactionGrid = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.buttonChangeCargo = new System.Windows.Forms.Button();
            this.addCargoButton = new System.Windows.Forms.Button();
            this.removeCargoButton = new System.Windows.Forms.Button();
            this.discardCargoButton = new System.Windows.Forms.Button();
            this.saveCargoButton = new System.Windows.Forms.Button();
            this.piCargoGrid = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.buttonAddEquipment = new System.Windows.Forms.Button();
            this.removeEquipmentButton = new System.Windows.Forms.Button();
            this.changeEquipmentButton = new System.Windows.Forms.Button();
            this.discardEquipmentButton = new System.Windows.Forms.Button();
            this.saveEquipmentButton = new System.Windows.Forms.Button();
            this.piEquipmentGrid = new System.Windows.Forms.DataGridView();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.label19 = new System.Windows.Forms.Label();
            this.richTextBoxPlayerInfoAdminText = new System.Windows.Forms.RichTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBoxPlayerInfoPlayerText = new System.Windows.Forms.RichTextBox();
            this.buttonPlayerInfoSaveAdminText = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.checkBoxShowOriginalFile = new System.Windows.Forms.CheckBox();
            this.piFileView = new System.Windows.Forms.RichTextBox();
            this.saveFileManualButton = new System.Windows.Forms.Button();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bannedPlayersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchIPtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchLoginIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fLFileEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.rescanAccountFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadGameDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.charListDataGridView = new System.Windows.Forms.DataGridView();
            this.LastOnLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OnLineSecs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkBoxFilterDeleted = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripDBPending = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripHookState = new System.Windows.Forms.ToolStripStatusLabel();
            this.updatePlayerInfoTimer = new System.Windows.Forms.Timer(this.components);
            this.checkBoxFilterSameIP = new System.Windows.Forms.CheckBox();
            this.timerPeriodicTasks = new System.Windows.Forms.Timer(this.components);
            this.checkBoxFilterSameLoginID = new System.Windows.Forms.CheckBox();
            this.checkBoxFilterSameAccount = new System.Windows.Forms.CheckBox();
            this.timerFilter = new System.Windows.Forms.Timer(this.components);
            this.timerDBSave = new System.Windows.Forms.Timer(this.components);
            this.timerShutdown = new System.Windows.Forms.Timer(this.components);
            this.charPathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accDirDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.charNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isDeletedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.locationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shipDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.moneyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rankDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updatedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.onLineSecsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastOnLineDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.characterListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.itemDescriptionDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemNicknameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemRepDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pIFactionTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.itemDescriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemHashDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pICargoListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.itemHardpointDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemDescriptionDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemHashDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemAllowedTypesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemGameDataTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pIEquipmentTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.piReputationEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.piFactionGrid)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.piCargoGrid)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.piEquipmentGrid)).BeginInit();
            this.tabPage7.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.charListDataGridView)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.characterListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pIFactionTableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pICargoListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pIEquipmentTableBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // changeShipButton
            // 
            resources.ApplyResources(this.changeShipButton, "changeShipButton");
            this.changeShipButton.Name = "changeShipButton";
            this.changeShipButton.UseVisualStyleBackColor = true;
            this.changeShipButton.Click += new System.EventHandler(this.changeShipButton_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // textBoxFilter
            // 
            resources.ApplyResources(this.textBoxFilter, "textBoxFilter");
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Controls.Add(this.buttonAddCompleteMap);
            this.tabPage1.Controls.Add(this.buttonCheckFile);
            this.tabPage1.Controls.Add(this.buttonResetLastOnline);
            this.tabPage1.Controls.Add(this.buttonBanInfo);
            this.tabPage1.Controls.Add(this.changeShipButton);
            this.tabPage1.Controls.Add(this.reloadFileButton);
            this.tabPage1.Controls.Add(this.piOnline);
            this.tabPage1.Controls.Add(this.label18);
            this.tabPage1.Controls.Add(this.kickPlayerButton);
            this.tabPage1.Controls.Add(this.deletePlayerButton);
            this.tabPage1.Controls.Add(this.piIPAddresses);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.piPath);
            this.tabPage1.Controls.Add(this.openDirButton);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.changeMoneyButton);
            this.tabPage1.Controls.Add(this.changeBanButton);
            this.tabPage1.Controls.Add(this.changeNameButton);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.changeLocationButton);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.piName);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.piAccountID);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.piCreated);
            this.tabPage1.Controls.Add(this.piMoney);
            this.tabPage1.Controls.Add(this.piLastOnline);
            this.tabPage1.Controls.Add(this.piTimePlayed);
            this.tabPage1.Controls.Add(this.piKills);
            this.tabPage1.Controls.Add(this.piRank);
            this.tabPage1.Controls.Add(this.piLocation);
            this.tabPage1.Controls.Add(this.piShip);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonAddCompleteMap
            // 
            resources.ApplyResources(this.buttonAddCompleteMap, "buttonAddCompleteMap");
            this.buttonAddCompleteMap.Name = "buttonAddCompleteMap";
            this.buttonAddCompleteMap.UseVisualStyleBackColor = true;
            this.buttonAddCompleteMap.Click += new System.EventHandler(this.buttonAddCompleteMap_Click);
            // 
            // buttonCheckFile
            // 
            resources.ApplyResources(this.buttonCheckFile, "buttonCheckFile");
            this.buttonCheckFile.Name = "buttonCheckFile";
            this.buttonCheckFile.UseVisualStyleBackColor = true;
            this.buttonCheckFile.Click += new System.EventHandler(this.buttonCheckFile_Click);
            // 
            // buttonResetLastOnline
            // 
            resources.ApplyResources(this.buttonResetLastOnline, "buttonResetLastOnline");
            this.buttonResetLastOnline.Name = "buttonResetLastOnline";
            this.buttonResetLastOnline.UseVisualStyleBackColor = true;
            this.buttonResetLastOnline.Click += new System.EventHandler(this.buttonResetLastOnline_Click);
            // 
            // buttonBanInfo
            // 
            resources.ApplyResources(this.buttonBanInfo, "buttonBanInfo");
            this.buttonBanInfo.Name = "buttonBanInfo";
            this.buttonBanInfo.UseVisualStyleBackColor = true;
            this.buttonBanInfo.Click += new System.EventHandler(this.buttonBanInfo_Click);
            // 
            // reloadFileButton
            // 
            resources.ApplyResources(this.reloadFileButton, "reloadFileButton");
            this.reloadFileButton.Name = "reloadFileButton";
            this.reloadFileButton.UseVisualStyleBackColor = true;
            this.reloadFileButton.Click += new System.EventHandler(this.reloadFileButton_Click);
            // 
            // piOnline
            // 
            resources.ApplyResources(this.piOnline, "piOnline");
            this.piOnline.Name = "piOnline";
            this.piOnline.ReadOnly = true;
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // kickPlayerButton
            // 
            resources.ApplyResources(this.kickPlayerButton, "kickPlayerButton");
            this.kickPlayerButton.Name = "kickPlayerButton";
            this.kickPlayerButton.UseVisualStyleBackColor = true;
            this.kickPlayerButton.Click += new System.EventHandler(this.kickPlayerButton_Click);
            // 
            // deletePlayerButton
            // 
            resources.ApplyResources(this.deletePlayerButton, "deletePlayerButton");
            this.deletePlayerButton.Name = "deletePlayerButton";
            this.deletePlayerButton.UseVisualStyleBackColor = true;
            this.deletePlayerButton.Click += new System.EventHandler(this.deletePlayerButton_Click);
            // 
            // piIPAddresses
            // 
            resources.ApplyResources(this.piIPAddresses, "piIPAddresses");
            this.piIPAddresses.Name = "piIPAddresses";
            this.piIPAddresses.ReadOnly = true;
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // piPath
            // 
            resources.ApplyResources(this.piPath, "piPath");
            this.piPath.Name = "piPath";
            this.piPath.ReadOnly = true;
            // 
            // openDirButton
            // 
            resources.ApplyResources(this.openDirButton, "openDirButton");
            this.openDirButton.Name = "openDirButton";
            this.openDirButton.UseVisualStyleBackColor = true;
            this.openDirButton.Click += new System.EventHandler(this.openDirButton_Click);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // changeMoneyButton
            // 
            resources.ApplyResources(this.changeMoneyButton, "changeMoneyButton");
            this.changeMoneyButton.Name = "changeMoneyButton";
            this.changeMoneyButton.UseVisualStyleBackColor = true;
            this.changeMoneyButton.Click += new System.EventHandler(this.changeMoneyButton_Click);
            // 
            // changeBanButton
            // 
            resources.ApplyResources(this.changeBanButton, "changeBanButton");
            this.changeBanButton.Name = "changeBanButton";
            this.changeBanButton.UseVisualStyleBackColor = true;
            this.changeBanButton.Click += new System.EventHandler(this.changeBanButton_Click);
            // 
            // changeNameButton
            // 
            resources.ApplyResources(this.changeNameButton, "changeNameButton");
            this.changeNameButton.Name = "changeNameButton";
            this.changeNameButton.UseVisualStyleBackColor = true;
            this.changeNameButton.Click += new System.EventHandler(this.changeNameButton_Click);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // changeLocationButton
            // 
            resources.ApplyResources(this.changeLocationButton, "changeLocationButton");
            this.changeLocationButton.Name = "changeLocationButton";
            this.changeLocationButton.UseVisualStyleBackColor = true;
            this.changeLocationButton.Click += new System.EventHandler(this.changeLocationButton_Click);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // piName
            // 
            resources.ApplyResources(this.piName, "piName");
            this.piName.Name = "piName";
            this.piName.ReadOnly = true;
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // piAccountID
            // 
            resources.ApplyResources(this.piAccountID, "piAccountID");
            this.piAccountID.Name = "piAccountID";
            this.piAccountID.ReadOnly = true;
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // piCreated
            // 
            resources.ApplyResources(this.piCreated, "piCreated");
            this.piCreated.Name = "piCreated";
            this.piCreated.ReadOnly = true;
            // 
            // piMoney
            // 
            resources.ApplyResources(this.piMoney, "piMoney");
            this.piMoney.Name = "piMoney";
            this.piMoney.ReadOnly = true;
            // 
            // piLastOnline
            // 
            resources.ApplyResources(this.piLastOnline, "piLastOnline");
            this.piLastOnline.Name = "piLastOnline";
            this.piLastOnline.ReadOnly = true;
            // 
            // piTimePlayed
            // 
            resources.ApplyResources(this.piTimePlayed, "piTimePlayed");
            this.piTimePlayed.Name = "piTimePlayed";
            this.piTimePlayed.ReadOnly = true;
            // 
            // piKills
            // 
            resources.ApplyResources(this.piKills, "piKills");
            this.piKills.Name = "piKills";
            this.piKills.ReadOnly = true;
            // 
            // piRank
            // 
            resources.ApplyResources(this.piRank, "piRank");
            this.piRank.Name = "piRank";
            this.piRank.ReadOnly = true;
            // 
            // piLocation
            // 
            resources.ApplyResources(this.piLocation, "piLocation");
            this.piLocation.Name = "piLocation";
            this.piLocation.ReadOnly = true;
            // 
            // piShip
            // 
            resources.ApplyResources(this.piShip, "piShip");
            this.piShip.Name = "piShip";
            this.piShip.ReadOnly = true;
            // 
            // tabPage2
            // 
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Controls.Add(this.trackBar1);
            this.tabPage2.Controls.Add(this.discardFactionButton);
            this.tabPage2.Controls.Add(this.piReputationEdit);
            this.tabPage2.Controls.Add(this.saveFactionButton);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.piAffiliation);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.piFactionGrid);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            resources.ApplyResources(this.trackBar1, "trackBar1");
            this.trackBar1.BackColor = System.Drawing.SystemColors.Control;
            this.trackBar1.Minimum = -10;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // discardFactionButton
            // 
            resources.ApplyResources(this.discardFactionButton, "discardFactionButton");
            this.discardFactionButton.Name = "discardFactionButton";
            this.discardFactionButton.UseVisualStyleBackColor = true;
            this.discardFactionButton.Click += new System.EventHandler(this.discardFactionButton_Click);
            // 
            // piReputationEdit
            // 
            resources.ApplyResources(this.piReputationEdit, "piReputationEdit");
            this.piReputationEdit.DecimalPlaces = 2;
            this.piReputationEdit.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.piReputationEdit.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.piReputationEdit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.piReputationEdit.Name = "piReputationEdit";
            this.piReputationEdit.ValueChanged += new System.EventHandler(this.piReputationEdit_ValueChanged);
            // 
            // saveFactionButton
            // 
            resources.ApplyResources(this.saveFactionButton, "saveFactionButton");
            this.saveFactionButton.Name = "saveFactionButton";
            this.saveFactionButton.UseVisualStyleBackColor = true;
            this.saveFactionButton.Click += new System.EventHandler(this.saveFactionButton_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // piAffiliation
            // 
            resources.ApplyResources(this.piAffiliation, "piAffiliation");
            this.piAffiliation.Name = "piAffiliation";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // piFactionGrid
            // 
            resources.ApplyResources(this.piFactionGrid, "piFactionGrid");
            this.piFactionGrid.AllowUserToAddRows = false;
            this.piFactionGrid.AllowUserToDeleteRows = false;
            this.piFactionGrid.AutoGenerateColumns = false;
            this.piFactionGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.piFactionGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.piFactionGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.itemDescriptionDataGridViewTextBoxColumn2,
            this.itemNicknameDataGridViewTextBoxColumn,
            this.itemRepDataGridViewTextBoxColumn});
            this.piFactionGrid.DataSource = this.pIFactionTableBindingSource;
            this.piFactionGrid.Name = "piFactionGrid";
            this.piFactionGrid.ReadOnly = true;
            this.piFactionGrid.RowHeadersVisible = false;
            this.piFactionGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.piFactionGrid.SelectionChanged += new System.EventHandler(this.piFactionGrid_SelectionChanged);
            // 
            // tabPage3
            // 
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Controls.Add(this.buttonChangeCargo);
            this.tabPage3.Controls.Add(this.addCargoButton);
            this.tabPage3.Controls.Add(this.removeCargoButton);
            this.tabPage3.Controls.Add(this.discardCargoButton);
            this.tabPage3.Controls.Add(this.saveCargoButton);
            this.tabPage3.Controls.Add(this.piCargoGrid);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // buttonChangeCargo
            // 
            resources.ApplyResources(this.buttonChangeCargo, "buttonChangeCargo");
            this.buttonChangeCargo.Name = "buttonChangeCargo";
            this.buttonChangeCargo.UseVisualStyleBackColor = true;
            this.buttonChangeCargo.Click += new System.EventHandler(this.buttonChangeCargo_Click);
            // 
            // addCargoButton
            // 
            resources.ApplyResources(this.addCargoButton, "addCargoButton");
            this.addCargoButton.Name = "addCargoButton";
            this.addCargoButton.UseVisualStyleBackColor = true;
            this.addCargoButton.Click += new System.EventHandler(this.buttonAddCargo_Click);
            // 
            // removeCargoButton
            // 
            resources.ApplyResources(this.removeCargoButton, "removeCargoButton");
            this.removeCargoButton.Name = "removeCargoButton";
            this.removeCargoButton.UseVisualStyleBackColor = true;
            this.removeCargoButton.Click += new System.EventHandler(this.buttonRemoveCargo_Click);
            // 
            // discardCargoButton
            // 
            resources.ApplyResources(this.discardCargoButton, "discardCargoButton");
            this.discardCargoButton.Name = "discardCargoButton";
            this.discardCargoButton.UseVisualStyleBackColor = true;
            this.discardCargoButton.Click += new System.EventHandler(this.discardCargoButton_Click);
            // 
            // saveCargoButton
            // 
            resources.ApplyResources(this.saveCargoButton, "saveCargoButton");
            this.saveCargoButton.Name = "saveCargoButton";
            this.saveCargoButton.UseVisualStyleBackColor = true;
            this.saveCargoButton.Click += new System.EventHandler(this.saveCargoButton_Click);
            // 
            // piCargoGrid
            // 
            resources.ApplyResources(this.piCargoGrid, "piCargoGrid");
            this.piCargoGrid.AllowUserToAddRows = false;
            this.piCargoGrid.AllowUserToDeleteRows = false;
            this.piCargoGrid.AutoGenerateColumns = false;
            this.piCargoGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.piCargoGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.itemDescriptionDataGridViewTextBoxColumn,
            this.itemCountDataGridViewTextBoxColumn,
            this.itemHashDataGridViewTextBoxColumn});
            this.piCargoGrid.DataSource = this.pICargoListBindingSource;
            this.piCargoGrid.Name = "piCargoGrid";
            this.piCargoGrid.ReadOnly = true;
            this.piCargoGrid.RowHeadersVisible = false;
            this.piCargoGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // tabPage4
            // 
            resources.ApplyResources(this.tabPage4, "tabPage4");
            this.tabPage4.Controls.Add(this.buttonAddEquipment);
            this.tabPage4.Controls.Add(this.removeEquipmentButton);
            this.tabPage4.Controls.Add(this.changeEquipmentButton);
            this.tabPage4.Controls.Add(this.discardEquipmentButton);
            this.tabPage4.Controls.Add(this.saveEquipmentButton);
            this.tabPage4.Controls.Add(this.piEquipmentGrid);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // buttonAddEquipment
            // 
            resources.ApplyResources(this.buttonAddEquipment, "buttonAddEquipment");
            this.buttonAddEquipment.Name = "buttonAddEquipment";
            this.buttonAddEquipment.UseVisualStyleBackColor = true;
            this.buttonAddEquipment.Click += new System.EventHandler(this.addEquipmentButton_Click);
            // 
            // removeEquipmentButton
            // 
            resources.ApplyResources(this.removeEquipmentButton, "removeEquipmentButton");
            this.removeEquipmentButton.Name = "removeEquipmentButton";
            this.removeEquipmentButton.UseVisualStyleBackColor = true;
            this.removeEquipmentButton.Click += new System.EventHandler(this.removeEquipmentButton_Click);
            // 
            // changeEquipmentButton
            // 
            resources.ApplyResources(this.changeEquipmentButton, "changeEquipmentButton");
            this.changeEquipmentButton.Name = "changeEquipmentButton";
            this.changeEquipmentButton.UseVisualStyleBackColor = true;
            this.changeEquipmentButton.Click += new System.EventHandler(this.changeEquipmentButton_Click);
            // 
            // discardEquipmentButton
            // 
            resources.ApplyResources(this.discardEquipmentButton, "discardEquipmentButton");
            this.discardEquipmentButton.Name = "discardEquipmentButton";
            this.discardEquipmentButton.UseVisualStyleBackColor = true;
            this.discardEquipmentButton.Click += new System.EventHandler(this.discardEquipmentButton_Click);
            // 
            // saveEquipmentButton
            // 
            resources.ApplyResources(this.saveEquipmentButton, "saveEquipmentButton");
            this.saveEquipmentButton.Name = "saveEquipmentButton";
            this.saveEquipmentButton.UseVisualStyleBackColor = true;
            this.saveEquipmentButton.Click += new System.EventHandler(this.saveEquipmentButton_Click);
            // 
            // piEquipmentGrid
            // 
            resources.ApplyResources(this.piEquipmentGrid, "piEquipmentGrid");
            this.piEquipmentGrid.AllowUserToAddRows = false;
            this.piEquipmentGrid.AllowUserToDeleteRows = false;
            this.piEquipmentGrid.AutoGenerateColumns = false;
            this.piEquipmentGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.piEquipmentGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.itemHardpointDataGridViewTextBoxColumn,
            this.itemDescriptionDataGridViewTextBoxColumn1,
            this.itemHashDataGridViewTextBoxColumn1,
            this.itemAllowedTypesDataGridViewTextBoxColumn,
            this.itemGameDataTypeDataGridViewTextBoxColumn});
            this.piEquipmentGrid.DataSource = this.pIEquipmentTableBindingSource;
            this.piEquipmentGrid.Name = "piEquipmentGrid";
            this.piEquipmentGrid.ReadOnly = true;
            this.piEquipmentGrid.RowHeadersVisible = false;
            this.piEquipmentGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.piEquipmentGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.piEquipmentGrid_MouseDoubleClick);
            // 
            // tabPage7
            // 
            resources.ApplyResources(this.tabPage7, "tabPage7");
            this.tabPage7.Controls.Add(this.label19);
            this.tabPage7.Controls.Add(this.richTextBoxPlayerInfoAdminText);
            this.tabPage7.Controls.Add(this.label12);
            this.tabPage7.Controls.Add(this.label1);
            this.tabPage7.Controls.Add(this.richTextBoxPlayerInfoPlayerText);
            this.tabPage7.Controls.Add(this.buttonPlayerInfoSaveAdminText);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // richTextBoxPlayerInfoAdminText
            // 
            resources.ApplyResources(this.richTextBoxPlayerInfoAdminText, "richTextBoxPlayerInfoAdminText");
            this.richTextBoxPlayerInfoAdminText.Name = "richTextBoxPlayerInfoAdminText";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // richTextBoxPlayerInfoPlayerText
            // 
            resources.ApplyResources(this.richTextBoxPlayerInfoPlayerText, "richTextBoxPlayerInfoPlayerText");
            this.richTextBoxPlayerInfoPlayerText.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBoxPlayerInfoPlayerText.Name = "richTextBoxPlayerInfoPlayerText";
            this.richTextBoxPlayerInfoPlayerText.ReadOnly = true;
            // 
            // buttonPlayerInfoSaveAdminText
            // 
            resources.ApplyResources(this.buttonPlayerInfoSaveAdminText, "buttonPlayerInfoSaveAdminText");
            this.buttonPlayerInfoSaveAdminText.Name = "buttonPlayerInfoSaveAdminText";
            this.buttonPlayerInfoSaveAdminText.UseVisualStyleBackColor = true;
            this.buttonPlayerInfoSaveAdminText.Click += new System.EventHandler(this.buttonPlayerInfoSaveAdminText_Click);
            // 
            // tabPage5
            // 
            resources.ApplyResources(this.tabPage5, "tabPage5");
            this.tabPage5.Controls.Add(this.checkBoxShowOriginalFile);
            this.tabPage5.Controls.Add(this.piFileView);
            this.tabPage5.Controls.Add(this.saveFileManualButton);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowOriginalFile
            // 
            resources.ApplyResources(this.checkBoxShowOriginalFile, "checkBoxShowOriginalFile");
            this.checkBoxShowOriginalFile.Name = "checkBoxShowOriginalFile";
            this.checkBoxShowOriginalFile.UseVisualStyleBackColor = true;
            // 
            // piFileView
            // 
            resources.ApplyResources(this.piFileView, "piFileView");
            this.piFileView.Name = "piFileView";
            // 
            // saveFileManualButton
            // 
            resources.ApplyResources(this.saveFileManualButton, "saveFileManualButton");
            this.saveFileManualButton.Name = "saveFileManualButton";
            this.saveFileManualButton.UseVisualStyleBackColor = true;
            this.saveFileManualButton.Click += new System.EventHandler(this.saveFileManualButton_Click);
            // 
            // tabPage6
            // 
            resources.ApplyResources(this.tabPage6, "tabPage6");
            this.tabPage6.Controls.Add(this.richTextBoxLog);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // richTextBoxLog
            // 
            resources.ApplyResources(this.richTextBoxLog, "richTextBoxLog");
            this.richTextBoxLog.Name = "richTextBoxLog";
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.propertiesToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            // 
            // propertiesToolStripMenuItem
            // 
            resources.ApplyResources(this.propertiesToolStripMenuItem, "propertiesToolStripMenuItem");
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bannedPlayersToolStripMenuItem,
            this.statisticsToolStripMenuItem,
            this.searchIPtoolStripMenuItem,
            this.searchLoginIDToolStripMenuItem,
            this.itemListToolStripMenuItem,
            this.fLFileEditorToolStripMenuItem,
            this.toolStripSeparator1,
            this.rescanAccountFilesToolStripMenuItem,
            this.reloadGameDataToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            // 
            // bannedPlayersToolStripMenuItem
            // 
            resources.ApplyResources(this.bannedPlayersToolStripMenuItem, "bannedPlayersToolStripMenuItem");
            this.bannedPlayersToolStripMenuItem.Name = "bannedPlayersToolStripMenuItem";
            this.bannedPlayersToolStripMenuItem.Click += new System.EventHandler(this.bannedPlayersToolStripMenuItem_Click);
            // 
            // statisticsToolStripMenuItem
            // 
            resources.ApplyResources(this.statisticsToolStripMenuItem, "statisticsToolStripMenuItem");
            this.statisticsToolStripMenuItem.Name = "statisticsToolStripMenuItem";
            this.statisticsToolStripMenuItem.Click += new System.EventHandler(this.statisticsToolStripMenuItem_Click);
            // 
            // searchIPtoolStripMenuItem
            // 
            resources.ApplyResources(this.searchIPtoolStripMenuItem, "searchIPtoolStripMenuItem");
            this.searchIPtoolStripMenuItem.Name = "searchIPtoolStripMenuItem";
            this.searchIPtoolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // searchLoginIDToolStripMenuItem
            // 
            resources.ApplyResources(this.searchLoginIDToolStripMenuItem, "searchLoginIDToolStripMenuItem");
            this.searchLoginIDToolStripMenuItem.Name = "searchLoginIDToolStripMenuItem";
            this.searchLoginIDToolStripMenuItem.Click += new System.EventHandler(this.searchLoginIDToolStripMenuItem_Click);
            // 
            // itemListToolStripMenuItem
            // 
            resources.ApplyResources(this.itemListToolStripMenuItem, "itemListToolStripMenuItem");
            this.itemListToolStripMenuItem.Name = "itemListToolStripMenuItem";
            this.itemListToolStripMenuItem.Click += new System.EventHandler(this.hashcodeListToolStripMenuItem_Click);
            // 
            // fLFileEditorToolStripMenuItem
            // 
            resources.ApplyResources(this.fLFileEditorToolStripMenuItem, "fLFileEditorToolStripMenuItem");
            this.fLFileEditorToolStripMenuItem.Name = "fLFileEditorToolStripMenuItem";
            this.fLFileEditorToolStripMenuItem.Click += new System.EventHandler(this.fLFileEditorToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // rescanAccountFilesToolStripMenuItem
            // 
            resources.ApplyResources(this.rescanAccountFilesToolStripMenuItem, "rescanAccountFilesToolStripMenuItem");
            this.rescanAccountFilesToolStripMenuItem.Name = "rescanAccountFilesToolStripMenuItem";
            this.rescanAccountFilesToolStripMenuItem.Click += new System.EventHandler(this.rescanAccountFilesToolStripMenuItem_Click);
            // 
            // reloadGameDataToolStripMenuItem
            // 
            resources.ApplyResources(this.reloadGameDataToolStripMenuItem, "reloadGameDataToolStripMenuItem");
            this.reloadGameDataToolStripMenuItem.Name = "reloadGameDataToolStripMenuItem";
            this.reloadGameDataToolStripMenuItem.Click += new System.EventHandler(this.reloadGameDataToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            // 
            // toolStripSeparator5
            // 
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            // 
            // aboutToolStripMenuItem
            // 
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // charListDataGridView
            // 
            resources.ApplyResources(this.charListDataGridView, "charListDataGridView");
            this.charListDataGridView.AllowUserToAddRows = false;
            this.charListDataGridView.AllowUserToDeleteRows = false;
            this.charListDataGridView.AllowUserToOrderColumns = true;
            this.charListDataGridView.AutoGenerateColumns = false;
            this.charListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.charListDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LastOnLine,
            this.OnLineSecs,
            this.charPathDataGridViewTextBoxColumn,
            this.accDirDataGridViewTextBoxColumn,
            this.accIDDataGridViewTextBoxColumn,
            this.charNameDataGridViewTextBoxColumn,
            this.isDeletedDataGridViewCheckBoxColumn,
            this.locationDataGridViewTextBoxColumn,
            this.shipDataGridViewTextBoxColumn,
            this.moneyDataGridViewTextBoxColumn,
            this.rankDataGridViewTextBoxColumn,
            this.createdDataGridViewTextBoxColumn,
            this.updatedDataGridViewTextBoxColumn,
            this.onLineSecsDataGridViewTextBoxColumn,
            this.lastOnLineDataGridViewTextBoxColumn});
            this.charListDataGridView.DataSource = this.characterListBindingSource;
            this.charListDataGridView.MultiSelect = false;
            this.charListDataGridView.Name = "charListDataGridView";
            this.charListDataGridView.ReadOnly = true;
            this.charListDataGridView.RowHeadersVisible = false;
            this.charListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.charListDataGridView.SelectionChanged += new System.EventHandler(this.charListDataGridView_SelectionChanged);
            // 
            // LastOnLine
            // 
            this.LastOnLine.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.LastOnLine.DataPropertyName = "LastOnLine";
            resources.ApplyResources(this.LastOnLine, "LastOnLine");
            this.LastOnLine.Name = "LastOnLine";
            this.LastOnLine.ReadOnly = true;
            // 
            // OnLineSecs
            // 
            this.OnLineSecs.DataPropertyName = "OnLineSecs";
            resources.ApplyResources(this.OnLineSecs, "OnLineSecs");
            this.OnLineSecs.Name = "OnLineSecs";
            this.OnLineSecs.ReadOnly = true;
            // 
            // checkBoxFilterDeleted
            // 
            resources.ApplyResources(this.checkBoxFilterDeleted, "checkBoxFilterDeleted");
            this.checkBoxFilterDeleted.Name = "checkBoxFilterDeleted";
            this.checkBoxFilterDeleted.UseVisualStyleBackColor = true;
            this.checkBoxFilterDeleted.CheckedChanged += new System.EventHandler(this.checkBoxFilterDeleted_CheckedChanged);
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel3,
            this.progressBar,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabelStatus,
            this.toolStripStatusLabel4,
            this.toolStripDBPending,
            this.toolStripStatusLabel1,
            this.toolStripHookState});
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel3
            // 
            resources.ApplyResources(this.toolStripStatusLabel3, "toolStripStatusLabel3");
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            this.progressBar.Step = 1;
            // 
            // toolStripStatusLabel2
            // 
            resources.ApplyResources(this.toolStripStatusLabel2, "toolStripStatusLabel2");
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            // 
            // toolStripStatusLabelStatus
            // 
            resources.ApplyResources(this.toolStripStatusLabelStatus, "toolStripStatusLabelStatus");
            this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
            this.toolStripStatusLabelStatus.Spring = true;
            // 
            // toolStripStatusLabel4
            // 
            resources.ApplyResources(this.toolStripStatusLabel4, "toolStripStatusLabel4");
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            // 
            // toolStripDBPending
            // 
            resources.ApplyResources(this.toolStripDBPending, "toolStripDBPending");
            this.toolStripDBPending.Name = "toolStripDBPending";
            // 
            // toolStripStatusLabel1
            // 
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(6, 3, 0, 2);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            // 
            // toolStripHookState
            // 
            resources.ApplyResources(this.toolStripHookState, "toolStripHookState");
            this.toolStripHookState.Name = "toolStripHookState";
            this.toolStripHookState.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // updatePlayerInfoTimer
            // 
            this.updatePlayerInfoTimer.Interval = 1;
            this.updatePlayerInfoTimer.Tick += new System.EventHandler(this.updatePlayerInfoTimer_Tick);
            // 
            // checkBoxFilterSameIP
            // 
            resources.ApplyResources(this.checkBoxFilterSameIP, "checkBoxFilterSameIP");
            this.checkBoxFilterSameIP.Name = "checkBoxFilterSameIP";
            this.checkBoxFilterSameIP.UseVisualStyleBackColor = true;
            this.checkBoxFilterSameIP.CheckedChanged += new System.EventHandler(this.checkBoxFilterSameIP_CheckedChanged);
            // 
            // timerPeriodicTasks
            // 
            this.timerPeriodicTasks.Interval = 10000;
            this.timerPeriodicTasks.Tick += new System.EventHandler(this.timerPeriodicTasks_Tick);
            // 
            // checkBoxFilterSameLoginID
            // 
            resources.ApplyResources(this.checkBoxFilterSameLoginID, "checkBoxFilterSameLoginID");
            this.checkBoxFilterSameLoginID.Name = "checkBoxFilterSameLoginID";
            this.checkBoxFilterSameLoginID.UseVisualStyleBackColor = true;
            this.checkBoxFilterSameLoginID.CheckedChanged += new System.EventHandler(this.checkBoxFilterSameLoginID_CheckedChanged);
            // 
            // checkBoxFilterSameAccount
            // 
            resources.ApplyResources(this.checkBoxFilterSameAccount, "checkBoxFilterSameAccount");
            this.checkBoxFilterSameAccount.Name = "checkBoxFilterSameAccount";
            this.checkBoxFilterSameAccount.UseVisualStyleBackColor = true;
            this.checkBoxFilterSameAccount.CheckedChanged += new System.EventHandler(this.checkBoxFilterSameAcc_CheckedChanged);
            // 
            // timerFilter
            // 
            this.timerFilter.Interval = 1;
            this.timerFilter.Tick += new System.EventHandler(this.FilterUpdate);
            // 
            // timerDBSave
            // 
            this.timerDBSave.Interval = 10000;
            this.timerDBSave.Tick += new System.EventHandler(this.timerDBSave_Tick);
            // 
            // timerShutdown
            // 
            this.timerShutdown.Tick += new System.EventHandler(this.timerShutdown_Tick);
            // 
            // charPathDataGridViewTextBoxColumn
            // 
            this.charPathDataGridViewTextBoxColumn.DataPropertyName = "CharPath";
            resources.ApplyResources(this.charPathDataGridViewTextBoxColumn, "charPathDataGridViewTextBoxColumn");
            this.charPathDataGridViewTextBoxColumn.Name = "charPathDataGridViewTextBoxColumn";
            this.charPathDataGridViewTextBoxColumn.ReadOnly = true;
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
            // charNameDataGridViewTextBoxColumn
            // 
            this.charNameDataGridViewTextBoxColumn.DataPropertyName = "CharName";
            resources.ApplyResources(this.charNameDataGridViewTextBoxColumn, "charNameDataGridViewTextBoxColumn");
            this.charNameDataGridViewTextBoxColumn.Name = "charNameDataGridViewTextBoxColumn";
            this.charNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isDeletedDataGridViewCheckBoxColumn
            // 
            this.isDeletedDataGridViewCheckBoxColumn.DataPropertyName = "IsDeleted";
            resources.ApplyResources(this.isDeletedDataGridViewCheckBoxColumn, "isDeletedDataGridViewCheckBoxColumn");
            this.isDeletedDataGridViewCheckBoxColumn.Name = "isDeletedDataGridViewCheckBoxColumn";
            this.isDeletedDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // locationDataGridViewTextBoxColumn
            // 
            this.locationDataGridViewTextBoxColumn.DataPropertyName = "Location";
            resources.ApplyResources(this.locationDataGridViewTextBoxColumn, "locationDataGridViewTextBoxColumn");
            this.locationDataGridViewTextBoxColumn.Name = "locationDataGridViewTextBoxColumn";
            this.locationDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // shipDataGridViewTextBoxColumn
            // 
            this.shipDataGridViewTextBoxColumn.DataPropertyName = "Ship";
            resources.ApplyResources(this.shipDataGridViewTextBoxColumn, "shipDataGridViewTextBoxColumn");
            this.shipDataGridViewTextBoxColumn.Name = "shipDataGridViewTextBoxColumn";
            this.shipDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // moneyDataGridViewTextBoxColumn
            // 
            this.moneyDataGridViewTextBoxColumn.DataPropertyName = "Money";
            resources.ApplyResources(this.moneyDataGridViewTextBoxColumn, "moneyDataGridViewTextBoxColumn");
            this.moneyDataGridViewTextBoxColumn.Name = "moneyDataGridViewTextBoxColumn";
            this.moneyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rankDataGridViewTextBoxColumn
            // 
            this.rankDataGridViewTextBoxColumn.DataPropertyName = "Rank";
            resources.ApplyResources(this.rankDataGridViewTextBoxColumn, "rankDataGridViewTextBoxColumn");
            this.rankDataGridViewTextBoxColumn.Name = "rankDataGridViewTextBoxColumn";
            this.rankDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // createdDataGridViewTextBoxColumn
            // 
            this.createdDataGridViewTextBoxColumn.DataPropertyName = "Created";
            resources.ApplyResources(this.createdDataGridViewTextBoxColumn, "createdDataGridViewTextBoxColumn");
            this.createdDataGridViewTextBoxColumn.Name = "createdDataGridViewTextBoxColumn";
            this.createdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // updatedDataGridViewTextBoxColumn
            // 
            this.updatedDataGridViewTextBoxColumn.DataPropertyName = "Updated";
            resources.ApplyResources(this.updatedDataGridViewTextBoxColumn, "updatedDataGridViewTextBoxColumn");
            this.updatedDataGridViewTextBoxColumn.Name = "updatedDataGridViewTextBoxColumn";
            this.updatedDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // onLineSecsDataGridViewTextBoxColumn
            // 
            this.onLineSecsDataGridViewTextBoxColumn.DataPropertyName = "OnLineSecs";
            resources.ApplyResources(this.onLineSecsDataGridViewTextBoxColumn, "onLineSecsDataGridViewTextBoxColumn");
            this.onLineSecsDataGridViewTextBoxColumn.Name = "onLineSecsDataGridViewTextBoxColumn";
            this.onLineSecsDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lastOnLineDataGridViewTextBoxColumn
            // 
            this.lastOnLineDataGridViewTextBoxColumn.DataPropertyName = "LastOnLine";
            resources.ApplyResources(this.lastOnLineDataGridViewTextBoxColumn, "lastOnLineDataGridViewTextBoxColumn");
            this.lastOnLineDataGridViewTextBoxColumn.Name = "lastOnLineDataGridViewTextBoxColumn";
            this.lastOnLineDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // characterListBindingSource
            // 
            this.characterListBindingSource.DataSource = typeof(Root.Components.CharacterItem);
            // 
            // itemDescriptionDataGridViewTextBoxColumn2
            // 
            this.itemDescriptionDataGridViewTextBoxColumn2.DataPropertyName = "itemDescription";
            resources.ApplyResources(this.itemDescriptionDataGridViewTextBoxColumn2, "itemDescriptionDataGridViewTextBoxColumn2");
            this.itemDescriptionDataGridViewTextBoxColumn2.Name = "itemDescriptionDataGridViewTextBoxColumn2";
            this.itemDescriptionDataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // itemNicknameDataGridViewTextBoxColumn
            // 
            this.itemNicknameDataGridViewTextBoxColumn.DataPropertyName = "itemNickname";
            resources.ApplyResources(this.itemNicknameDataGridViewTextBoxColumn, "itemNicknameDataGridViewTextBoxColumn");
            this.itemNicknameDataGridViewTextBoxColumn.Name = "itemNicknameDataGridViewTextBoxColumn";
            this.itemNicknameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // itemRepDataGridViewTextBoxColumn
            // 
            this.itemRepDataGridViewTextBoxColumn.DataPropertyName = "itemRep";
            resources.ApplyResources(this.itemRepDataGridViewTextBoxColumn, "itemRepDataGridViewTextBoxColumn");
            this.itemRepDataGridViewTextBoxColumn.Name = "itemRepDataGridViewTextBoxColumn";
            this.itemRepDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // pIFactionTableBindingSource
            // 
            this.pIFactionTableBindingSource.DataSource = typeof(Root.Components.PIFaction);
            // 
            // itemDescriptionDataGridViewTextBoxColumn
            // 
            this.itemDescriptionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.itemDescriptionDataGridViewTextBoxColumn.DataPropertyName = "itemDescription";
            resources.ApplyResources(this.itemDescriptionDataGridViewTextBoxColumn, "itemDescriptionDataGridViewTextBoxColumn");
            this.itemDescriptionDataGridViewTextBoxColumn.Name = "itemDescriptionDataGridViewTextBoxColumn";
            this.itemDescriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // itemCountDataGridViewTextBoxColumn
            // 
            this.itemCountDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.itemCountDataGridViewTextBoxColumn.DataPropertyName = "itemCount";
            resources.ApplyResources(this.itemCountDataGridViewTextBoxColumn, "itemCountDataGridViewTextBoxColumn");
            this.itemCountDataGridViewTextBoxColumn.Name = "itemCountDataGridViewTextBoxColumn";
            this.itemCountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // itemHashDataGridViewTextBoxColumn
            // 
            this.itemHashDataGridViewTextBoxColumn.DataPropertyName = "itemHash";
            resources.ApplyResources(this.itemHashDataGridViewTextBoxColumn, "itemHashDataGridViewTextBoxColumn");
            this.itemHashDataGridViewTextBoxColumn.Name = "itemHashDataGridViewTextBoxColumn";
            this.itemHashDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // pICargoListBindingSource
            // 
            this.pICargoListBindingSource.DataSource = typeof(Root.Components.PICargo);
            // 
            // itemHardpointDataGridViewTextBoxColumn
            // 
            this.itemHardpointDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.itemHardpointDataGridViewTextBoxColumn.DataPropertyName = "itemHardpoint";
            resources.ApplyResources(this.itemHardpointDataGridViewTextBoxColumn, "itemHardpointDataGridViewTextBoxColumn");
            this.itemHardpointDataGridViewTextBoxColumn.Name = "itemHardpointDataGridViewTextBoxColumn";
            this.itemHardpointDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // itemDescriptionDataGridViewTextBoxColumn1
            // 
            this.itemDescriptionDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.itemDescriptionDataGridViewTextBoxColumn1.DataPropertyName = "itemDescription";
            resources.ApplyResources(this.itemDescriptionDataGridViewTextBoxColumn1, "itemDescriptionDataGridViewTextBoxColumn1");
            this.itemDescriptionDataGridViewTextBoxColumn1.Name = "itemDescriptionDataGridViewTextBoxColumn1";
            this.itemDescriptionDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // itemHashDataGridViewTextBoxColumn1
            // 
            this.itemHashDataGridViewTextBoxColumn1.DataPropertyName = "itemHash";
            resources.ApplyResources(this.itemHashDataGridViewTextBoxColumn1, "itemHashDataGridViewTextBoxColumn1");
            this.itemHashDataGridViewTextBoxColumn1.Name = "itemHashDataGridViewTextBoxColumn1";
            this.itemHashDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // itemAllowedTypesDataGridViewTextBoxColumn
            // 
            this.itemAllowedTypesDataGridViewTextBoxColumn.DataPropertyName = "itemAllowedTypes";
            resources.ApplyResources(this.itemAllowedTypesDataGridViewTextBoxColumn, "itemAllowedTypesDataGridViewTextBoxColumn");
            this.itemAllowedTypesDataGridViewTextBoxColumn.Name = "itemAllowedTypesDataGridViewTextBoxColumn";
            this.itemAllowedTypesDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // itemGameDataTypeDataGridViewTextBoxColumn
            // 
            this.itemGameDataTypeDataGridViewTextBoxColumn.DataPropertyName = "itemGameDataType";
            resources.ApplyResources(this.itemGameDataTypeDataGridViewTextBoxColumn, "itemGameDataTypeDataGridViewTextBoxColumn");
            this.itemGameDataTypeDataGridViewTextBoxColumn.Name = "itemGameDataTypeDataGridViewTextBoxColumn";
            this.itemGameDataTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // pIEquipmentTableBindingSource
            // 
            this.pIEquipmentTableBindingSource.DataSource = typeof(Root.Components.PIEquipment);
            // 
            // MainWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxFilterSameAccount);
            this.Controls.Add(this.checkBoxFilterSameLoginID);
            this.Controls.Add(this.checkBoxFilterSameIP);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.checkBoxFilterDeleted);
            this.Controls.Add(this.charListDataGridView);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.textBoxFilter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.piReputationEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.piFactionGrid)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.piCargoGrid)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.piEquipmentGrid)).EndInit();
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.charListDataGridView)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.characterListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pIFactionTableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pICargoListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pIEquipmentTableBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.DataGridView charListDataGridView;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.BindingSource characterListBindingSource;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView piFactionGrid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button saveFactionButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox piAffiliation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox piName;
        private System.Windows.Forms.TextBox piAccountID;
        private System.Windows.Forms.TextBox piRank;
        private System.Windows.Forms.TextBox piMoney;
        private System.Windows.Forms.TextBox piLocation;
        private System.Windows.Forms.TextBox piShip;
        private System.Windows.Forms.TextBox piKills;
        private System.Windows.Forms.TextBox piTimePlayed;
        private System.Windows.Forms.TextBox piLastOnline;
        private System.Windows.Forms.TextBox piCreated;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button changeMoneyButton;
        private System.Windows.Forms.Button changeNameButton;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button changeBanButton;
        private System.Windows.Forms.Button changeLocationButton;
        private System.Windows.Forms.NumericUpDown piReputationEdit;
        private System.Windows.Forms.Button discardFactionButton;
        private System.Windows.Forms.TextBox piPath;
        private System.Windows.Forms.Button openDirButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button deletePlayerButton;
        private System.Windows.Forms.CheckBox checkBoxFilterDeleted;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button kickPlayerButton;
        private System.Windows.Forms.TextBox piOnline;
        private System.Windows.Forms.Button reloadFileButton;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button discardCargoButton;
        private System.Windows.Forms.Button saveCargoButton;
        private System.Windows.Forms.Button discardEquipmentButton;
        private System.Windows.Forms.Button saveEquipmentButton;
        private System.Windows.Forms.Button addCargoButton;
        private System.Windows.Forms.Button removeCargoButton;
        private System.Windows.Forms.Button removeEquipmentButton;
        private System.Windows.Forms.Button changeEquipmentButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripHookState;
        public System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.DataGridView piCargoGrid;
        public System.Windows.Forms.DataGridView piEquipmentGrid;
        private System.Windows.Forms.Button changeShipButton;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bannedPlayersToolStripMenuItem;
        private System.Windows.Forms.BindingSource pICargoListBindingSource;
        private System.Windows.Forms.BindingSource pIEquipmentTableBindingSource;
        private System.Windows.Forms.Button saveFileManualButton;
        private System.Windows.Forms.RichTextBox piFileView;
        private System.Windows.Forms.Timer updatePlayerInfoTimer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button buttonBanInfo;
        private System.Windows.Forms.BindingSource pIFactionTableBindingSource;
        private System.Windows.Forms.Button buttonChangeCargo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem itemListToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemDescriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemHashDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemHardpointDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemDescriptionDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemHashDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemAllowedTypesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemGameDataTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button buttonAddEquipment;
        private System.Windows.Forms.ToolStripMenuItem rescanAccountFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.CheckBox checkBoxFilterSameIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn OnLineSecs;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button buttonResetLastOnline;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripDBPending;
        private System.Windows.Forms.ToolStripMenuItem statisticsToolStripMenuItem;
        private System.Windows.Forms.Timer timerPeriodicTasks;
        private System.Windows.Forms.CheckBox checkBoxFilterSameLoginID;
        private System.Windows.Forms.CheckBox checkBoxFilterSameAccount;
        private System.Windows.Forms.Timer timerFilter;
        private System.Windows.Forms.Timer timerDBSave;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.Button buttonCheckFile;
        private System.Windows.Forms.ToolStripMenuItem searchIPtoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fLFileEditorToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxShowOriginalFile;
        private System.Windows.Forms.Button buttonAddCompleteMap;
        private System.Windows.Forms.TextBox piIPAddresses;
        private System.Windows.Forms.Timer timerShutdown;
        private System.Windows.Forms.ToolStripMenuItem reloadGameDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchLoginIDToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.RichTextBox richTextBoxPlayerInfoAdminText;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBoxPlayerInfoPlayerText;
        private System.Windows.Forms.Button buttonPlayerInfoSaveAdminText;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DataGridViewTextBoxColumn charPathDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accDirDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn charNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isDeletedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn shipDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn moneyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rankDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn updatedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn onLineSecsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastOnLineDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemDescriptionDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemNicknameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemRepDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastOnLine;
    }
}

