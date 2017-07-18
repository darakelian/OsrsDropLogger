using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;

namespace OsrsDropEditor
{
    class OsrsDataContainers
    {
        public const string OsrsWikiBase = "http://oldschoolrunescape.wikia.com";

        private const string osrsWikiBestiaryLink = "/wiki/Category:Bestiary";
        private const string osrsWikiRareDropTableLink = "/wiki/Rare_drop_table";
        private const string osbPriceLink = "https://rsbuddy.com/exchange/summary.json";

        private static Browser browser = new Browser();

        /// <summary>
        /// Stores the links for the NPCs in a dictionary so we don't parse them
        /// all at once. Key is the name of the NPC and the value is the link.
        /// </summary>
        public static Dictionary<string, string> NpcLinks = new Dictionary<string, string>();

        /// <summary>
        /// Stores the price for each item in an ItemPrice struct. Key is the ID
        /// of the item and the value is the ItemPrice struct.
        /// </summary>
        public static Dictionary<int, ItemPrice> ItemPrices = new Dictionary<int, ItemPrice>();

        public static List<Drop> RareDropTable = new List<Drop>();

        public static Dictionary<string, List<Drop>> CachedDropTables = new Dictionary<string, List<Drop>>();

        #region Loading of initial links/prices
        public static void LoadData()
        {
            LoadNpcLinks();
            LoadItemPrices();
            LoadRareDropTable();
        }

        /// <summary>
        /// Loads all the links for NPCs from the OSRS wiki and stores them in a dictionary.
        /// </summary>
        public static void LoadNpcLinks()
        {
            if (!File.Exists(@"../../OfflineJson/links.json"))
            {
                browser.Navigate(osrsWikiBestiaryLink);

                GetLinksOnPage();

                string linksAsJson = JsonConvert.SerializeObject(NpcLinks);
                File.WriteAllText(@"../../OfflineJson/links.json", linksAsJson);

                return;
            }
            else
            {
                string cachedJson = Utility.ReadFileToEnd("links.json");
                if (!String.IsNullOrEmpty(cachedJson))
                    NpcLinks = JsonConvert.DeserializeObject<Dictionary<string, string>>(cachedJson);
            }
        }

        /// <summary>
        /// Loads the links for all NPCs that have any drops at all. Note: this method is very, very slow so
        /// don't delete the json file it creates or it will take a couple of minutes to load. The reason this
        /// is slow is because there is no good way to check if the NPC has a drop table or not unless we
        /// actually navigate to the page and try to get drop tables.
        /// </summary>
        private static void GetLinksOnPage()
        {
            IEnumerable<XmlNode> linksOnPage = browser.SelectNodes("//*[local-name()='div']//*[local-name()='table']//*[local-name()='a' and not(contains(@class, 'CategoryTreeLabel')) and not(contains(., 'User')) and not(contains(., 'Bestiary/Levels'))]");
            string currentUri = browser.Uri;
            foreach (XmlNode link in linksOnPage)
            {
                browser.Navigate(link.Attributes["href"].Value);
                if (GetDropTables().Any())
                    NpcLinks[link.InnerText] = link.Attributes["href"].Value;
            }
            browser.Navigate(currentUri, true);

            XmlNode nextPageLink = GetNextPageNode();
            if (nextPageLink != null)
            {
                browser.Navigate(nextPageLink.InnerText, true);
                GetLinksOnPage();
            }
        }

        /// <summary>
        /// Gets all the drop tables on the page.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<XmlNode> GetDropTables()
        {
            return browser.SelectNodes("//*[local-name()='table' and contains(@class, 'dropstable')]");
        }

        /// <summary>
        /// Gets the link that navigates us to the next page for the monster links
        /// </summary>
        /// <returns></returns>
        private static XmlNode GetNextPageNode()
        {
            return browser.SelectSingleNode("//*[local-name()='a' and contains(@href, 'Category:Bestiary') and contains(@class, 'paginator-next')]/@href");
        }

        /// <summary>
        /// Loads all the prices for tradeable items from the OSB price data API and stores them in
        /// a dictionary.
        /// </summary>
        public static void LoadItemPrices()
        {
            if (!Utility.FileExists("prices.json") || Utility.ShouldRefreshPrices())
            {
                try
                {
                    browser.Navigate(osbPriceLink, true);

                    JObject priceDataAsJson = (JObject)JToken.Parse(browser.InnerText);
                    IEnumerable<JToken> itemPricesJson = priceDataAsJson.Values();

                    ItemPrices = itemPricesJson.ToDictionary(itemToken => itemToken.Value<int>("id"), CreateItemPrice);

                    string pricesAsJson = JsonConvert.SerializeObject(ItemPrices);
                    File.WriteAllText(@"..\..\OfflineJson\prices.json", pricesAsJson);

                    Properties.Settings.Default.TimeSinceLastRefresh = DateTime.Now;
                    Properties.Settings.Default.Save();
                }
                catch (WebException)
                {
                    TryLoadItemPricesFromCache();
                }
            }
            else
            {
                TryLoadItemPricesFromCache();
            }
        }

        private static void TryLoadItemPricesFromCache()
        {
            string cachedItemPrices = Utility.ReadFileToEnd("prices.json");
            if (!String.IsNullOrEmpty(cachedItemPrices))
                ItemPrices = JsonConvert.DeserializeObject<Dictionary<int, ItemPrice>>(cachedItemPrices);
        }

        /// <summary>
        /// Loads all the drops from the rare drop table page.
        /// </summary>
        public static void LoadRareDropTable()
        {
            if (!File.Exists(@"..\..\OfflineJson\raredrops.json"))
            {
                browser.Navigate(osrsWikiRareDropTableLink);

                IEnumerable<XmlNode> tableNodes = browser.SelectNodes("//*[local-name()='table' and contains(@class, 'wikitable')]");
                foreach (XmlNode tableNode in tableNodes)
                {
                    IEnumerable<XmlNode> dropRows = browser.SelectNodes(tableNode, ".//*[local-name()='tr' and not(.//*[local-name()='th'])]");
                    Dictionary<string, int> headerMap = GetHeaderMap(tableNode);

                    RareDropTable.AddRange(dropRows.Select(dropRow => GetDropFromRow(headerMap, dropRow)));
                }

                string rareDropsAsJson = JsonConvert.SerializeObject(RareDropTable);
                File.WriteAllText(@"../../OfflineJson/raredrops.json", rareDropsAsJson);

                return;
            }
            else
            {
                string cachedRareDrops = Utility.ReadFileToEnd("raredrops.json");
                if (!String.IsNullOrEmpty(cachedRareDrops))
                    RareDropTable = JsonConvert.DeserializeObject<List<Drop>>(cachedRareDrops);
            }
        }
        #endregion

        #region Helper methods for getting data
        /// <summary>
        /// Converts the JToken containing the price data to the ItemPrice struct.
        /// </summary>
        /// <param name="itemPriceJson">JToken containing the price data</param>
        /// <returns></returns>
        public static ItemPrice CreateItemPrice(JToken itemPriceJson)
        {
            return JsonConvert.DeserializeObject<ItemPrice>(itemPriceJson.ToString());
        }

        /// <summary>
        /// Creates a header map from a table node. Using the header map we can access certain
        /// columns from the table based on header name rather than hard coding the indices.
        /// </summary>
        /// <param name="tableNode"></param>
        /// <returns></returns>
        public static Dictionary<string, int> GetHeaderMap(XmlNode tableNode)
        {
            Dictionary<string, int> headerMap = new Dictionary<string, int>();

            XmlNodeList headers = tableNode.SelectNodes(".//*[local-name()='th']");
            int count = 1;

            foreach (XmlNode header in headers)
            {
                if (header.SelectSingleNode("./@colspan") != null)
                {
                    headerMap["Image"] = count;
                    headerMap[header.InnerText.Trim()] = ++count;
                    continue;
                }
                headerMap[header.InnerText.Trim()] = ++count;
            }

            return headerMap;
        }

        private static Drop GetDropFromRow(Dictionary<string, int> headers, XmlNode row)
        {
            Drop drop = new Drop();
            drop.ImageLink = row.SelectSingleNode($".//*[local-name()='td'][{headers["Image"]}]//*[local-name()='img']/@data-src").InnerText;
            drop.Name = row.SelectSingleNode($".//*[local-name()='td'][{headers["Item"]}]").InnerText.Trim();

            string quantity = row.SelectSingleNode($".//*[local-name()='td'][{headers["Quantity"]}]").InnerText.Replace("(noted)", "").Trim();
            if (Regex.IsMatch(quantity, @"^\d+$"))
            {
                drop.Quantity = Convert.ToInt32(quantity);
            }
            Match rangeMatch = Regex.Match(quantity, @"(\d+)–(\d+)");
            if (rangeMatch.Success)
            {
                drop.Quantity = -1;
                drop.IsRangeOfDrops = true;
                drop.RangeLowBound = Convert.ToInt32(rangeMatch.Groups[1].Value);
                drop.RangeHighBound = Convert.ToInt32(rangeMatch.Groups[2].Value);
            }
            if (Regex.IsMatch(quantity, @"\d+; \d+"))
            {
                string[] quantities = quantity.Split(';');
                drop.HasMultipleQuantities = true;
                drop.MultipleQuantities = quantities.Select(q => Convert.ToInt32(q.Trim())).ToArray();
            }

            return drop;
        }

        /// <summary>
        /// Returns the ID of the first item that matches the name provided to this method.
        /// </summary>
        /// <param name="itemName">Name of the item we are trying to get an ID for</param>
        /// <returns></returns>
        public static int GetItemIdForName(string itemName)
        {
            return ItemPrices.Values.Where(item => item.Name.ToLower().Contains(itemName)).First().Id;
        }

        public static IEnumerable<Drop> GetDropsForNpc(string npcName)
        {
            //Dictionary<string, Drop> drops = new Dictionary<string, Drop>();
            List<Drop> drops = new List<Drop>();

            string npcLink = NpcLinks[npcName];
            if (String.IsNullOrEmpty(npcLink))
                return Enumerable.Empty<Drop>();

            if (CachedDropTables.ContainsKey(npcName))
                return CachedDropTables[npcName];

            if (!Utility.FileExists(npcName + ".json"))
            {
                try
                {
                    browser.Navigate(npcLink);

                    IEnumerable<XmlNode> tableNodes = GetDropTables();
                    foreach (XmlNode tableNode in tableNodes)
                    {
                        if (tableNode.Attributes["class"].Value.Contains("rdtable"))
                        {
                            drops.Add(CreateRareDrop());
                            continue;
                        }
                        IEnumerable<XmlNode> dropRows = browser.SelectNodes(tableNode, ".//*[local-name()='tr' and not(.//*[local-name()='th'])]");
                        Dictionary<string, int> headerMap = GetHeaderMap(tableNode);

                        //foreach (Drop drop in dropRows.Select(dropRow => GetDropFromRow(headerMap, dropRow)))
                        //drops[drop.Name] = drop;
                        drops.AddRange(dropRows.Select(dropRow => GetDropFromRow(headerMap, dropRow)));
                    }

                    string dropsAsJson = JsonConvert.SerializeObject(drops);
                    File.WriteAllText($@"..\..\OfflineJson\DropTables\{npcName}.json", dropsAsJson);
                }
                catch (WebException)
                {
                    return Enumerable.Empty<Drop>();
                }
            }
            else
            {
                string cachedDropJson = Utility.ReadFileToEnd($@"DropTables\{npcName}.json");
                if (!String.IsNullOrEmpty(cachedDropJson))
                {
                    List<Drop> cachedDrops = JsonConvert.DeserializeObject<List<Drop>>(cachedDropJson);
                    drops.AddRange(cachedDrops);
                }
            }

            return CachedDropTables[npcName] = drops;
        }

        public static Drop CreateRareDrop()
        {
            return new Drop
            {
                ImageLink = null,
                Name = "RareDropTable",
                Quantity = 1
            };
        }
        #endregion
    }

    #region Structs for serialization
    /// <summary>
    /// Used for deserializing OSB price data to a usable format. Value type because there is slightly
    /// less overhead and the price data is immutable anyways.
    /// </summary>
    public struct ItemPrice
    {
        public int Id { get; set; }
        public int OverallAverage { get; set; }
        public int SellAverage { get; set; }
        public int Sp { get; set; }
        public string Name { get; set; }
        public bool Members { get; set; }
    }

    /// <summary>
    /// Used to store information about drops.
    /// </summary>
    public struct Drop
    {
        public string ImageLink { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        public bool IsRangeOfDrops { get; set; }
        public int? RangeLowBound { get; set; }
        public int? RangeHighBound { get; set; }

        public bool HasMultipleQuantities { get; set; }
        public int[] MultipleQuantities { get; set; }

        public override string ToString()
        {
            if (IsRangeOfDrops)
                return $"{Name}: {RangeLowBound}-{RangeHighBound}";
            if (HasMultipleQuantities)
                return $"{Name}: {String.Join(", ", MultipleQuantities)}";

            return $"{Name}: {Quantity}";
        }
    }
    #endregion

}
