
using CraftingGillionaire.API.GarlandTools;
using CraftingGillionaire.API.GarlandTools.API;
using CraftingGillionaire.API.Saddlebag.API;
using CraftingGillionaire.API.Universalis;
using CraftingGillionaire.Models.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CraftingGillionaire
{
    internal static class ApplicationCache
    {
        internal static Dictionary<int, string> ItemNameDictionary { get; } = new Dictionary<int, string>();

        internal static Dictionary<string, Dictionary<int, int>> DatacenterItemMinPriceDictionary = new Dictionary<string, Dictionary<int, int>>();

        internal static void FillItemNameDictionary(MarketshareResponse response)
        {
            if (response.Data != null)
            {
                foreach (MarketshareResonseItem item in response.Data)
                {
                    string itemIDString = item.ItemID;
                    int itemID = Convert.ToInt32(item.ItemID);

                    if (!ItemNameDictionary.ContainsKey(itemID))
                    {
                        ItemNameDictionary.Add(itemID, item.Name);
                    }
                }
            }
        }

        internal static async Task<string> GetItemName(int itemID)
        {
            if (!ItemNameDictionary.TryGetValue(itemID, out string itemName))
            {
                ItemResponse response = await GarlandToolsHelper.GetItemResponse(itemID);
                itemName = response.ItemInfo.Name;
                ItemNameDictionary.Add(itemID, itemName);
            }

            return itemName;
        }

        internal static async Task<int> GetItemMinPriceByDatacenter(int itemID, string datacenterName)
        {
            if (!DatacenterItemMinPriceDictionary.TryGetValue(datacenterName, out Dictionary<int, int> itemMinPriceDictionary))
            {
                int itemMinPrice = await UniversalisHelper.GetItemMinPrice(itemID, datacenterName);
                itemMinPriceDictionary = new Dictionary<int, int>();
                itemMinPriceDictionary.Add(itemID, itemMinPrice);
                DatacenterItemMinPriceDictionary.Add(datacenterName, itemMinPriceDictionary);

                return itemMinPrice;
            }
            else
            {
                if (!itemMinPriceDictionary.TryGetValue(itemID, out int itemMinPrice))
                {
                    itemMinPrice = await UniversalisHelper.GetItemMinPrice(itemID, datacenterName);
                    itemMinPriceDictionary.Add(itemID, itemMinPrice);
                }

                return itemMinPrice;
            }
        }      
    }
}
