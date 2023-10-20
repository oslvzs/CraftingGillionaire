using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingGillionaire.API.Universalis
{
    internal class MarketMinPriceResult
    {
        internal MarketMinPriceResult(int price)
        {
            this.MinPrice = price;
            this.HasException = false;
            this.Exception = String.Empty;
        }

        internal MarketMinPriceResult(string exception)
        {
            this.HasException = true;
            this.Exception = exception;
        }

        internal int MinPrice { get; }

        internal bool HasException { get; }

        internal string Exception { get; }
    }
}
