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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.rangeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(67, 34);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(155, 20);
            this.textBox1.TabIndex = 1;
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
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(64, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
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
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rangeLabel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dropNameLabel);
            this.Name = "AddDropRangeForm";
            this.Text = "Add Drop (Range)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label dropNameLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label rangeLabel;
        private System.Windows.Forms.Button addDropButton;
        public System.Windows.Forms.Label label1;
    }
}