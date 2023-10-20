using System;
using System.Collections.Generic;

namespace CraftingGillionaire.Models.CraftingAnalyzer
{
    public class CraftingAnalyzerItem
    {
        public CraftingAnalyzerItem() { }

        public CraftingAnalyzerItem(string serverName, CraftingItemInfo itemInfo, List<CraftingPart> craftingParts, ItemCraftingProfitInfo itemCraftingProfitInfo, CraftingJobInfo craftingJobInfo, bool isItemCraftable)
        {
            this.ServerName = serverName;
            this.ItemInfo = itemInfo;
            this.CraftingParts = craftingParts;
            this.ItemCraftingProfitInfo = itemCraftingProfitInfo;
            this.CraftingJobInfo = craftingJobInfo;
            this.IsItemCraftable = isItemCraftable;
            this.HasException = false;
            this.Exception = String.Empty;
        }

        public CraftingAnalyzerItem(string exception)
        {
            this.HasException = true;
            this.Exception = exception;
        }

        public string ServerName { get; set; }

        public CraftingItemInfo ItemInfo { get; set; }

        public List<CraftingPart> CraftingParts { get; set; }

        public ItemCraftingProfitInfo ItemCraftingProfitInfo { get; set; }

        public CraftingJobInfo CraftingJobInfo { get; set; }

        public bool IsItemCraftable { get; set; }

        public bool HasException { get; set; }

        public string Exception { get; set; }
    }
}
