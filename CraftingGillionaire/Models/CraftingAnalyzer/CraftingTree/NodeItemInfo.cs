namespace CraftingGillionaire.Models.CraftingAnalyzer
{
    public class NodeItemInfo
    {
        internal NodeItemInfo(int itemID, string itemName, bool isCraftable) 
        { 
            this.ItemID = itemID;
            this.ItemName = itemName;
            this.IsCraftable = isCraftable;
        }

        public int ItemID { get; }

        public string ItemName { get; }

        public bool IsCraftable { get; }
    }
}
