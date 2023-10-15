using CraftingGillionaire.API.Saddlebag;
using CraftingGillionaire.ViewModels;
using ReactiveUI;
using System;
namespace CraftingGillionaire.Models
{
	public class SearchRequestData
	{
		public SearchRequestData(MainWindowViewModel mainWindowViewModel)
		{
			_mainWindowViewModel = mainWindowViewModel;
		}

		private readonly MainWindowViewModel _mainWindowViewModel;

		public string ServerName { get; set; }

		public int TimePeriod { get; set; }
		public string TimePeriodString
		{
			get => this.TimePeriod.ToString();
			set
			{
				if (Int32.TryParse(value, out int timePeriod))
				{
					this.TimePeriod = timePeriod;
					this._mainWindowViewModel.RaisePropertyChanged(nameof(TimePeriodString));
				}
			}
		}

		public int SalesAmount { get; set; }

		public string SalesAmountString
		{
			get => this.SalesAmount.ToString();
			set
			{
				if (Int32.TryParse(value, out int salesAmount))
				{
					this.SalesAmount = salesAmount;
					this._mainWindowViewModel.RaisePropertyChanged(nameof(SalesAmountString));
				}
			}
		}

		public int AveragePrice { get; set; }
		public string AveragePriceString
		{
			get => this.AveragePrice.ToString();
			set
			{
				if (Int32.TryParse(value, out int averagePrice))
				{
					this.AveragePrice = averagePrice;
					this._mainWindowViewModel.RaisePropertyChanged(nameof(AveragePriceString));
				}
			}
		}

		public SortBy SortBy { get; set; } = SortBy.AveragePrice;
	}
}
