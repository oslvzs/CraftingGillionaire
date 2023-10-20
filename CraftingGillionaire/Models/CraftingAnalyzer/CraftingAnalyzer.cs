using CraftingGillionaire.API.GarlandTools.API;
using CraftingGillionaire.API.Universalis.API;
using CraftingGillionaire.API.Universalis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CraftingGillionaire.Models.User;

namespace CraftingGillionaire.Models.CraftingAnalyzer
{
    internal class CraftingAnalyzerItemBuilder
    {
        internal CraftingAnalyzerItemBuilder(UserInfo userInfo)
        {
            this.UserInfo = userInfo;
        }

        private UserInfo UserInfo { get; }

        internal async Task<CraftingAnalyzerItem> PrepareInfo(ItemResponse response, string serverName, int quantitySold)
        {        
            int itemID = response.ItemInfo.ID;
            string itemName = response.ItemInfo.Name;

            HashSet<int> itemIDs = new HashSet<int>();

            if (response.ItemInfo.CraftsList != null && response.ItemInfo.CraftsList.Count > 0)
            {
                CraftInfo craftInfo = response.ItemInfo.CraftsList.First();
                foreach (RecipePart recipePart in craftInfo.RecipePartsList)
                {
                    itemIDs.Add(recipePart.ID);
                }
            }
            if (response.IngredientsList != null)
            {
                foreach (IngredientInfo ingredientInfo in response.IngredientsList)
                {
                    int ingredientID = ingredientInfo.ID;
                    itemIDs.Add(ingredientID);

                    if (ingredientInfo.CraftsList != null && ingredientInfo.CraftsList.Count > 0)
                    {
                        CraftInfo ingredientCraftInfo = ingredientInfo.CraftsList[0];
                        if (ingredientCraftInfo.RecipePartsList != null)
                        {
                            foreach (RecipePart ingredientRecipePart in ingredientCraftInfo.RecipePartsList)
                            {
                                itemIDs.Add(ingredientRecipePart.ID);
                            }
                        }
                    }
                }
            }
            itemIDs.Add(itemID);

            string datacenterName = CommonInfoHelper.GetDatacenterByServerName(serverName);
            MarketMultipleMinPricesResult multipleMinPricesResult = await UniversalisHelper.GetItemsMinPriceDictionary(itemIDs, datacenterName);
            if (multipleMinPricesResult.HasException)
                return new CraftingAnalyzerItem(multipleMinPricesResult.Exception);

            Dictionary<int, int> minPricesDictionary = multipleMinPricesResult.MinPricesDictionary;

            CraftingItemInfo craftingItemInfo = null;
            ItemCraftingProfitInfo profitInfo = null;
            CraftingJobInfo craftingJobInfo = null;
            bool isItemCraftable = true;
            List<CraftingPart> craftingParts = new List<CraftingPart>();
            if (response.ItemInfo.CraftsList != null && response.ItemInfo.CraftsList.Count > 0)
            {
                CraftInfo craftInfo = response.ItemInfo.CraftsList.First();
                string jobName = String.Empty;
                int jobLevel = craftInfo.JobLevel;
                int jobID = craftInfo.JobID;
                if (jobID > 0)
                    jobName = CommonInfoHelper.GetJobNameByID(jobID);

                bool canCraftItem = this.CanCraftItem(jobID, jobLevel);


                foreach (RecipePart recipePart in craftInfo.RecipePartsList)
                {
                    CraftingPart craftingPart = await this.GetCraftingPart(serverName, response.IngredientsList, recipePart, minPricesDictionary);
                    if (craftingPart.HasException)
                        return new CraftingAnalyzerItem(craftingPart.Exception);
                    craftingParts.Add(craftingPart);
                }

                int recipeCosts;
                if (craftingParts.Count > 0)
                {
                    recipeCosts = craftingParts.Sum(x => x.CraftingInfo.CraftingTotalCosts);
                }
                else
                {
                    recipeCosts = minPricesDictionary[itemID];
                }
                MarketMinPriceResult itemMarketboardMinPriceResult = await UniversalisHelper.GetItemMinPrice(itemID, serverName);

                craftingItemInfo = new CraftingItemInfo(itemID, itemName, canCraftItem);
                profitInfo = new ItemCraftingProfitInfo(recipeCosts, itemMarketboardMinPriceResult.MinPrice, quantitySold);
                craftingJobInfo = new CraftingJobInfo(jobName, jobLevel);
            }
            else
            {
                craftingItemInfo = new CraftingItemInfo(itemID, itemName, true);
                isItemCraftable = false;
            }

            CraftingAnalyzerItem itemCraftingInfo = new CraftingAnalyzerItem(serverName, craftingItemInfo, craftingParts, profitInfo, craftingJobInfo, isItemCraftable);
            return itemCraftingInfo;
        }


        private async Task<CraftingPart> GetCraftingPart(string serverName, List<IngredientInfo> allIngredientsInfo, RecipePart recipePart, Dictionary<int, int> minPricesDictionary)
        {
            int recipePartItemID = recipePart.ID;
            int recipePartAmount = recipePart.Amount;
            string recipePartItemName;
            List<CraftingPart> recipePartCrafringParts = new List<CraftingPart>();
            IngredientInfo ingredientInfo = allIngredientsInfo.FirstOrDefault(x => x.ID == recipePartItemID);
            int jobID = 0;
            int jobLevel = 0;
            string jobName = String.Empty;
            CraftingJobInfo craftingJobInfo = null;
            bool canCraftItem = true;
            int vendorPrice = 0;
            if (ingredientInfo != null)
            {
                recipePartItemName = ingredientInfo.Name;
                if (ingredientInfo.CraftsList != null && ingredientInfo.CraftsList.Count > 0)
                {
                    CraftInfo craftInfo = ingredientInfo.CraftsList[0];

                    jobID = craftInfo.JobID;
                    if (jobID > 0)
                        jobName = CommonInfoHelper.GetJobNameByID(jobID);
                    jobLevel = craftInfo.JobLevel;
                    craftingJobInfo = new CraftingJobInfo(jobName, jobLevel);
                    canCraftItem = this.CanCraftItem(jobID, jobLevel);
                    foreach (RecipePart innerRecipePart in craftInfo.RecipePartsList)
                    {
                        CraftingPart innerCraftingPart = await this.GetCraftingPart(serverName, allIngredientsInfo, innerRecipePart, minPricesDictionary);
                        if (innerCraftingPart.HasException)
                            return new CraftingPart(innerCraftingPart.Exception);
                        recipePartCrafringParts.Add(innerCraftingPart);
                    }
                }

                if (ingredientInfo.Vendors != null && ingredientInfo.Vendors.Count > 0 && ingredientInfo.Price > 0)
                {
                    vendorPrice = ingredientInfo.Price;
                }
            }
            else
            {
                ItemNameInfo itemNameInfo = await ApplicationCache.GetItemName(recipePartItemID);
                if (itemNameInfo.HasException)
                {
                    return new CraftingPart(itemNameInfo.Exception);
                }
                else 
                {
                    recipePartItemName = itemNameInfo.ItemName;
                }
            }

            int recipePartCostPerUnit;
            int marketboardCheaperPrice = 0;
            int marketboardCheaperAmount = 0;
            string datacenterName = CommonInfoHelper.GetDatacenterByServerName(serverName);
            if (recipePartCrafringParts.Count > 0)
            {
                recipePartCostPerUnit = recipePartCrafringParts.Sum(x => x.CraftingInfo.CraftingTotalCosts);
                MarketListingsResult marketListingsResult = await UniversalisHelper.GetItemListings(recipePartItemID, datacenterName);
                List<MarketListing> cheaperListings = marketListingsResult.MarketListings.Where(x => x.PricePerUnit <= recipePartCostPerUnit).ToList();
                if (cheaperListings.Count > 0)
                {
                    marketboardCheaperPrice = cheaperListings.Last().PricePerUnit;
                    marketboardCheaperAmount = cheaperListings.Sum(x => x.Quantity);
                }
            }
            else
            {
                recipePartCostPerUnit = minPricesDictionary[recipePartItemID];
            }

            bool isMarketBoardCostsLess = false;
            bool isVendorCostsLess = false;

            if (marketboardCheaperPrice > 0)
            {
                if (vendorPrice < marketboardCheaperPrice)
                {
                    if (vendorPrice > 0 && vendorPrice <= recipePartCostPerUnit)
                    {
                        recipePartCostPerUnit = vendorPrice;
                        isVendorCostsLess = true;
                    }
                }
                else
                {
                    if (marketboardCheaperPrice <= recipePartCostPerUnit)
                    {
                        recipePartCostPerUnit = marketboardCheaperPrice;
                        isMarketBoardCostsLess = true;
                    }
                }
            }
            else
            {
                if (vendorPrice > 0 && vendorPrice <= recipePartCostPerUnit)
                {
                    recipePartCostPerUnit = vendorPrice;
                    isVendorCostsLess = true;
                }
            }

            CraftingItemInfo craftingItemInfo = new CraftingItemInfo(recipePartItemID, recipePartItemName, canCraftItem);
            CraftingInfo craftingInfo = new CraftingInfo(recipePartAmount, recipePartCostPerUnit, recipePartCrafringParts, craftingJobInfo);
            MarketboardInfo marketboardInfo = new MarketboardInfo(isMarketBoardCostsLess, isVendorCostsLess, marketboardCheaperPrice, marketboardCheaperAmount, vendorPrice);
            return new CraftingPart(craftingItemInfo, craftingInfo, marketboardInfo);
        }

        private bool CanCraftItem(int jobID, int minJobLevel)
        {
            if (jobID == 0)
                return false;

            int currentUserJobLevel = this.UserInfo.GetLevelByJobID(jobID);
            return currentUserJobLevel >= minJobLevel;
        }
    }
}
