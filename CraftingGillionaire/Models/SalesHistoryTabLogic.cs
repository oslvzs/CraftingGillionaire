using CraftingGillionaire.API.Universalis.API;
using CraftingGillionaire.API.Universalis;
using CraftingGillionaire.API.XIVAPI;
using CraftingGillionaire.Models.SalesHistory;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CraftingGillionaire.Models
{
    public class SalesHistoryTabLogic: ReactiveObject
    {
        public SalesHistoryTabLogic()
        {
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
        }

        public SalesHistorySearchRequestData SalesHistoryRequestData { get; set; }
        public bool IsStartSearchHistoryLabelVisible { get; set; } = true;
        public bool HasSalesHistoryException { get; set; } = false;
        public string SalesHistoryException { get; set; } = String.Empty;
        public bool IsSearchHistoryPreparingPanelVisible { get; set; } = false;
        public bool IsSearchHistoryPanelVisible { get; set; } = false;
        public ObservableCollection<SaleDisplayItem> NQSalesHistory { get; set; }
        public ObservableCollection<SaleDisplayItem> HQSalesHistory { get; set; }
        public SalesHistoryStats NQSalesHistoryStats { get; set; }
        public SalesHistoryStats HQSalesHistoryStats { get; set; }
        public bool IsNQSalesHistoryEmpty { get; set; } = false;
        public bool IsHQSalesHistoryEmpty { get; set; } = false;

        public async Task OnSalesHistorySearchClick()
        {
            this.IsStartSearchHistoryLabelVisible = false;
            this.HasSalesHistoryException = false;
            this.SalesHistoryException = String.Empty;
            this.IsSearchHistoryPanelVisible = false;
            this.IsSearchHistoryPreparingPanelVisible = true;

            this.RaisePropertyChanged(nameof(this.IsStartSearchHistoryLabelVisible));
            this.RaisePropertyChanged(nameof(this.IsSearchHistoryPanelVisible));
            this.RaisePropertyChanged(nameof(this.HasSalesHistoryException));
            this.RaisePropertyChanged(nameof(this.SalesHistoryException));
            this.RaisePropertyChanged(nameof(this.IsSearchHistoryPanelVisible));
            this.RaisePropertyChanged(nameof(this.IsSearchHistoryPreparingPanelVisible));

            ItemIDResult itemIDResult = await XIVAPIHelper.GetItemNameByID(this.SalesHistoryRequestData.ItemName);
            if (itemIDResult.HasException)
            {
                this.HasSalesHistoryException = true;
                this.SalesHistoryException = itemIDResult.Exception;
                this.IsSearchHistoryPreparingPanelVisible = false;
                this.IsSearchHistoryPanelVisible = false;

                this.RaisePropertyChanged(nameof(this.HasSalesHistoryException));
                this.RaisePropertyChanged(nameof(this.SalesHistoryException));
                this.RaisePropertyChanged(nameof(this.IsSearchHistoryPreparingPanelVisible));
                this.RaisePropertyChanged(nameof(this.IsSearchHistoryPanelVisible));
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
                    this.RaisePropertyChanged(nameof(this.IsSearchHistoryPreparingPanelVisible));
                    this.RaisePropertyChanged(nameof(this.IsStartSearchHistoryLabelVisible));
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

                    this.RaisePropertyChanged(nameof(this.IsNQSalesHistoryEmpty));
                    this.RaisePropertyChanged(nameof(this.IsHQSalesHistoryEmpty));
                    this.RaisePropertyChanged(nameof(this.HasSalesHistoryException));
                    this.RaisePropertyChanged(nameof(this.SalesHistoryException));
                    this.RaisePropertyChanged(nameof(this.IsSearchHistoryPreparingPanelVisible));
                    this.RaisePropertyChanged(nameof(this.IsSearchHistoryPanelVisible));
                    this.RaisePropertyChanged(nameof(this.NQSalesHistoryStats));
                    this.RaisePropertyChanged(nameof(this.HQSalesHistoryStats));
                }
            }
        }
    }
}
