using CraftingGillionaire.API.Saddlebag;
using CraftingGillionaire.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CraftingGillionaire.Models
{
	public class SearchRequestData : ReactiveObject
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


		public List<MarketshareFilterItem> FilterItems { get; private set; } = new List<MarketshareFilterItem>()
		{
			new MarketshareFilterItem("Everything", 0),
            new MarketshareFilterItem("Purchasable from NPC Vendor", -1),
            new MarketshareFilterItem("Furniture Purchasable from NPC Vendor", -4),
            new MarketshareFilterItem("Supply and Provisioning Mission Quest Items", -2),
            new MarketshareFilterItem("Crafter Class Quest Items", -3),
            new MarketshareFilterItem("Exclude Crafted Gear", -5),
            new MarketshareFilterItem("Arms", 1),
            new MarketshareFilterItem("-- Archanist's Arms", 16),
            new MarketshareFilterItem("-- Archer's Arms", 12),
            new MarketshareFilterItem("-- Astrologian's Arms", 78),
            new MarketshareFilterItem("-- Blue Mage's Arms", 105),
            new MarketshareFilterItem("-- Conjurer's Arms", 15),
            new MarketshareFilterItem("-- Dancer's Arms", 87),
            new MarketshareFilterItem("-- Dark Knight's Arms", 76),
            new MarketshareFilterItem("-- Gunbreaker's Arms", 86),
            new MarketshareFilterItem("-- Lancer's Arms", 13),
            new MarketshareFilterItem("-- Machinist's Arms", 77),
            new MarketshareFilterItem("-- Marauder's Arms", 11),
            new MarketshareFilterItem("-- Pugilist's Arms", 9),
            new MarketshareFilterItem("-- Red Mage's Arms", 84),
            new MarketshareFilterItem("-- Rogue's Arms", 73),
            new MarketshareFilterItem("-- Reaper's Arms", 88),
            new MarketshareFilterItem("-- Samurai's Arms", 83),
            new MarketshareFilterItem("-- Scholar's Arms", 85),
            new MarketshareFilterItem("-- Sage's Arms", 89),
            new MarketshareFilterItem("-- Thaumaturge's Arms", 14),
            new MarketshareFilterItem("Tools", 2),
            new MarketshareFilterItem("-- Alchemist's Tools", 25),
            new MarketshareFilterItem("-- Armorer's Tools", 21),
            new MarketshareFilterItem("-- Blacksmith's Tools", 20),
            new MarketshareFilterItem("-- Botanist's Tools", 28),
            new MarketshareFilterItem("-- Carpenter's Tools", 19),
            new MarketshareFilterItem("-- Culinarian's Tools", 26),
            new MarketshareFilterItem("-- Fisher's Tackle", 30),
            new MarketshareFilterItem("-- Fisher's Tools", 29),
            new MarketshareFilterItem("-- Goldsmith's Tools", 22),
            new MarketshareFilterItem("-- Leatherworker's Tools", 23),
            new MarketshareFilterItem("-- Miner's Tools", 27),
            new MarketshareFilterItem("-- Weaver's Tools", 24),
            new MarketshareFilterItem("Armor", 3),
            new MarketshareFilterItem("-- Shields", 17),
            new MarketshareFilterItem("-- Head", 31),
            new MarketshareFilterItem("-- Body", 33),
            new MarketshareFilterItem("-- Legs", 35),
            new MarketshareFilterItem("-- Hands", 36),
            new MarketshareFilterItem("-- Feet", 37),
            new MarketshareFilterItem("Accessories", 4),
            new MarketshareFilterItem("-- Necklaces", 39),
            new MarketshareFilterItem("-- Earrings", 40),
            new MarketshareFilterItem("-- Bracelets", 41),
            new MarketshareFilterItem("-- Rings", 42),
            new MarketshareFilterItem("Medicines & Meals", 5),
            new MarketshareFilterItem("-- Medicine", 43),
            new MarketshareFilterItem("-- Ingredients", 44),
            new MarketshareFilterItem("-- Meals", 45),
            new MarketshareFilterItem("-- Seafood", 46),
            new MarketshareFilterItem("Materials", 6),
            new MarketshareFilterItem("-- Stone", 47),
            new MarketshareFilterItem("-- Metal", 48),
            new MarketshareFilterItem("-- Lumber", 49),
            new MarketshareFilterItem("-- Cloth", 50),
            new MarketshareFilterItem("-- Leather", 51),
            new MarketshareFilterItem("-- Bone", 52),
            new MarketshareFilterItem("-- Reagents", 53),
            new MarketshareFilterItem("-- Dyes", 54),
            new MarketshareFilterItem("-- Weapon Parts", 55),
            new MarketshareFilterItem("Other", 7),
            new MarketshareFilterItem("-- Furnishings", 56),
            new MarketshareFilterItem("-- Materia", 57),
            new MarketshareFilterItem("-- Crystals", 58),
            new MarketshareFilterItem("-- Catalysts", 59),
            new MarketshareFilterItem("-- Miscellany", 60),
            new MarketshareFilterItem("-- Exterior Fixtures", 65),
            new MarketshareFilterItem("-- Interior Fixtures", 66),
            new MarketshareFilterItem("-- Outdoor Furnishings", 67),
            new MarketshareFilterItem("-- Chairs and Beds", 68),
            new MarketshareFilterItem("-- Tables", 69),
            new MarketshareFilterItem("-- Tabletop", 70),
            new MarketshareFilterItem("-- Wall-mounted", 71),
            new MarketshareFilterItem("-- Rugs", 72),
            new MarketshareFilterItem("-- Seasonal Miscellany", 74),
            new MarketshareFilterItem("-- Minions", 75),
            new MarketshareFilterItem("-- Airship/Submersible Components", 79),
            new MarketshareFilterItem("-- Orchestration Components", 80),
            new MarketshareFilterItem("-- Gardening Items", 81),
            new MarketshareFilterItem("-- Paintings", 82),
            new MarketshareFilterItem("-- Registrable Miscellany", 90)
        };

		public ObservableCollection<MarketshareFilterItem> SelectedFilterItems { get; } = new ObservableCollection<MarketshareFilterItem>();
        
        public int SelectedFilterItemsCount { get; set; } = 0;

        public List<RowsFilterItem> RowsFilterItems { get; private set; } = new List<RowsFilterItem>()
        {
            new RowsFilterItem("Everything", 0),
            new RowsFilterItem("Only craftable", 1),
            new RowsFilterItem("Only craftable by me", 2),
            new RowsFilterItem("Only fully craftable by me", 3)
        };

        public RowsFilterItem RowsSelectedFilterItem { get; set; }

		public void OnFiltersButtonClick()
		{
			this._mainWindowViewModel.OnFiltersButtonClick();
		}

		public void OnFiltersPanelOkClick()
		{
			this.SelectedFilterItemsCount = this.SelectedFilterItems.Count;
			this.RaisePropertyChanged(nameof(this.SelectedFilterItemsCount));
		}

        public void OnRowsFilterButtonClick()
        {
            this._mainWindowViewModel.OnRowsFilterButtonClick();
        }

        public void OnRowsFilterPanelOkClick()
        {
            this.RaisePropertyChanged(nameof(this.RowsSelectedFilterItem));
        }
	}
}
