using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CraftingGillionaire.API.Universalis.API
{
    public class ListingsResult
    {
        [JsonPropertyName("items")]
        public Dictionary<string, MarketListingsList>? ListingsDictionary { get; set; }
    }
}
