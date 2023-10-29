using CraftingGillionaire.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingGillionaire.Models
{
    public class SalesHistorySearchRequestData
    {
        public SalesHistorySearchRequestData(MainWindowViewModel mainWindowViewModel)
        {
            this.MainWindowViewModel = mainWindowViewModel;
        }

        public MainWindowViewModel MainWindowViewModel { get; }

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
                    this.MainWindowViewModel.RaisePropertyChanged(nameof(TimePeriodString));
                }
            }
        }
    }
}
