using CraftingGillionaire.API.GarlandTools.API;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace CraftingGillionaire.API.GarlandTools
{
    internal static class GarlandToolsHelper
    {
        internal static async Task<ItemResponse> GetItemResponse(int itemID)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responseObject = await httpClient.GetAsync($"https://www.garlandtools.org/db/doc/item/en/3/{itemID}.json");
            responseObject.EnsureSuccessStatusCode();
            string responseBody = await responseObject.Content.ReadAsStringAsync();
            ItemResponse response = JsonSerializer.Deserialize<ItemResponse>(responseBody) ?? new ItemResponse();
            return response;
        }
    }
}
