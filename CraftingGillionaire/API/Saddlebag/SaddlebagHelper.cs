using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using CraftingGillionaire.API.Saddlebag.API;
using CraftingGillionaire.Models;

namespace CraftingGillionaire.API.Saddlebag
{
    internal static class SaddlebagHelper
    {
        internal static MarketshareRequest CreateRequestObject(SearchRequestData requestData, SortBy? sortByValue, int[] filters)
        {
            string sortBy = String.Empty;
            switch (sortByValue)
            {
                case SortBy.AveragePrice:
                    sortBy = "avg";
                    break;
                case SortBy.MedianPrice:
                    sortBy = "median";
                    break;
                case SortBy.MarketValue:
                    sortBy = "marketValue";
                    break;
                case SortBy.PurchaseAmount:
                    sortBy = "purchaseAmount";
                    break;
                case SortBy.QuantitySold:
                    sortBy = "quantitySold";
                    break;
                default:
                    throw new Exception("Unsupported sortBy value");
            }

            MarketshareRequest request = new MarketshareRequest()
            {
                ServerName = requestData.ServerName,
                AveragePrice = requestData.AveragePrice,
                SalesAmount = requestData.SalesAmount,
                TimePeriod = requestData.TimePeriod,
                SortBy = sortBy,
                Filters = filters
            };

            return request;
        }

        internal static async Task<MarketshareResponseData> GetMarketshareResonseItemsAsync(MarketshareRequest request)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string requestString = JsonSerializer.Serialize(request, JsonSerializerOptions.Default);
            var content = new StringContent(requestString, Encoding.UTF8, "application/json");
            HttpResponseMessage responseObject = await httpClient.PostAsync("http://api.saddlebagexchange.com/api/ffxivmarketshare/", content);
            string responseBody = await responseObject.Content.ReadAsStringAsync();
            MarketshareResponse response = JsonSerializer.Deserialize<MarketshareResponse>(responseBody) ?? new MarketshareResponse();
            
            return new MarketshareResponseData(response.Data, response.Exception);
        }
    }
}
