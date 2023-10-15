using System.Text.Json.Serialization;

namespace CraftingGillionaire.API.Saddlebag.API
{
    public class MarketshareRequest
    {
        [JsonPropertyName("server")]
        public string? ServerName { get; set; }

        [JsonPropertyName("time_period")]
        public int TimePeriod { get; set; }

        [JsonPropertyName("sales_amount")]
        public int SalesAmount { get; set; }

        [JsonPropertyName("average_price")]
        public int AveragePrice { get; set; }

        [JsonPropertyName("filters")]
        public int[]? Filters { get; set; }

        [JsonPropertyName("sort_by")]
        public string? SortBy { get; set; }
    }
}
