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

        public AddTreasureTrailRewardForm(OsrsDataContainers osrsDataContainers)
        {
            InitializeComponent();
            this.osrsDataContainers = osrsDataContainers;

            SetupCommonRewards();
            SetupEasyRewards();
            SetupMediumRewards();
            SetupHardRewards();
            SetupEliteRewards();
            SetupMasterRewards();
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
            
        }

        private void SetupMediumRewards()
        {

        }

        private void SetupHardRewards()
        {

        }

        private void SetupEliteRewards()
        {

        }

        private void SetupMasterRewards()
        {

        }
    }
}
