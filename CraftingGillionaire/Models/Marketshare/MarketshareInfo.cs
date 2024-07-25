using CraftingGillionaire.Models.CraftingAnalyzer;

namespace CraftingGillionaire.Models
{
	public class MarketshareInfo
	{
		public int ItemID { get; set; }

		public int AveragePrice { get; set; }

		public int MarketValue { get; set; }

		public int Revenue { get; set; }

		public string ItemName { get; set; }

		public double PercentChange { get; set; }

		public int QuantitySold { get; set; }

		public int SalesAmount { get; set; }

		public string State { get; set; }

		public string URL { get; set; }

		public CraftingTreeRootNode TreeRootNode { get; set; }

        public void OnLinkClick(string url)
        {
            CommonHelper.OpenLink(url);
        }
    }
}
