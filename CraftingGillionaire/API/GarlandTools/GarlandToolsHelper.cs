using CraftingGillionaire.API.GarlandTools.API;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System;

namespace CraftingGillionaire.API.GarlandTools
{
    internal static class GarlandToolsHelper
    {
        internal static async Task<ItemInfoResult> GetItemResponse(int itemID)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string responseBody;
            try
            {
                HttpResponseMessage responseObject = await httpClient.GetAsync($"https://www.garlandtools.org/db/doc/item/en/3/{itemID}.json");
                responseObject.EnsureSuccessStatusCode();
                responseBody = await responseObject.Content.ReadAsStringAsync();
                ItemResponse response = JsonSerializer.Deserialize<ItemResponse>(responseBody) ?? new ItemResponse();
                return new ItemInfoResult(response);
            }
            catch (HttpRequestException ex)
            {
                return new ItemInfoResult($"Could not get response from GarlandTools. Try again later!\r\nError code:{ex.StatusCode}");
            }
            catch(JsonException ex)
            {
                return new ItemInfoResult($"Could not parse answer from GarlandTools.");
            }
        }
    }
}
