using CraftingGillionaire.API.GarlandTools.API;
using ReactiveUI;
using System.Collections.Generic;

namespace CraftingGillionaire.Models.CraftingAnalyzer
{
    public class CraftingTreeRootNode
    {
        internal CraftingTreeRootNode() { }

        internal CraftingTreeRootNode(NodeItemInfo itemInfo) 
        { 
            this.ItemInfo = itemInfo;
        }

        internal CraftingTreeRootNode(string exception) 
        { 
            this.Exception = exception;
            this.HasException = true;
        }

        public string ServerName { get; set; }

        public NodeItemInfo ItemInfo { get; }

        public NodeJobInfo JobInfo { get; set; }

        public List<CraftingTreeNode> ChildrenNodes { get; set; }

        public CraftingTreeProfitInfo ProfitInfo { get; set; }

        public bool HasException { get; set; }

        public string Exception { get; set; }

        internal List<IngredientInfo> Ingredients { get; set; }

        public bool ShowLowLevelText
        {
            get
            {
                return this.JobInfo != null && !this.JobInfo.UserCanCraft;
            }
        }
    }
}
