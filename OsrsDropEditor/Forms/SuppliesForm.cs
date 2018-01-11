using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OsrsDropEditor.Forms
{
    public partial class SuppliesForm : Form
    {
        public SuppliesForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Prompt the user to add a supply name and amount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addSupplyButton_Click(object sender, EventArgs e)
        {
            SupplyDialogForm supplyDialogForm = new SupplyDialogForm();
            if (supplyDialogForm.ShowDialog(this) == DialogResult.OK)
            {
                //Attempt to add the supply
                string supplyName = supplyDialogForm.itemInput.Text;
                string supplyQuantity = supplyDialogForm.quantityInput.Text;
                int quantity = 1;

                if (Int32.TryParse(supplyQuantity, out quantity))
                {

                }
            }
        }
    }
}
