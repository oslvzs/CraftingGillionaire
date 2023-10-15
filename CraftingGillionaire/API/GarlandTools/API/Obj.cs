using System.Text.Json.Serialization;

namespace CraftingGillionaire.API.GarlandTools.API
{
    public class Obj
    {
        [JsonPropertyName("i")]
        public int i { get; set; }

        [JsonPropertyName("n")]
        public string n { get; set; }

        [JsonPropertyName("l")]
        public int l { get; set; }

        [JsonPropertyName("c")]
        public object c { get; set; }

        [JsonPropertyName("t")]
        public object t { get; set; }

        [JsonPropertyName("z")]
        public int? z { get; set; }

        [JsonPropertyName("p")]
        public int? p { get; set; }

        [JsonPropertyName("s")]
        public int? s { get; set; }

        [JsonPropertyName("a")]
        public int? a { get; set; }

        [JsonPropertyName("q")]
        public int? q { get; set; }
    }
}
