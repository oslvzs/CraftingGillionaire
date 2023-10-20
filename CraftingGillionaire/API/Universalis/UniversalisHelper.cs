using CraftingGillionaire.API.Universalis.API;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace CraftingGillionaire.API.Universalis
{
    internal static class UniversalisHelper
    {
        internal static async Task<MarketMinPriceResult> GetItemMinPrice(int itemID, string entityName)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage responseObject = await httpClient.GetAsync($"https://universalis.app/api/v2/{entityName}/{itemID}?fields=minPrice");
                responseObject.EnsureSuccessStatusCode();
                string responseBody = await responseObject.Content.ReadAsStringAsync();
                MinPriceResult response = JsonSerializer.Deserialize<MinPriceResult>(responseBody) ?? new MinPriceResult();
                return new MarketMinPriceResult(response.MinPrice);
            }
            catch(HttpRequestException ex)
            {
                return new MarketMinPriceResult("Could not get response from Universalis. Try again later!");
            }
        }

        internal static async Task<MarketMultipleMinPricesResult> GetItemsMinPriceDictionary(HashSet<int> itemIDs, string entityName)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage responseObject = await httpClient.GetAsync($"https://universalis.app/api/v2/{entityName}/{String.Join(",", itemIDs)}?fields=items.minPrice");
                responseObject.EnsureSuccessStatusCode();
                string responseBody = await responseObject.Content.ReadAsStringAsync();
                MultipleMinPricesResult response = JsonSerializer.Deserialize<MultipleMinPricesResult>(responseBody) ?? new MultipleMinPricesResult();
                
                Dictionary<int, int> minPricesDictionary = new Dictionary<int, int>();
                if (response.MinPriceDictionary != null)
                {
                    foreach (string itemIDString in response.MinPriceDictionary.Keys)
                    {
                        int itemID = Int32.Parse(itemIDString);
                        int minPrice = response.MinPriceDictionary[itemIDString].MinPrice;
                        minPricesDictionary.Add(itemID, minPrice);
                    }
                }
                return new MarketMultipleMinPricesResult(minPricesDictionary);
            }
            catch (HttpRequestException ex)
            {
                return new MarketMultipleMinPricesResult("Could not get response from Universalis. Try again later!");
            }
        }

        internal static async Task<MarketListingsResult> GetItemListings(int itemID, string entityName)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage responseObject = await httpClient.GetAsync($"https://universalis.app/api/v2/{entityName}/{itemID}?fields=listings.pricePerUnit%2Clistings.quantity");
                responseObject.EnsureSuccessStatusCode();
                string responseBody = await responseObject.Content.ReadAsStringAsync();
                ListingsResult response = JsonSerializer.Deserialize<ListingsResult>(responseBody) ?? new ListingsResult();
                return new MarketListingsResult(response.Listings);
            }
            catch (HttpRequestException ex)
            {
                return new MarketListingsResult("Could not get response from Universalis. Try again later!");
            }
        }
    }
}
