using Avalonia.Controls.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingGillionaire.API.XIVAPI
{
    internal class ItemIDResult
    {
        internal ItemIDResult(int itemID)
        {
            this.ID = itemID;
            this.HasException = false;
        }

        internal ItemIDResult(string exception)
        {
            this.HasException = true;
            this.Exception = exception;
        }

        internal int ID { get; }

        internal bool HasException { get; }

        internal string Exception { get; }
    }
}
