using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace OsrsDropEditor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load up a list of NPCs in the game and their respective links. Only do this once.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            OsrsDataContainers.LoadData();

            //Setup the grid view
            npcNameBindingSource.DataSource = OsrsDataContainers.NpcLinks.Keys.Select(key => new NpcName { Name = key });
            npcListGridView.ClearSelection();

            //Setup the autocomplete for the textbox
            AutoCompleteStringCollection autoCompleteSource = new AutoCompleteStringCollection();
            autoCompleteSource.AddRange(OsrsDataContainers.NpcLinks.Select(kvp => kvp.Key).ToArray());
            npcNameTextBox.AutoCompleteCustomSource = autoCompleteSource;
            npcNameTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            npcNameTextBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            npcNameTextBox.KeyDown += npcNameTextBox_KeyEnter;
        }

        private void npcNameTextBox_KeyEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                TextBox box = sender as TextBox;
                string name = box.Text;
                DataGridViewRow newRow = npcListGridView.Rows.Cast<DataGridViewRow>().Where(row => row.DataBoundItem.ToString().ToLower().Equals(name.ToLower())).First();
                int index = newRow.Index;

                npcListGridView.Rows[index].Selected = true;
                npcListGridView.FirstDisplayedScrollingRowIndex = npcListGridView.SelectedRows[0].Index;

                ShowDropsForNpc(newRow);
            }
        }

        private void ShowDropsForNpc(DataGridViewRow npcRow)
        {
            string npcName = npcRow.DataBoundItem.ToString();
            List<Drop> drops = OsrsDataContainers.GetDropsForNpc(npcName).ToList();

            if (!drops.Any())
            {
                MessageBox.Show(this, "Unable to load any drops for the NPC.", "Error", MessageBoxButtons.OK);
            }
            Dictionary<string, string> imageLinks = drops.ToDictionary(drop => drop.Name, drop => drop.ImageLink);
            dropsListView.Items.Clear();
            dropsListView.LargeImageList = new ImageList();
            ListViewItem item = new ListViewItem();
            item.Text = "This is a test";
        }

        private void npcListGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;

            npcListGridView.Focus();
            DataGridViewRow row = npcListGridView.Rows[rowIndex];
            if (!row.Selected)
            {
                Console.WriteLine(row.DataBoundItem);
                ShowDropsForNpc(row);
            }
        }

        public void dropListGridView_CellMouseDown(object sender, ListViewItemMouseHoverEventArgs e)
        {
            Drop dataBoundDrop = new Drop();
        }
    }

    public struct NpcName
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
