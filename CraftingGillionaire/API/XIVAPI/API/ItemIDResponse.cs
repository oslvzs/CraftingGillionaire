using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CraftingGillionaire.API.XIVAPI.API
{
    internal class ItemIDResponse
    {
        [JsonPropertyName("results")]
        public List<ItemResult>? Results { get; set; }
    }
}
