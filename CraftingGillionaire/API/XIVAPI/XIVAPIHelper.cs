using CraftingGillionaire.API.Universalis.API;
using CraftingGillionaire.API.Universalis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using CraftingGillionaire.API.XIVAPI.API;

namespace CraftingGillionaire.API.XIVAPI
{
    internal static class XIVAPIHelper
    {
        internal static async Task<ItemIDResult> GetItemNameByID(string itemName)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                string encodedName = HttpUtility.UrlEncode(itemName);
                HttpResponseMessage responseObject = await httpClient.GetAsync($"https://xivapi.com/search?indexes=Item&string={encodedName}&string_algo=match&pretty=1");
                responseObject.EnsureSuccessStatusCode();
                string responseBody = await responseObject.Content.ReadAsStringAsync();
                ItemIDResponse response = JsonSerializer.Deserialize<ItemIDResponse>(responseBody) ?? new ItemIDResponse();
                if (response.Results.Count == 0)
                    return new ItemIDResult($"Could not find item with name \"{itemName}\"");
                return new ItemIDResult(response.Results.First().ID);
            }
            catch (HttpRequestException ex)
            {
                return new ItemIDResult("Could not get response from XIVAPI. Try again later!");
            }
        }
    }
}
