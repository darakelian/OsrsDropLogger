using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;

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

                if (ShowDropsForNpc(newRow))
                {
                    npcListGridView.Rows[index].Selected = true;
                    npcListGridView.FirstDisplayedScrollingRowIndex = npcListGridView.SelectedRows[0].Index;
                }
            }
        }

        private bool ShowDropsForNpc(DataGridViewRow npcRow)
        {
            string npcName = npcRow.DataBoundItem.ToString();
            List<Drop> drops = OsrsDataContainers.GetDropsForNpc(npcName).ToList();

            if (!drops.Any())
            {
                MessageBox.Show(this, "Unable to load any drops for the NPC.", "Error", MessageBoxButtons.OK);
                return false;
            }

            npcNameTextBox.Text = npcName;

            IEnumerable<Bitmap> images = Utility.GetImagesFromDrops(drops);
            dropsListView.Items.Clear();
            dropsListView.LargeImageList = new ImageList();
            dropsListView.LargeImageList.ImageSize = new Size(64, 64);
            dropsListView.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            dropsListView.LargeImageList.Images.AddRange(images.ToArray());

            dropsListView.Items.AddRange(drops.Select(GetListViewItemForDrop).ToArray());

            return true;
        }

        private ListViewItem GetListViewItemForDrop(Drop drop, int slot)
        {
            ListViewItem item = new ListViewItem();
            item.Tag = drop;
            item.Text = drop.ToString();
            item.ImageIndex = slot;

            return item;
        }

        private void npcListGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;

            npcListGridView.Focus();
            DataGridViewRow row = npcListGridView.Rows[rowIndex];
            if (!row.Selected)
                ShowDropsForNpc(row);
        }

        public void dropsListView_ItemActivate(object sender, EventArgs e)
        {
            ListView listView = (ListView)sender;
            ListViewItem listViewItem = listView.SelectedItems[0];
            Drop dropToLog = (Drop)listViewItem.Tag;
            if (dropToLog.IsRangeOfDrops)
            {
                AddDropRangeForm addDropRangeFrom = new AddDropRangeForm();
                addDropRangeFrom.label1.Text = dropToLog.Name;
                addDropRangeFrom.Show(this);
            }
            if (dropToLog.HasMultipleQuantities)
            {

            }
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
