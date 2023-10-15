using System.Text.Json.Serialization;

namespace CraftingGillionaire.API.Universalis.API
{
    public class MinPriceResult
    {
        [JsonPropertyName("minPrice")]
        public int MinPrice { get; set; }
    }
}
