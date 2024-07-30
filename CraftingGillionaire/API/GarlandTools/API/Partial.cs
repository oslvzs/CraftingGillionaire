using System.Text.Json.Serialization;

namespace CraftingGillionaire.API.GarlandTools.API
{
    public class Partial
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("id")]
        public string? ID { get; set; }

        [JsonPropertyName("obj")]
        public Obj? Object { get; set; }
    }
}
