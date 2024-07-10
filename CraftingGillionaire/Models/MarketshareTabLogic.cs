using CraftingGillionaire.API.GarlandTools;
using CraftingGillionaire.API.Saddlebag.API;
using CraftingGillionaire.API.Saddlebag;
using CraftingGillionaire.Models.CraftingAnalyzer;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CraftingGillionaire.Models.Marketshare;
using System.Collections.Generic;
using DynamicData;
using CraftingGillionaire.Models.User;
using System.Threading;

namespace CraftingGillionaire.Models
{
    public class MarketshareTabLogic : ReactiveObject
    {
        public MarketshareTabLogic(UserInfo userInfo)
        {
            this.UserInfo = userInfo;
            this.CraftingAnalyzerItem = new CraftingTreeRootNode();
            this.MarketshareInfos = new ObservableCollection<MarketshareInfo>();
            this.SearchRequestData = new MarketshareSearchRequestData(this)
            {
                ServerName = "Moogle",
                SalesAmount = 30,
                AveragePrice = 10000,
                TimePeriod = 168,
            };
        }
        private UserInfo UserInfo { get; }

        public ObservableCollection<MarketshareInfo> MarketshareInfos { get; set; }
        public MarketshareSearchRequestData SearchRequestData { get; set; }
        public CraftingTreeRootNode CraftingAnalyzerItem { get; set; }
        public bool IsStartSearchLabelVisible { get; set; } = true;
        public bool IsLoadingSearchResultPanelVisible { get; set; } = false;
        public bool IsSearchDataGridVisible { get; set; } = false;
        public bool IsSearchButtonVisible { get; set; } = true;
        public bool IsSaddlebagGridVisible { get; set; } = true;
        public bool IsCraftingAnalyzerVisible { get; set; } = false;
        public bool IsCraftingAnalyzerPreparingLabelVisible { get; set; } = false;
        public bool IsCraftingAnalyzerContentVisible { get; set; } = false;
        public bool HasGarlandToolsException { get; set; }
        public string GarlandToosException { get; set; } = String.Empty;
        public bool HasSaddlebagException { get; set; } = false;
        public string SaddlebagException { get; set; } = String.Empty;
        public bool IsFilterPanelVisible { get; set; } = false;
        public bool IsRowsFilterPanelVisible { get; set; } = false;

        public async Task OnMarketshareSearchClick()
        {
            this.IsSearchDataGridVisible = false;
            this.IsStartSearchLabelVisible = false;
            this.IsLoadingSearchResultPanelVisible = true;
            this.HasSaddlebagException = false;
            this.HasGarlandToolsException = false;
            this.IsFilterPanelVisible = false;

            this.RaisePropertyChanged(nameof(this.IsSearchDataGridVisible));
            this.RaisePropertyChanged(nameof(this.IsStartSearchLabelVisible));
            this.RaisePropertyChanged(nameof(this.IsLoadingSearchResultPanelVisible));
            this.RaisePropertyChanged(nameof(this.HasSaddlebagException));
            this.RaisePropertyChanged(nameof(this.HasGarlandToolsException));
            this.RaisePropertyChanged(nameof(this.IsFilterPanelVisible));

            ObservableCollection<MarketshareInfo> marketshareInfoList = await this.GetMarketshareInfo();
            this.MarketshareInfos.Clear();
            this.MarketshareInfos.AddRange(marketshareInfoList);

            if (!this.HasSaddlebagException && !this.HasGarlandToolsException)
            {
                if (this.SearchRequestData.RowsSelectedFilterItem != null)
                    this.FilterMarketshareInfo(this.SearchRequestData.RowsSelectedFilterItem.ID);

                this.IsSearchDataGridVisible = true;
                this.RaisePropertyChanged(nameof(this.IsSearchDataGridVisible));
            }

            this.IsLoadingSearchResultPanelVisible = false;
            this.RaisePropertyChanged(nameof(this.IsLoadingSearchResultPanelVisible));
        }

        private async Task<ObservableCollection<MarketshareInfo>> GetMarketshareInfo()
        {
            this.IsCraftingAnalyzerPreparingLabelVisible = false;
            this.RaisePropertyChanged(nameof(this.IsCraftingAnalyzerPreparingLabelVisible));

            ObservableCollection<MarketshareInfo> marketshareInfoList = new ObservableCollection<MarketshareInfo>();
            int[] filters = this.ConvertFiltersToIDs();
            MarketshareRequest request = SaddlebagHelper.CreateRequestObject(this.SearchRequestData, SortBy.PurchaseAmount, filters);
            int requestExceptionCount = 0;
            MarketshareResponseData? responseData = null;
            while (true)
            {
                responseData = await SaddlebagHelper.GetMarketshareResonseItemsAsync(request);
                if (!String.IsNullOrEmpty(responseData.Exception))
                {
                    if (requestExceptionCount > 5)
                    {
                        this.HasSaddlebagException = true;
                        this.SaddlebagException = responseData.Exception;
                        this.RaisePropertyChanged(nameof(this.HasSaddlebagException));
                        this.RaisePropertyChanged(nameof(this.SaddlebagException));
                        return new ObservableCollection<MarketshareInfo>();
                    }

                    requestExceptionCount++;
                    Thread.Sleep(2000);
                }
                else
                    break;
            }

            CraftingAnalyzerItemBuilder craftingAnalyzerItemBuilder = new CraftingAnalyzerItemBuilder(this.UserInfo, this.SearchRequestData.ServerName);
            foreach (MarketshareResonseItem responseItem in responseData.ResonseItems)
            {
                int itemID = Int32.Parse(responseItem.ItemID);
                int itemInfoExceptionCount = 0;
                ItemInfoResult? itemInfoResult = null;
                while (true)
                {
                    itemInfoResult = await GarlandToolsHelper.GetItemResponse(itemID);
                    if (itemInfoResult.HasException)
                    {
                        if (itemInfoExceptionCount > 5)
                        {
                            this.HasGarlandToolsException = true;
                            this.GarlandToosException = itemInfoResult.Exception;

                            this.RaisePropertyChanged(nameof(this.HasGarlandToolsException));
                            this.RaisePropertyChanged(nameof(this.GarlandToosException));
                            return new ObservableCollection<MarketshareInfo>();
                        }

                        itemInfoExceptionCount++;
                        Thread.Sleep(2000);
                    }
                    else
                        break;
                }

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

            return marketshareInfoList;
        }

        private void FilterMarketshareInfo(int mode)
        {
            ObservableCollection<MarketshareInfo> result = new ObservableCollection<MarketshareInfo>();
            if (mode == 0)
            {
                return;
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

            this.MarketshareInfos.Clear();
            foreach (MarketshareInfo marketshareInfo in result)
                this.MarketshareInfos.Add(marketshareInfo);
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

        public void OnFiltersButtonClick()
        {
            this.IsFilterPanelVisible = true;
            this.IsSearchDataGridVisible = false;
            this.IsStartSearchLabelVisible = false;
            this.IsLoadingSearchResultPanelVisible = false;
            this.HasSaddlebagException = false;
            this.IsRowsFilterPanelVisible = false;

            this.RaisePropertyChanged(nameof(this.IsFilterPanelVisible));
            this.RaisePropertyChanged(nameof(this.IsSearchDataGridVisible));
            this.RaisePropertyChanged(nameof(this.IsStartSearchLabelVisible));
            this.RaisePropertyChanged(nameof(this.IsLoadingSearchResultPanelVisible));
            this.RaisePropertyChanged(nameof(this.HasSaddlebagException));
            this.RaisePropertyChanged(nameof(this.IsRowsFilterPanelVisible));
        }

        public void OnFiltersPanelOkClick()
        {
            this.IsFilterPanelVisible = false;
            this.SearchRequestData.OnFiltersPanelOkClick();
            this.IsStartSearchLabelVisible = true;

            this.RaisePropertyChanged(nameof(this.IsFilterPanelVisible));
            this.RaisePropertyChanged(nameof(this.IsStartSearchLabelVisible));
        }

        public void OnRowsFilterButtonClick()
        {
            this.IsRowsFilterPanelVisible = true;
            this.IsSearchDataGridVisible = false;
            this.IsStartSearchLabelVisible = false;
            this.IsLoadingSearchResultPanelVisible = false;
            this.HasSaddlebagException = false;
            this.IsFilterPanelVisible = false;

            this.RaisePropertyChanged(nameof(this.IsRowsFilterPanelVisible));
            this.RaisePropertyChanged(nameof(this.IsSearchDataGridVisible));
            this.RaisePropertyChanged(nameof(this.IsStartSearchLabelVisible));
            this.RaisePropertyChanged(nameof(this.IsLoadingSearchResultPanelVisible));
            this.RaisePropertyChanged(nameof(this.HasSaddlebagException));
            this.RaisePropertyChanged(nameof(this.IsFilterPanelVisible));
        }

        public void OnRowsFiltersPanelOkClick()
        {
            this.IsRowsFilterPanelVisible = false;
            this.SearchRequestData.OnRowsFiltersPanelOkClick();
            this.IsStartSearchLabelVisible = true;

            this.RaisePropertyChanged(nameof(this.IsRowsFilterPanelVisible));
            this.RaisePropertyChanged(nameof(this.IsStartSearchLabelVisible));
        }

        public void OnCraftingAnalyzerBackClick()
        {
            this.IsSaddlebagGridVisible = true;
            this.IsSearchButtonVisible = true;
            this.IsCraftingAnalyzerVisible = false;

            this.RaisePropertyChanged(nameof(this.IsSaddlebagGridVisible));
            this.RaisePropertyChanged(nameof(this.IsSearchButtonVisible));
            this.RaisePropertyChanged(nameof(this.IsCraftingAnalyzerVisible));
        }

        public async void OnAnalyzeClick(MarketshareInfo marketshareInfo)
        {
            this.IsSaddlebagGridVisible = false;
            this.IsSearchButtonVisible = false;
            this.IsCraftingAnalyzerVisible = true;
            this.IsCraftingAnalyzerPreparingLabelVisible = true;
            this.IsCraftingAnalyzerContentVisible = false;
            this.HasGarlandToolsException = false;
            this.HasSaddlebagException = false;

            this.RaisePropertyChanged(nameof(this.IsSaddlebagGridVisible));
            this.RaisePropertyChanged(nameof(this.IsSearchButtonVisible));
            this.RaisePropertyChanged(nameof(this.IsCraftingAnalyzerVisible));
            this.RaisePropertyChanged(nameof(this.IsCraftingAnalyzerPreparingLabelVisible));
            this.RaisePropertyChanged(nameof(this.IsCraftingAnalyzerContentVisible));
            this.RaisePropertyChanged(nameof(this.HasGarlandToolsException));
            this.RaisePropertyChanged(nameof(this.HasSaddlebagException));

            CraftingAnalyzerItemBuilder craftingAnalyzerItemBuilder = new CraftingAnalyzerItemBuilder(this.UserInfo, this.SearchRequestData.ServerName);
            await craftingAnalyzerItemBuilder.FillTreeCosts(marketshareInfo.TreeRootNode, marketshareInfo.QuantitySold);
            this.IsCraftingAnalyzerPreparingLabelVisible = false;
            this.IsCraftingAnalyzerContentVisible = true;
            this.CraftingAnalyzerItem = marketshareInfo.TreeRootNode;

            this.RaisePropertyChanged(nameof(this.IsCraftingAnalyzerPreparingLabelVisible));
            this.RaisePropertyChanged(nameof(this.IsCraftingAnalyzerContentVisible));
            this.RaisePropertyChanged(nameof(this.CraftingAnalyzerItem));
        }

        private int[] ConvertFiltersToIDs()
        {
            List<int> result = new List<int>();
            foreach (MarketshareFilterItem selectedFilter in this.SearchRequestData.SelectedFilterItems)
                result.Add(selectedFilter.ID);

            return result.ToArray();
        }
    }
}
