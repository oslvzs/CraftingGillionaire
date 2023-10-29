using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingGillionaire.Models
{
    public class SalesHistoryStats
    {
        public SalesHistoryStats(ObservableCollection<SaleDisplayItem> salesHistory)
        {
            if (salesHistory.Count > 0)
            {
                this.TotalSells = salesHistory.Count;
                this.TotalUnitsSold = salesHistory.Sum(x => x.Quantity);
                this.AveragePrice = (int)salesHistory.Average(x => x.PricePerUnit);
            }
        }

        public int TotalSells { get; }

        public int TotalUnitsSold { get; }

        public int AveragePrice { get; }
    }
}
