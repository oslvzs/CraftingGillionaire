using CraftingGillionaire.API.Universalis.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingGillionaire.Models
{
    public class SaleDisplayItem
    {
        public SaleDisplayItem(SaleEntry saleEntry)
        {
            this.IsHQ = saleEntry.IsHQ;
            this.PricePerUnit = saleEntry.PricePerUnit;
            this.Quantity = saleEntry.Quantity;
            this.BuyerName = saleEntry.BuyerName;
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(saleEntry.Timestamp);
            this.Date = dateTimeOffset.LocalDateTime;
        }

        public bool IsHQ { get; }

        public int PricePerUnit { get; }

        public int Quantity { get; }

        public string BuyerName { get; }

        public DateTime Date { get; }
    }
}
