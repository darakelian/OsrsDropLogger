namespace OsrsDropEditor
{
    partial class SettingsForm
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
            this.usernameLabel = new System.Windows.Forms.Label();
            this.usernameInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gameModeListBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(14, 16);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(58, 13);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "Username:";
            // 
            // usernameInput
            // 
            this.usernameInput.Location = new System.Drawing.Point(78, 13);
            this.usernameInput.MaxLength = 40;
            this.usernameInput.Name = "usernameInput";
            this.usernameInput.Size = new System.Drawing.Size(241, 20);
            this.usernameInput.TabIndex = 1;
            this.usernameInput.TextChanged += new System.EventHandler(this.usernameInput_TextChanged);
            this.usernameInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.usernameInput_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Game Mode:";
            // 
            // gameModeListBox
            // 
            this.gameModeListBox.FormattingEnabled = true;
            this.gameModeListBox.Items.AddRange(new object[] {
            "Normal",
            "Ironman",
            "Ultimate Ironman",
            "Hardcore Ironman",
            "Deadman Mode",
            "Seasonal"});
            this.gameModeListBox.Location = new System.Drawing.Point(91, 46);
            this.gameModeListBox.Name = "gameModeListBox";
            this.gameModeListBox.Size = new System.Drawing.Size(228, 21);
            this.gameModeListBox.TabIndex = 3;
            this.gameModeListBox.SelectedIndexChanged += new System.EventHandler(this.gameModeListBox_SelectedIndexChanged);
            this.gameModeListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gameModeListBox_KeyDown);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 234);
            this.Controls.Add(this.gameModeListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.usernameInput);
            this.Controls.Add(this.usernameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TextBox usernameInput;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox gameModeListBox;
    }
}