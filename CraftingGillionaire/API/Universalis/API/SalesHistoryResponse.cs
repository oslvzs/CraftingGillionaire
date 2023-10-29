using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CraftingGillionaire.API.Universalis.API
{
    public class SalesHistoryResponse
    {
        [JsonPropertyName("itemID")]
        public int ItemID { get; set; }

        [JsonPropertyName("worldID")]
        public int WorldID { get; set; }

        [JsonPropertyName("lastUploadTime")]
        public long LastUploadTime { get; set; }

        [JsonPropertyName("entries")]
        public List<SaleEntry> Entries { get; set; }

        [JsonPropertyName("regularSaleVelocity")]
        public double RegularSaleVelocity { get; set; }

        [JsonPropertyName("nqSaleVelocity")]
        public double NQSaleVelocity { get; set; }

        [JsonPropertyName("hqSaleVelocity")]
        public double HQSaleVelocity { get; set; }

        [JsonPropertyName("worldName")]
        public string WorldName { get; set; }
    }
}
