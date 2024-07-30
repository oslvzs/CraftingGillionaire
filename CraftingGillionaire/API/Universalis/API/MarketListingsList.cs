using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraftingGillionaire.API.Universalis.API
{
    public class MarketListingsList
    {
        [JsonPropertyName("listings")]
        public List<MarketListing>? MarketListings { get; set; }
    }
}
