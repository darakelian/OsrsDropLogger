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

        private Browser browser;

        /// <summary>
        /// Dictionary with links for the images; key is item name
        /// </summary>
        private Dictionary<string, string> treasureTrailRewardImageLinks;

        public TreasureTrailUtility(Browser browser)
        {
            this.browser = browser;
            treasureTrailRewardImageLinks = new Dictionary<string, string>();
        }

        public void GetTreasureTrailItems()
        {
            browser.Navigate(osrsWikiTreasureTrailRewardsLink);
            IEnumerable<XmlNode> clueRewardTables = browser.SelectNodes("//*[local-name()='table' and @class='wikitable' and .//*[local-name()='th' and contains(., 'Image')]]");
            foreach (XmlNode clueRewardTable in clueRewardTables)
            {
                IEnumerable<XmlNode> rewardImageLinks = browser.SelectNodes(clueRewardTable, ".//*[local-name()='img']");
                foreach (XmlNode rewardImage in rewardImageLinks)
                    GetRewardForImage(rewardImage);
            }            
        }

        private void GetRewardForImage(XmlNode rewardImage)
        {
            string itemName = rewardImage.Attributes["alt"].Value;
            string imagePath = rewardImage.Attributes["src"]?.Value ?? rewardImage.Attributes["data-src"]?.Value;

            treasureTrailRewardImageLinks[itemName] = imagePath;
        }
    }
}
