using CraftingGillionaire.API.Saddlebag.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingGillionaire.API.Saddlebag
{
	public class MarketshareResponseData
	{
		public MarketshareResponseData(MarketshareResonseItem[] resonseItems, string exception) 
		{ 
			this.ResonseItems = resonseItems;
			this.Exception = exception;
		}

		public string Exception { get; }

		public MarketshareResonseItem[] ResonseItems { get; }
	}
}
