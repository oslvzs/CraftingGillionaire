namespace CraftingGillionaire.Models.CraftingAnalyzer
{
    public class ItemCraftingProfitInfo
    {
        public ItemCraftingProfitInfo(int craftingCosts, int minimalPriceOnMarketboard, int quantitySold)
        {
            CraftingCosts = craftingCosts;
            MinimalPriceOnMarketboard = minimalPriceOnMarketboard;
            IncomePerUnit = (int)(MinimalPriceOnMarketboard * 0.95);
            ProfitPerUnit = IncomePerUnit - CraftingCosts;
            QuantitySold = quantitySold;
            TotalProfit = QuantitySold * ProfitPerUnit;
            IsOutOfStock = this.MinimalPriceOnMarketboard < 1;
        }

        public int CraftingCosts { get; }

        public int MinimalPriceOnMarketboard { get; }

        public int IncomePerUnit { get; }

        public int ProfitPerUnit { get; }

        public int QuantitySold { get; }

        public int TotalProfit { get; }

        public bool IsOutOfStock { get; }
    }
}
