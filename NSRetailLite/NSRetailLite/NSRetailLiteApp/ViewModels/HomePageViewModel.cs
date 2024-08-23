using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NSRetailLiteApp.Models;
using NSRetailLiteApp.ViewModels.StockCounting;
using NSRetailLiteApp.Views.StockCounting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels
{
    internal partial class HomePageViewModel : BaseViewModel
    {
        [ObservableProperty]
        public LoggedInUser _model;

        public HomePageViewModel(LoggedInUser loggedInUser) 
        {
            LogoutCommand = new AsyncRelayCommand(Logout);
            OpenStockCountingCommand = new AsyncRelayCommand(OpenStockCounting);
            _model = loggedInUser;
        }

        public IAsyncRelayCommand LogoutCommand { get; }

        private async Task Logout()
        {
            if(Application.Current == null || Application.Current.MainPage == null) return;

            bool confirm = await Application.Current.MainPage.DisplayAlert("Confirm", "Are you sure to logout?", "Yes", "No");
            if (confirm)
                Application.Current?.MainPage?.Navigation.PopAsync();
        }

        public IAsyncRelayCommand OpenStockCountingCommand { get; }

        private async Task OpenStockCounting()
        {
            if (Application.Current == null || Application.Current.MainPage == null) return;

            StockCountingModel stockCounting = new();

            GetAsync("stockcounting/getcounting", ref stockCounting
                , new Dictionary<string, string?>() 
                { 
                    { "UserID", Model.UserId.ToString() },
                    { "isNested", "True" }
                }, false);

            if(stockCounting.Exception != null) 
            {
                if(stockCounting.Exception.Message != "No counting data found")
                {
                    DisplayErrorMessage(stockCounting.Exception.Message);
                    return;
                }

                HolderClass holderClass = new HolderClass();

                GetAsync("stockcounting/getbranch", ref holderClass
                    , new Dictionary<string, string?>()
                {
                    { "UserID", Model.UserId.ToString() },
                    { "isNested", "True" }
                });
                Application.Current?.MainPage?.Navigation.PushAsync(new BranchSelectionPage(new BranchSelectionViewModel(holderClass.Branch)));
            }

            if(stockCounting.StockCountingId <= 0)
            {
                DisplayErrorMessage("Something went wrong");
                return;
            }

            Application.Current?.MainPage?.Navigation.PushAsync(new StockCountingDetailListPage(new StockCountingDetailListViewModel(stockCounting)));
        }
    }
}
