using CraftingGillionaire.API.Universalis.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingGillionaire.API.Universalis
{
    internal class SalesHistoryResult
    {
        internal SalesHistoryResult(List<SaleEntry> sales)
        {
            this.SaleEntries = sales;
            this.HasException = false;
        }

        internal SalesHistoryResult(string exception)
        {
            this.HasException = true;
            this.Exception = exception;
        }

        internal List<SaleEntry> SaleEntries { get; }

        internal bool HasException { get; }

        internal string Exception { get; }
    }
}
