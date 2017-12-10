using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsrsDropEditor.DataGathering
{
    /// <summary>
    /// Used to store information about drops.
    /// </summary>
    public struct Drop
    {
        public string ImageLink { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Rarity { get; set; }
        public string Rate { get; set; }

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

        public override bool Equals(object obj)
        {
            return obj is Drop && ((Drop)obj).Name.Equals(Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Quantity.GetHashCode();
        }
    }
}
