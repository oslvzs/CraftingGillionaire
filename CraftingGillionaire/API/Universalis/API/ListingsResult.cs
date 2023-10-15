using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CraftingGillionaire.API.Universalis.API
{
    public class ListingsResult
    {
        [JsonPropertyName("listings")]
        public List<MarketListing> Listings { get; set; }
    }
}
