﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.PlatformConfiguration;
using NSRetailLiteApp.Models;
using NSRetailLiteApp.ViewModels.Billing;
using NSRetailLiteApp.ViewModels.Common;
using NSRetailLiteApp.ViewModels.DispatchReceive;
using NSRetailLiteApp.ViewModels.ItemDetails;
using NSRetailLiteApp.ViewModels.StockCounting;
using NSRetailLiteApp.ViewModels.StockDispatch.Indent;
using NSRetailLiteApp.Views.Billing;
using NSRetailLiteApp.Views.Common;
using NSRetailLiteApp.Views.DispatchReceive;
using NSRetailLiteApp.Views.ItemDetails;
using NSRetailLiteApp.Views.StockCounting;
using NSRetailLiteApp.Views.StockDispatch;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel.Channels;
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
            ChangePasswordCommand = new AsyncRelayCommand(ChangePassword);
            CustomerRefundCommand = new AsyncRelayCommand(CustomerRefund);
            ItemDetailsCommand = new AsyncRelayCommand(ItemDetails);
            StockDispatchCommand = new AsyncRelayCommand(StockDispatch);
            DispatchRecieveCommand = new AsyncRelayCommand(DispatchReceive);

            _model = loggedInUser;
            User = loggedInUser;
        }

        public IAsyncRelayCommand LogoutCommand { get; }

        private async Task Logout()
        {
            if (Application.Current == null || Application.Current.MainPage == null) return;

            bool confirm = await DisplayAlert("Confirm", "Are you sure to logout?", "Yes", "No");
            if (confirm)
                Application.Current?.MainPage?.Navigation.PopAsync();
        }

        public IAsyncRelayCommand OpenStockCountingCommand { get; }

        private async Task OpenStockCounting()
        {
            if (Application.Current == null || Application.Current.MainPage == null) return;

            StockCountingModel stockCounting = new();

            stockCounting = await GetAsync("stockcounting/getcounting", stockCounting
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

                holderClass = await GetAsync("stockcounting/getbranch", holderClass
                    , new Dictionary<string, string?>()
                {
                    { "UserID", Model.UserId.ToString() },
                    { "isNested", "True" }
                });

                BranchSelectionViewModel branchSelectionViewModel = new BranchSelectionViewModel(holderClass.Holder.BranchList);
                await ShowPopup(holderClass, new BranchSelectionPage(branchSelectionViewModel));

                if (branchSelectionViewModel.SelectedBranch == null) return;

                string location = await Application.Current?.MainPage?.DisplayPromptAsync("Location", "Enter the stock location for counting:", "OK");

                if (string.IsNullOrEmpty(location)) return;

                if (!await Application.Current.MainPage.DisplayAlert("Confirm"
                    , $"Are you sure you want to start counting for {branchSelectionViewModel.SelectedBranch.BranchName} in location {location}?"
                    , "Yes", "No")) return;

                holderClass = new HolderClass();
                holderClass = await PostAsync("stockcounting/savecounting", holderClass
                , new Dictionary<string, string?>()
                {
                    { "StockCountingID", "0" },
                    { "UserID", Model.UserId.ToString() },
                    { "BranchID", branchSelectionViewModel.SelectedBranch.BranchID.ToString() },
                    { "StockLocationName", location }
                }, true);

                if (holderClass.Exception != null) return;

                stockCounting.StockCountingId = holderClass.GenericID;

                stockCounting = await GetAsync("stockcounting/getcounting", stockCounting
                , new Dictionary<string, string?>()
                {
                    { "UserID", Model.UserId.ToString() },
                    { "isNested", "True" }
                }, true);
            }

            if (stockCounting.StockCountingId <= 0)
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
            daySequence = await GetAsync("billing/getinitialload", daySequence
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
            device_id = Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
#endif

            HolderClass holder = new();

            holder = await GetAsync("billing/getcounterbyidentifier", holder
                , new Dictionary<string, string?>()
                {
                    { "Identifier", device_id },
                    { "BranchId", Model.BranchId.ToString() },
                }, false);

            if (holder.Exception != null || holder.GenericID <= 0)
            {
                Branch branch = new();
                branch = await GetAsync("billing/getcounters", branch
                    , new Dictionary<string, string?>()
                    {
                        { "BranchID", Model.BranchId.ToString() }
                    }, true);

                BranchCounterSelectionViewModel branchCounterSelectionViewModel = new(branch.BranchCounterList);
                await ShowPopup(branch, new BranchCounterSelectionPage(branchCounterSelectionViewModel));
                if (branchCounterSelectionViewModel.SelectedBranchCounter == null) return counterId;

                holder = new();
                holder = await PostAsync("billing/savecounteridentifier", holder
                    , new Dictionary<string, string?>()
                    {
                        { "Identifier", device_id },
                        { "CounterId",  branchCounterSelectionViewModel.SelectedBranchCounter.CounterId.ToString() }
                    }, true);

                if (holder.Exception != null) { return counterId; }

                holder = new();
                holder = await GetAsync("billing/getcounterbyidentifier", holder
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
            holder = await GetAsync("Billing/getdayclosure", holder, new Dictionary<string, string?>() { { "CounterId", counterId.ToString() } });

            if (holder.Exception != null) return;

            DaySequence daySequence = new();
            daySequence = await GetAsync("billing/getinitialload", daySequence
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


        public IAsyncRelayCommand ChangePasswordCommand { get; }

        private async Task ChangePassword()
        {
            await RedirectToPage(Model, new ChangePasswordPage(new ChangePasswordViewModel(Model)));
        }

        public IAsyncRelayCommand CustomerRefundCommand { get; }

        private async Task CustomerRefund()
        {
            int counterId = await GetCounterId();

            string billnumber = await Application.Current?.MainPage?.DisplayPromptAsync("Customer refund", "Enter\\scan the bill number for refund", "OK");
            if (string.IsNullOrEmpty(billnumber)) return;

            HolderClass holderClass = new();
            holderClass = await GetAsync("CRefund/GetBillByNumber", holderClass, new Dictionary<string, string?>()
            {
                { "billNumber", billnumber },
                { "branchCounterId", counterId.ToString() }
            });

            await RedirectToPage(holderClass, new CustomerRefundPage(new CustomerRefundViewModel(holderClass.CR_Bill, counterId)));
        }

        public IAsyncRelayCommand ItemDetailsCommand { get; }

        private async Task ItemDetails()
        {
            string eanCode = await Application.Current?.MainPage?.DisplayPromptAsync("Item Detail", "Enter\\scan the EAN Code", "OK");
            if (string.IsNullOrEmpty(eanCode)) return;

            HolderClass holderClass = new();
            holderClass = await GetAsync("Item/getItem", holderClass, new Dictionary<string, string?>()
            {
                { "ItemCode", eanCode }
            });

            Item item = holderClass.Item;

            if (item == null || item.ItemCodeList == null || !item.ItemCodeList.Any()) return;

            ItemCodeData selectedItemCode;

            if (item.ItemCodeList.Count > 1)
            {
                ItemCodeSelectionViewModel itemCodeSelectionViewModel = new ItemCodeSelectionViewModel(item);
                await base.ShowPopup(item, new ItemCodeSelectionPage(itemCodeSelectionViewModel));
                selectedItemCode = itemCodeSelectionViewModel.SelectedItemCode;
            }
            else
            {
                selectedItemCode = item.ItemCodeList[0];
            }

            if (selectedItemCode == null) return;

            holderClass = new();
            holderClass = await GetAsync("Item/getItemdata", holderClass, new Dictionary<string, string?>()
            {
                { "ItemCodeID", selectedItemCode.ItemCodeID },
                { "BranchID", HomePageViewModel.User.BranchId.ToString() }
            });

            await RedirectToPage(holderClass, new ItemDetailsPage(new ItemDetailsViewModel(item, holderClass.ItemCode)));
        }

        public IAsyncRelayCommand StockDispatchCommand { get; }

        private async Task StockDispatch()
        {
            if (Application.Current == null || Application.Current.MainPage == null) return;

            if (User.CategoryId == 13)
            {
                await DisplayAlert("Error", $"User {User.FullName} with All Category cannot perform stock dispatch", "Ok");
                return;
            }

            StockDispatchTypeSelectionPage stockDispatchTypeSelectionPage = new StockDispatchTypeSelectionPage(
                new StockDispatch.StockDispatchTypeSelectionViewModel(User));

            await ShowPopup(null, stockDispatchTypeSelectionPage);

            if (stockDispatchTypeSelectionPage.SelectedDispatchType == "Generate Branch Indent")
            {
                await GenerateBranchIndent();
                return;
            }
            else if (stockDispatchTypeSelectionPage.SelectedDispatchType == "View Branch Indents")
            {
                await ViewBranchIndent();
                return;
            }
            else if (stockDispatchTypeSelectionPage.SelectedDispatchType == "Indent based Dispatch")
            {
                await IndentBasedDispatch();
                return;
            }
            else if (stockDispatchTypeSelectionPage.SelectedDispatchType == "Manual Dispatch")
            {
                await ManualDispatch();
                return;
            }
        }

        private async Task<Branch> GetBranchSelection(bool showAllBranches)
        {
            HolderClass holderClass = new HolderClass();
            holderClass = await GetAsync("stockdispatch_v2/getbranch", holderClass
                   , new Dictionary<string, string?>()
                {
                    { "UserID", Model.UserId.ToString() },
                    { "IsManualDispatch", showAllBranches.ToString() }
                });

            BranchSelectionViewModel branchSelectionViewModel = new BranchSelectionViewModel(holderClass.Holder.BranchList);
            await ShowPopup(holderClass, new BranchSelectionPage(branchSelectionViewModel));

            return branchSelectionViewModel.SelectedBranch;
        }

        private async Task GenerateBranchIndent()
        {
            HolderClass holderClass = new HolderClass(); // reset exception

            Branch branch = await GetBranchSelection(User.SubCategoryId == 0);

            if (branch == null) return;

            holderClass = new HolderClass(); // reset exception

            string noOfDays = await Application.Current?.MainPage?.DisplayPromptAsync(
                        $"{branch.BranchName}"
                        , "Enter the no. of Indent days:"
                        , keyboard: Keyboard.Numeric);

            if (string.IsNullOrEmpty(noOfDays)) return;

            if (!await DisplayAlert("Confirm"
                , $"Do you want to run Branch Indent for {branch.BranchName} for {noOfDays} days? The operation can take some time"
                , "Yes", "No")) return;

            holderClass = await GetAsync("Stockdispatch_v2/getbranchindent", holderClass, new Dictionary<string, string?>()
                            {
                                { "BranchID", branch.BranchID.ToString() }
                                , { "CategoryID", User.CategoryId.ToString() }
                                , { "NoOfDays", noOfDays }
                                , { "SubCategoryID", User.SubCategoryId.ToString() }
                                , { "ISMobileCall", "true" }
                            }, timeOut: 120);

            await RedirectToPage(holderClass
                    , new StockDispatchPage(
                        new StockDispatchViewModel(holderClass.StockDispatch, User)));
        }

        private async Task ViewBranchIndent()
        {
            HolderClass holderClass = new HolderClass();

            holderClass = await GetAsync("Stockdispatch_v2/Getbranchindentlist", holderClass, new Dictionary<string, string?>()
                                    {
                                        { "CategoryID", User.CategoryId.ToString() }
                                        , { "ISMobileCall", "true" }
                                    }, timeOut: 120);
            await RedirectToPage(holderClass, new StockDispatchIndentListPage(
                new StockDispatch.StockDispatchIndentListViewModel(holderClass.Holder.BranchIndentList, Model)));
        }

        private async Task IndentBasedDispatch()
        {
            HolderClass holderClass = new();

            holderClass = await GetAsync("Stockdispatch_v2/getdispatchwithbi", holderClass
                   , new Dictionary<string, string?>()
                    {
                        { "UserID", Model.UserId.ToString() },
                        { "IsManualDispatch", false.ToString() }
                    }, displayAlert: false);

            if (holderClass.StockDispatch == null)
            {
                holderClass = new(); // reset exception

                Branch branch = await GetBranchSelection(User.SubCategoryId == 0);
                if (branch == null) return;

                if (!await DisplayAlert("Confirm"
                               , $"Do you want to start dispatch for {branch.BranchName}?"
                            , "Yes", "No")) return;

                StockDispatchModel model = new StockDispatchModel()
                {
                    UserId = User.UserId
                    , ToBranchId = branch.BranchID
                    , FromBranchId = User.BranchId
                    , CategoryId = User.CategoryId
                    , SubCategoryId = User.SubCategoryId
                    , BranchIndentId = branch.BranchIndentID
                };

                holderClass.StockDispatch = await PostAsync("Stockdispatch_v2/savedispatch", model, new Dictionary<string, string?>()
                {
                    { "StockDispatchID", 0.ToString() },
                    { "FromBranchID", User.BranchId.ToString() },
                    { "ToBranchID",  branch.BranchID.ToString() },
                    { "CategoryId", User.CategoryId.ToString() },
                    { "SubCategoryId", User.SubCategoryId.ToString() },
                    { "BranchIndentId", branch.BranchIndentID.ToString() },
                    { "UserId", User.UserId.ToString() }
                });
            }

            await RedirectToPage(holderClass
                    , new StockDispatchPage(
                        new StockDispatchViewModel(holderClass.StockDispatch, User)));
        }

        private async Task ManualDispatch()
        {
            HolderClass holderClass = new();

            holderClass = await GetAsync("Stockdispatch_v2/getdispatchwithbi", holderClass
                   , new Dictionary<string, string?>()
                    {
                        { "UserID", Model.UserId.ToString() },
                        { "IsManualDispatch", true.ToString() }
                    }, displayAlert: false);

            if (holderClass.StockDispatch == null)
            {
                holderClass = new(); // reset exception

                Branch branch = await GetBranchSelection(true);
                if (branch == null) return;

                if (!await DisplayAlert("Confirm"
                        , $"Do you create a new manual stock dispatch for {branch.BranchName}?"
                        , "Yes", "No")) return;

                StockDispatchModel model = new StockDispatchModel()
                {
                    UserId = Model.UserId,
                    ToBranchId = branch.BranchID,
                    FromBranchId = User.BranchId
                };

                holderClass.StockDispatch = await PostAsync("Stockdispatch_v2/savedispatch", model, new Dictionary<string, string?>()
                {
                    { "StockDispatchID", 0.ToString() },
                    { "FromBranchID", User.BranchId.ToString() },
                    { "ToBranchID",  branch.BranchID.ToString() },
                    { "CategoryId", User.CategoryId.ToString() },
                    { "SubCategoryId", User.SubCategoryId.ToString() },
                    { "UserId", User.UserId.ToString() }
                });
            }

            await RedirectToPage(holderClass
                    , new StockDispatchPage(
                        new StockDispatchViewModel(holderClass.StockDispatch, User)));
        }

        public IAsyncRelayCommand DispatchRecieveCommand { get; }

        private async Task DispatchReceive()
        {
            if (Application.Current == null || Application.Current.MainPage == null) return;

            DispatchReceiveTypeSelectionPage dispatchTypeReceiveTypeSelectionPage = new DispatchReceiveTypeSelectionPage();

            await ShowPopup(null, dispatchTypeReceiveTypeSelectionPage);

            if (dispatchTypeReceiveTypeSelectionPage.SelectedDispatchRecieveType == "View open dispatches")
            {
                HolderClass holder = new HolderClass();
                holder = await GetAsync("StockDispatch_In/getdispatchlist", holder, new Dictionary<string, string?>()
                {
                    { "BranchID", Model.BranchId.ToString() }                    
                });

                await RedirectToPage(holder, new DispatchReceiveListPage(
                    new DispatchReceiveListViewModel(holder.Holder?.DispatchList, Model)));
            }
            else if (dispatchTypeReceiveTypeSelectionPage.SelectedDispatchRecieveType == "Tray wise data")
            {
                string trayNumberString = await Application.Current?.MainPage?.DisplayPromptAsync(
                    "Tray #", "Enter\\scan the tray #:", "OK", keyboard:Keyboard.Numeric);

                if (string.IsNullOrEmpty(trayNumberString)) return;

                HolderClass holder = new HolderClass();
                holder = await GetAsync("StockDispatch_In/getdispatchdetailbytraynumber", holder, new Dictionary<string, string?>()
                {
                    { "BranchID", Model.BranchId.ToString() }
                    , { "TrayNumber", trayNumberString }
                });

                await RedirectToPage(holder, new DispatchReceiveDetailListPage
                    (new DispatchReceiveDetailListViewModel(holder.DispatchReceive, Model)));
            }
        }
    }
}
