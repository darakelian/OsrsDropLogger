using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OsrsDropEditor
{
    class TreasureTrailUtility
    {
        private const string osrsWikiTreasureTrailRewardsLink = "/wiki/Treasure_Trails";

        public static readonly int COMMON_REWARDS = 0;
        public static readonly int EASY_REWARDS = 1;
        public static readonly int MEDIUM_REWARDS = 2;
        public static readonly int HARD_REWARDS = 3;
        public static readonly int ELITE_REWARDS = 4;
        public static readonly int MASTER_REWARDS = 5;

        private Browser browser;

        /// <summary>
        /// Dictionary with links for the images; key is an integer representing
        /// the level of reward and the value is a list of ClueReward objects
        /// </summary>
        public Dictionary<int, IEnumerable<ClueReward>> treasureTrailRewardImageLinks;

        public TreasureTrailUtility(Browser browser)
        {
            this.browser = browser;
            treasureTrailRewardImageLinks = new Dictionary<int, IEnumerable<ClueReward>>();
        }

        /// <summary>
        /// Gets the items that are possible clue rewards. There is no good way to delineate clue level tables
        /// so we use a temp variable to track which table we are working on.
        /// -------------------
        /// | Index  |  Level |
        /// |-----------------|
        /// | 0      | Common |
        /// | 1      | Easy   |
        /// | 2      | Medium |
        /// | 3      | Hard   |
        /// | 4      | Elite  |
        /// | 5      | Master |
        /// -------------------
        /// </summary>
        public void GetTreasureTrailItems()
        {
            browser.Navigate(osrsWikiTreasureTrailRewardsLink);
            IEnumerable<XmlNode> clueRewardTables = browser.SelectNodes("//*[local-name()='table' and @class='wikitable' and .//*[local-name()='th' and contains(., 'Image')]]");
            int index = 0;
            foreach (XmlNode clueRewardTable in clueRewardTables)
            {
                IEnumerable<XmlNode> rewardImageLinks = browser.SelectNodes(clueRewardTable, ".//*[local-name()='img' and not(.//*[local-name()='noscript'])]");
                treasureTrailRewardImageLinks[index++] = rewardImageLinks.Select(GetRewardForImage);
            }            
        }

        private ClueReward GetRewardForImage(XmlNode rewardImage)
        {
            string itemName = rewardImage.Attributes["alt"].Value.Replace("&#039;", "'");
            string imagePath = rewardImage.Attributes["src"]?.Value ?? rewardImage.Attributes["data-src"]?.Value;

            return new ClueReward { itemName = itemName, imagePath = imagePath };
        }
    }

    public struct ClueReward
    {
        public string itemName { get; set; }
        public string imagePath { get; set; }
    }
}
