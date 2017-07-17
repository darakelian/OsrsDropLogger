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
            this.npcNameBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.npcNameTextBox = new System.Windows.Forms.TextBox();
            this.npcNameLabel = new System.Windows.Forms.Label();
            this.npcListGridView = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dropsPanel = new System.Windows.Forms.Panel();
            this.userInteractionPanel = new System.Windows.Forms.Panel();
            this.dropsListView = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.npcNameBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npcListGridView)).BeginInit();
            this.dropsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // npcNameBindingSource
            // 
            this.npcNameBindingSource.DataSource = typeof(OsrsDropEditor.NpcName);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.npcNameTextBox);
            this.panel1.Controls.Add(this.npcNameLabel);
            this.panel1.Controls.Add(this.npcListGridView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(483, 821);
            this.panel1.TabIndex = 1;
            // 
            // npcNameTextBox
            // 
            this.npcNameTextBox.Location = new System.Drawing.Point(161, 13);
            this.npcNameTextBox.Name = "npcNameTextBox";
            this.npcNameTextBox.Size = new System.Drawing.Size(319, 35);
            this.npcNameTextBox.TabIndex = 3;
            // 
            // npcNameLabel
            // 
            this.npcNameLabel.Location = new System.Drawing.Point(13, 13);
            this.npcNameLabel.Name = "npcNameLabel";
            this.npcNameLabel.Size = new System.Drawing.Size(152, 45);
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
            this.npcListGridView.Location = new System.Drawing.Point(0, 61);
            this.npcListGridView.MultiSelect = false;
            this.npcListGridView.Name = "npcListGridView";
            this.npcListGridView.RowHeadersVisible = false;
            this.npcListGridView.RowTemplate.Height = 37;
            this.npcListGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.npcListGridView.Size = new System.Drawing.Size(483, 760);
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
            // dropsPanel
            // 
            this.dropsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dropsPanel.Controls.Add(this.dropsListView);
            this.dropsPanel.Location = new System.Drawing.Point(489, 0);
            this.dropsPanel.Name = "dropsPanel";
            this.dropsPanel.Size = new System.Drawing.Size(684, 821);
            this.dropsPanel.TabIndex = 2;
            // 
            // userInteractionPanel
            // 
            this.userInteractionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userInteractionPanel.Location = new System.Drawing.Point(1179, 0);
            this.userInteractionPanel.Name = "userInteractionPanel";
            this.userInteractionPanel.Size = new System.Drawing.Size(393, 821);
            this.userInteractionPanel.TabIndex = 3;
            // 
            // dropsListView
            // 
            this.dropsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dropsListView.Location = new System.Drawing.Point(0, 0);
            this.dropsListView.Name = "dropsListView";
            this.dropsListView.Size = new System.Drawing.Size(684, 821);
            this.dropsListView.TabIndex = 0;
            this.dropsListView.UseCompatibleStateImageBehavior = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1572, 821);
            this.Controls.Add(this.userInteractionPanel);
            this.Controls.Add(this.dropsPanel);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "MainForm";
            this.Text = "OSRS Drop Logger";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.npcNameBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npcListGridView)).EndInit();
            this.dropsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

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
    }
}

