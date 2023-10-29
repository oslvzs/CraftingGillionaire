using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraftingGillionaire.API.Universalis.API
{
    public class SaleEntry
    {
        [JsonPropertyName("hq")]
        public bool IsHQ { get; set; }

        [JsonPropertyName("pricePerUnit")]
        public int PricePerUnit { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("buyerName")]
        public string BuyerName { get; set; }

        [JsonPropertyName("onMannequin")]
        public bool OnMannequin { get; set; }

        [JsonPropertyName("timestamp")]
        public int Timestamp { get; set; }
    }
}
