using CraftingGillionaire.API.Universalis.API;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace CraftingGillionaire.API.Universalis
{
    internal static class UniversalisHelper
    {
        internal static async Task<int> GetItemMinPrice(int itemID, string entityName)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responseObject = await httpClient.GetAsync($"https://universalis.app/api/v2/{entityName}/{itemID}?fields=minPrice");
            responseObject.EnsureSuccessStatusCode();
            string responseBody = await responseObject.Content.ReadAsStringAsync();
            MinPriceResult response = JsonSerializer.Deserialize<MinPriceResult>(responseBody) ?? new MinPriceResult();
            return response.MinPrice;
        }

        internal static async Task<List<MarketListing>> GetItemListings(int itemID, string entityName)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responseObject = await httpClient.GetAsync($"https://universalis.app/api/v2/{entityName}/{itemID}?fields=listings.pricePerUnit%2Clistings.quantity");
            responseObject.EnsureSuccessStatusCode();
            string responseBody = await responseObject.Content.ReadAsStringAsync();
            ListingsResult response = JsonSerializer.Deserialize<ListingsResult>(responseBody) ?? new ListingsResult();
            return response.Listings;
        }
    }
}
