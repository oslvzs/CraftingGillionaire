using System;
using System.Collections.Generic;

namespace CraftingGillionaire.API.Universalis
{
    internal class MarketMultipleMinPricesResult
    {
        internal MarketMultipleMinPricesResult(Dictionary<int, int> minPricesDictionary)
        {
            this.MinPricesDictionary = minPricesDictionary;
            this.HasException = false;
            this.Exception = String.Empty;
        }

        internal MarketMultipleMinPricesResult(string exception)
        {
            this.HasException = true;
            this.Exception = exception;
        }

        internal Dictionary<int, int> MinPricesDictionary { get; }

        internal bool HasException { get; }

        internal string Exception { get; }
    }
}
