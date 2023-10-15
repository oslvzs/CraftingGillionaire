using System.Text.Json.Serialization;

namespace CraftingGillionaire.API.Universalis.API
{
    public class MarketListing
    {
        [JsonPropertyName("pricePerUnit")]
        public int PricePerUnit { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}
