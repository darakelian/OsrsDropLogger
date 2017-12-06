using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OsrsDropEditor
{
    /// <summary>
    /// Class for loading player's stats.
    /// </summary>
    class Highscores
    {
        private Browser browser = new Browser();

        private Dictionary<string, string> playerTypeLinks;

        private Dictionary<string, LevelContainer> playerSkillLevels = new Dictionary<string, LevelContainer>();

        private string[] skills;

        public Highscores()
        {
            playerTypeLinks = new Dictionary<string, string>()
            {
                { "normal", "http://services.runescape.com/m=hiscore_oldschool/index_lite.ws?player=" },
                { "ironman", "http://services.runescape.com/m=hiscore_oldschool_ironman/index_lite.ws?player=" },
                { "ultimate", "http://services.runescape.com/m=highscore_oldschool_ultimate/index_lite.ws?player=" },
                { "hardcore", "http://services.runescape.com/m=hiscore_oldschool_hardcore_ironman/index_lite.ws?player=" },
                { "deadman", "http://services.runescape.com/m=hiscore_oldschool_deadman/index_lite.ws?player=" },
                { "seasonal", "http://services.runescape.com/m=hiscore_oldschool_seasonal/index_lite.ws?player=" }
            };
            skills = new string[]
            {
                "Overall",
                "Attack",
                "Defence",
                "Strength",
                "Hitpoints",
                "Ranged",
                "Prayer",
                "Magic",
                "Cooking",
                "Woodcutting",
                "Fletching",
                "Fishing",
                "Firemaking",
                "Crafting",
                "Smithing",
                "Mining",
                "Herblore",
                "Agility",
                "Thieving",
                "Slayer",
                "Farming",
                "Runecrafting",
                "Hunter",
                "Construction"
            };
        }

        public void GetHighscoresForPlayer(string name)
        {
            GetHighscoresForPlayer(name, "normal");
        }

        public void GetHighscoresForPlayer(string name, string playerType)
        {
            string link = playerTypeLinks[playerType];

            browser.ExpectNonHtmlResponse = true;
            browser.Navigate(link + HttpUtility.UrlEncode(name), true);
            browser.ExpectNonHtmlResponse = false;

            string[] experienceRows = browser.InnerText.Split('\n');

            for (int i = 0; i < skills.Length; i++)
                playerSkillLevels[skills[i]] = GetLevelContainerForRow(experienceRows[i]);
        }

        private LevelContainer GetLevelContainerForRow(string row)
        {
            string[] values = row.Split(',');
            int experience = Convert.ToInt32(values[2]);
            int level = Convert.ToInt32(values[1]);

            return new LevelContainer(level, experience);
        }
    }

    public struct LevelContainer
    {
        public int Level { get; }

        public int Experience { get; }

        public LevelContainer(int level, int experience)
        {
            Level = level;
            Experience = experience;
        }
    }
}
