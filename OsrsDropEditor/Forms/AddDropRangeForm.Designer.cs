namespace OsrsDropEditor
{
    partial class AddDropRangeForm
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
            this.dropNameLabel = new System.Windows.Forms.Label();
            this.rangeTextBox = new System.Windows.Forms.TextBox();
            this.rangeLabel = new System.Windows.Forms.Label();
            this.dropLabel = new System.Windows.Forms.Label();
            this.addDropButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dropNameLabel
            // 
            this.dropNameLabel.AutoSize = true;
            this.dropNameLabel.Location = new System.Drawing.Point(13, 13);
            this.dropNameLabel.Name = "dropNameLabel";
            this.dropNameLabel.Size = new System.Drawing.Size(36, 13);
            this.dropNameLabel.TabIndex = 0;
            this.dropNameLabel.Text = "Drop: ";
            // 
            // rangeTextBox
            // 
            this.rangeTextBox.Location = new System.Drawing.Point(67, 34);
            this.rangeTextBox.Name = "rangeTextBox";
            this.rangeTextBox.Size = new System.Drawing.Size(155, 20);
            this.rangeTextBox.TabIndex = 1;
            // 
            // rangeLabel
            // 
            this.rangeLabel.AutoSize = true;
            this.rangeLabel.Location = new System.Drawing.Point(13, 37);
            this.rangeLabel.Name = "rangeLabel";
            this.rangeLabel.Size = new System.Drawing.Size(46, 13);
            this.rangeLabel.TabIndex = 2;
            this.rangeLabel.Text = "Amount:";
            // 
            // dropLabel
            // 
            this.dropLabel.Location = new System.Drawing.Point(64, 13);
            this.dropLabel.Name = "dropLabel";
            this.dropLabel.Size = new System.Drawing.Size(158, 13);
            this.dropLabel.TabIndex = 3;
            this.dropLabel.Text = "label1";
            // 
            // addDropButton
            // 
            this.addDropButton.Location = new System.Drawing.Point(16, 64);
            this.addDropButton.Name = "addDropButton";
            this.addDropButton.Size = new System.Drawing.Size(206, 23);
            this.addDropButton.TabIndex = 4;
            this.addDropButton.Text = "Add Drop";
            this.addDropButton.UseVisualStyleBackColor = true;
            // 
            // AddDropRangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 99);
            this.Controls.Add(this.addDropButton);
            this.Controls.Add(this.dropLabel);
            this.Controls.Add(this.rangeLabel);
            this.Controls.Add(this.rangeTextBox);
            this.Controls.Add(this.dropNameLabel);
            this.Name = "AddDropRangeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Drop (Range)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label dropNameLabel;
        private System.Windows.Forms.Label rangeLabel;
        public System.Windows.Forms.Label dropLabel;
        public System.Windows.Forms.TextBox rangeTextBox;
        public System.Windows.Forms.Button addDropButton;
    }
}