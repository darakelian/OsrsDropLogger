﻿using OsrsDropEditor.DataGathering;

namespace OsrsDropEditor
{
    partial class MainForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.npcNameTextBox = new System.Windows.Forms.TextBox();
            this.npcNameLabel = new System.Windows.Forms.Label();
            this.npcListGridView = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.npcNameBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dropsPanel = new System.Windows.Forms.Panel();
            this.dropsListView = new System.Windows.Forms.ListView();
            this.userInteractionPanel = new System.Windows.Forms.Panel();
            this.labelLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.totalValueLabel = new System.Windows.Forms.Label();
            this.gpPerHourLabel = new System.Windows.Forms.Label();
            this.loggedDropView = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalPriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loggedDropBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buttonTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.starButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updatePricesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateDropsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateTreasureTrailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logClueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highscoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tripToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startTripToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.endTripToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logSuppliesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopwatchUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.totalValueToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.gpPerHourTimer = new System.Windows.Forms.Timer(this.components);
            this.rarityTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npcListGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcNameBindingSource)).BeginInit();
            this.dropsPanel.SuspendLayout();
            this.userInteractionPanel.SuspendLayout();
            this.labelLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loggedDropView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loggedDropBindingSource)).BeginInit();
            this.buttonTableLayoutPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.npcNameTextBox);
            this.panel1.Controls.Add(this.npcNameLabel);
            this.panel1.Controls.Add(this.npcListGridView);
            this.panel1.Location = new System.Drawing.Point(0, 48);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(414, 660);
            this.panel1.TabIndex = 1;
            // 
            // npcNameTextBox
            // 
            this.npcNameTextBox.Location = new System.Drawing.Point(138, 0);
            this.npcNameTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.npcNameTextBox.Name = "npcNameTextBox";
            this.npcNameTextBox.Size = new System.Drawing.Size(274, 31);
            this.npcNameTextBox.TabIndex = 3;
            // 
            // npcNameLabel
            // 
            this.npcNameLabel.Location = new System.Drawing.Point(12, 0);
            this.npcNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.npcNameLabel.Name = "npcNameLabel";
            this.npcNameLabel.Size = new System.Drawing.Size(130, 38);
            this.npcNameLabel.TabIndex = 2;
            this.npcNameLabel.Text = "NPC Name:";
            // 
            // npcListGridView
            // 
            this.npcListGridView.AllowUserToAddRows = false;
            this.npcListGridView.AllowUserToResizeRows = false;
            this.npcListGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.npcListGridView.AutoGenerateColumns = false;
            this.npcListGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.npcListGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn});
            this.npcListGridView.DataSource = this.npcNameBindingSource;
            this.npcListGridView.Location = new System.Drawing.Point(0, 42);
            this.npcListGridView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.npcListGridView.MultiSelect = false;
            this.npcListGridView.Name = "npcListGridView";
            this.npcListGridView.RowHeadersVisible = false;
            this.npcListGridView.RowTemplate.Height = 37;
            this.npcListGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.npcListGridView.Size = new System.Drawing.Size(414, 617);
            this.npcListGridView.TabIndex = 1;
            this.npcListGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.npcListGridView_CellMouseDown);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "NPC Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // npcNameBindingSource
            // 
            this.npcNameBindingSource.DataSource = typeof(OsrsDropEditor.NpcName);
            // 
            // dropsPanel
            // 
            this.dropsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dropsPanel.Controls.Add(this.dropsListView);
            this.dropsPanel.Location = new System.Drawing.Point(420, 48);
            this.dropsPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dropsPanel.Name = "dropsPanel";
            this.dropsPanel.Size = new System.Drawing.Size(540, 660);
            this.dropsPanel.TabIndex = 2;
            // 
            // dropsListView
            // 
            this.dropsListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.dropsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dropsListView.Location = new System.Drawing.Point(0, 0);
            this.dropsListView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dropsListView.Name = "dropsListView";
            this.dropsListView.ShowItemToolTips = true;
            this.dropsListView.Size = new System.Drawing.Size(540, 660);
            this.dropsListView.TabIndex = 0;
            this.dropsListView.UseCompatibleStateImageBehavior = false;
            this.dropsListView.ItemActivate += new System.EventHandler(this.dropsListView_ItemActivate);
            // 
            // userInteractionPanel
            // 
            this.userInteractionPanel.Controls.Add(this.labelLayoutPanel);
            this.userInteractionPanel.Controls.Add(this.loggedDropView);
            this.userInteractionPanel.Controls.Add(this.buttonTableLayoutPanel);
            this.userInteractionPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.userInteractionPanel.Location = new System.Drawing.Point(966, 46);
            this.userInteractionPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.userInteractionPanel.Name = "userInteractionPanel";
            this.userInteractionPanel.Size = new System.Drawing.Size(382, 662);
            this.userInteractionPanel.TabIndex = 3;
            // 
            // labelLayoutPanel
            // 
            this.labelLayoutPanel.ColumnCount = 2;
            this.labelLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.labelLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.labelLayoutPanel.Controls.Add(this.totalValueLabel, 0, 0);
            this.labelLayoutPanel.Controls.Add(this.gpPerHourLabel, 1, 0);
            this.labelLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelLayoutPanel.Location = new System.Drawing.Point(0, 572);
            this.labelLayoutPanel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.labelLayoutPanel.Name = "labelLayoutPanel";
            this.labelLayoutPanel.RowCount = 1;
            this.labelLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.labelLayoutPanel.Size = new System.Drawing.Size(382, 38);
            this.labelLayoutPanel.TabIndex = 3;
            // 
            // totalValueLabel
            // 
            this.totalValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.totalValueLabel.Location = new System.Drawing.Point(6, 7);
            this.totalValueLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.totalValueLabel.Name = "totalValueLabel";
            this.totalValueLabel.Size = new System.Drawing.Size(242, 31);
            this.totalValueLabel.TabIndex = 1;
            this.totalValueLabel.Text = "Total Value: ";
            // 
            // gpPerHourLabel
            // 
            this.gpPerHourLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpPerHourLabel.Location = new System.Drawing.Point(260, 7);
            this.gpPerHourLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.gpPerHourLabel.Name = "gpPerHourLabel";
            this.gpPerHourLabel.Size = new System.Drawing.Size(116, 31);
            this.gpPerHourLabel.TabIndex = 2;
            // 
            // loggedDropView
            // 
            this.loggedDropView.AllowUserToAddRows = false;
            this.loggedDropView.AllowUserToResizeRows = false;
            this.loggedDropView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loggedDropView.AutoGenerateColumns = false;
            this.loggedDropView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.loggedDropView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn1,
            this.quantityDataGridViewTextBoxColumn,
            this.totalPriceDataGridViewTextBoxColumn});
            this.loggedDropView.DataSource = this.loggedDropBindingSource;
            this.loggedDropView.Location = new System.Drawing.Point(0, 0);
            this.loggedDropView.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.loggedDropView.Name = "loggedDropView";
            this.loggedDropView.ReadOnly = true;
            this.loggedDropView.RowHeadersVisible = false;
            this.loggedDropView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.loggedDropView.Size = new System.Drawing.Size(384, 569);
            this.loggedDropView.TabIndex = 2;
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            this.nameDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn1.DataPropertyName = "DisplayName";
            this.nameDataGridViewTextBoxColumn1.FillWeight = 150F;
            this.nameDataGridViewTextBoxColumn1.HeaderText = "Drop";
            this.nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            this.nameDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // quantityDataGridViewTextBoxColumn
            // 
            this.quantityDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.quantityDataGridViewTextBoxColumn.DataPropertyName = "Quantity";
            this.quantityDataGridViewTextBoxColumn.FillWeight = 75F;
            this.quantityDataGridViewTextBoxColumn.HeaderText = "Quantity";
            this.quantityDataGridViewTextBoxColumn.Name = "quantityDataGridViewTextBoxColumn";
            this.quantityDataGridViewTextBoxColumn.ReadOnly = true;
            this.quantityDataGridViewTextBoxColumn.Visible = false;
            // 
            // totalPriceDataGridViewTextBoxColumn
            // 
            this.totalPriceDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.totalPriceDataGridViewTextBoxColumn.DataPropertyName = "TotalPrice";
            this.totalPriceDataGridViewTextBoxColumn.FillWeight = 75F;
            this.totalPriceDataGridViewTextBoxColumn.HeaderText = "Total Price";
            this.totalPriceDataGridViewTextBoxColumn.Name = "totalPriceDataGridViewTextBoxColumn";
            this.totalPriceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // loggedDropBindingSource
            // 
            this.loggedDropBindingSource.DataSource = typeof(OsrsDropEditor.DataGathering.LoggedDrop);
            // 
            // buttonTableLayoutPanel
            // 
            this.buttonTableLayoutPanel.ColumnCount = 3;
            this.buttonTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.buttonTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.buttonTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.buttonTableLayoutPanel.Controls.Add(this.starButton, 0, 0);
            this.buttonTableLayoutPanel.Controls.Add(this.pauseButton, 1, 0);
            this.buttonTableLayoutPanel.Controls.Add(this.clearButton, 2, 0);
            this.buttonTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonTableLayoutPanel.Location = new System.Drawing.Point(0, 610);
            this.buttonTableLayoutPanel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.buttonTableLayoutPanel.Name = "buttonTableLayoutPanel";
            this.buttonTableLayoutPanel.RowCount = 1;
            this.buttonTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.buttonTableLayoutPanel.Size = new System.Drawing.Size(382, 52);
            this.buttonTableLayoutPanel.TabIndex = 0;
            // 
            // starButton
            // 
            this.starButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.starButton.Location = new System.Drawing.Point(0, 0);
            this.starButton.Margin = new System.Windows.Forms.Padding(0);
            this.starButton.Name = "starButton";
            this.starButton.Size = new System.Drawing.Size(127, 52);
            this.starButton.TabIndex = 0;
            this.starButton.Text = "Start";
            this.starButton.UseVisualStyleBackColor = true;
            this.starButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pauseButton.Location = new System.Drawing.Point(127, 0);
            this.pauseButton.Margin = new System.Windows.Forms.Padding(0);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(127, 52);
            this.pauseButton.TabIndex = 1;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clearButton.Location = new System.Drawing.Point(254, 0);
            this.clearButton.Margin = new System.Windows.Forms.Padding(0);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(128, 52);
            this.clearButton.TabIndex = 2;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.logClueToolStripMenuItem,
            this.highscoresToolStripMenuItem,
            this.tripToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(12, 4, 0, 4);
            this.menuStrip1.Size = new System.Drawing.Size(1348, 46);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.updatePricesToolStripMenuItem,
            this.updateDropsToolStripMenuItem,
            this.updateTreasureTrailsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(64, 36);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(347, 38);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // updatePricesToolStripMenuItem
            // 
            this.updatePricesToolStripMenuItem.Name = "updatePricesToolStripMenuItem";
            this.updatePricesToolStripMenuItem.Size = new System.Drawing.Size(347, 38);
            this.updatePricesToolStripMenuItem.Text = "Update Prices";
            this.updatePricesToolStripMenuItem.Click += new System.EventHandler(this.updatePricesToolStripMenuItem_Click);
            // 
            // updateDropsToolStripMenuItem
            // 
            this.updateDropsToolStripMenuItem.Name = "updateDropsToolStripMenuItem";
            this.updateDropsToolStripMenuItem.Size = new System.Drawing.Size(347, 38);
            this.updateDropsToolStripMenuItem.Text = "Update Drops";
            this.updateDropsToolStripMenuItem.Click += new System.EventHandler(this.updateDropsToolStripMenuItem_Click);
            // 
            // updateTreasureTrailsToolStripMenuItem
            // 
            this.updateTreasureTrailsToolStripMenuItem.Name = "updateTreasureTrailsToolStripMenuItem";
            this.updateTreasureTrailsToolStripMenuItem.Size = new System.Drawing.Size(347, 38);
            this.updateTreasureTrailsToolStripMenuItem.Text = "Update Treasure Trails";
            // 
            // logClueToolStripMenuItem
            // 
            this.logClueToolStripMenuItem.Name = "logClueToolStripMenuItem";
            this.logClueToolStripMenuItem.Size = new System.Drawing.Size(121, 36);
            this.logClueToolStripMenuItem.Text = "Log Clue";
            this.logClueToolStripMenuItem.Click += new System.EventHandler(this.logClueToolStripMenuItem_Click);
            // 
            // highscoresToolStripMenuItem
            // 
            this.highscoresToolStripMenuItem.Name = "highscoresToolStripMenuItem";
            this.highscoresToolStripMenuItem.Size = new System.Drawing.Size(144, 36);
            this.highscoresToolStripMenuItem.Text = "Highscores";
            this.highscoresToolStripMenuItem.Click += new System.EventHandler(this.highscoresToolStripMenuItem_Click);
            // 
            // tripToolStripMenuItem
            // 
            this.tripToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startTripToolStripMenuItem,
            this.endTripToolStripMenuItem,
            this.logSuppliesToolStripMenuItem});
            this.tripToolStripMenuItem.Name = "tripToolStripMenuItem";
            this.tripToolStripMenuItem.Size = new System.Drawing.Size(66, 36);
            this.tripToolStripMenuItem.Text = "Trip";
            // 
            // startTripToolStripMenuItem
            // 
            this.startTripToolStripMenuItem.Name = "startTripToolStripMenuItem";
            this.startTripToolStripMenuItem.Size = new System.Drawing.Size(250, 38);
            this.startTripToolStripMenuItem.Text = "Start Trip";
            // 
            // endTripToolStripMenuItem
            // 
            this.endTripToolStripMenuItem.Name = "endTripToolStripMenuItem";
            this.endTripToolStripMenuItem.Size = new System.Drawing.Size(250, 38);
            this.endTripToolStripMenuItem.Text = "End Trip";
            // 
            // logSuppliesToolStripMenuItem
            // 
            this.logSuppliesToolStripMenuItem.Name = "logSuppliesToolStripMenuItem";
            this.logSuppliesToolStripMenuItem.Size = new System.Drawing.Size(250, 38);
            this.logSuppliesToolStripMenuItem.Text = "Log Supplies";
            this.logSuppliesToolStripMenuItem.Click += new System.EventHandler(this.logSuppliesToolStripMenuItem_Click);
            // 
            // stopwatchUpdateTimer
            // 
            this.stopwatchUpdateTimer.Interval = 250;
            this.stopwatchUpdateTimer.Tick += new System.EventHandler(this.stopwatchUpdateTimer_Tick);
            // 
            // gpPerHourTimer
            // 
            this.gpPerHourTimer.Interval = 10000;
            this.gpPerHourTimer.Tick += new System.EventHandler(this.gpPerHourTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1348, 708);
            this.Controls.Add(this.userInteractionPanel);
            this.Controls.Add(this.dropsPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OSRS Drop Logger";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npcListGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcNameBindingSource)).EndInit();
            this.dropsPanel.ResumeLayout(false);
            this.userInteractionPanel.ResumeLayout(false);
            this.labelLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.loggedDropView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loggedDropBindingSource)).EndInit();
            this.buttonTableLayoutPanel.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource npcNameBindingSource;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView npcListGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label npcNameLabel;
        private System.Windows.Forms.TextBox npcNameTextBox;
        private System.Windows.Forms.Panel dropsPanel;
        private System.Windows.Forms.Panel userInteractionPanel;
        private System.Windows.Forms.ListView dropsListView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel buttonTableLayoutPanel;
        private System.Windows.Forms.Button starButton;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Button clearButton;
        public System.Windows.Forms.DataGridView loggedDropView;
        public System.Windows.Forms.BindingSource loggedDropBindingSource;
        public System.Windows.Forms.Label totalValueLabel;
        private System.Windows.Forms.Timer stopwatchUpdateTimer;
        private System.Windows.Forms.ToolTip totalValueToolTip;
        private System.Windows.Forms.Timer gpPerHourTimer;
        private System.Windows.Forms.TableLayoutPanel labelLayoutPanel;
        private System.Windows.Forms.Label gpPerHourLabel;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updatePricesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateDropsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateTreasureTrailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logClueToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalPriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem highscoresToolStripMenuItem;
        private System.Windows.Forms.ToolTip rarityTooltip;
        private System.Windows.Forms.ToolStripMenuItem tripToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startTripToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem endTripToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logSuppliesToolStripMenuItem;
    }
}

