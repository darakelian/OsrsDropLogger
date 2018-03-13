using Newtonsoft.Json;
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
        private Dictionary<string, int> supplies = new Dictionary<string, int>();

        public SuppliesForm()
        {
            InitializeComponent();
            if (Utility.FileExists("supplies.json"))
                LoadSupplies();
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

                if (supplyName.ToLower().Contains("potion"))
                {

                }
                string supplyQuantity = supplyDialogForm.quantityInput.Text;
                int quantity = 1;

                if (Utility.ConvertStringToInt(supplyQuantity, out quantity))
                {
                    if (supplies.ContainsKey(supplyName.ToLower()))
                        supplies[supplyName.ToLower()] += quantity;
                    else
                        supplies[supplyName.ToLower()] = quantity;
                    RefreshSupplyDisplay();
                    SaveSupplies();
                }
            }
        }

        /// <summary>
        /// Updates the display of supplies for the trip.
        /// </summary>
        private void RefreshSupplyDisplay()
        {
            supplyLogBox.Clear();
            foreach (var kvp in supplies)
                supplyLogBox.AppendText($"{kvp.Key.ToTitleCase()} x{kvp.Value}\n");
        }

        /// <summary>
        /// Save the supplies to JSON so the user can close the program and resume
        /// it at a later point.
        /// </summary>
        private void SaveSupplies()
        {
            Utility.SaveObjectToJson("supplies.json", "OfflineJson", supplies);
        }

        private void LoadSupplies()
        {
            supplies = JsonConvert.DeserializeObject<Dictionary<string, int>>(Utility.ReadFileToEnd("supplies.json"));
            RefreshSupplyDisplay();
        }
    }
}
