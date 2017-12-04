using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsrsDropEditor.DataGathering
{
    /// <summary>
    /// Used for deserializing OSB price data to a usable format. Value type because there is slightly
    /// less overhead and the price data is immutable anyways.
    /// </summary>
    public struct ItemPrice
    {
        public int Id { get; set; }
        [JsonProperty("overall_average")]
        public int OverallAverage { get; set; }
        [JsonProperty("buy_average")]
        public int BuyAverage { get; set; }
        [JsonProperty("sell_average")]
        public int SellAverage { get; set; }
        public int Sp { get; set; }
        public string Name { get; set; }
        public bool Members { get; set; }
    }
}
