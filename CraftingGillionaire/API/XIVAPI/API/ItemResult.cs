using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraftingGillionaire.API.XIVAPI.API
{
    internal class ItemResult
    {
        [JsonPropertyName("ID")]
        public int ID { get; set; }

        [JsonPropertyName("Icon")]
        public string? Icon { get; set; }

        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("Url")]
        public string? Url { get; set; }

        [JsonPropertyName("UrlType")]
        public string? UrlType { get; set; }

        [JsonPropertyName("_")]
        public string? Type { get; set; }

        [JsonPropertyName("_Score")]
        public string? _Score { get; set; }
    }
}
