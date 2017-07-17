using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsrsDropEditor
{
    class Utility
    {

        /// <summary>
        /// Attempts to load a file from the default path.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ReadFileToEnd(string fileName, string filePath = @"..\..\OfflineJson\")
        {
            try
            {
                using (FileStream stream = File.OpenRead($@"{filePath}{fileName}"))
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

        public static bool FileExists(string fileName, string path = "OfflineJson")
        {
            return File.Exists($@"..\..\{path}\{fileName}");
        }

        /// <summary>
        /// Checks if the prices should be refreshed on start up. We only want to refresh the prices
        /// every 6 hours since that is how often OSB updates the prices. This time delay can be
        /// switched in the properties.
        /// </summary>
        /// <returns></returns>
        public static bool ShouldRefreshPrices()
        {
            DateTime timeSinceLastRefresh = Properties.Settings.Default.TimeSinceLastRefresh;

            return (DateTime.Now - timeSinceLastRefresh).Hours >= Properties.Settings.Default.DelayBetweenPriceUpdate;
        }

        public static IEnumerable<Bitmap> GetImagesFromLinks(Dictionary<string, string> linksDictionary)
        {
            foreach (string name in linksDictionary.Keys)
            {
                if (FileExists(name + ".jpg", "CachedImages"))
                {

                }
                else
                {
                    string link = linksDictionary[name];
                }
            }
        }
    }
}
