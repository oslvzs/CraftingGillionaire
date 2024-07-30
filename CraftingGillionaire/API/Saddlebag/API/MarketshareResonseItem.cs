using System.Text.Json.Serialization;

namespace CraftingGillionaire.API.Saddlebag.API
{
    public class MarketshareResonseItem
    {
        [JsonPropertyName("avg")]
        public int AveragePrice { get; set; }

        [JsonPropertyName("itemID")]
        public string? ItemID { get; set; }

        [JsonPropertyName("marketValue")]
        public int MarketValue { get; set; }

        [JsonPropertyName("median")]
        public int MedianPrice { get; set; }

        [JsonPropertyName("minPrice")]
        public int MinPrice { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("npc_vendor_info")]
        public string? NPCVendorInfo { get; set; }

        [JsonPropertyName("percentChange")]
        public double PercentChange { get; set; }

        [JsonPropertyName("purchaseAmount")]
        public int PurchaseAmount { get; set; }

        [JsonPropertyName("quantitySold")]
        public int QuantitySold { get; set; }

        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("url")]
        public string? URL { get; set; }
    }
}
