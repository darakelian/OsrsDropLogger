﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;

namespace OsrsDropEditor
{
    /// <summary>
    /// Class for loading player's stats.
    /// </summary>
    class Highscores
    {
        public static Dictionary<string, string> PlayerTypeLinks = new Dictionary<string, string>()
        {
            { "normal", "http://services.runescape.com/m=hiscore_oldschool/index_lite.ws?player=" },
            { "ironman", "http://services.runescape.com/m=hiscore_oldschool_ironman/index_lite.ws?player=" },
            { "ultimate", "http://services.runescape.com/m=highscore_oldschool_ultimate/index_lite.ws?player=" },
            { "hardcore", "http://services.runescape.com/m=hiscore_oldschool_hardcore_ironman/index_lite.ws?player=" },
            { "deadman", "http://services.runescape.com/m=hiscore_oldschool_deadman/index_lite.ws?player=" },
            { "seasonal", "http://services.runescape.com/m=hiscore_oldschool_seasonal/index_lite.ws?player=" }
        };

        public static string[] Skills = new string[]
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

        private readonly string skillIconsPage = "/wiki/Skills";

        private Browser browser = new Browser();

        public Dictionary<string, LevelContainer> playerSkillLevels = new Dictionary<string, LevelContainer>();

        public Highscores()
        {
            //Load the images from the wiki and cache them
            browser.Navigate(skillIconsPage);
            foreach (string skill in Skills)
            {
                //Get image link
                XmlNode imageLinkNode = browser.SelectSingleNode($"//*[local-name()='a' and contains(@href, '{skill}') and contains(@href, '.png')]");
                if (imageLinkNode != null)
                {
                    string link = imageLinkNode.SelectSingleNode("./@href").InnerText;
                    //We don't need the image at this point, just attempt to fetch it
                    Utility.GetImageFromLink(skill, link, !link.Contains("http"));
                }
            }
        }

        public bool GetHighscoresForPlayer(string name)
        {
            return GetHighscoresForPlayer(name, "normal");
        }

        /// <summary>
        /// Navigates to the highscores API page and creates the highscores data structure.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="playerType"></param>
        public bool GetHighscoresForPlayer(string name, string playerType)
        {
            string link = PlayerTypeLinks[PlayerTypeLinks.Keys.FirstOrDefault(key => playerType.ToLower().Contains(key))];

            try
            {
                browser.ExpectNonHtmlResponse = true;
                browser.Navigate(link + HttpUtility.UrlEncode(name), true);
                browser.ExpectNonHtmlResponse = false;
            }
            catch (WebException)
            {
                Console.WriteLine("Unable to load highscores");
                return false;
            }

            string[] experienceRows = browser.InnerText.Split('\n');

            for (int i = 0; i < Skills.Length; i++)
                playerSkillLevels[Skills[i].ToLower()] = GetLevelContainerForRow(experienceRows[i]);

            return true;
        }

        private LevelContainer GetLevelContainerForRow(string row)
        {
            string[] values = row.Split(',');

            int rank = Convert.ToInt32(values[0]);
            int level = Convert.ToInt32(values[1]);
            int experience = Convert.ToInt32(values[2]);

            return new LevelContainer(rank, level, experience);
        }

        public int GetRankForSkill(string skillName)
        {
            return playerSkillLevels.Where(kvp => skillName.ToLower().Contains(kvp.Key)).FirstOrDefault().Value.Rank;
        }

        public int GetLevelForSkill(string skillName)
        {
            return playerSkillLevels.Where(kvp => skillName.ToLower().Contains(kvp.Key)).FirstOrDefault().Value.Level;
        }

        public int GetExperienceForSkill(string skillName)
        {
            return playerSkillLevels.Where(kvp => skillName.ToLower().Contains(kvp.Key)).FirstOrDefault().Value.Experience;
        }

        public int GetExperienceForNextLevel(string skillName)
        {
            return Utility.GetExperienceLeft(GetExperienceForSkill(skillName), GetLevelForSkill(skillName));
        }

        public Tuple<string, int> GetExperienceForClosestLevel()
        {
            string skillClosestToLevel = playerSkillLevels.OrderBy(kvp => GetExperienceForNextLevel(kvp.Key)).First().Key;

            return new Tuple<string, int>(skillClosestToLevel, GetExperienceForNextLevel(skillClosestToLevel));
        }

        public double GetPercentNextLevel(string skillName)
        {
            return Utility.GetPercentageToNextLevel(GetExperienceForSkill(skillName), GetLevelForSkill(skillName));
        }

    }

    public struct LevelContainer
    {
        public int Rank { get; }

        public int Level { get; }

        public int Experience { get; }

        public LevelContainer(int rank, int level, int experience)
        {
            Rank = rank;
            Level = level;
            Experience = experience;
        }
    }
}
