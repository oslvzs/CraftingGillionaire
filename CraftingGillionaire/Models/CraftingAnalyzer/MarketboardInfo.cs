namespace CraftingGillionaire.Models.CraftingAnalyzer
{
    public class MarketboardInfo
    {
        public MarketboardInfo(bool isCraftingCostsLess, bool isVendorCostsLess, int marketboardCheaperPrice, int marketboardCheaperAmount, int vendorPrice)
        {
            IsMarketboardCostsLess = isCraftingCostsLess;
            IsVendorCostsLess = isVendorCostsLess;
            MarketboardCheaperPrice = marketboardCheaperPrice;
            MarketboardCheaperAmount = marketboardCheaperAmount;
            VendorPrice = vendorPrice;
        }

        public bool IsMarketboardCostsLess { get; }

        public bool IsVendorCostsLess { get; }

        public int MarketboardCheaperPrice { get; set; }

        public int MarketboardCheaperAmount { get; set; }

        public int VendorPrice { get; set; }
    }
}
