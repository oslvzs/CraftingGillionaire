using System.Text.Json.Serialization;

namespace CraftingGillionaire.API.GarlandTools.API
{
    public class RecipePart
    {
        [JsonPropertyName("id")]
        [JsonRequired]
        public int ID { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("quality")]
        public double Quality { get; set; }
    }
}
