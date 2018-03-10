namespace OsrsDropEditor.Forms
{
    partial class SuppliesForm
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
            this.addSupplyButton = new System.Windows.Forms.Button();
            this.supplyLogBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addSupplyButton
            // 
            this.addSupplyButton.Location = new System.Drawing.Point(26, 435);
            this.addSupplyButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.addSupplyButton.Name = "addSupplyButton";
            this.addSupplyButton.Size = new System.Drawing.Size(518, 44);
            this.addSupplyButton.TabIndex = 0;
            this.addSupplyButton.Text = "Add Supply";
            this.addSupplyButton.UseVisualStyleBackColor = true;
            this.addSupplyButton.Click += new System.EventHandler(this.addSupplyButton_Click);
            // 
            // supplyLogBox
            // 
            this.supplyLogBox.Enabled = false;
            this.supplyLogBox.Location = new System.Drawing.Point(26, 13);
            this.supplyLogBox.Multiline = true;
            this.supplyLogBox.Name = "supplyLogBox";
            this.supplyLogBox.Size = new System.Drawing.Size(518, 413);
            this.supplyLogBox.TabIndex = 1;
            // 
            // SuppliesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 502);
            this.Controls.Add(this.supplyLogBox);
            this.Controls.Add(this.addSupplyButton);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "SuppliesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Supplies Used";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addSupplyButton;
        private System.Windows.Forms.TextBox supplyLogBox;
    }
}