using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CraftingGillionaire.API.XIVAPI.API
{
    internal class ItemIDResponse
    {
        [JsonPropertyName("Results")]
        public List<ItemResult> Results { get; set; }

        [JsonPropertyName("SpeedMs")]
        public int SpeedMs { get; set; }
    }
}
