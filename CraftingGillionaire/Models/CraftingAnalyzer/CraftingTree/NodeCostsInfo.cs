using System;

namespace CraftingGillionaire.Models.CraftingAnalyzer
{
    public class NodeCostsInfo
    {
        public int TotalCosts { get; set; }

        public int MinCosts { get; set; }

        public int CraftingCosts { get; set; } = Int32.MaxValue;

        public int MarketboardCosts { get; set; } = Int32.MaxValue;

        public int VendorCosts { get; set; } = Int32.MaxValue;

        public bool IsCraftingCheaper { get; set; } = false;

        public bool IsMarketboardCheaper { get; set; } = false;

        public int MarketboardCheaperAmount { get; set; }

        public bool IsVendorCheaper { get; set; } = false;
    }
}
