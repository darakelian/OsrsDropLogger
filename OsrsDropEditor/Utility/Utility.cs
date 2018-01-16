using Newtonsoft.Json;
using OsrsDropEditor.DataGathering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml;

namespace OsrsDropEditor
{
    class Utility
    {
        public static string RootPath => Properties.Settings.Default.FilePath;

        /// <summary>
        /// Attempts to load a file from the default path.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ReadFileToEnd(string fileName, string filePath = "OfflineJson")
        {
            try
            {
                using (FileStream stream = File.OpenRead($@"{RootPath}\{filePath}\{fileName}"))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Unable to load file: " + fileName);

                return String.Empty;
            }
        }

        public static bool FileExists(string fileName, string filePath = "OfflineJson")
        {
            return File.Exists($@"{RootPath}\{filePath}\{fileName}");
        }

        /// <summary>
        /// Checks if the prices should be refreshed on start up. We only want to refresh the prices
        /// every 6 hours since that is how often OSB updates the prices. This time delay can be
        /// switched in the properties.
        /// </summary>
        /// <returns></returns>
        public static bool ShouldRefreshPrices(bool overrideSettings = false)
        {
            if (overrideSettings)
                return true;

            DateTime timeSinceLastRefresh = Properties.Settings.Default.TimeSinceLastRefresh;

            return (DateTime.Now - timeSinceLastRefresh).Hours >= Properties.Settings.Default.DelayBetweenPriceUpdate;
        }

        private const string placeHolderImagePath = @".\missing_image.png";
        private const string rareDropTableImagePath = @".\rdt.png";

        /// <summary>
        /// Returns an enumerable of the bitmaps used to display the items. First it tries to load the image from
        /// the saved files. If that doesn't work, it tries to download the image. In the case that the image
        /// can't be downloaded, it falls back to the default error image.
        /// </summary>
        /// <param name="drops"></param>
        /// <returns></returns>
        public static IEnumerable<Bitmap> GetImagesFromDrops(List<Drop> drops)
        {
            return drops.Select(drop => GetImageFromObject(drop));
        }

        /// <summary>
        /// Returns an enumerable of the bitmaps used to display the items. First it tries to load the image from
        /// the saved files. If that doesn't work, it tries to download the image. In the case that the image
        /// can't be downloaded, it falls back to the default error image.
        /// </summary>
        /// <param name="rewards"></param>
        /// <returns></returns>
        public static IEnumerable<Bitmap> GetImagesFromClues(List<ClueReward> rewards)
        {
            return rewards.Select(reward => GetImageFromObject(reward));
        }

        public static string GetImageLink(XmlNode imageRow)
        {
            return imageRow.Attributes["src"].Value.Contains("http") ? imageRow.Attributes["src"].Value : imageRow.Attributes["data-src"].Value;
        }

        /// <summary>
        /// Allows loading of image for single drop or clue reward object.
        /// </summary>
        /// <param name="drop"></param>
        /// <returns></returns>
        private static Bitmap GetImageFromObject(object dataObject)
        {
            string name = String.Empty;
            string link = String.Empty;

            if (dataObject is Drop)
            {
                name = ((Drop)dataObject).Name;
                link = ((Drop)dataObject).ImageLink;
            }
            else if (dataObject is ClueReward)
            {
                name = ((ClueReward)dataObject).ItemName;
                link = ((ClueReward)dataObject).ImagePath;
            }

            return GetImageFromLink(name, link);
        }

        /// <summary>
        /// Returns a Bitmap image from the link provided. If the image has already been downloaded,
        /// we return that one instead.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        public static Bitmap GetImageFromLink(string name, string link, bool relative = false)
        {
            if (name.Equals("RareDropTable"))
                return (Bitmap)Image.FromFile(rareDropTableImagePath, true);
            else if (FileExists(name + ".png", "CachedImages"))
                return GetImageFromFile(name);
            else
            {
                using (WebClient client = new WebClient())
                {
                    try
                    {
                        string destinationUrl = relative ? OsrsDataContainers.OsrsWikiBase + link : link;
                        using (Stream stream = client.OpenRead(destinationUrl))
                        {
                            Bitmap bitmap = new Bitmap(stream);
                            if (bitmap != null)
                            {
                                string path = $@"{RootPath}\CachedImages\";
                                (new FileInfo(path)).Directory.Create();
                                bitmap.Save($"{path}{name}.png", ImageFormat.Png);
                                return bitmap;
                            }
                        }
                    }
                    catch (WebException e)
                    {
                        return GetPlaceHolderImage(e.StackTrace);
                    }
                }
            }

            return GetPlaceHolderImage();
        }

        public static Bitmap GetImageFromFile(string name)
        {
            return (Bitmap)Image.FromFile($@"{RootPath}\CachedImages\{name}.png", true);
        }

        private static Bitmap GetPlaceHolderImage(string stackTrace = "")
        {
            Console.WriteLine("Error downloading file: " + stackTrace);
            return (Bitmap)Image.FromFile(placeHolderImagePath, true);
        }

        /// <summary>
        /// Serializes an object to JSON then saves it to the provided path as a new file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="objectToSave"></param>
        public static void SaveObjectToJson(string fileName, string filePath, object objectToSave)
        {
            string basePath = $@"{RootPath}\{filePath}\";

            FileInfo fileInfo = new FileInfo(basePath);
            fileInfo.Directory.Create();

            string json = JsonConvert.SerializeObject(objectToSave);
            File.WriteAllText($"{basePath}{fileName}", json);
        }

        /// <summary>
        /// Converts a number without commas to display according to how it is done in OSRS (e.g. 999,999 becomes 999k).
        /// This method will also place the appropriate commas.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatNumberForDisplay(int value)
        {
            if (value >= 100_000 && value <= 9_999_999)
                return $"{value / 1000}K";
            else if (value >= 10_000_000)
                return $"{value / 1_000_000}M";
            else
                return String.Format("{0:n0}", value);
        }

        public static ListViewItem GetListViewItemFromDrop(Drop drop, int slot)
        {
            return GetListViewItemFromObject(drop, slot);
        }

        public static ListViewItem GetListViewItemFromClueReward(ClueReward reward, int slot)
        {
            return GetListViewItemFromObject(reward, slot);
        }

        /// <summary>
        /// Converts a new ListViewItem using the provided Drop object. The slot is so we can determine what image
        /// gets displayed in the ListView.
        /// </summary>
        /// <param name="drop"></param>
        /// <param name="slot"></param>
        /// <returns></returns>
        private static ListViewItem GetListViewItemFromObject(object dataObject, int slot)
        {
            ListViewItem item = new ListViewItem();
            item.Tag = dataObject;
            item.Text = dataObject is Drop ? ((Drop)dataObject).ToString() : ((ClueReward)dataObject).ToString();
            item.ImageIndex = slot;
            item.BackColor = GetColorForDataObject(dataObject);
            if (dataObject is Drop)
            {
                Drop drop = (Drop)dataObject;
                item.ToolTipText = $"{drop.Rarity} {drop.Rate}".Trim();
            }

            return item;
        }

        public static readonly string ALWAYS_HEX = "#AFEEEE";
        public static readonly string COMMON_HEX = "#56E156";
        public static readonly string UNCOMMON_HEX = "#FFED4C";
        public static readonly string RARE_HEX = "#FF863C";
        public static readonly string VERY_RARE_HEX = "#FF6262";

        private static Color GetColorForDataObject(object dataObject)
        {
            if (dataObject is Drop)
            {
                Drop drop = (Drop)dataObject;
                switch (drop.Rarity?.ToLower())
                {
                    case "always":
                        return ColorTranslator.FromHtml(ALWAYS_HEX);
                    case "common":
                        return ColorTranslator.FromHtml(COMMON_HEX);
                    case "uncommon":
                        return ColorTranslator.FromHtml(UNCOMMON_HEX);
                    case "rare":
                        return ColorTranslator.FromHtml(RARE_HEX);
                    case "very rare":
                        return ColorTranslator.FromHtml(VERY_RARE_HEX);
                    default:
                        return ColorTranslator.FromHtml(COMMON_HEX);
                }
            }
            return Color.White;
        }

        /// <summary>
        /// Saves the given username in the application settings.
        /// </summary>
        /// <param name="username"></param>
        public static void SaveUsername(string username)
        {
            Properties.Settings.Default.username = username;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Computes the amount of experience necessary for a given level
        /// </summary>
        /// <param name="targetLevel"></param>
        /// <returns></returns>
        public static double GetExperienceForLevel(int targetLevel)
        {
            double experience = 0;
            for (int x = 1; x <= targetLevel - 1; x++)
            {
                double sumTemp = x + 300 * Math.Pow(2, (double)x / 7);
                experience += Math.Floor(sumTemp);
            }
            experience /= 4;
            //XP in RS is capped to 200M
            return Math.Min(Math.Floor(experience), 200_000_000_000);
        }

        public static int GetExperienceLeft(int currentXp, int currentLevel)
        {
            return (int)GetExperienceForLevel(currentLevel + 1) - currentXp;
        }

        /// <summary>
        /// Computes the percentage towards the next level.
        /// </summary>
        /// <param name="currentXp"></param>
        /// <param name="currentLevel"></param>
        /// <returns></returns>
        public static double GetPercentageToNextLevel(int currentXp, int currentLevel)
        {
            double xpForNextLevel = GetExperienceForLevel(currentLevel + 1);
            double xpForCurrentLevel = GetExperienceForLevel(currentLevel);
            double xpLeft = GetExperienceLeft(currentXp, currentLevel);
            double xpBand = xpForNextLevel - xpForCurrentLevel;

            return 100 * (1 - (xpLeft / xpBand));
        }

        /// <summary>
        /// Convert a string to an integer allowing for modifiers such as K or M.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ConvertStringToInt(string input, out int parsedValue)
        {
            input = input.ToLower();

            int multiplier = 1;
            if (input.EndsWith("k"))
            {
                multiplier = 1000;
                input = input.Trim('k');
            }
            if (input.EndsWith("m"))
            {
                multiplier = 1000000;
                input = input.Trim('m');
            }

            int baseValue = 0;
            Int32.TryParse(input, out baseValue);
            parsedValue = baseValue * multiplier;

            return baseValue != 0;
        }
    }
}
