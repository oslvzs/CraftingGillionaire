﻿using ReactiveUI;
using System;
using System.Collections.Generic;

namespace CraftingGillionaire.Models.CraftingAnalyzer
{
    public class CraftingTreeNode
    {
        internal CraftingTreeNode(NodeItemInfo nodeItemInfo, int amount) 
        { 
            this.ItemInfo = nodeItemInfo;
            this.Amount = amount;
        }

        internal CraftingTreeNode(string exception)
        {
            this.HasException = true;
            this.Exception = exception;
        }

        public NodeItemInfo ItemInfo { get; }

        public NodeJobInfo JobInfo { get; set; }

        public List<CraftingTreeNode> ChildrenNodes { get; set; }

        public NodeCostsInfo CostsInfo { get; set; }

        public int Amount { get; set; }

        public bool HasException { get; set; }

        public string Exception { get; set; }

        public bool ShowLowLevelText
        {
            get
            {
                return this.JobInfo != null && !this.JobInfo.UserCanCraft;
            }
        }

        public bool ShowCheaperMarketpoard
        {
            get
            {
                return this.CostsInfo.IsMarketboardCheaper && this.CostsInfo.CraftingCosts != Int32.MaxValue && this.CostsInfo.CraftingCosts > this.CostsInfo.MarketboardCosts;
            }
        }
    }
}
