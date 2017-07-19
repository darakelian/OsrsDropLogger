namespace OsrsDropEditor
{
    partial class AddRareDropForm
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
            this.rareDropsOptionList = new System.Windows.Forms.ComboBox();
            this.addRareDropButton = new System.Windows.Forms.Button();
            this.dropLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rareDropsOptionList
            // 
            this.rareDropsOptionList.FormattingEnabled = true;
            this.rareDropsOptionList.Location = new System.Drawing.Point(66, 12);
            this.rareDropsOptionList.Name = "rareDropsOptionList";
            this.rareDropsOptionList.Size = new System.Drawing.Size(156, 21);
            this.rareDropsOptionList.TabIndex = 15;
            // 
            // addRareDropButton
            // 
            this.addRareDropButton.Location = new System.Drawing.Point(16, 39);
            this.addRareDropButton.Name = "addRareDropButton";
            this.addRareDropButton.Size = new System.Drawing.Size(206, 23);
            this.addRareDropButton.TabIndex = 14;
            this.addRareDropButton.Text = "Add Drop";
            this.addRareDropButton.UseVisualStyleBackColor = true;
            // 
            // dropLabel
            // 
            this.dropLabel.AutoSize = true;
            this.dropLabel.Location = new System.Drawing.Point(13, 15);
            this.dropLabel.Name = "dropLabel";
            this.dropLabel.Size = new System.Drawing.Size(33, 13);
            this.dropLabel.TabIndex = 12;
            this.dropLabel.Text = "Drop:";
            // 
            // AddRareDropForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 72);
            this.Controls.Add(this.rareDropsOptionList);
            this.Controls.Add(this.addRareDropButton);
            this.Controls.Add(this.dropLabel);
            this.Name = "AddRareDropForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddRareDropForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox rareDropsOptionList;
        public System.Windows.Forms.Button addRareDropButton;
        private System.Windows.Forms.Label dropLabel;
    }
}