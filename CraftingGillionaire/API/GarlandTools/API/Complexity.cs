using System.Text.Json.Serialization;

namespace CraftingGillionaire.API.GarlandTools.API
{
    public class Complexity
    {
        [JsonPropertyName("nq")]
        public int NormalQuality { get; set; }

        [JsonPropertyName("hq")]
        public int HighQuality { get; set; }
    }
}
