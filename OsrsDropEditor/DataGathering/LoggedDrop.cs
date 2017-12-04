using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsrsDropEditor.DataGathering
{
    public class LoggedDrop
    {
        public string Name { get; set; }
        public string DisplayName { get { return ToString(); } }
        public int Quantity { get; set; }
        public int TotalPrice { get { return OsrsDataContainers.GetPriceForDrops(this); } }

        /// <summary>
        /// Create a LoggedDrop object out of a Drop object.
        /// </summary>
        /// <param name="drop"></param>
        public LoggedDrop(Drop drop)
        {
            Name = drop.Name;
            Quantity = drop.Quantity;
        }

        public override string ToString()
        {
            return $"{Name}: {Quantity}";
        }

        public override bool Equals(object obj)
        {
            return obj is LoggedDrop && ((LoggedDrop)obj).Name.Equals(Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Quantity.GetHashCode();
        }
    }
}
