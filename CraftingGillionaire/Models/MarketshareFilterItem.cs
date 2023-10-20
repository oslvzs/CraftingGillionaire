using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingGillionaire.Models
{
    public class MarketshareFilterItem
    {
        public MarketshareFilterItem(string filterName, int filterID) 
        { 
            this.Name = filterName;
            this.ID = filterID;
        }

        public string Name { get; set; }

        public int ID { get; set; }
    }
}
