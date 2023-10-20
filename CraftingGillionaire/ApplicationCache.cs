
using CraftingGillionaire.API.GarlandTools;
using CraftingGillionaire.API.GarlandTools.API;
using CraftingGillionaire.API.Saddlebag.API;
using CraftingGillionaire.API.Universalis;
using CraftingGillionaire.Models;
using CraftingGillionaire.Models.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CraftingGillionaire
{
    internal static class ApplicationCache
    {
        internal static Dictionary<int, string> ItemNameDictionary { get; } = new Dictionary<int, string>();

        internal static async Task<ItemNameInfo> GetItemName(int itemID)
        {
            if (!ItemNameDictionary.TryGetValue(itemID, out string itemName))
            {
                ItemInfoResult itemInfoResult = await GarlandToolsHelper.GetItemResponse(itemID);
                if (itemInfoResult.HasException)
                {
                    return new ItemNameInfo()
                    {
                        HasException = true,
                        Exception = itemInfoResult.Exception
                    };
                }
                else
                {
                    itemName = itemInfoResult.ItemResponse.ItemInfo.Name;
                    ItemNameDictionary.Add(itemID, itemName);
                }
            }

            return new ItemNameInfo()
            {
                HasException = false,
                ItemName = itemName
            };
        } 
    }
}
