using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CraftingGillionaire.API.GarlandTools.API
{
    public class IngredientInfo
    {
        [JsonPropertyName("id")]
        [JsonRequired]
        public int ID { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("icon")]
        public object? Icon { get; set; }

        [JsonPropertyName("category")]
        public int Category { get; set; }

        [JsonPropertyName("ilvl")]
        public int ItemLevel { get; set; }

        [JsonPropertyName("price")]
        public int Price { get; set; }

        [JsonPropertyName("craft")]
        public List<CraftInfo>? CraftsList { get; set; }

        [JsonPropertyName("ventures")]
        public List<int>? Ventures { get; set; }

        [JsonPropertyName("nodes")]
        public List<int>? Nodes { get; set; }

        [JsonPropertyName("vendors")]
        public List<int>? Vendors { get; set; }
    }
}
