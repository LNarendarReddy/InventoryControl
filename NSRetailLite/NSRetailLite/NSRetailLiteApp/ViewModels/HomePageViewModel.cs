using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.PlatformConfiguration;
using NSRetailLiteApp.Models;
using NSRetailLiteApp.ViewModels.Billing;
using NSRetailLiteApp.ViewModels.Common;
using NSRetailLiteApp.ViewModels.StockCounting;
using NSRetailLiteApp.Views.Billing;
using NSRetailLiteApp.Views.Common;
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
            OpenBillingCommand = new AsyncRelayCommand(OpenBilling);
            DayclosureCommand = new AsyncRelayCommand(Dayclose);
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

                HolderClass holderClass = new();

                GetAsync("stockcounting/getbranch", ref holderClass
                    , new Dictionary<string, string?>()
                {
                    { "UserID", Model.UserId.ToString() },
                    { "isNested", "True" }
                });

                BranchSelectionViewModel branchSelectionViewModel = new BranchSelectionViewModel(holderClass.Holder.BranchList);
                await ShowPopup(holderClass, new BranchSelectionPage(branchSelectionViewModel));

                if (branchSelectionViewModel.SelectedBranch == null) return;

                string location = await Application.Current?.MainPage?.DisplayPromptAsync("Location", "Enter the stock location for counting:", "OK");

                if(string.IsNullOrEmpty(location)) return;

                if (!await Application.Current.MainPage.DisplayAlert("Confirm"
                    , $"Are you sure you want to start counting for {branchSelectionViewModel.SelectedBranch.BranchName} in location {location}?"
                    , "Yes", "No")) return;

                holderClass = new HolderClass();
                PostAsync("stockcounting/savecounting", ref holderClass
                , new Dictionary<string, string?>()
                {
                    { "StockCountingID", "0" },
                    { "UserID", Model.UserId.ToString() },
                    { "BranchID", branchSelectionViewModel.SelectedBranch.BranchID.ToString() },
                    { "StockLocationName", location }
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

            await RedirectToPage(stockCounting, new StockCountingDetailListPage(new StockCountingDetailListViewModel(stockCounting, Model.UserId)));
        }

        public IAsyncRelayCommand OpenBillingCommand { get; }

        private async Task OpenBilling()
        {
            if (Application.Current == null || Application.Current.MainPage == null) return;

            int counterId = await GetCounterId();

            if (counterId <= 0) return;

            DaySequence daySequence = new();
            GetAsync("billing/getinitialload", ref daySequence
                , new Dictionary<string, string?>()
                {
                    { "userID", Model.UserId.ToString() },
                    { "branchCounterID", counterId.ToString() }
                });

            await RedirectToPage(daySequence, new BillingPage(new Billing.BillingViewModel(daySequence.BillList.FirstOrDefault(), counterId, daySequence.DaySequenceId)));
        }

        private async Task<int> GetCounterId()
        {

            string device_id = string.Empty;
            int counterId = 0;

#if ANDROID
            device_id =  Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
#endif

            HolderClass holder = new();

            GetAsync("billing/getcounterbyidentifier", ref holder
                , new Dictionary<string, string?>()
                {
                    { "Identifier", device_id }
                }, false);

            if (holder.Exception != null || holder.GenericID <= 0)
            {
                Branch branch = new();
                GetAsync("billing/getcounters", ref branch
                    , new Dictionary<string, string?>()
                    {
                        { "BranchID", Model.BranchId.ToString() }
                    }, true);

                BranchCounterSelectionViewModel branchCounterSelectionViewModel = new(branch.BranchCounterList);
                await ShowPopup(branch, new BranchCounterSelectionPage(branchCounterSelectionViewModel));
                if (branchCounterSelectionViewModel.SelectedBranchCounter == null) return counterId;

                holder = new();
                PostAsync("billing/savecounteridentifier", ref holder
                    , new Dictionary<string, string?>()
                    {
                        { "Identifier", device_id },
                        { "CounterId",  branchCounterSelectionViewModel.SelectedBranchCounter.CounterId.ToString() }
                    }, true);

                if (holder.Exception != null) { return counterId; }

                holder = new();
                GetAsync("billing/getcounterbyidentifier", ref holder
                    , new Dictionary<string, string?>()
                    {
                        { "Identifier", device_id }
                    }, true);

                if (holder.Exception != null) { return counterId; }

                counterId = holder.GenericID;
            }
            else
            {
                counterId = holder.GenericID;
            }

            if (counterId <= 0)
            {
                DisplayErrorMessage("Something went wrong, counter not set");
            }

            return counterId;
        }

        public IAsyncRelayCommand DayclosureCommand { get; }

        private async Task Dayclose()
        {
            await DisplayAlert("Info", "Any open bills will be voided by default", "OK");

            if (!await DisplayAlert("Confirm", "Are you sure you want to close billing? This operation cannot be undone", "Yes", "No"))
                return;

            HolderClass holder = new();
            int counterId = await GetCounterId();
            GetAsync("Billing/getdayclosure", ref holder, new Dictionary<string, string?>() { { "CounterId", counterId.ToString() } });

            if (holder.Exception != null) return;

            DaySequence daySequence = new();
            GetAsync("billing/getinitialload", ref daySequence
                , new Dictionary<string, string?>()
                {
                    { "userID", Model.UserId.ToString() },
                    { "branchCounterID", counterId.ToString() }
                });

            await RedirectToPage(holder
                , new DayClosurePage(
                    new DayClosureViewModel(
                        holder.Holder.DenominationList
                        , holder.MOPList
                        , holder.RefundList
                        , counterId
                        , daySequence.DaySequenceId)));
        }
    }
}
