using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CraftingGillionaire.API.GarlandTools.API
{
    public class CraftInfo
    {
        [JsonPropertyName("job")]
        public int JobID { get; set; }

        [JsonPropertyName("rlvl")]
        public int RequiredLevel { get; set; }

        [JsonPropertyName("durability")]
        public int Durability { get; set; }

        [JsonPropertyName("quality")]
        public int Quality { get; set; }

        [JsonPropertyName("progress")]
        public int Progress { get; set; }

        [JsonPropertyName("lvl")]
        public int JobLevel { get; set; }

        [JsonPropertyName("suggestedCraftsmanship")]
        public int SuggestedCraftsmanship { get; set; }

        [JsonPropertyName("suggestedControl")]
        public int SuggestedControl { get; set; }

        [JsonPropertyName("materialQualityFactor")]
        public int MaterialQualityFactor { get; set; }

        [JsonPropertyName("ingredients")]
        public List<RecipePart>? RecipePartsList { get; set; }

        [JsonPropertyName("complexity")]
        public Complexity? Complexity { get; set; }

        [JsonPropertyName("hq")]
        public int CanBeHQ { get; set; }

        [JsonPropertyName("quickSynth")]
        public int CanBeQuickSynthesized { get; set; }
    }
}
