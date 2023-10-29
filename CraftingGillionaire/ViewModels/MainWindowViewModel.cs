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
using System;
using CraftingGillionaire.API.GarlandTools;
using CraftingGillionaire.Models.CraftingAnalyzer;
using System.IO;
using System.Text.Json;
using CraftingGillionaire.API.XIVAPI;
using CraftingGillionaire.API.Universalis;
using System.Linq;
using CraftingGillionaire.API.Universalis.API;

namespace CraftingGillionaire.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            this.MarketshareInfos = new ObservableCollection<MarketshareInfo>();
            this.MarketshareSearchRequestData = new MarketshareSearchRequestData(this)
            {
                ServerName = "Moogle",
                SalesAmount = 30,
                AveragePrice = 10000,
                TimePeriod = 168,
            };
            this.SalesHistoryRequestData = new SalesHistorySearchRequestData(this)
            {
                ServerName = "Moogle",
                ItemName = "White Rectangular Partition",
                TimePeriod = 168
            };
            this.NQSalesHistory = new ObservableCollection<SaleDisplayItem>();
            this.NQSalesHistoryStats = new SalesHistoryStats(this.NQSalesHistory);
            this.HQSalesHistory = new ObservableCollection<SaleDisplayItem>();
            this.HQSalesHistoryStats = new SalesHistoryStats(this.HQSalesHistory);
            this.UserInfo = new UserInfo(this);
            this.CraftingAnalyzerItem = new CraftingTreeRootNode();

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
        public MarketshareSearchRequestData MarketshareSearchRequestData { get; private set; }
        public SalesHistorySearchRequestData SalesHistoryRequestData { get; private set; }
        public UserInfo UserInfo { get; private set; }
        public CraftingTreeRootNode CraftingAnalyzerItem { get; set; }

        public bool IsSplitViewPaneOpen { get; private set; } = true;
        public bool IsStartSearchLabelVisible { get; private set; } = true;
        public bool IsLoadingSearchResultPanelVisible { get; private set; } = false;
        public bool IsSearchDataGridVisible { get; private set; } = false;
        public bool IsSearchButtonVisible { get; private set; } = true;
        public bool IsSaddlebagGridVisible { get; private set; } = true;
        public bool IsCraftingAnalyzerVisible { get; private set; } = false;
        public bool IsCraftingAnalyzerPreparingLabelVisible { get; private set; } = false;
        public bool IsCraftingAnalyzerContentVisible { get; private set; } = false;
        public bool HasGarlandToolsException { get; private set; }
        public string GarlandToosException { get; private set; }
        public bool HasSaddlebagException { get; private set; } = false;
        public string SaddlebagException { get; private set; } = String.Empty;
        public bool IsFilterPanelVisible { get; private set; } = false;

        public bool IsRowsFilterPanelVisible { get; private set; } = false;
        public bool IsStartSearchHistoryLabelVisible { get; private set; } = true;
        public bool HasSalesHistoryException { get; private set; } = false;
        public string SalesHistoryException { get; private set; } = String.Empty;
        public bool IsSearchHistoryPreparingPanelVisible { get; private set; } = false;
        public bool IsSearchHistoryPanelVisible { get; private set; } = false;

        public ObservableCollection<SaleDisplayItem> NQSalesHistory { get; private set; }
        public ObservableCollection<SaleDisplayItem> HQSalesHistory { get; private set; } 

        public SalesHistoryStats NQSalesHistoryStats { get; private set; }
        public SalesHistoryStats HQSalesHistoryStats { get; private set; }

        public bool IsNQSalesHistoryEmpty { get; private set; } = false;
        public bool IsHQSalesHistoryEmpty { get; private set; } = false;

        public void OnSaveClick()
        {
            string text = JsonSerializer.Serialize(this.UserInfo);
            string fileName = "user.json";
            File.WriteAllText(fileName, text);
            this.IsSplitViewPaneOpen = false;
            this.RaisePropertyChanged(nameof(this.IsSplitViewPaneOpen));
        }

        public void OnSettingsClick()
        {
            this.IsSplitViewPaneOpen = !this.IsSplitViewPaneOpen;
            this.RaisePropertyChanged(nameof(this.IsSplitViewPaneOpen));
        }

        #region Marketshare 

        public async void OnMarketshareSearchClick()
        {
            this.IsSearchDataGridVisible = false;
            this.IsStartSearchLabelVisible = false;
            this.IsLoadingSearchResultPanelVisible = true;
            this.HasSaddlebagException = false;
            this.IsFilterPanelVisible = false;
            this.RaisePropertyChanged(nameof(this.IsFilterPanelVisible));
            this.RaisePropertyChanged(nameof(this.IsSearchDataGridVisible));
            this.RaisePropertyChanged(nameof(this.IsStartSearchLabelVisible));
            this.RaisePropertyChanged(nameof(this.IsLoadingSearchResultPanelVisible));
            this.RaisePropertyChanged(nameof(this.HasSaddlebagException));

            ObservableCollection<MarketshareInfo> marketshareInfoList = await this.GetMarketshareInfo();
            this.MarketshareInfos.Clear();
            this.MarketshareInfos.AddRange(marketshareInfoList);

            if (this.MarketshareSearchRequestData.RowsSelectedFilterItem != null)
            {
                this.MarketshareInfos = this.FilterMarketshareInfo(this.MarketshareSearchRequestData.RowsSelectedFilterItem.ID);
            }

            this.RaisePropertyChanged(nameof(this.MarketshareInfos));

            this.IsLoadingSearchResultPanelVisible = false;
            this.RaisePropertyChanged(nameof(this.IsLoadingSearchResultPanelVisible));

            if (!this.HasSaddlebagException)
            {
                this.IsSearchDataGridVisible = true;
                this.RaisePropertyChanged(nameof(this.IsSearchDataGridVisible));
            }
            else
            {
                this.RaisePropertyChanged(nameof(this.SaddlebagException));
                this.RaisePropertyChanged(nameof(this.HasSaddlebagException));
            }

        }

        private async Task<ObservableCollection<MarketshareInfo>> GetMarketshareInfo()
        {
            ObservableCollection<MarketshareInfo> marketshareInfoList = new ObservableCollection<MarketshareInfo>();
            int[] filters = this.ConvertFiltersToIDs();
            MarketshareRequest request = SaddlebagHelper.CreateRequestObject(this.MarketshareSearchRequestData, SortBy.PurchaseAmount, filters);
            MarketshareResponseData responseData = await SaddlebagHelper.GetMarketshareResonseItemsAsync(request);
            if (!String.IsNullOrEmpty(responseData.Exception))
            {
                this.HasSaddlebagException = true;
                this.SaddlebagException = responseData.Exception;
                return new ObservableCollection<MarketshareInfo>();
            }
            else
            {
                CraftingAnalyzerItemBuilder craftingAnalyzerItemBuilder = new CraftingAnalyzerItemBuilder(this.UserInfo, this.MarketshareSearchRequestData.ServerName);
                foreach (MarketshareResonseItem responseItem in responseData.ResonseItems)
                {
                    int itemID = Int32.Parse(responseItem.ItemID);
                    ItemInfoResult itemInfoResult = await GarlandToolsHelper.GetItemResponse(itemID);
                    if (itemInfoResult.HasException)
                    {
                        this.HasGarlandToolsException = true;
                        this.GarlandToosException = itemInfoResult.Exception;
                        this.IsCraftingAnalyzerPreparingLabelVisible = false;
                        this.RaisePropertyChanged(nameof(this.HasGarlandToolsException));
                        this.RaisePropertyChanged(nameof(this.GarlandToosException));
                        this.RaisePropertyChanged(nameof(this.IsCraftingAnalyzerPreparingLabelVisible));
                        break;
                    }
                    else
                    {
                        MarketshareInfo marketshareInfo = new MarketshareInfo()
                        {
                            ItemID = itemID,
                            ItemName = responseItem.Name,
                            AveragePrice = responseItem.AveragePrice,
                            MarketValue = responseItem.MarketValue,
                            MinPrice = responseItem.MinPrice,
                            PercentChange = responseItem.PercentChange,
                            QuantitySold = responseItem.QuantitySold,
                            SalesAmount = responseItem.PurchaseAmount,
                            State = responseItem.State,
                            URL = responseItem.URL,
                            TreeRootNode = await craftingAnalyzerItemBuilder.BuildCraftingTree(itemInfoResult.ItemResponse)
                        };
                        marketshareInfoList.Add(marketshareInfo);
                    }
                }
            }

            return marketshareInfoList;
        }

        private ObservableCollection<MarketshareInfo> FilterMarketshareInfo(int mode)
        {
            ObservableCollection<MarketshareInfo> result = new ObservableCollection<MarketshareInfo>();
            if (mode == 0)
            {
                return this.MarketshareInfos;
            }

            foreach (MarketshareInfo marketshareInfo in this.MarketshareInfos)
            {
                if (mode == 1)
                {
                    if (marketshareInfo.TreeRootNode.ItemInfo.IsCraftable)
                        result.Add(marketshareInfo);
                }
                else if (mode == 2)
                {
                    if (!marketshareInfo.TreeRootNode.ItemInfo.IsCraftable)
                        continue;

                    if (!marketshareInfo.TreeRootNode.JobInfo.UserCanCraft)
                        continue;

                    result.Add(marketshareInfo);
                }
                else if (mode == 3)
                {
                    if (!marketshareInfo.TreeRootNode.ItemInfo.IsCraftable)
                        continue;

                    if (!marketshareInfo.TreeRootNode.JobInfo.UserCanCraft)
                        continue;

                    bool areInnerNodesCraftable = true;

                    foreach (CraftingTreeNode innerNode in marketshareInfo.TreeRootNode.ChildrenNodes)
                    {
                        if (!this.CheckNodeCraftable(innerNode))
                        {
                            areInnerNodesCraftable = false;
                            break;
                        }
                    }

                    if (areInnerNodesCraftable)
                        result.Add(marketshareInfo);
                }
            }

            return result;
        }

        private bool CheckNodeCraftable(CraftingTreeNode node)
        {
            if (!node.ItemInfo.IsCraftable)
                return true;

            if (!node.JobInfo.UserCanCraft)
                return false;

            bool areInnerNodesCraftable = true;
            foreach (CraftingTreeNode innerNode in node.ChildrenNodes)
            {
                if (!this.CheckNodeCraftable(innerNode))
                {
                    areInnerNodesCraftable = false;
                    break;
                }
            }

            return areInnerNodesCraftable;
        }

        public async void OnAnalyzeClick(MarketshareInfo marketshareInfo)
        {
            this.IsSaddlebagGridVisible = false;
            this.IsSearchButtonVisible = false;
            this.IsCraftingAnalyzerVisible = true;
            this.IsCraftingAnalyzerPreparingLabelVisible = true;
            this.IsCraftingAnalyzerContentVisible = false;
            this.HasGarlandToolsException = false;
            this.RaisePropertyChanged(nameof(this.IsSaddlebagGridVisible));
            this.RaisePropertyChanged(nameof(this.IsCraftingAnalyzerVisible));
            this.RaisePropertyChanged(nameof(this.IsSearchButtonVisible));
            this.RaisePropertyChanged(nameof(this.IsCraftingAnalyzerPreparingLabelVisible));
            this.RaisePropertyChanged(nameof(this.IsCraftingAnalyzerContentVisible));
            this.RaisePropertyChanged(nameof(this.HasGarlandToolsException));

            CraftingAnalyzerItemBuilder craftingAnalyzerItemBuilder = new CraftingAnalyzerItemBuilder(this.UserInfo, this.MarketshareSearchRequestData.ServerName);
            await craftingAnalyzerItemBuilder.FillTreeCosts(marketshareInfo.TreeRootNode, marketshareInfo.QuantitySold);
            this.IsCraftingAnalyzerPreparingLabelVisible = false;
            this.IsCraftingAnalyzerContentVisible = true;
            this.CraftingAnalyzerItem = marketshareInfo.TreeRootNode;
            this.RaisePropertyChanged(nameof(this.IsCraftingAnalyzerPreparingLabelVisible));
            this.RaisePropertyChanged(nameof(this.IsCraftingAnalyzerContentVisible));
            this.RaisePropertyChanged(nameof(this.CraftingAnalyzerItem));

        }

        public void OnCraftingAnalyzerBackClick()
        {
            this.IsSaddlebagGridVisible = true;
            this.IsSearchButtonVisible = true;
            this.IsCraftingAnalyzerVisible = false;

            this.RaisePropertyChanged(nameof(this.IsSaddlebagGridVisible));
            this.RaisePropertyChanged(nameof(this.IsCraftingAnalyzerVisible));
            this.RaisePropertyChanged(nameof(this.IsSearchButtonVisible));
        }

        public void OnLinkClick(string url)
        {
            this.OpenLink(url);
        }

        public void OnItemLinkClick(int itemID)
        {
            string url = $"https://universalis.app/market/{itemID}";
            this.OpenLink(url);
        }


        public void OnFiltersButtonClick()
        {
            this.IsFilterPanelVisible = true;
            this.RaisePropertyChanged(nameof(this.IsFilterPanelVisible));

            this.IsSearchDataGridVisible = false;
            this.IsStartSearchLabelVisible = false;
            this.IsLoadingSearchResultPanelVisible = false;
            this.HasSaddlebagException = false;
            this.IsRowsFilterPanelVisible = false;
            this.RaisePropertyChanged(nameof(this.IsSearchDataGridVisible));
            this.RaisePropertyChanged(nameof(this.IsStartSearchLabelVisible));
            this.RaisePropertyChanged(nameof(this.IsLoadingSearchResultPanelVisible));
            this.RaisePropertyChanged(nameof(this.HasSaddlebagException));
            this.RaisePropertyChanged(nameof(this.IsRowsFilterPanelVisible));
        }

        public void OnFiltersPanelOkClick()
        {
            this.IsFilterPanelVisible = false;
            this.RaisePropertyChanged(nameof(this.IsFilterPanelVisible));

            this.MarketshareSearchRequestData.OnFiltersPanelOkClick();
            this.RaisePropertyChanged(nameof(this.MarketshareSearchRequestData));
            this.IsStartSearchLabelVisible = true;
            this.RaisePropertyChanged(nameof(this.IsStartSearchLabelVisible));
        }

        public void OnRowsFilterButtonClick()
        {
            this.IsRowsFilterPanelVisible = true;
            this.RaisePropertyChanged(nameof(this.IsRowsFilterPanelVisible));

            this.IsSearchDataGridVisible = false;
            this.IsStartSearchLabelVisible = false;
            this.IsLoadingSearchResultPanelVisible = false;
            this.HasSaddlebagException = false;
            this.IsFilterPanelVisible = false;
            this.RaisePropertyChanged(nameof(this.IsSearchDataGridVisible));
            this.RaisePropertyChanged(nameof(this.IsStartSearchLabelVisible));
            this.RaisePropertyChanged(nameof(this.IsLoadingSearchResultPanelVisible));
            this.RaisePropertyChanged(nameof(this.HasSaddlebagException));
            this.RaisePropertyChanged(nameof(this.IsFilterPanelVisible));
        }

        public void OnRowsFiltersPanelOkClick()
        {
            this.IsRowsFilterPanelVisible = false;
            this.RaisePropertyChanged(nameof(this.IsRowsFilterPanelVisible));

            this.MarketshareSearchRequestData.OnRowsFilterPanelOkClick();
            this.RaisePropertyChanged(nameof(this.MarketshareSearchRequestData));
            this.IsStartSearchLabelVisible = true;
            this.RaisePropertyChanged(nameof(this.IsStartSearchLabelVisible));
        }

        #endregion

        #region SalesHistory
        
        public async void OnSalesHistorySearchClick()
        {
            this.IsStartSearchHistoryLabelVisible = false;
            this.HasSalesHistoryException = false;
            this.SalesHistoryException = String.Empty;
            this.IsSearchHistoryPanelVisible = false;
            this.IsSearchHistoryPreparingPanelVisible = true;

            this.RaisePropertyChanged(nameof(this.HasSalesHistoryException));
            this.RaisePropertyChanged(nameof(this.SalesHistoryException));
            this.RaisePropertyChanged(nameof(this.IsSearchHistoryPanelVisible));
            this.RaisePropertyChanged(nameof(this.IsSearchHistoryPreparingPanelVisible));
            this.RaisePropertyChanged(nameof(this.IsStartSearchHistoryLabelVisible));

            ItemIDResult itemIDResult = await XIVAPIHelper.GetItemNameByID(this.SalesHistoryRequestData.ItemName);
            if (itemIDResult.HasException)
            {
                this.HasSalesHistoryException = true;
                this.SalesHistoryException = itemIDResult.Exception;
                this.IsSearchHistoryPreparingPanelVisible = false;
                this.IsSearchHistoryPanelVisible = false;

                this.RaisePropertyChanged(nameof(this.HasSalesHistoryException));
                this.RaisePropertyChanged(nameof(this.SalesHistoryException));
                this.RaisePropertyChanged(nameof(this.IsSearchHistoryPanelVisible));
                this.RaisePropertyChanged(nameof(this.IsSearchHistoryPreparingPanelVisible));
            }
            else
            {
                SalesHistoryResult result = await UniversalisHelper.GetSalesHistory(this.SalesHistoryRequestData.ServerName, itemIDResult.ID, this.SalesHistoryRequestData.TimePeriod);
                if (result.HasException)
                {
                    this.HasSalesHistoryException = true;
                    this.SalesHistoryException = result.Exception;
                    this.IsSearchHistoryPreparingPanelVisible = false;
                    this.IsSearchHistoryPanelVisible = false;

                    this.RaisePropertyChanged(nameof(this.HasSalesHistoryException));
                    this.RaisePropertyChanged(nameof(this.SalesHistoryException));
                    this.RaisePropertyChanged(nameof(this.IsSearchHistoryPanelVisible));
                    this.RaisePropertyChanged(nameof(this.IsSearchHistoryPreparingPanelVisible));
                }
                else
                {
                    this.HQSalesHistory.Clear();
                    this.NQSalesHistory.Clear();
                    foreach (SaleEntry saleEntry in result.SaleEntries)
                    {
                        SaleDisplayItem saleDisplayItem = new SaleDisplayItem(saleEntry);
                        if (saleDisplayItem.IsHQ)
                            this.HQSalesHistory.Add(saleDisplayItem);
                        else
                            this.NQSalesHistory.Add(saleDisplayItem);
                    }

                    this.IsNQSalesHistoryEmpty = this.NQSalesHistory.Count == 0;
                    this.IsHQSalesHistoryEmpty = this.HQSalesHistory.Count == 0;
                    this.NQSalesHistoryStats = new SalesHistoryStats(this.NQSalesHistory);
                    this.HQSalesHistoryStats = new SalesHistoryStats(this.HQSalesHistory);

                    this.HasSalesHistoryException = false;
                    this.SalesHistoryException = String.Empty;
                    this.IsSearchHistoryPreparingPanelVisible = false;
                    this.IsSearchHistoryPanelVisible = true;

                    this.RaisePropertyChanged(nameof(this.NQSalesHistory));
                    this.RaisePropertyChanged(nameof(this.HQSalesHistory));
                    this.RaisePropertyChanged(nameof(this.NQSalesHistoryStats));
                    this.RaisePropertyChanged(nameof(this.HQSalesHistoryStats));
                    this.RaisePropertyChanged(nameof(this.HasSalesHistoryException));
                    this.RaisePropertyChanged(nameof(this.SalesHistoryException));
                    this.RaisePropertyChanged(nameof(this.IsSearchHistoryPanelVisible));
                    this.RaisePropertyChanged(nameof(this.IsSearchHistoryPreparingPanelVisible));
                    this.RaisePropertyChanged(nameof(this.IsHQSalesHistoryEmpty));
                    this.RaisePropertyChanged(nameof(this.IsNQSalesHistoryEmpty));
                }
            }
        }

        #endregion

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
            foreach (MarketshareFilterItem selectedFilter in this.MarketshareSearchRequestData.SelectedFilterItems)
                result.Add(selectedFilter.ID);

            return result.ToArray();
        }
    }
}