﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OsrsDropEditor.DataGathering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;

namespace OsrsDropEditor
{
    /// <summary>
    /// This class handles loading all the data as well as manipulating things like drop tables, prices etc.
    /// </summary>
    public class OsrsDataContainers
    {
        public const string OsrsWikiBase = "http://oldschoolrunescape.wikia.com";

        private const string osrsWikiBestiaryLink = "/wiki/Category:Bestiary";
        private const string osrsWikiRareDropTableLink = "/wiki/Rare_drop_table";
        private const string osbPriceLink = "https://rsbuddy.com/exchange/summary.json";
        

        private Browser browser = new Browser();
        private MainForm mainForm;
        private TreasureTrailUtility treasureTrailUtility;

        /// <summary>
        /// Stores all the drops that the user has logged. The key is the name of the item and the value is a
        /// Drop object. When the user adds more of the same drop, we will increment the quantity of the
        /// Drop object already stored in the dictionary. This is because we only really need the name and quantity
        /// properties of the Drop.
        /// </summary>
        public BindingList<LoggedDrop> LoggedDrops = new BindingList<LoggedDrop>();

        /// <summary>
        /// Stores the links for the NPCs in a dictionary so we don't parse them
        /// all at once. Key is the name of the NPC and the value is the link.
        /// </summary>
        public Dictionary<string, string> NpcLinks = new Dictionary<string, string>();

        /// <summary>
        /// Stores drop tables in memory so that we don't have to do another navigation to retrieve the information
        /// on a second click of the NPC in the list.
        /// </summary>
        public Dictionary<string, List<Drop>> CachedDropTables = new Dictionary<string, List<Drop>>();

        /// <summary>
        /// Stores the price for each item in an ItemPrice struct. Key is the ID
        /// of the item and the value is the ItemPrice struct.
        /// </summary>
        public static Dictionary<int, ItemPrice> ItemPrices = new Dictionary<int, ItemPrice>();

        /// <summary>
        /// Stores all the drops in the rare drop table.
        /// </summary>
        public List<Drop> RareDropTable = new List<Drop>();

        public OsrsDataContainers(MainForm mainForm)
        {
            this.mainForm = mainForm;
            treasureTrailUtility = new TreasureTrailUtility(browser);
        }

        /// <summary>
        /// Load up all the data we need for the app.
        /// </summary>
        public void LoadData()
        {
            LoadNpcLinks();
            LoadItemPrices();
            LoadRareDropTable();
            treasureTrailUtility.GetTreasureTrailItems();

            mainForm.loggedDropBindingSource.DataSource = LoggedDrops;

            TryLoadSavedDrops();
        }

        /// <summary>
        /// Loads all the links for NPCs from the OSRS wiki and stores them in a dictionary.
        /// </summary>
        public void LoadNpcLinks()
        {
            if (!Utility.FileExists("links.json"))
            {
                browser.Navigate(osrsWikiBestiaryLink);

                GetLinksOnPage();
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
        private void GetLinksOnPage()
        {
            IEnumerable<XmlNode> linksOnPage = browser.SelectNodes("//*[local-name()='div']//*[local-name()='table']//*[local-name()='a' and not(contains(@class, 'CategoryTreeLabel')) and not(contains(., 'User')) and not(contains(., 'Bestiary/Levels'))]");
            string currentUri = browser.Uri;
            foreach (XmlNode link in linksOnPage)
            {
                browser.Navigate(link.Attributes["href"].Value);
                if (GetDropTables().Any())
                {
                    NpcLinks[link.InnerText] = link.Attributes["href"].Value;
                    Utility.SaveObjectToJson("links.json", "OfflineJson", NpcLinks);
                }
            }
            browser.Navigate(currentUri, true);

            XmlNode nextPageLink = GetNextPageNode();
            if (nextPageLink != null)
            {
                browser.Navigate(nextPageLink.InnerText);
                GetLinksOnPage();
            }
        }

        /// <summary>
        /// Gets all the drop tables on the page.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<XmlNode> GetDropTables()
        {
            return browser.SelectNodes("//*[local-name()='table' and contains(@class, 'dropstable')]");
        }

        /// <summary>
        /// Gets the link that navigates us to the next page for the monster links
        /// </summary>
        /// <returns></returns>
        private XmlNode GetNextPageNode()
        {
            return browser.SelectSingleNode("//*[local-name()='a' and contains(@href, 'Category:Bestiary') and contains(., 'next')]/@href");
        }

        /// <summary>
        /// Loads all the prices for tradeable items from the OSB price data API and stores them in
        /// a dictionary.
        /// </summary>
        public void LoadItemPrices(bool forceRefresh = false)
        {
            if (!Utility.FileExists("prices.json") || Utility.ShouldRefreshPrices(forceRefresh))
            {
                try
                {
                    browser.ExpectNonHtmlResponse = true;
                    browser.Navigate(osbPriceLink, true);
                    browser.ExpectNonHtmlResponse = false;

                    JObject priceDataAsJson = (JObject)JToken.Parse(browser.InnerText);
                    IEnumerable<JToken> itemPricesJson = priceDataAsJson.Values();

                    ItemPrices = itemPricesJson.ToDictionary(itemToken => itemToken.Value<int>("id"), CreateItemPrice);

                    Utility.SaveObjectToJson("prices.json", "OfflineJson", ItemPrices);

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

        private void TryLoadItemPricesFromCache()
        {
            string cachedItemPrices = Utility.ReadFileToEnd("prices.json");
            if (!String.IsNullOrEmpty(cachedItemPrices))
                ItemPrices = JsonConvert.DeserializeObject<Dictionary<int, ItemPrice>>(cachedItemPrices);
        }

        /// <summary>
        /// Loads all the drops from the rare drop table page.
        /// </summary>
        public void LoadRareDropTable()
        {
            if (!Utility.FileExists("raredrops.json"))
            {
                browser.Navigate(osrsWikiRareDropTableLink);

                IEnumerable<XmlNode> tableNodes = browser.SelectNodes("//*[local-name()='table' and contains(@class, 'wikitable')]");
                foreach (XmlNode tableNode in tableNodes)
                {
                    IEnumerable<XmlNode> dropRows = browser.SelectNodes(tableNode, ".//*[local-name()='tr' and not(.//*[local-name()='th'])]");
                    Dictionary<string, int> headerMap = GetHeaderMap(tableNode);

                    if (headerMap.ContainsKey("Image"))
                        RareDropTable.AddRange(dropRows.SelectMany(dropRow => GetDropsFromRow(headerMap, dropRow)));
                }

                Utility.SaveObjectToJson("raredrops.json", "OfflineJson", RareDropTable);

                return;
            }
            else
            {
                string cachedRareDrops = Utility.ReadFileToEnd("raredrops.json");
                if (!String.IsNullOrEmpty(cachedRareDrops))
                    RareDropTable = JsonConvert.DeserializeObject<List<Drop>>(cachedRareDrops);
            }
        }

        /// <summary>
        /// Converts the JToken containing the price data to the ItemPrice struct.
        /// </summary>
        /// <param name="itemPriceJson">JToken containing the price data</param>
        /// <returns></returns>
        public ItemPrice CreateItemPrice(JToken itemPriceJson)
        {
            return JsonConvert.DeserializeObject<ItemPrice>(itemPriceJson.ToString());
        }

        /// <summary>
        /// Creates a header map from a table node. Using the header map we can access certain
        /// columns from the table based on header name rather than hard coding the indices.
        /// </summary>
        /// <param name="tableNode"></param>
        /// <returns></returns>
        public Dictionary<string, int> GetHeaderMap(XmlNode tableNode)
        {
            Dictionary<string, int> headerMap = new Dictionary<string, int>();

            XmlNodeList headers = tableNode.SelectNodes(".//*[local-name()='th']");
            int count = 1;

            foreach (XmlNode header in headers)
            {
                /*if (header.SelectSingleNode("./@colspan") != null || header.Attributes["class"]?.Value.Contains("unsortable"))
                {
                    headerMap["Image"] = count;
                    headerMap[header.InnerText.Trim()] = count++;
                    continue;
                }*/
                if (String.IsNullOrWhiteSpace(header.InnerText))
                {
                    headerMap["Image"] = count;
                    continue;
                }
                headerMap[header.InnerText.Trim()] = ++count;
            }

            return headerMap;
        }

        /// <summary>
        /// Converts an HTML row from the wiki to a Drop object.
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private IEnumerable<Drop> GetDropsFromRow(Dictionary<string, int> headers, XmlNode row)
        {
            List<Drop> drops = new List<Drop>();

            XmlNode imageRow = row.SelectSingleNode($".//*[local-name()='td'][{headers["Image"]}]//*[local-name()='img']");
            string name = row.SelectSingleNode($".//*[local-name()='td'][{headers["Item"]}]").InnerText.Trim();
            XmlNode rarityTag = row.SelectSingleNode($".//*[local-name()='td'][{headers["Rarity"]}]");

            string quantity = row.SelectSingleNode($".//*[local-name()='td'][{headers["Quantity"]}]").InnerText.Replace("(noted)", "")
                .Replace(",", "").Trim();

            string[] quantities = quantity.Split(';');
            foreach (string dropQuantity in quantities)
            {
                Drop drop = new Drop();
                drop.ImageLink = Utility.GetImageLink(imageRow);
                drop.Name = name;
                ProcessRarityNode(ref drop, rarityTag);
                if (Regex.IsMatch(dropQuantity, @"^\d+$"))
                {
                    drop.Quantity = Convert.ToInt32(dropQuantity);
                }
                if (IsRangeQuantity(dropQuantity))
                {
                    //Some drops have range and hard value
                    if (dropQuantity.Contains(";"))
                    {
                        string[] q = dropQuantity.Split(';');

                        //Since order is unknown, find token w/o dash
                        string qString = q.Where(s => !s.Contains('–')).First();
                        Drop hardValueDrop = new Drop();
                        hardValueDrop.ImageLink = drop.ImageLink;
                        hardValueDrop.Name = drop.Name;
                        hardValueDrop.Quantity = Convert.ToInt32(qString);
                        drops.Add(hardValueDrop);

                        quantity = q.Where(s => s.Contains('–')).First();
                    }

                    string[] quants = dropQuantity.Split('–');

                    drop.Quantity = -1;
                    drop.IsRangeOfDrops = true;
                    drop.RangeLowBound = Convert.ToInt32(quants[0]);
                    drop.RangeHighBound = Convert.ToInt32(quants[1]);
                }
                if (Regex.IsMatch(dropQuantity, @"\d+; \d+"))
                {
                    string[] quants = quantity.Split(';');

                    drop.Quantity = -1;
                    drop.HasMultipleQuantities = true;
                    drop.MultipleQuantities = quants.Select(q => Convert.ToInt32(q.Trim())).ToArray();
                }
                drops.Add(drop);
            }

            return drops;
        }

        /// <summary>
        /// Sets more information about the drop based on the rarity node.
        /// </summary>
        /// <param name="drop"></param>
        /// <param name="rarityNode"></param>
        private void ProcessRarityNode(ref Drop drop, XmlNode rarityNode)
        {
            if (rarityNode == null)
                return;

            string text = rarityNode.InnerText.Replace("~", "");
            //Some rows have more precise drop rates
            string actualDropRate = Regex.Match(text, @"(\d+\/\d+,?\d+\.?\d+)")?.Value;
            string rarity = rarityNode.SelectSingleNode("./text()").InnerText.Trim();
            drop.Rarity = rarity;
            drop.Rate = actualDropRate;
        }

        private bool IsRangeQuantity(string quantity)
        {
            return Regex.IsMatch(quantity, @"(\d+)–(\d+)");
        }

        /// <summary>
        /// Returns the ID of the first item that matches the name provided to this method.
        /// </summary>
        /// <param name="itemName">Name of the item we are trying to get an ID for</param>
        /// <returns></returns>
        public static int GetItemIdForName(string itemName)
        {
            return ItemPrices.Values.Where(item => item.Name.Contains(itemName)).FirstOrDefault().Id;
        }

        /// <summary>
        /// Returns all drops that a given NPC can drop.
        /// </summary>
        /// <param name="npcName"></param>
        /// <returns></returns>
        public IEnumerable<Drop> GetDropsForNpc(string npcName)
        {
            //Dictionary<string, Drop> drops = new Dictionary<string, Drop>();
            List<Drop> drops = new List<Drop>();

            string npcLink = NpcLinks[npcName];
            if (String.IsNullOrEmpty(npcLink))
                return Enumerable.Empty<Drop>();

            if (CachedDropTables.ContainsKey(npcName))
                return CachedDropTables[npcName];

            if (!Utility.FileExists(npcName + ".json", "OfflineJson\\DropTables"))
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

                        drops.AddRange(dropRows.SelectMany(dropRow => GetDropsFromRow(headerMap, dropRow)));
                    }

                    Utility.SaveObjectToJson($"{npcName}.json", @"OfflineJson\DropTables\", drops);
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

        /// <summary>
        /// Creates a dummy drop representing the rare drop table.
        /// </summary>
        /// <returns></returns>
        public Drop CreateRareDrop()
        {
            return new Drop
            {
                ImageLink = null,
                Name = "RareDropTable",
                Quantity = 1
            };
        }

        /// <summary>
        /// Adds a drop to the dictionary of logged drops.
        /// </summary>
        /// <param name="drop"></param>
        public void LogDrop(Drop drop)
        {
            LoggedDrop dropToAdd = new LoggedDrop(drop);

            if (LoggedDrops.Contains(dropToAdd))
            {
                int existingDropIndex = LoggedDrops.IndexOf(dropToAdd);
                dropToAdd.Quantity += LoggedDrops[existingDropIndex].Quantity;
                LoggedDrops[existingDropIndex] = dropToAdd;
            }
            else
            {
                LoggedDrops.Add(dropToAdd);
            }
        }

        /// <summary>
        /// Computes the sum of all logged drops.
        /// </summary>
        /// <returns></returns>
        public int GetTotalDropsValue()
        {
            return LoggedDrops.Sum(drop => drop.TotalPrice);
        }

        /// <summary>
        /// Computes the total value of the drops for a specific item ID. This method looks up the price of the
        /// item and then multiplies it by the number of items.
        /// </summary>
        /// <param name="loggedDrop"></param>
        /// <returns></returns>
        public static int GetPriceForDrops(LoggedDrop loggedDrop)
        {
            int itemId = GetItemIdForName(loggedDrop.Name);

            if (loggedDrop.Name.Contains("Coin"))
                return loggedDrop.Quantity;

            if (itemId == 0)
                return 0;

            ItemPrice price = ItemPrices[itemId];
            int priceToUse = price.OverallAverage > 0 ? price.OverallAverage : (price.SellAverage + price.BuyAverage) / 2;

            return priceToUse * loggedDrop.Quantity;
        }

        /// <summary>
        /// Attempts to load the saved drops from the json file at program startup.
        /// </summary>
        private void TryLoadSavedDrops()
        {
            if (Utility.FileExists("logged_drops.json"))
            {
                string savedDropsJson = Utility.ReadFileToEnd("logged_drops.json");
                BindingList<LoggedDrop> savedDrops = JsonConvert.DeserializeObject<BindingList<LoggedDrop>>(savedDropsJson);

                LoggedDrops = savedDrops;
                mainForm.loggedDropBindingSource.DataSource = LoggedDrops;

                mainForm.UpdateTotalValueLabel();
            }
        }

        public Dictionary<int, IEnumerable<ClueReward>> GetTreasureTrailRewards()
        {
            return treasureTrailUtility.treasureTrailRewardImageLinks;
        }
    }
}
