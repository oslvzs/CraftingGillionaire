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


		public List<string> FilterItems { get; private set; } = new List<string>()
		{
			"Everything",
			"Purchasable from NPC Vendor",
			"Furniture Purchasable from NPC Vendor",
			"Supply and Provisioning Mission Quest Items",
			"Crafter Class Quest Items",
			"Exclude Crafted Gear",
			"Arms",
			"-- Archanist's Arms",
			"-- Archer's Arms",
			"-- Astrologian's Arms",
			"-- Blue Mage's Arms",
			"-- Conjurer's Arms",
			"-- Dancer's Arms",
			"-- Dark Knight's Arms",
			"-- Gunbreaker's Arms",
			"-- Lancer's Arms",
			"-- Machinist's Arms",
			"-- Marauder's Arms",
			"-- Pugilist's Arms",
			"-- Red Mage's Arms",
			"-- Rogue's Arms",
			"-- Reaper's Arms",
			"-- Samurai's Arms",
			"-- Scholar's Arms",
			"-- Sage's Arms",
			"-- Thaumaturge's Arms",
			"Tools",
			"-- Alchemist's Tools",
			"-- Armorer's Tools",
			"-- Blacksmith's Tools",
			"-- Botanist's Tools",
			"-- Carpenter's Tools",
			"-- Culinarian's Tools",
			"-- Fisher's Tackle",
			"-- Fisher's Tools",
			"-- Goldsmith's Tools",
			"-- Leatherworker's Tools",
			"-- Miner's Tools",
			"-- Weaver's Tools",
			"Armor",
			"-- Shields",
			"-- Head",
			"-- Body",
			"-- Legs",
			"-- Hands",
			"-- Feet",
			"Accessories",
			"-- Necklaces",
			"-- Earrings",
			"-- Bracelets",
			"-- Rings",
			"Medicines & Meals",
			"-- Medicine",
			"-- Ingredients",
			"-- Meals",
			"-- Seafood",
			"Materials",
			"-- Stone",
			"-- Metal",
			"-- Lumber",
			"-- Cloth",
			"-- Leather",
			"-- Bone",
			"-- Reagents",
			"-- Dyes",
			"-- Weapon Parts",
			"Other",
			"-- Furnishings",
			"-- Materia",
			"-- Crystals",
			"-- Catalysts",
			"-- Miscellany",
			"-- Exterior Fixtures",
			"-- Interior Fixtures",
			"-- Outdoor Furnishings",
			"-- Chairs and Beds",
			"-- Tables",
			"-- Tabletop",
			"-- Wall-mounted",
			"-- Rugs",
			"-- Seasonal Miscellany",
			"-- Minions",
			"-- Airship/Submersible Components",
			"-- Orchestration Components",
			"-- Gardening Items",
			"-- Paintings",
			"-- Registrable Miscellany"
		};

		public ObservableCollection<string> SelectedFilterItems { get; } = new ObservableCollection<string>();
		
		public int SelectedFilterItemsCount { get; set; } = 0;

		public void OnFiltersButtonClick()
		{
			this._mainWindowViewModel.OnFiltersButtonClick();
		}

		public void OnFiltersPanelOkClick()
		{
			this.SelectedFilterItemsCount = this.SelectedFilterItems.Count;
			this.RaisePropertyChanged(nameof(this.SelectedFilterItemsCount));
		}
	}
}
