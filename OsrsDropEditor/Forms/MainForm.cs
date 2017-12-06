using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Diagnostics;
using OsrsDropEditor.DataGathering;
using OsrsDropEditor.Forms;

namespace OsrsDropEditor
{
    public partial class MainForm : Form
    {
        private OsrsDataContainers osrsDropContainers;

        private DataGridViewRow currentNpcRow;

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
            //Prompt for folder select
            string savedPath = Properties.Settings.Default.FilePath;
            if (String.IsNullOrEmpty(savedPath))
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.Description = "Select a folder to save data for offline functionality.";

                DialogResult dialogResult = folderBrowserDialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    Properties.Settings.Default.FilePath = folderBrowserDialog.SelectedPath;
                    Properties.Settings.Default.Save();
                    Console.WriteLine(Properties.Settings.Default.FilePath);
                }
                else
                    Close();
            }
            osrsDropContainers = new OsrsDataContainers(this);

            osrsDropContainers.LoadData();

            //Setup the grid view
            npcNameBindingSource.DataSource = osrsDropContainers.NpcLinks.Keys.Select(key => new NpcName { Name = key });
            npcListGridView.ClearSelection();

            //Setup the logged drops grid view
            loggedDropBindingSource.DataSource = osrsDropContainers.LoggedDrops;
            loggedDropBindingSource.ListChanged += LoggedDropBindingSource_ListChanged;

            //Setup the autocomplete for the textbox
            AutoCompleteStringCollection autoCompleteSource = new AutoCompleteStringCollection();
            autoCompleteSource.AddRange(osrsDropContainers.NpcLinks.Select(kvp => kvp.Key).ToArray());
            npcNameTextBox.AutoCompleteCustomSource = autoCompleteSource;
            npcNameTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            npcNameTextBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            npcNameTextBox.KeyDown += npcNameTextBox_KeyEnter;
        }

        private void LoggedDropBindingSource_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            UpdateTotalValueLabel();
            loggedDropView.Refresh();

            Utility.SaveObjectToJson("logged_drops.json", "OfflineJson", osrsDropContainers.LoggedDrops);
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

        /// <summary>
        /// Attempts to populate the drop list view with all the drops that the NPC has. In the event of
        /// being unable to find drops, we display an error letting the user know that there was a problem.
        /// </summary>
        /// <param name="npcRow"></param>
        /// <returns></returns>
        private bool ShowDropsForNpc(DataGridViewRow npcRow, bool forceUpdate = false)
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

            dropsListView.Items.AddRange(drops.Select(Utility.GetListViewItemFromDrop).ToArray());
            currentNpcRow = npcRow;

            return true;
        }

        /// <summary>
        /// Called whenever we click an NPC's name in the list. We also make sure that this code only
        /// gets executed if the row isn't already selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void npcListGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;

            npcListGridView.Focus();
            DataGridViewRow row = npcListGridView.Rows[rowIndex];
            if (!row.Selected)
                ShowDropsForNpc(row);
        }

        /// <summary>
        /// Keeps track of whether or not we are attempting to add a special type of drop. This is so that
        /// we do not end up with multiple forms open at once if the user spam clicks an item that requires
        /// showing a form.
        /// </summary>
        private bool hasDropFormOpen = false;

        /// <summary>
        /// Called whenever an item is clicked in the NPC's drops panel. First we check to see if the drop is
        /// a drop with a range of quantities. Next, we check if it is a drop with multiple possible quantities.
        /// Third, we check to see if the drop is a rare drop table drop. If the drop meets any of those
        /// criteria, we show the special form for handling those instances. Otherwise we just log the drop.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                addDropRangeFrom.rangeTextBox.Tag = dropToLog;

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
                addRareDropForm.addRareDropButton.Click += AddRareDropButton_Click;

                addRareDropForm.Show(this);
                hasDropFormOpen = true;
            }
            else
            {
                osrsDropContainers.LogDrop(dropToLog);
            }
        }

        /// <summary>
        /// Called whenever the user submits the input to the text box. Validates the quantity provided by making
        /// sure that it is not outside the range specified in the drop object. If it successfully validates
        /// then the drop is logged.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Called when a drop is added with a custom range.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Called when a drop is added with a selected drop amount.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// Adds the selected rare drop to the logged drops.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddRareDropButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            AddRareDropForm form = (AddRareDropForm)button.TopLevelControl;
            object selectedObject = form.rareDropsOptionList.SelectedItem;
            
            if (selectedObject != null)
            {
                Drop dropToLog = (Drop)selectedObject;
                osrsDropContainers.LogDrop(dropToLog);

                form.Close();
                hasDropFormOpen = false;
            }
        }

        /// <summary>
        /// Clears all the drops from the logged drops view. TODO: reset other variables.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearButton_Click(object sender, EventArgs e)
        {
            osrsDropContainers.LoggedDrops.Clear();
            loggedDropBindingSource.Clear();
        }

        /// <summary>
        /// Updates the text of the total value label to reflect the current value.
        /// </summary>
        public void UpdateTotalValueLabel()
        {
            string oldValueText = totalValueLabel.Text;
            string formattedValue = Utility.FormatNumberForDisplay(osrsDropContainers.GetTotalDropsValue());

            totalValueLabel.Text = Regex.Replace(oldValueText, @"(?<=Total Value: )(\d*(,)?)*(K|M)?", formattedValue);
            totalValueToolTip.SetToolTip(totalValueLabel, "" + osrsDropContainers.GetTotalDropsValue());
        }

        private Stopwatch stopWatch;
        private int gpAtStart;

        private void startButton_Click(object sender, EventArgs e)
        {
            if (stopWatch == null)
            {
                gpAtStart = osrsDropContainers.GetTotalDropsValue();

                stopWatch = new Stopwatch();
                StartTimers();
            }
            else if (stopWatch != null && !stopWatch.IsRunning)
            {
                StartTimers();
            } 
        }


        private void StartTimers()
        {
            stopWatch.Start();
            stopwatchUpdateTimer.Start();
            gpPerHourTimer.Start();
        }

        /// <summary>
        /// Pauses the stopwatch and stopwatch update timer from running. The second press will reset the time completely.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (stopWatch != null && !stopWatch.IsRunning)
            {
                Text = Regex.Replace(Text, @"\s-\s([0-9]*):([0-9]*):([0-9]*)", "");
                CalculateGpPerHour(true);
                stopWatch.Reset();
            }

            stopWatch?.Stop();
            stopwatchUpdateTimer.Stop();
            gpPerHourTimer.Stop();
        }

        /// <summary>
        /// Updates the title of the main form to display the timer's value every second. Also calculates the GP/hr.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopwatchUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (ShouldUpdateTimer())
            {
                TimeSpan span = stopWatch.Elapsed;
                string spanToTimeStamp = span.ToString(@"hh\:mm\:ss");

                string timestampText = $" - {spanToTimeStamp}";

                if (!Text.Contains("-"))
                    Text += timestampText;
                else
                    Text = Regex.Replace(Text, @"\s-\s([0-9]*):([0-9]*):([0-9]*)", timestampText);
            }
        }

        /// <summary>
        /// Checks to see if we should update the title of the main form.
        /// </summary>
        /// <returns></returns>
        private bool ShouldUpdateTimer()
        {
            return stopWatch != null && stopWatch.IsRunning;
        }

        /// <summary>
        /// Called whenever the gpPerHourTimer's 'Tick' function gets invoked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gpPerHourTimer_Tick(object sender, EventArgs e)
        {
            CalculateGpPerHour();
        }

        /// <summary>
        /// Calculates the GP/hour of the drops.
        /// </summary>
        /// <param name="overrideTimerCheck">Normally we only check if the timer is running, true indicates calculate the value anywas.</param>
        private void CalculateGpPerHour(bool overrideTimerCheck = false)
        {
            if (ShouldUpdateTimer() || overrideTimerCheck)
            {
                long totalTime = stopWatch.ElapsedMilliseconds;
                //Convert elapsed milliseconds to decimale value of total hours
                float totalHours = (float)totalTime / (60 * 60 * 1000);
                int totalValue = osrsDropContainers.GetTotalDropsValue() - gpAtStart;

                float gpPerHour = totalTime != 0 && overrideTimerCheck ? 0 : totalValue / totalHours;

                string gpPerHourAsText = Utility.FormatNumberForDisplay((int)gpPerHour);
                gpPerHourLabel.Text = gpPerHourAsText;
                totalValueToolTip.SetToolTip(gpPerHourLabel, $"GP/hr: {gpPerHourAsText}");
            }
        }

        /// <summary>
        /// Opens up the settings menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();

            //Attempt to load existing username and gamemode from settings before showing
            string username = Properties.Settings.Default.username;
            string gamemode = Properties.Settings.Default.gamemode;

            if (!String.IsNullOrEmpty(username))
                form.usernameInput.Text = username;
            if (!String.IsNullOrEmpty(gamemode))
                form.gameModeListBox.Text = gamemode;

            form.Show(this);
        }

        /// <summary>
        /// Refreshes the price before 6 hours has passed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updatePricesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            osrsDropContainers.LoadItemPrices(true);
        }

        /// <summary>
        /// Refreshes the current drop table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateDropsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentNpcRow != null)
                ShowDropsForNpc(currentNpcRow, true);
        }

        /// <summary>
        /// Shows the treasure trail form to let the user add their drop.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logClueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AddTreasureTrailRewardForm(osrsDropContainers).Show(this);
        }

        /// <summary>
        /// Display highscores information to the user. Prompt them to enter a username if they haven't already.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void highscoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string username = Properties.Settings.Default.username;
            //Check for username saved in settings
            if (String.IsNullOrEmpty(username))
            {
                DialogResult promptForUsername = MessageBox.Show(this, "Would you like to enter your username for highscores functionality?", "Warning", MessageBoxButtons.YesNo);
                if (promptForUsername == DialogResult.Yes)
                {
                    //Prompt them for the username now
                    TextDialogForm textDialogForm = new TextDialogForm();
                    if (textDialogForm.ShowDialog(this) == DialogResult.OK)
                    {
                        username = textDialogForm.textInput.Text;
                        textDialogForm.Dispose();
                        Utility.SaveUsername(username);
                    }
                    else
                        return;
                }
                else
                    return;
            }

            //Open up the highscores form
            HighscoresForm highscoresForm = new HighscoresForm();
            highscoresForm.Show(this);
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
