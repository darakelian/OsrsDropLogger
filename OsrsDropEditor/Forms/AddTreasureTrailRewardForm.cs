using OsrsDropEditor.DataGathering;
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
    public partial class AddTreasureTrailRewardForm : Form
    {
        private OsrsDataContainers osrsDataContainers;

        List<Drop> clueDrops;

        public AddTreasureTrailRewardForm(OsrsDataContainers osrsDataContainers)
        {
            InitializeComponent();
            this.osrsDataContainers = osrsDataContainers;
            clueDrops = new List<Drop>();

            SetupCommonRewards();
            SetupEasyRewards();
            SetupMediumRewards();
            SetupHardRewards();
            SetupEliteRewards();
            SetupMasterRewards();

            commonListView.ItemActivate += listView_ItemActivate;
            easyListView.ItemActivate += listView_ItemActivate;
            mediumListView.ItemActivate += listView_ItemActivate;
            hardListView.ItemActivate += listView_ItemActivate;
            eliteListView.ItemActivate += listView_ItemActivate;
            masterListView.ItemActivate += listView_ItemActivate;
        }

        private void SetupCommonRewards()
        {
            List<ClueReward> commonRewards = osrsDataContainers.GetTreasureTrailRewards()[TreasureTrailUtility.COMMON_REWARDS].ToList();
            IEnumerable<Bitmap> images = Utility.GetImagesFromClues(commonRewards.ToList());
            commonListView.Items.Clear();
            commonListView.LargeImageList = new ImageList();
            commonListView.LargeImageList.ImageSize = new Size(64, 64);
            commonListView.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            commonListView.LargeImageList.Images.AddRange(images.ToArray());

            commonListView.Items.AddRange(commonRewards.Select(Utility.GetListViewItemFromClueReward).ToArray());
        }

        private void SetupEasyRewards()
        {
            List<ClueReward> easyRewards = osrsDataContainers.GetTreasureTrailRewards()[TreasureTrailUtility.EASY_REWARDS].ToList();
            IEnumerable<Bitmap> images = Utility.GetImagesFromClues(easyRewards.ToList());
            easyListView.Items.Clear();
            easyListView.LargeImageList = new ImageList();
            easyListView.LargeImageList.ImageSize = new Size(64, 64);
            easyListView.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            easyListView.LargeImageList.Images.AddRange(images.ToArray());

            easyListView.Items.AddRange(easyRewards.Select(Utility.GetListViewItemFromClueReward).ToArray());
        }

        private void SetupMediumRewards()
        {
            List<ClueReward> mediumRewards = osrsDataContainers.GetTreasureTrailRewards()[TreasureTrailUtility.MEDIUM_REWARDS].ToList();
            IEnumerable<Bitmap> images = Utility.GetImagesFromClues(mediumRewards.ToList());
            mediumListView.Items.Clear();
            mediumListView.LargeImageList = new ImageList();
            mediumListView.LargeImageList.ImageSize = new Size(64, 64);
            mediumListView.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            mediumListView.LargeImageList.Images.AddRange(images.ToArray());

            mediumListView.Items.AddRange(mediumRewards.Select(Utility.GetListViewItemFromClueReward).ToArray());
        }

        private void SetupHardRewards()
        {
            List<ClueReward> hardRewards = osrsDataContainers.GetTreasureTrailRewards()[TreasureTrailUtility.HARD_REWARDS].ToList();
            IEnumerable<Bitmap> images = Utility.GetImagesFromClues(hardRewards.ToList());
            hardListView.Items.Clear();
            hardListView.LargeImageList = new ImageList();
            hardListView.LargeImageList.ImageSize = new Size(64, 64);
            hardListView.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            hardListView.LargeImageList.Images.AddRange(images.ToArray());

            hardListView.Items.AddRange(hardRewards.Select(Utility.GetListViewItemFromClueReward).ToArray());
        }

        private void SetupEliteRewards()
        {
            List<ClueReward> eliteRewards = osrsDataContainers.GetTreasureTrailRewards()[TreasureTrailUtility.ELITE_REWARDS].ToList();
            IEnumerable<Bitmap> images = Utility.GetImagesFromClues(eliteRewards.ToList());
            eliteListView.Items.Clear();
            eliteListView.LargeImageList = new ImageList();
            eliteListView.LargeImageList.ImageSize = new Size(64, 64);
            eliteListView.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            eliteListView.LargeImageList.Images.AddRange(images.ToArray());

            eliteListView.Items.AddRange(eliteRewards.Select(Utility.GetListViewItemFromClueReward).ToArray());
        }

        private void SetupMasterRewards()
        {
            List<ClueReward> masterRewards = osrsDataContainers.GetTreasureTrailRewards()[TreasureTrailUtility.MASTER_REWARDS].ToList();
            IEnumerable<Bitmap> images = Utility.GetImagesFromClues(masterRewards.ToList());
            masterListView.Items.Clear();
            masterListView.LargeImageList = new ImageList();
            masterListView.LargeImageList.ImageSize = new Size(64, 64);
            masterListView.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            masterListView.LargeImageList.Images.AddRange(images.ToArray());

            masterListView.Items.AddRange(masterRewards.Select(Utility.GetListViewItemFromClueReward).ToArray());
        }

        /// <summary>
        /// Finalize the clue reward and add them to the logged drops.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logRewardButton_Click(object sender, EventArgs e)
        {
            foreach (Drop drop in clueDrops)
                osrsDataContainers.LogDrop(drop);
            Dispose();
        }

        /// <summary>
        /// Called whenever an item of a list view is clicked. Adds the clue item to the text box of rewards.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_ItemActivate(object sender, EventArgs e)
        {
            ListView listView = (ListView)sender;
            ListViewItem listViewItem = listView.SelectedItems[0];
            ClueReward rewardToLog = (ClueReward)listViewItem.Tag;
            QuantityInputForm quantityInputForm = new QuantityInputForm();

            string input = String.Empty;

            if (quantityInputForm.ShowDialog(this) == DialogResult.OK)
            {
                input = quantityInputForm.quantityTextInput.Text;
            }
            else
            {
                input = "0";
            }
            quantityInputForm.Dispose();
            try
            {
                int quantity = Convert.ToInt32(input);
                Drop drop = new Drop();
                drop.Quantity = quantity;
                drop.Name = rewardToLog.ItemName;
                if (clueDrops.Contains(drop))
                {
                    int existingDropIndex = clueDrops.IndexOf(drop);
                    drop.Quantity += clueDrops[existingDropIndex].Quantity;
                    clueDrops[existingDropIndex] = drop;
                }
                else
                {
                    clueDrops.Add(drop);
                }
                refreshRewardsLog();
            }
            catch (FormatException)
            {
                return;
            }
        }

        private void refreshRewardsLog()
        {
            rewardsLogBox.Clear();
            foreach (Drop drop in clueDrops)
                rewardsLogBox.AppendText(drop.ToString() + "\n");
        }
    }
}
