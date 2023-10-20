using Avalonia.Controls.Documents;
using CraftingGillionaire.API.GarlandTools.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingGillionaire.API.GarlandTools
{
    internal class ItemInfoResult
    {
        internal ItemInfoResult(ItemResponse itemResponse)
        {
            this.ItemResponse = itemResponse;
            this.HasException = false;
        }

        internal ItemInfoResult(string exception)
        {
            this.HasException = true;
            this.Exception = exception;
        }

        internal ItemResponse ItemResponse { get; }

        internal bool HasException { get; }

        internal string Exception { get; }
    }
}
