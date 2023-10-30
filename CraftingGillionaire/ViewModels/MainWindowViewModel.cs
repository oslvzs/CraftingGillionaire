using CraftingGillionaire.Models;
using ReactiveUI;
using CraftingGillionaire.Models.User;
using System.IO;
using System.Text.Json;

namespace CraftingGillionaire.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            this.UserInfo = new UserInfo();
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

            this.MarketshareTabLogic = new MarketshareTabLogic(this.UserInfo);
            this.SalesHistoryTabLogic = new SalesHistoryTabLogic();            
        }

        public bool IsSplitViewPaneOpen { get; private set; } = true;
        public UserInfo UserInfo { get; private set; }
        public MarketshareTabLogic MarketshareTabLogic { get; set; }
        public SalesHistoryTabLogic SalesHistoryTabLogic { get; set; }

        public void OnSaveClick()
        {
            string text = JsonSerializer.Serialize(this.UserInfo);
            string fileName = "user.json";
            File.WriteAllText(fileName, text);
            this.IsSplitViewPaneOpen = false;
            this.RaisePropertyChanged(nameof(this.IsSplitViewPaneOpen));
            this.RaisePropertyChanged(nameof(this.UserInfo));
        }

        public void OnSettingsClick()
        {
            this.IsSplitViewPaneOpen = !this.IsSplitViewPaneOpen;
            this.RaisePropertyChanged(nameof(this.IsSplitViewPaneOpen));
        }

        internal void UpdateMarketshareTab()
        {
            this.RaisePropertyChanged(nameof(this.MarketshareTabLogic));
        }

        internal void UpdateSalesHistoryTab()
        {
            this.RaisePropertyChanged(nameof(this.SalesHistoryTabLogic));
        }
    }
}