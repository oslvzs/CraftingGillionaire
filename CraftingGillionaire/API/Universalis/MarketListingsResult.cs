using CraftingGillionaire.API.Universalis.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingGillionaire.API.Universalis
{
    internal class MarketListingsResult
    {
        internal MarketListingsResult(List<MarketListing> marketListings)
        {
            this.MarketListings = marketListings;
            this.HasException = false;
            this.Exception = String.Empty;
        }

        internal MarketListingsResult(string exception)
        {
            this.HasException = true;
            this.Exception = exception;
        }

        internal List<MarketListing> MarketListings { get; }

        internal bool HasException { get; }

        internal string Exception { get; }
    }
}
