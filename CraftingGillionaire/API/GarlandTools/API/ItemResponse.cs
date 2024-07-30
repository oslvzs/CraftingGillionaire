using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CraftingGillionaire.API.GarlandTools.API
{
    public class ItemResponse
    {
        [JsonPropertyName("item")]
        public ItemInfo? ItemInfo { get; set; }

        [JsonPropertyName("ingredients")]
        public List<IngredientInfo>? IngredientsList { get; set; }

        [JsonPropertyName("partials")]
        public List<PartialInfo>? PartialsList { get; set; }
    }
}
