namespace CraftingGillionaire.Models.CraftingAnalyzer
{
    public class CraftingPart
    {
        public CraftingPart(CraftingItemInfo itemInfo, CraftingInfo craftingInfo, MarketboardInfo marketboardInfo)
        {
            ItemInfo = itemInfo;
            CraftingInfo = craftingInfo;
            MarketboardInfo = marketboardInfo;
        }

        public CraftingItemInfo ItemInfo { get; }

        public CraftingInfo CraftingInfo { get; }

        public MarketboardInfo MarketboardInfo { get; }
    }
}
