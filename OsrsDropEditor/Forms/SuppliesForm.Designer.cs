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
            this.SuspendLayout();
            // 
            // addSupplyButton
            // 
            this.addSupplyButton.Location = new System.Drawing.Point(13, 226);
            this.addSupplyButton.Name = "addSupplyButton";
            this.addSupplyButton.Size = new System.Drawing.Size(259, 23);
            this.addSupplyButton.TabIndex = 0;
            this.addSupplyButton.Text = "Add Supply";
            this.addSupplyButton.UseVisualStyleBackColor = true;
            this.addSupplyButton.Click += new System.EventHandler(this.addSupplyButton_Click);
            // 
            // SuppliesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.addSupplyButton);
            this.Name = "SuppliesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Supplies Used";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addSupplyButton;
    }
}