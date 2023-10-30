using ReactiveUI;
using System;

namespace CraftingGillionaire.Models
{
    public class SalesHistorySearchRequestData: ReactiveObject
    {
        public SalesHistorySearchRequestData(SalesHistoryTabLogic tabLogic) 
        { 
            this.TabLogic = tabLogic;
        }

        public SalesHistoryTabLogic TabLogic { get; }

        public string ServerName { get; set; }

        public string ItemName { get; set; }

        public int TimePeriod { get; set; }
        public string TimePeriodString
        {
            get => this.TimePeriod.ToString();
            set
            {
                if (Int32.TryParse(value, out int timePeriod))
                {
                    this.TimePeriod = timePeriod;
                    this.RaisePropertyChanged(nameof(this.TimePeriodString));
                }
            }
        }

        public async void OnSalesHistorySearchClick()
        {
            await this.TabLogic.OnSalesHistorySearchClick();
        }
    }
}
