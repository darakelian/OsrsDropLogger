namespace OsrsDropEditor.Forms
{
    partial class SupplyDialogForm
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.itemInput = new System.Windows.Forms.TextBox();
            this.quantityInput = new System.Windows.Forms.TextBox();
            this.itemLabel = new System.Windows.Forms.Label();
            this.quantityLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(157, 64);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(115, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(13, 64);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(115, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // itemInput
            // 
            this.itemInput.Location = new System.Drawing.Point(82, 12);
            this.itemInput.Name = "itemInput";
            this.itemInput.Size = new System.Drawing.Size(190, 20);
            this.itemInput.TabIndex = 3;
            // 
            // quantityInput
            // 
            this.quantityInput.Location = new System.Drawing.Point(82, 38);
            this.quantityInput.Name = "quantityInput";
            this.quantityInput.Size = new System.Drawing.Size(190, 20);
            this.quantityInput.TabIndex = 6;
            // 
            // itemLabel
            // 
            this.itemLabel.Location = new System.Drawing.Point(13, 12);
            this.itemLabel.Name = "itemLabel";
            this.itemLabel.Size = new System.Drawing.Size(63, 20);
            this.itemLabel.TabIndex = 7;
            this.itemLabel.Text = "Supply:";
            this.itemLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // quantityLabel
            // 
            this.quantityLabel.Location = new System.Drawing.Point(13, 38);
            this.quantityLabel.Name = "quantityLabel";
            this.quantityLabel.Size = new System.Drawing.Size(63, 20);
            this.quantityLabel.TabIndex = 8;
            this.quantityLabel.Text = "Quantity:";
            this.quantityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SupplyDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 98);
            this.Controls.Add(this.quantityLabel);
            this.Controls.Add(this.itemLabel);
            this.Controls.Add(this.quantityInput);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.itemInput);
            this.Name = "SupplyDialogForm";
            this.Text = "Confirm Supply";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        public System.Windows.Forms.TextBox itemInput;
        public System.Windows.Forms.TextBox quantityInput;
        private System.Windows.Forms.Label itemLabel;
        private System.Windows.Forms.Label quantityLabel;
    }
}