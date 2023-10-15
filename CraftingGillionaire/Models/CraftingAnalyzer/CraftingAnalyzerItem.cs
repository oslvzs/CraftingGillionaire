using DynamicData;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CraftingGillionaire.Models.CraftingAnalyzer
{
    public class CraftingAnalyzerItem
    {
        public CraftingAnalyzerItem(string serverName, CraftingItemInfo itemInfo, List<CraftingPart> craftingParts, ItemCraftingProfitInfo itemCraftingProfitInfo, CraftingJobInfo craftingJobInfo, bool isItemCraftable)
        {
            ServerName = serverName;
            ItemInfo = itemInfo;
            CraftingParts = craftingParts;
            ItemCraftingProfitInfo = itemCraftingProfitInfo;
            CraftingJobInfo = craftingJobInfo;
            IsItemCraftable = isItemCraftable;
        }

        public string ServerName { get; set; }

        public CraftingItemInfo ItemInfo { get; set; }

        public List<CraftingPart> CraftingParts { get; set; }

        public ItemCraftingProfitInfo ItemCraftingProfitInfo { get; set; }

        public CraftingJobInfo CraftingJobInfo { get; set; }

        public bool IsItemCraftable { get; set; }
    }
}
