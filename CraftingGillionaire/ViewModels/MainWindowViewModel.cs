using CraftingGillionaire.API.Saddlebag.API;
using CraftingGillionaire.API.Saddlebag;
using CraftingGillionaire.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ReactiveUI;
using CraftingGillionaire.Models.User;
using System.Diagnostics;
using System.Runtime.InteropServices;
using DynamicData;
using CraftingGillionaire.API.GarlandTools.API;
using CraftingGillionaire.API.Universalis.API;
using CraftingGillionaire.API.Universalis;
using System.Linq;
using System;
using CraftingGillionaire.API.GarlandTools;
using CraftingGillionaire.Models.CraftingAnalyzer;
using Avalonia.Controls.Selection;
using System.IO;
using System.Text.Json;

namespace CraftingGillionaire.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel() { 
			this.MarketshareInfos = new ObservableCollection<MarketshareInfo>();
			this.SearchRequestData = new SearchRequestData(this)
			{
				ServerName = "Moogle",
				SalesAmount = 30,
				AveragePrice = 10000,
				TimePeriod = 168
			};
			this.UserInfo = new UserInfo(this);
			this.SaddlebagSelection = new SelectionModel<string>();

			if (File.Exists("user.json"))
			{
				string json = File.ReadAllText("user.json");
				UserInfo savedUserInfo = JsonSerializer.Deserialize<UserInfo>(json) ?? new UserInfo();

				this.UserInfo.AlchemistLevel = savedUserInfo.AlchemistLevel;
				this.UserInfo.ArmorerLevel = savedUserInfo.ArmorerLevel;
				this.UserInfo.GoldsmithLevel = savedUserInfo.GoldsmithLevel;
				this.UserInfo.CarpenterLevel = savedUserInfo.CarpenterLevel;
				this.UserInfo.CulinarianLevel = savedUserInfo.CulinarianLevel;
				this.UserInfo.BlacksmithLevel = savedUserInfo.BlacksmithLevel;
				this.UserInfo.LeatherworkerLevel = savedUserInfo.LeatherworkerLevel;
				this.UserInfo.WeaverLevel = savedUserInfo.WeaverLevel;
			}

			
		}

		public ObservableCollection<MarketshareInfo> MarketshareInfos { get; private set; }

		public SearchRequestData SearchRequestData { get; private set; }

		public UserInfo UserInfo { get; private set; }

		public CraftingAnalyzerItem CraftingAnalyzerItem { get; set; }

		public bool IsSplitViewPaneOpen { get; private set; } = true;
		public bool IsStartSearchLabelVisible { get; private set; } = true;
		public bool IsLoadingSearchResultPanelVisible { get; private set; } = false;
		public bool IsSearchDataGridVisible { get; private set; } = false;
		public bool IsSearchButtonVisible { get; private set; } = true;		
		public bool IsSaddlebagGridVisible { get; private set; } = true;
		public bool IsCraftingAnalyzerVisible {  get; private set; } = false;
		public bool IsCraftingAnalyzerPreparingLabelVisible { get; private set; } = false;
		public bool IsCraftingAnalyzerContentVisible { get; private set; } = false;
		public bool IsSaddlebagException { get; private set; } = false;
		public string SaddlebagException { get; private set; } = String.Empty;

		public bool IsFilterPanelVisible { get; private set; } = false;

		public List<string> SaddlebagFilterItems { get; private set; } = new List<string>()
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

		public SelectionModel<string> SaddlebagSelection { get; }
		public ObservableCollection<string> SaddlebagSelectedFilterItems { get; } = new ObservableCollection<string>();

		public int SaddlebagSelectedFilterItemsCount { get; private set; } = 0;

		public void OnSaveClick()
		{
			string text = JsonSerializer.Serialize(this.UserInfo);
			string fileName = "user.json";
			File.WriteAllText(fileName, text);
			this.IsSplitViewPaneOpen = false;
			this.RaisePropertyChanged(nameof(IsSplitViewPaneOpen));
		}

		public void OnSettingsClick()
		{
			this.IsSplitViewPaneOpen = !this.IsSplitViewPaneOpen;
			this.RaisePropertyChanged(nameof(IsSplitViewPaneOpen));
		}

		public async void OnSearchClick()
		{
			this.IsSearchDataGridVisible = false;
			this.IsStartSearchLabelVisible = false;
			this.IsLoadingSearchResultPanelVisible = true;
			this.IsSaddlebagException = false;
			this.IsFilterPanelVisible = false;
			this.RaisePropertyChanged(nameof(IsFilterPanelVisible));
			this.RaisePropertyChanged(nameof(IsSearchDataGridVisible));
			this.RaisePropertyChanged(nameof(IsStartSearchLabelVisible));
			this.RaisePropertyChanged(nameof(IsLoadingSearchResultPanelVisible));
			this.RaisePropertyChanged(nameof(IsSaddlebagException));

			ObservableCollection<MarketshareInfo> marketshareInfoList = await this.GetMarketshareInfo();
			this.MarketshareInfos.Clear();
			this.MarketshareInfos.AddRange(marketshareInfoList);
			this.RaisePropertyChanged(nameof(MarketshareInfos));

			this.IsLoadingSearchResultPanelVisible = false;
			this.RaisePropertyChanged(nameof(IsLoadingSearchResultPanelVisible));

			if (!this.IsSaddlebagException)
			{
				this.IsSearchDataGridVisible = true;
				this.RaisePropertyChanged(nameof(IsSearchDataGridVisible));			
			}
			else
			{
				this.RaisePropertyChanged(nameof(SaddlebagException));
				this.RaisePropertyChanged(nameof(IsSaddlebagException));
			}

		}

		private async Task<ObservableCollection<MarketshareInfo>> GetMarketshareInfo()
		{
			ObservableCollection<MarketshareInfo> marketshareInfoList = new ObservableCollection<MarketshareInfo>();
			int[] filters = this.ConvertFiltersToIDs();
			MarketshareRequest request = SaddlebagHelper.CreateRequestObject(this.SearchRequestData, SortBy.PurchaseAmount, filters);
			MarketshareResponseData responseData = await SaddlebagHelper.GetMarketshareResonseItemsAsync(request);
			if (!String.IsNullOrEmpty(responseData.Exception))
			{
				this.IsSaddlebagException = true;
				this.SaddlebagException = responseData.Exception;
				return new ObservableCollection<MarketshareInfo>();
			}
			else
			{
				foreach (MarketshareResonseItem responseItem in responseData.ResonseItems)
				{
					MarketshareInfo marketshareInfo = new MarketshareInfo()
					{
						ItemID = Int32.Parse(responseItem.ItemID),
						ItemName = responseItem.Name,
						AveragePrice = responseItem.AveragePrice,
						MarketValue = responseItem.MarketValue,
						MinPrice = responseItem.MinPrice,
						PercentChange = responseItem.PercentChange,
						QuantitySold = responseItem.QuantitySold,
						SalesAmount = responseItem.PurchaseAmount,
						State = responseItem.State,
						URL = responseItem.URL
					};
					marketshareInfoList.Add(marketshareInfo);
				}
			}

			return marketshareInfoList;
		}

		public async void OnAnalyzeClick(MarketshareInfo marketshareInfo)
		{
			this.IsSaddlebagGridVisible = false;
			this.IsSearchButtonVisible = false;
			this.IsCraftingAnalyzerVisible = true;
			this.IsCraftingAnalyzerPreparingLabelVisible = true;
			this.IsCraftingAnalyzerContentVisible = false;

			this.RaisePropertyChanged(nameof(IsSaddlebagGridVisible));
			this.RaisePropertyChanged(nameof(IsCraftingAnalyzerVisible));
			this.RaisePropertyChanged(nameof(IsSearchButtonVisible));
			this.RaisePropertyChanged(nameof(IsCraftingAnalyzerPreparingLabelVisible));
			this.RaisePropertyChanged(nameof(IsCraftingAnalyzerContentVisible));

			ItemResponse response = await GarlandToolsHelper.GetItemResponse(marketshareInfo.ItemID);
			this.CraftingAnalyzerItem = await this.PrepareInfo(response, this.SearchRequestData.ServerName, marketshareInfo.QuantitySold);
			this.IsCraftingAnalyzerPreparingLabelVisible = false;
			this.IsCraftingAnalyzerContentVisible = true;

			this.RaisePropertyChanged(nameof(IsCraftingAnalyzerPreparingLabelVisible));
			this.RaisePropertyChanged(nameof(IsCraftingAnalyzerContentVisible));
			this.RaisePropertyChanged(nameof(CraftingAnalyzerItem));
		}

		public void OnCraftingAnalyzerBackClick()
		{
			this.IsSaddlebagGridVisible = true;
			this.IsSearchButtonVisible = true;
			this.IsCraftingAnalyzerVisible = false;

			this.RaisePropertyChanged(nameof(IsSaddlebagGridVisible));
			this.RaisePropertyChanged(nameof(IsCraftingAnalyzerVisible));
			this.RaisePropertyChanged(nameof(IsSearchButtonVisible));
		}

		public void OnLinkClick(string url)
		{
			this.OpenLink(url);
		}

		private async Task<CraftingAnalyzerItem> PrepareInfo(ItemResponse response, string serverName, int quantitySold)
		{
			int itemID = response.ItemInfo.ID;
			string itemName = response.ItemInfo.Name;
			CraftingItemInfo craftingItemInfo = null;
			ItemCraftingProfitInfo profitInfo = null;
			CraftingJobInfo craftingJobInfo = null;
			bool isItemCraftable = true;
			List<CraftingPart> craftingParts = new List<CraftingPart>();
			if (response.ItemInfo.CraftsList != null && response.ItemInfo.CraftsList.Count > 0)
			{
				CraftInfo craftInfo = response.ItemInfo.CraftsList.First();
				string jobName = String.Empty;
				int jobLevel = craftInfo.JobLevel;
				int jobID = craftInfo.JobID;
				if (jobID > 0)
					jobName = CommonInfoHelper.GetJobNameByID(jobID);

				bool canCraftItem = this.CanCraftItem(jobID, jobLevel);


				foreach (RecipePart recipePart in craftInfo.RecipePartsList)
				{
					CraftingPart craftingPart = await this.GetCraftingPart(serverName, response.IngredientsList, recipePart);
					craftingParts.Add(craftingPart);
				}

				int recipeCosts;
				if (craftingParts.Count > 0)
				{
					recipeCosts = craftingParts.Sum(x => x.CraftingInfo.CraftingTotalCosts);
				}
				else
				{
					string datacenterName = CommonInfoHelper.GetDatacenterByServerName(serverName);
					recipeCosts = await ApplicationCache.GetItemMinPriceByDatacenter(itemID, datacenterName);
				}
				int itemMarketboardMinPrice = await UniversalisHelper.GetItemMinPrice(itemID, serverName);

				craftingItemInfo = new CraftingItemInfo(itemID, itemName, canCraftItem);
				profitInfo = new ItemCraftingProfitInfo(recipeCosts, itemMarketboardMinPrice, quantitySold);
				craftingJobInfo = new CraftingJobInfo(jobName, jobLevel);
			}
			else
			{
				craftingItemInfo = new CraftingItemInfo(itemID, itemName, true);
				isItemCraftable = false;
			}

            CraftingAnalyzerItem itemCraftingInfo = new CraftingAnalyzerItem(serverName, craftingItemInfo, craftingParts, profitInfo, craftingJobInfo, isItemCraftable);
			return itemCraftingInfo;
		}


		private async Task<CraftingPart> GetCraftingPart(string serverName, List<IngredientInfo> allIngredientsInfo, RecipePart recipePart)
		{
			int recipePartItemID = recipePart.ID;
			int recipePartAmount = recipePart.Amount;
			string recipePartItemName;
			List<CraftingPart> recipePartCrafringParts = new List<CraftingPart>();
			IngredientInfo ingredientInfo = allIngredientsInfo.FirstOrDefault(x => x.ID == recipePartItemID);
			int jobID = 0;
			int jobLevel = 0;
			string jobName = String.Empty;
			CraftingJobInfo craftingJobInfo = null;
			bool canCraftItem = true;
			int vendorPrice = 0;
			if (ingredientInfo != null)
			{
				recipePartItemName = ingredientInfo.Name;
				if (ingredientInfo.CraftsList != null && ingredientInfo.CraftsList.Count > 0)
				{
					CraftInfo craftInfo = ingredientInfo.CraftsList[0];

					jobID = craftInfo.JobID;
					if (jobID > 0)
						jobName = CommonInfoHelper.GetJobNameByID(jobID);
					jobLevel = craftInfo.JobLevel;
					craftingJobInfo = new CraftingJobInfo(jobName, jobLevel);
					canCraftItem = this.CanCraftItem(jobID, jobLevel);
					foreach (RecipePart innerRecipePart in craftInfo.RecipePartsList)
					{
						CraftingPart innerCraftingPart = await this.GetCraftingPart(serverName, allIngredientsInfo, innerRecipePart);
						recipePartCrafringParts.Add(innerCraftingPart);
					}
				}

				if (ingredientInfo.Vendors != null && ingredientInfo.Vendors.Count > 0 && ingredientInfo.Price > 0)
				{
					vendorPrice = ingredientInfo.Price;
				}
			}
			else
			{
				recipePartItemName = await ApplicationCache.GetItemName(recipePartItemID);
			}

			int recipePartCostPerUnit;
			int marketboardCheaperPrice = 0;
			int marketboardCheaperAmount = 0;
			string datacenterName = CommonInfoHelper.GetDatacenterByServerName(serverName);
			if (recipePartCrafringParts.Count > 0)
			{
				recipePartCostPerUnit = recipePartCrafringParts.Sum(x => x.CraftingInfo.CraftingTotalCosts);
				List<MarketListing> marketListings = await UniversalisHelper.GetItemListings(recipePartItemID, datacenterName);
				List<MarketListing> cheaperListings = marketListings.Where(x => x.PricePerUnit <= recipePartCostPerUnit).ToList();
				if (cheaperListings.Count > 0)
				{
					marketboardCheaperPrice = cheaperListings.Last().PricePerUnit;
					marketboardCheaperAmount = cheaperListings.Sum(x => x.Quantity);
				}
			}
			else
			{
				recipePartCostPerUnit = await ApplicationCache.GetItemMinPriceByDatacenter(recipePartItemID, datacenterName);
			}

			bool isMarketBoardCostsLess = false;
			bool isVendorCostsLess = false;

			if (marketboardCheaperPrice > 0)
			{
				if (vendorPrice < marketboardCheaperPrice)
				{
					if (vendorPrice > 0 && vendorPrice <= recipePartCostPerUnit)
					{
						recipePartCostPerUnit = vendorPrice;
						isVendorCostsLess = true;
					}
				}
				else
				{
					if (marketboardCheaperPrice <= recipePartCostPerUnit)
					{
						recipePartCostPerUnit = marketboardCheaperPrice;
						isMarketBoardCostsLess = true;
					}
				}
			}
			else
			{
				if (vendorPrice > 0 && vendorPrice <= recipePartCostPerUnit)
				{
					recipePartCostPerUnit = vendorPrice;
					isVendorCostsLess = true;
				}
			}

			CraftingItemInfo craftingItemInfo = new CraftingItemInfo(recipePartItemID, recipePartItemName, canCraftItem);			
			CraftingInfo craftingInfo = new CraftingInfo(recipePartAmount, recipePartCostPerUnit, recipePartCrafringParts, craftingJobInfo);
			MarketboardInfo marketboardInfo = new MarketboardInfo(isMarketBoardCostsLess, isVendorCostsLess, marketboardCheaperPrice, marketboardCheaperAmount, vendorPrice);
			return new CraftingPart(craftingItemInfo, craftingInfo, marketboardInfo);
		}

		private bool CanCraftItem(int jobID, int minJobLevel)
		{
			if (jobID == 0)
				return false;

			int currentUserJobLevel = this.UserInfo.GetLevelByJobID(jobID);
			return currentUserJobLevel >= minJobLevel;
		}

		public void OnItemLinkClick(int itemID)
		{
			string url = $"https://universalis.app/market/{itemID}";
			this.OpenLink(url);
		}

		public void OnFiltersButtonClick()
		{
			this.IsFilterPanelVisible = true;
			this.RaisePropertyChanged(nameof(IsFilterPanelVisible));

			this.IsSearchDataGridVisible = false;
			this.IsStartSearchLabelVisible = false;
			this.IsLoadingSearchResultPanelVisible = false;
			this.IsSaddlebagException = false;

			this.RaisePropertyChanged(nameof(IsSearchDataGridVisible));
			this.RaisePropertyChanged(nameof(IsStartSearchLabelVisible));
			this.RaisePropertyChanged(nameof(IsLoadingSearchResultPanelVisible));
			this.RaisePropertyChanged(nameof(IsSaddlebagException));
		}

		public void OnFiltersPanelOkClick()
		{
			this.IsFilterPanelVisible = false;
			this.RaisePropertyChanged(nameof(IsFilterPanelVisible));

			this.SaddlebagSelectedFilterItemsCount = this.SaddlebagSelectedFilterItems.Count;
			this.RaisePropertyChanged(nameof(SaddlebagSelectedFilterItemsCount));

			this.IsStartSearchLabelVisible = true;
			this.RaisePropertyChanged(nameof(IsStartSearchLabelVisible));
		}

		private void OpenLink(string url)
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				url = url.Replace("&", "^&");
				Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				Process.Start("xdg-open", url);
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				Process.Start("open", url);
			}
		}

		private int[] ConvertFiltersToIDs()
		{
			List<int> result = new List<int>();
			foreach(string selectedFilter in this.SaddlebagSelectedFilterItems)
			{
				int filterID = CommonInfoHelper.ConvertFilterNameToID(selectedFilter);
				result.Add(filterID);
			}

			return result.ToArray();
		}
	}
}