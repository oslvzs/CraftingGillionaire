namespace CraftingGillionaire.Models.CraftingAnalyzer
{
    public class CraftingItemInfo
    {
        public CraftingItemInfo(int itemID, string itemName, bool canCraftItem)
        {
            ItemID = itemID;
            ItemName = itemName;
            CanCraftItem = canCraftItem;
        }

        public int ItemID { get; }

        public string ItemName { get; }

        public bool CanCraftItem { get; }
    }
}
