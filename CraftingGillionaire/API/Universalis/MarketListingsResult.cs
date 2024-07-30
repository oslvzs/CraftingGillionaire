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
        internal MarketListingsResult(Dictionary<int, List<MarketListing>> marketListingsDictionary)
        {
            this.MarketListingsDictionary = marketListingsDictionary;
            this.HasException = false;
            this.Exception = String.Empty;
        }

        internal MarketListingsResult(string exception)
        {
            this.HasException = true;
            this.Exception = exception;
        }

        internal Dictionary<int, List<MarketListing>>? MarketListingsDictionary { get; }

        internal bool HasException { get; }

        internal string? Exception { get; }
    }
}
