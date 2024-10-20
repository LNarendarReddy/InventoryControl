using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NSRetailLiteApp.Models;
using NSRetailLiteApp.ViewModels.StockCounting;
using NSRetailLiteApp.Views.StockCounting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels
{
    internal partial class HomePageViewModel : BaseViewModel
    {
        [ObservableProperty]
        public LoggedInUser _model;

        public static LoggedInUser User { get; private set; }

        public HomePageViewModel(LoggedInUser loggedInUser) 
        {
            LogoutCommand = new AsyncRelayCommand(Logout);
            OpenStockCountingCommand = new AsyncRelayCommand(OpenStockCounting);
            _model = loggedInUser;
            User = loggedInUser;
        }

        public IAsyncRelayCommand LogoutCommand { get; }

        private async Task Logout()
        {
            if(Application.Current == null || Application.Current.MainPage == null) return;

            bool confirm = await DisplayAlert("Confirm", "Are you sure to logout?", "Yes", "No");
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

            if (stockCounting.Exception != null)
            {
                if (stockCounting.Exception.Message != "No counting data found")
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

                BranchSelectionViewModel branchSelectionViewModel = new BranchSelectionViewModel(holderClass.Branch);
                await ShowPopup(holderClass, new BranchSelectionPage(branchSelectionViewModel));

                if (branchSelectionViewModel.SelectedBranch == null) return;

                if (!await Application.Current.MainPage.DisplayAlert("Confirm", $"Are you sure you want to start counting for {branchSelectionViewModel.SelectedBranch.BranchName}?", "Yes", "No")) return;

                holderClass = new HolderClass();
                PostAsync("stockcounting/savecounting", ref holderClass
                , new Dictionary<string, string?>()
                {
                    { "StockCountingID", "0" },
                    { "UserID", Model.UserId.ToString() },
                    { "BranchID", branchSelectionViewModel.SelectedBranch.BranchID.ToString() }
                }, true);

                if(holderClass.Exception != null) return;

                stockCounting.StockCountingId = holderClass.GenericID;

                GetAsync("stockcounting/getcounting", ref stockCounting
                , new Dictionary<string, string?>()
                {
                    { "UserID", Model.UserId.ToString() },
                    { "isNested", "True" }
                }, true);
            }

            if(stockCounting.StockCountingId <= 0)
            {
                DisplayErrorMessage("Something went wrong");
                return;
            }

            RedirectToPage(stockCounting, new StockCountingDetailListPage(new StockCountingDetailListViewModel(stockCounting, Model.UserId)));
        }
    }
}
