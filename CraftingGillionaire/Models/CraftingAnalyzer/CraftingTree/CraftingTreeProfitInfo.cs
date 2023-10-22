namespace CraftingGillionaire.Models.CraftingAnalyzer
{
    public class CraftingTreeProfitInfo
    {
        internal CraftingTreeProfitInfo(int craftingCosts, int minimalPriceOnMarketboard, int quantitySold)
        {
            this.CraftingCosts = craftingCosts;
            this.MinimalPriceOnMarketboard = minimalPriceOnMarketboard;
            this.IncomePerUnit = (int)(this.MinimalPriceOnMarketboard * 0.95);
            this.ProfitPerUnit = this.IncomePerUnit - this.CraftingCosts;
            this.QuantitySold = quantitySold;
            this.TotalProfit = this.QuantitySold * this.ProfitPerUnit;
            this.IsOutOfStock = this.MinimalPriceOnMarketboard < 1;
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
