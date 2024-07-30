using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CraftingGillionaire.API.Universalis.API
{
    internal class MultipleMinPricesResult
    {
        [JsonPropertyName("items")]
        public Dictionary<string, MinPriceResult>? MinPriceDictionary { get; set; }
    }
}
