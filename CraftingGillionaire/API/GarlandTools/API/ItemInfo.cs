using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CraftingGillionaire.API.GarlandTools.API
{
    public class ItemInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("patch")]
        public double Patch { get; set; }

        [JsonPropertyName("patchCategory")]
        public int PatchCategory { get; set; }

        [JsonPropertyName("price")]
        public int Price { get; set; }

        [JsonPropertyName("ilvl")]
        public int iLevel { get; set; }

        [JsonPropertyName("category")]
        public int Category { get; set; }

        [JsonPropertyName("dyeable")]
        public int IsDyeable { get; set; }

        [JsonPropertyName("tradeable")]
        public int IsTradeable { get; set; }

        [JsonPropertyName("sell_price")]
        public int SellPrice { get; set; }

        [JsonPropertyName("rarity")]
        public int Rarity { get; set; }

        [JsonPropertyName("stackSize")]
        public int StackSize { get; set; }

        [JsonPropertyName("repair")]
        public int Repair { get; set; }

        [JsonPropertyName("icon")]
        public object Icon { get; set; }

        [JsonPropertyName("craft")]
        public List<CraftInfo> CraftsList { get; set; }

        [JsonPropertyName("furniture")]
        public int IsFurniture { get; set; }

        [JsonPropertyName("models")]
        public List<string> Models { get; set; }
    }
}
