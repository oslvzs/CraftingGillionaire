using System;

namespace CraftingGillionaire.Models.CraftingAnalyzer
{
    public class CraftingPart
    {
        public CraftingPart(CraftingItemInfo itemInfo, CraftingInfo craftingInfo, MarketboardInfo marketboardInfo)
        {
            this.ItemInfo = itemInfo;
            this.CraftingInfo = craftingInfo;
            this.MarketboardInfo = marketboardInfo;
            this.HasException = false;
            this.Exception = String.Empty;
        }

        public CraftingPart(string exception)
        {
            this.HasException = true;
            this.Exception = exception;
        }

        public CraftingItemInfo ItemInfo { get; }

        public CraftingInfo CraftingInfo { get; }

        public MarketboardInfo MarketboardInfo { get; }

        public bool HasException { get; set; }

        public string Exception { get; set; }
    }
}
