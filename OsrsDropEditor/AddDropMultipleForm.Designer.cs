namespace OsrsDropEditor
{
    partial class AddDropMultipleForm
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
            this.addDropButton = new System.Windows.Forms.Button();
            this.dropLabel = new System.Windows.Forms.Label();
            this.rangeLabel = new System.Windows.Forms.Label();
            this.dropNameLabel = new System.Windows.Forms.Label();
            this.quantityOptionsListBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // addDropButton
            // 
            this.addDropButton.Location = new System.Drawing.Point(16, 66);
            this.addDropButton.Name = "addDropButton";
            this.addDropButton.Size = new System.Drawing.Size(206, 23);
            this.addDropButton.TabIndex = 9;
            this.addDropButton.Text = "Add Drop";
            this.addDropButton.UseVisualStyleBackColor = true;
            // 
            // dropLabel
            // 
            this.dropLabel.Location = new System.Drawing.Point(64, 15);
            this.dropLabel.Name = "dropLabel";
            this.dropLabel.Size = new System.Drawing.Size(158, 13);
            this.dropLabel.TabIndex = 8;
            this.dropLabel.Text = "label1";
            // 
            // rangeLabel
            // 
            this.rangeLabel.AutoSize = true;
            this.rangeLabel.Location = new System.Drawing.Point(13, 39);
            this.rangeLabel.Name = "rangeLabel";
            this.rangeLabel.Size = new System.Drawing.Size(46, 13);
            this.rangeLabel.TabIndex = 7;
            this.rangeLabel.Text = "Amount:";
            // 
            // dropNameLabel
            // 
            this.dropNameLabel.AutoSize = true;
            this.dropNameLabel.Location = new System.Drawing.Point(13, 15);
            this.dropNameLabel.Name = "dropNameLabel";
            this.dropNameLabel.Size = new System.Drawing.Size(36, 13);
            this.dropNameLabel.TabIndex = 5;
            this.dropNameLabel.Text = "Drop: ";
            // 
            // quantityOptionsListBox
            // 
            this.quantityOptionsListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.quantityOptionsListBox.FormattingEnabled = true;
            this.quantityOptionsListBox.Location = new System.Drawing.Point(66, 39);
            this.quantityOptionsListBox.Name = "quantityOptionsListBox";
            this.quantityOptionsListBox.Size = new System.Drawing.Size(156, 21);
            this.quantityOptionsListBox.TabIndex = 10;
            // 
            // AddDropMultipleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 99);
            this.Controls.Add(this.quantityOptionsListBox);
            this.Controls.Add(this.addDropButton);
            this.Controls.Add(this.dropLabel);
            this.Controls.Add(this.rangeLabel);
            this.Controls.Add(this.dropNameLabel);
            this.Name = "AddDropMultipleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Drop (Multiple)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button addDropButton;
        public System.Windows.Forms.Label dropLabel;
        private System.Windows.Forms.Label rangeLabel;
        private System.Windows.Forms.Label dropNameLabel;
        public System.Windows.Forms.ComboBox quantityOptionsListBox;
    }
}