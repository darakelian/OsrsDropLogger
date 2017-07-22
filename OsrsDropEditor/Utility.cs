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

        public static void WriteTextToFile(string fileName, string content, string filePath = "OfflineJson")
        {
            try
            {
                File.WriteAllText($@"{RootPath}\{filePath}\{fileName}", content);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
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
        public static bool ShouldRefreshPrices()
        {
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
                    Bitmap bitmap = (Bitmap)Image.FromFile($@"{RootPath}\CachedImages\{name}.png", true);
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
                                    string path = $@"{RootPath}\CachedImages\";
                                    (new FileInfo(path)).Directory.Create();
                                    bitmap.Save($"{path}{name}.png", ImageFormat.Png);
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
        public static void SaveObjectToJson(string fileName, string filePath, object objectToSave)
        {
            string basePath = $@"{RootPath}\{filePath}\";

            FileInfo fileInfo = new FileInfo(basePath);
            fileInfo.Directory.Create();

            string json = JsonConvert.SerializeObject(objectToSave);
            File.WriteAllText($"{basePath}{fileName}", json);
        }
    }
}
