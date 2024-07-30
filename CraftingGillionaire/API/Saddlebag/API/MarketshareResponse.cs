using System.Text.Json.Serialization;

namespace CraftingGillionaire.API.Saddlebag.API
{
    public class MarketshareResponse
    {
        [JsonPropertyName("data")]
        public MarketshareResonseItem[]? Data { get; set; }

		[JsonPropertyName("exception")]
		public string? Exception { get; set; }
    }
}
