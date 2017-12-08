namespace OsrsDropEditor.Forms
{
    partial class CompareHighscoresForm
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
            this.levelsGridView = new System.Windows.Forms.DataGridView();
            this.skillColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.levelColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.theirLevelColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.levelsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // levelsGridView
            // 
            this.levelsGridView.AllowUserToAddRows = false;
            this.levelsGridView.AllowUserToDeleteRows = false;
            this.levelsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.levelsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.skillColumn,
            this.levelColumn,
            this.theirLevelColumn});
            this.levelsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.levelsGridView.Location = new System.Drawing.Point(0, 0);
            this.levelsGridView.Name = "levelsGridView";
            this.levelsGridView.ReadOnly = true;
            this.levelsGridView.RowHeadersVisible = false;
            this.levelsGridView.Size = new System.Drawing.Size(351, 451);
            this.levelsGridView.TabIndex = 0;
            // 
            // skillColumn
            // 
            this.skillColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.skillColumn.HeaderText = "Skill";
            this.skillColumn.Name = "skillColumn";
            this.skillColumn.ReadOnly = true;
            // 
            // levelColumn
            // 
            this.levelColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.levelColumn.HeaderText = "Your Level";
            this.levelColumn.Name = "levelColumn";
            this.levelColumn.ReadOnly = true;
            // 
            // theirLevelColumn
            // 
            this.theirLevelColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.theirLevelColumn.HeaderText = "Their Level";
            this.theirLevelColumn.Name = "theirLevelColumn";
            this.theirLevelColumn.ReadOnly = true;
            // 
            // CompareHighscoresForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 451);
            this.Controls.Add(this.levelsGridView);
            this.Name = "CompareHighscoresForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Compare Highscores";
            this.Load += new System.EventHandler(this.CompareHighscoresForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.levelsGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView levelsGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn skillColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn levelColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn theirLevelColumn;
    }
}