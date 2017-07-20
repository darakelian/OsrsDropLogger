using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
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
        public static string ReadFileToEnd(string fileName, string filePath = @"OfflineJson\")
        {
            try
            {
                using (FileStream stream = File.OpenRead($@"..\..\{filePath}{fileName}"))
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

        private const string placeHolderImagePath = @"..\..\missing_image.png";
        private const string rareDropTableImagePath = @"..\..\rdt.png";

        public static IEnumerable<Bitmap> GetImagesFromDrops(List<Drop> drops)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            List<Bitmap> images = new List<Bitmap>();

            foreach (Drop drop in drops)
            {
                string name = drop.Name;

                if (name.Equals("RareDropTable"))
                {
                    Bitmap bitmap = (Bitmap)Image.FromFile(rareDropTableImagePath, true);
                    images.Add(bitmap);
                    continue;
                }

                if (FileExists(name + ".png", "CachedImages"))
                {
                    Bitmap bitmap = (Bitmap)Image.FromFile($@"..\..\CachedImages\{name}.png", true);
                    images.Add(bitmap);
                }
                else
                {
                    string link = drop.ImageLink;
                    using (WebClient client = new WebClient())
                    {
                        try
                        {
                            using (Stream stream = client.OpenRead(link))
                            {
                                Bitmap bitmap = new Bitmap(stream);
                                if (bitmap != null)
                                {
                                    bitmap.Save($@"..\..\CachedImages\{name}.png", ImageFormat.Png);
                                    images.Add(bitmap);
                                }
                            }
                        }
                        catch (WebException e)
                        {
                            //Return the placeholder image here.
                            Bitmap bitmap = (Bitmap)Image.FromFile(placeHolderImagePath, true);
                            images.Add(bitmap);
                            Console.WriteLine("Error downloading file: " + e.StackTrace);
                        }
                    }
                }
            }

            return images;
        }

        /// <summary>
        /// Serializes an object to JSON then saves it to the provided path as a new file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="objectToSave"></param>
        public static void SaveObjectToJson(string path, object objectToSave)
        {
            string json = JsonConvert.SerializeObject(objectToSave);
            File.WriteAllText(path, json);
        }
    }
}
