using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;

namespace OsrsDropEditor
{
    public partial class MainForm : Form
    {
        private OsrsDataContainers osrsDropContainers;

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
            osrsDropContainers = new OsrsDataContainers(this);

            osrsDropContainers.LoadData();

            //Setup the grid view
            npcNameBindingSource.DataSource = osrsDropContainers.NpcLinks.Keys.Select(key => new NpcName { Name = key });
            npcListGridView.ClearSelection();

            //Setup the logged drops grid view
            loggedDropBindingSource.DataSource = osrsDropContainers.LoggedDrops.Values.ToList();

            //Setup the autocomplete for the textbox
            AutoCompleteStringCollection autoCompleteSource = new AutoCompleteStringCollection();
            autoCompleteSource.AddRange(osrsDropContainers.NpcLinks.Select(kvp => kvp.Key).ToArray());
            npcNameTextBox.AutoCompleteCustomSource = autoCompleteSource;
            npcNameTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            npcNameTextBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            npcNameTextBox.KeyDown += npcNameTextBox_KeyEnter;
        }

        /// <summary>
        /// Called whenever we hit enter on the NPC name textbox or an autocomplete action is performed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            List<Drop> drops = osrsDropContainers.GetDropsForNpc(npcName).ToList();

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

        private bool hasDropFormOpen = false;

        public void dropsListView_ItemActivate(object sender, EventArgs e)
        {
            if (hasDropFormOpen)
                return;

            ListView listView = (ListView)sender;
            ListViewItem listViewItem = listView.SelectedItems[0];
            Drop dropToLog = (Drop)listViewItem.Tag;
            if (dropToLog.IsRangeOfDrops)
            {
                AddDropRangeForm addDropRangeFrom = new AddDropRangeForm();
                addDropRangeFrom.dropLabel.Text = dropToLog.Name;
                addDropRangeFrom.rangeTextBox.KeyDown += RangeTextBox_KeyDown;
                addDropRangeFrom.addDropButton.Click += AddDropButton_Click;
                addDropRangeFrom.Tag = dropToLog;

                addDropRangeFrom.Show(this);
                hasDropFormOpen = true;
            }
            else if (dropToLog.HasMultipleQuantities)
            {
                AddDropMultipleForm addDropMultipleForm = new AddDropMultipleForm();
                addDropMultipleForm.dropLabel.Text = dropToLog.Name;

                foreach (int quantityOption in dropToLog.MultipleQuantities)
                    addDropMultipleForm.quantityOptionsListBox.Items.Add(quantityOption);

                addDropMultipleForm.quantityOptionsListBox.Text = dropToLog.MultipleQuantities[0].ToString();
                addDropMultipleForm.quantityOptionsListBox.Tag = dropToLog;

                addDropMultipleForm.addDropButton.Click += AddMultipleRangeDropButton_Click;

                addDropMultipleForm.Show(this);
                hasDropFormOpen = true;
            }
            else if (dropToLog.Name.Equals("RareDropTable"))
            {
                AddRareDropForm addRareDropForm = new AddRareDropForm();
                addRareDropForm.rareDropsOptionList.Items.AddRange(osrsDropContainers.RareDropTable.Cast<object>().ToArray());

                addRareDropForm.Show(this);
                hasDropFormOpen = true;
            }
            else
            {
                osrsDropContainers.LogDrop(dropToLog);
            }
        }

        private void RangeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                TextBox box = sender as TextBox;
                Drop drop = (Drop)box.Tag;
                int quantity = 1;
                if (Int32.TryParse(box.Text, out quantity))
                {
                    if (quantity < drop.RangeLowBound || quantity > drop.RangeHighBound)
                        return;

                    drop.Quantity = quantity;
                    osrsDropContainers.LogDrop(drop);

                    ((Form)box.TopLevelControl).Close();
                    hasDropFormOpen = false;
                }
            }
        }

        private void AddDropButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            AddDropRangeForm form = (AddDropRangeForm)button.TopLevelControl;
            Drop drop = (Drop)form.Tag;
            int quantity = 1;
            if (Int32.TryParse(form.rangeTextBox.Text, out quantity))
            {
                if (quantity < drop.RangeLowBound || quantity > drop.RangeHighBound)
                    return;

                drop.Quantity = quantity;
                osrsDropContainers.LogDrop(drop);

                form.Close();
                hasDropFormOpen = false;
            }
        }

        private void AddMultipleRangeDropButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            AddDropMultipleForm form = (AddDropMultipleForm)button.TopLevelControl;
            Drop drop = (Drop)form.quantityOptionsListBox.Tag;
            int quantitySelected = Convert.ToInt32(form.quantityOptionsListBox.SelectedItem);

            drop.Quantity = quantitySelected;
            osrsDropContainers.LogDrop(drop);

            form.Close();
            hasDropFormOpen = false;
        }

        /// <summary>
        /// Clears all the drops from the logged drops view. TODO: reset other variables.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetButton_Click(object sender, EventArgs e)
        {
            osrsDropContainers.LoggedDrops.Clear();
            loggedDropBindingSource.Clear();
        }
    }

    /// <summary>
    /// We need to wrap the NPC's name in an object in order to have it properly show up in the grid
    /// view otherwise it just displays the length of the string.
    /// </summary>
    public struct NpcName
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
