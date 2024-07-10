using CraftingGillionaire.API.GarlandTools.API;
using CraftingGillionaire.API.Universalis.API;
using CraftingGillionaire.API.Universalis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CraftingGillionaire.Models.User;
using System.Threading;

namespace CraftingGillionaire.Models.CraftingAnalyzer
{
    internal class CraftingAnalyzerItemBuilder
    {
        internal CraftingAnalyzerItemBuilder(UserInfo userInfo, string serverName)
        {
            this.UserInfo = userInfo;
            this.ServerName = serverName;
            this.DataCenterName = CommonHelper.GetDatacenterByServerName(serverName);
        }

        private UserInfo UserInfo { get; }

        private string ServerName { get; }

        private string DataCenterName { get; }

        internal async Task<CraftingTreeRootNode> BuildCraftingTree(ItemResponse response)
        {
            int itemID = response.ItemInfo.ID;
            string itemName = response.ItemInfo.Name;
            bool isCraftable = response.ItemInfo.CraftsList != null && response.ItemInfo.CraftsList.Count > 0;
            NodeItemInfo itemInfo = new NodeItemInfo(itemID, itemName, isCraftable);
            CraftingTreeRootNode rootNode = new CraftingTreeRootNode(itemInfo);
            rootNode.ServerName = this.ServerName;
            if (isCraftable)
            {
                CraftInfo craftInfo = response.ItemInfo.CraftsList.First();
                rootNode.JobInfo = this.GetJobInfo(craftInfo);

                List<CraftingTreeNode> childrenNodes = new List<CraftingTreeNode>();
                foreach (RecipePart recipePart in craftInfo.RecipePartsList)
                {
                    CraftingTreeNode childNode = await this.BuildChildNode(response.IngredientsList, recipePart);
                    childrenNodes.Add(childNode);
                    if (childNode.HasException)
                    {
                        rootNode.HasException = true;
                        rootNode.Exception = childNode.Exception;
                        return rootNode;
                    }
                }
                rootNode.ChildrenNodes = childrenNodes;

            }

            rootNode.Ingredients = response.IngredientsList;
            return rootNode;
        }

        internal async Task<CraftingTreeNode> BuildChildNode(List<IngredientInfo> allIngredientsList, RecipePart recipePart)
        {
            int nodeItemID = recipePart.ID;
            int amount = recipePart.Amount;
            string nodeItemName;
            IngredientInfo? ingredientInfo = allIngredientsList.FirstOrDefault(x => x.ID == nodeItemID);
            if (ingredientInfo != null)
            {
                bool isCraftable = ingredientInfo.CraftsList != null && ingredientInfo.CraftsList.Count > 0;
                nodeItemName = ingredientInfo.Name;
                NodeItemInfo nodeItemInfo = new NodeItemInfo(nodeItemID, nodeItemName, isCraftable);
                CraftingTreeNode treeNode = new CraftingTreeNode(nodeItemInfo, amount);

                if (isCraftable)
                {                                   
                    CraftInfo craftInfo = ingredientInfo.CraftsList.First();
                    treeNode.JobInfo = this.GetJobInfo(craftInfo);
                    List<CraftingTreeNode> childrenNodes = new List<CraftingTreeNode>();
                    foreach (RecipePart innerRecipePart in craftInfo.RecipePartsList)
                    {
                        CraftingTreeNode childNode = await this.BuildChildNode(allIngredientsList, innerRecipePart);
                        childrenNodes.Add(childNode);
                        if (childNode.HasException)
                        {
                            treeNode.HasException = true;
                            treeNode.Exception = childNode.Exception;
                            return treeNode;
                        }
                    }
                    treeNode.ChildrenNodes = childrenNodes;
                }

                return treeNode;
            }
            else
            {
                ItemNameInfo itemNameInfo = await ApplicationCache.GetItemName(nodeItemID);
                if (itemNameInfo.HasException)
                {
                    return new CraftingTreeNode(itemNameInfo.Exception);
                }
                else
                {
                    nodeItemName = itemNameInfo.ItemName;
                    NodeItemInfo nodeItemInfo = new NodeItemInfo(nodeItemID, nodeItemName, false);
                    return new CraftingTreeNode(nodeItemInfo, amount);
                }
            }
        }

        internal async Task FillTreeCosts(CraftingTreeRootNode rootNode, int quantitySold)
        {
            rootNode.HasException = false;
            rootNode.Exception = String.Empty;

            HashSet<int> itemIDs = new HashSet<int>
            {
                rootNode.ItemInfo.ItemID
            };
            if (rootNode.ItemInfo.IsCraftable)
            {
                foreach(CraftingTreeNode innerNode in rootNode.ChildrenNodes)
                {
                    itemIDs.Add(innerNode.ItemInfo.ItemID);
                    this.FillHashset(innerNode, itemIDs);
                }
            }

            int multipleMinPricesExceptionCounter = 0;
            MarketMultipleMinPricesResult? multipleMinPricesResult = null;
            while (true)
            {
                multipleMinPricesResult = await UniversalisHelper.GetItemsMinPriceDictionary(itemIDs, this.DataCenterName);
                if (multipleMinPricesResult.HasException)
                {
                    if (multipleMinPricesExceptionCounter > 5)
                    {
                        rootNode.HasException = true;
                        rootNode.Exception = multipleMinPricesResult.Exception;
                        return;
                    }
                    multipleMinPricesExceptionCounter++;
                    Thread.Sleep(2000);
                }
                else
                    break;
            }

            Dictionary<int, int> minPricesDictionary = multipleMinPricesResult.MinPricesDictionary;
            int itemMarketboardMinPriceExceptionCounter = 0;
            MarketMinPriceResult? itemMarketboardMinPriceResult = null;
            while (true)
            {
                itemMarketboardMinPriceResult = await UniversalisHelper.GetItemMinPrice(rootNode.ItemInfo.ItemID, this.ServerName);
                if (itemMarketboardMinPriceResult.HasException)
                {
                    if (itemMarketboardMinPriceExceptionCounter > 5)
                    {
                        rootNode.HasException = true;
                        rootNode.Exception = itemMarketboardMinPriceResult.Exception;
                        return;
                    }
                    itemMarketboardMinPriceExceptionCounter++;
                    Thread.Sleep(2000);
                }
                else
                    break;
            }

            int marketListingsExceptionCounter = 0;
            MarketListingsResult? marketListingsResult = null;
            while (true)
            {
                marketListingsResult = await UniversalisHelper.GetItemsListings(itemIDs, this.DataCenterName);
                if (marketListingsResult.HasException)
                {
                    if (marketListingsExceptionCounter > 5)
                    {
                        rootNode.HasException = true;
                        rootNode.Exception = marketListingsResult.Exception;
                        return;
                    }
                    marketListingsExceptionCounter++;
                    Thread.Sleep(2000);
                }
                else
                    break;
            }

            int recipeCosts;
            if (rootNode.ItemInfo.IsCraftable)
            {
                foreach (CraftingTreeNode treeNode in rootNode.ChildrenNodes)
                {
                    await this.FillNodeCosts(treeNode, rootNode.Ingredients, minPricesDictionary, marketListingsResult.MarketListingsDictionary);
                }

                recipeCosts = rootNode.ChildrenNodes.Sum(x => x.CostsInfo.TotalCosts);
            }
            else
            {
                recipeCosts = itemMarketboardMinPriceResult.MinPrice;
            }

            rootNode.ProfitInfo = new CraftingTreeProfitInfo(recipeCosts, itemMarketboardMinPriceResult.MinPrice, quantitySold);
        }

        private async Task FillNodeCosts(CraftingTreeNode node, List<IngredientInfo> allIngredients, Dictionary<int, int> minPricesDictionary, Dictionary<int, List<MarketListing>> marketListingsDictionary)
        {
            node.CostsInfo = new NodeCostsInfo();
            if (node.ItemInfo.IsCraftable)
            {
                foreach (CraftingTreeNode childNode in node.ChildrenNodes)
                {
                    await this.FillNodeCosts(childNode, allIngredients, minPricesDictionary, marketListingsDictionary);
                    if (childNode.HasException)
                    {
                        node.HasException = true;
                        node.Exception = childNode.Exception;
                        return;
                    }
                }


                node.CostsInfo.CraftingCosts = node.ChildrenNodes.Sum(x => x.CostsInfo.TotalCosts);
            }
            
            node.CostsInfo.MarketboardCosts = minPricesDictionary[node.ItemInfo.ItemID];
            IngredientInfo? ingredientInfo = allIngredients.FirstOrDefault(x => x.ID == node.ItemInfo.ItemID);
            if (ingredientInfo != null && ingredientInfo.Vendors != null && ingredientInfo.Vendors.Count > 0 && ingredientInfo.Price > 0)
            {
                node.CostsInfo.VendorCosts = ingredientInfo.Price;
            }

            this.FindNodeMinPrice(node);

            if (node.CostsInfo.IsMarketboardCheaper)
            {
                List<MarketListing> cheaperListings = marketListingsDictionary[node.ItemInfo.ItemID].Where(x => x.PricePerUnit <= node.CostsInfo.MinCosts).ToList();
                if (cheaperListings.Count > 0)
                    node.CostsInfo.MarketboardCheaperAmount = cheaperListings.Sum(x => x.Quantity);
            }

            node.CostsInfo.TotalCosts = node.Amount * node.CostsInfo.MinCosts;
        }

        private void FindNodeMinPrice(CraftingTreeNode node)
        {
            if (node.CostsInfo.CraftingCosts != Int32.MaxValue && node.CostsInfo.CraftingCosts < node.CostsInfo.MarketboardCosts && node.CostsInfo.CraftingCosts < node.CostsInfo.VendorCosts)
                node.CostsInfo.IsCraftingCheaper = true;
            if (node.CostsInfo.MarketboardCosts != Int32.MaxValue && node.CostsInfo.MarketboardCosts < node.CostsInfo.CraftingCosts && node.CostsInfo.MarketboardCosts < node.CostsInfo.VendorCosts)
                node.CostsInfo.IsMarketboardCheaper = true;
            if (node.CostsInfo.VendorCosts != Int32.MaxValue && node.CostsInfo.VendorCosts < node.CostsInfo.CraftingCosts && node.CostsInfo.VendorCosts < node.CostsInfo.MarketboardCosts)
                node.CostsInfo.IsVendorCheaper = true;

            if (node.CostsInfo.IsCraftingCheaper)
                node.CostsInfo.MinCosts = node.CostsInfo.CraftingCosts;
            if (node.CostsInfo.IsMarketboardCheaper)
                node.CostsInfo.MinCosts = node.CostsInfo.MarketboardCosts;
            if (node.CostsInfo.IsVendorCheaper)
                node.CostsInfo.MinCosts = node.CostsInfo.VendorCosts;

            if (!node.CostsInfo.IsCraftingCheaper && !node.CostsInfo.IsMarketboardCheaper && !node.CostsInfo.IsVendorCheaper)
            {
                if (node.CostsInfo.VendorCosts < Int32.MaxValue)
                    node.CostsInfo.MinCosts = node.CostsInfo.VendorCosts;
                else if (node.CostsInfo.MarketboardCosts < Int32.MaxValue)
                    node.CostsInfo.MinCosts = node.CostsInfo.MarketboardCosts;
                else if (node.CostsInfo.CraftingCosts < Int32.MaxValue)
                    node.CostsInfo.MinCosts = node.CostsInfo.CraftingCosts;
            }
        }

        private NodeJobInfo GetJobInfo(CraftInfo craftInfo)
        {
            string jobName = "Other";
            int jobLevel = craftInfo.JobLevel;
            int jobID = craftInfo.JobID;
            if (jobID > 0)
                jobName = CommonHelper.GetJobNameByID(jobID);
            bool userCanCraft = this.CanCraftItem(jobID, jobLevel);

            return new NodeJobInfo(jobName, jobLevel, userCanCraft);
        }

        private bool CanCraftItem(int jobID, int minJobLevel)
        {
            if (jobID == 0)
                return true;

            int currentUserJobLevel = this.UserInfo.GetLevelByJobID(jobID);
            return currentUserJobLevel >= minJobLevel;
        }

        private void FillHashset(CraftingTreeNode node, HashSet<int> itemIDs)
        {
            if (node.ItemInfo.IsCraftable)
            {
                foreach (CraftingTreeNode treeNode in node.ChildrenNodes)
                {
                    itemIDs.Add(treeNode.ItemInfo.ItemID);
                    this.FillHashset(treeNode, itemIDs);
                }
            }
        }
    }
}
