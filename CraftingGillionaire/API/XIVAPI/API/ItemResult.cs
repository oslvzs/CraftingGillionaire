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
        [JsonPropertyName("row_id")]
        public int RowID { get; set; }
    }
}
