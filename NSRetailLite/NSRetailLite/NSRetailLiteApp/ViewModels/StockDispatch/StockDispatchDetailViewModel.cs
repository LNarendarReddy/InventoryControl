using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevExpress.PivotGrid.PivotTable;
using NSRetailLiteApp.Models;
using NSRetailLiteApp.Views.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NSRetailLiteApp.ViewModels.StockDispatch
{
    public partial class StockDispatchDetailViewModel : BaseViewModel
    {
        [ObservableProperty]
        public StockDispatchDetailModel _stockDispatchDetailModel;
        private readonly BranchIndentDetailModel branchIndentDetailModel;
        public StockDispatchModel StockDispatchModel { get; }

        public IAsyncRelayCommand SaveCommand { get; }

        public IAsyncRelayCommand LoadItemCommand { get; }

        public IAsyncRelayCommand AddTrayCommand { get; }

        public IAsyncRelayCommand DeleteTrayCommand { get; }

        public LoggedInUser User { get; }
        public bool IsEditable { get; }

        [ObservableProperty]
        private TrayInfo _selectedTrayInfo;

        public StockDispatchDetailViewModel(StockDispatchDetailModel stockDispatchDetailModel
            , BranchIndentDetailModel branchIndentDetailModel
            , StockDispatchModel stockDispatchModel
            , LoggedInUser user
            , bool isEditable = true)
        {
            StockDispatchDetailModel = stockDispatchDetailModel;
            this.branchIndentDetailModel = branchIndentDetailModel;
            StockDispatchModel = stockDispatchModel;
            User = user;
            IsEditable = isEditable;
            SaveCommand = new AsyncRelayCommand(Save);
            LoadItemCommand = new AsyncRelayCommand(LoadItem);
            AddTrayCommand = new AsyncRelayCommand(AddTray);
            DeleteTrayCommand = new AsyncRelayCommand(DeleteTray);

            SelectedTrayInfo =
                StockDispatchModel.TrayInfoList
                    .FirstOrDefault(x => x.TrayInfoId == StockDispatchDetailModel.TrayInfoId) 
                        ?? StockDispatchModel.LastKnownTrayNumber;
        }

        private async Task Save()
        {
            List<string> errors = [];

            if (StockDispatchDetailModel.ItemPriceId <= 0)
                errors.Add("Item or price not selected");

            if (!StockDispatchDetailModel.IsOpenItem && StockDispatchDetailModel.DispatchQuantity <= 0)
                errors.Add("Quantity cannot be empty");

            if (StockDispatchDetailModel.IsOpenItem && StockDispatchDetailModel.WeightInKGs <= 0)
                errors.Add("Weight cannot be empty");

            if (!StockDispatchDetailModel.IsOpenItem && StockDispatchDetailModel.DispatchQuantity > 9999)
                errors.Add("Quantity cannot be more than 4 digits");

            if (StockDispatchDetailModel.IsOpenItem && StockDispatchDetailModel.WeightInKGs >= 10000)
                errors.Add("Weight cannot be more than 4 digits");

            if (SelectedTrayInfo == null)
                errors.Add("Tray # not entered");

            if (errors.Any())
            {
                await DisplayAlert("Error"
                    , "Fix the following errors: \n\n"
                    + string.Join("", errors.Select(x => x = $"\r * {x}.\n"))
                    , "OK");
                return;
            }

            HolderClass holderClass = new HolderClass();
            holderClass = await PostAsync("Stockdispatch_v2/savedispatchdetail", holderClass
                , new Dictionary<string, string?>()
                {
                    { "StockDispatchID", StockDispatchDetailModel.StockDispatchId.ToString() },
                    { "StockDispatchDetailID", StockDispatchDetailModel.StockDispatchDetailId.ToString() },
                    { "BranchIndentDetailID", (branchIndentDetailModel?.BranchIndentDetailId ?? 0).ToString()},
                    { "ItemPriceID", StockDispatchDetailModel.ItemPriceId.ToString() },
                    { "DispatchQuantity", StockDispatchDetailModel.DispatchQuantity.ToString() },
                    { "WeightInKgs", StockDispatchDetailModel.WeightInKGs.ToString() },
                    { "UserID", User.UserId.ToString() },
                    { "TrayInfoId", StockDispatchDetailModel.TrayInfoId.ToString() },
                    { "TrayNumber", StockDispatchDetailModel.TrayNumber.ToString() }
                }, displayAlert: true, showResponse: false);

            if (holderClass.Exception != null) return;

            // if existing update, recalculate
            if (branchIndentDetailModel != null)
            {
                branchIndentDetailModel.RecalculateDispatchQuantity();
            }

            StockDispatchDetailModel.StockDispatchDetailId = holderClass.GenericID;
            Pop();

            if (!StockDispatchDetailModel.IsNew) return;
            StockDispatchDetailModel.IsNew = false;

            // find if existing update
            StockDispatchDetailModel existingUpdate = StockDispatchModel.StockDispatchDetailManualList
                .FirstOrDefault(x => x.StockDispatchDetailId == StockDispatchDetailModel.StockDispatchDetailId);
            BranchIndentDetailModel existingUpdateParent = null;

            if (existingUpdate == null)
            {
                existingUpdateParent =
                    StockDispatchModel.BranchIndentDetailList.FirstOrDefault(
                        x => x.StockDispatchDetailIndentList
                        .Any(y => y.StockDispatchDetailId == StockDispatchDetailModel.StockDispatchDetailId));

                if (existingUpdateParent != null)
                {
                    existingUpdate = existingUpdateParent.StockDispatchDetailIndentList
                        .First(x => x.StockDispatchDetailId == StockDispatchDetailModel.StockDispatchDetailId);
                }
            }

            // if existing update, update quantity and return
            if (existingUpdate != null)
            {
                existingUpdate.DispatchQuantity += StockDispatchDetailModel.DispatchQuantity;
                existingUpdateParent?.RecalculateDispatchQuantity();
                return;
            }

            // if not existing update, add item
            if (branchIndentDetailModel != null)
            {
                branchIndentDetailModel.StockDispatchDetailIndentList.Add(StockDispatchDetailModel);
                branchIndentDetailModel.RecalculateDispatchQuantity();
            }
            else
                StockDispatchModel.StockDispatchDetailManualList.Add(StockDispatchDetailModel);
        }

        private async Task LoadItem()
        {
            if (string.IsNullOrEmpty(StockDispatchDetailModel.ItemCode))
            {
                ClearData();
                return;
            }

            Item item = new() { SKUCode = StockDispatchDetailModel.ItemCode };

            item = await GetAsync("Stockdispatch_v2/getitem", item
                , new Dictionary<string, string?>()
                {
                    { "ItemCode", StockDispatchDetailModel.ItemCode },
                    { "CategoryID", User.CategoryId.ToString() },
                    { "SubCategoryID", User.SubCategoryId.ToString() }
                }, displayAlert: true);

            if (item.Exception != null) return;

            Tuple<ItemCodeData, ItemPrice> returnData = await new ItemPriceSelectionUtility().GetSelectedItemPrice(item);

            ItemCodeData itemCode = returnData.Item1;
            ItemPrice itemPrice = returnData.Item2;

            if (itemCode == null || itemPrice == null) return;

            StockDispatchDetailModel.ItemPriceId = itemPrice.ItemPriceID;
            StockDispatchDetailModel.ItemCode = itemCode.ItemCode;
            StockDispatchDetailModel.SkuCode = item.SKUCode;
            StockDispatchDetailModel.ItemName = item.ItemName;
            StockDispatchDetailModel.MRP = itemPrice.MRP;
            StockDispatchDetailModel.SalePrice = itemPrice.SalePrice;
            StockDispatchDetailModel.IsOpenItem = item.IsOpenItem;
            StockDispatchDetailModel.DispatchQuantity = 0;
            StockDispatchDetailModel.WeightInKGs = 0;
            //StockDispatchDetailModel.TrayNumber = string.Empty;
        }

        private async Task AddTray()
        {
            string trayNumber = await Application.Current?.MainPage?.DisplayPromptAsync($"Add Tray #", "Enter the tray #:", keyboard: Keyboard.Numeric);
            if (string.IsNullOrEmpty(trayNumber)) return;

            TrayInfo trayInfo = new TrayInfo() { TrayNumber = Convert.ToInt32(trayNumber) };
            trayInfo = await PostAsync("Stockdispatch_v2/savetrayinfo", trayInfo, new Dictionary<string, string?>()
                            {
                                { "StockDispatchID", StockDispatchModel.StockDispatchId.ToString() },
                                { "TrayNumber", trayNumber },
                                { "UserID", User.UserId.ToString() }
                            });

            if (trayInfo.Exception != null) return;

            trayInfo.TrayInfoId = trayInfo.ReturnId;
            StockDispatchModel.TrayInfoList.Add(trayInfo);

            SelectedTrayInfo = trayInfo;
        }

        private async Task DeleteTray()
        {
            if(SelectedTrayInfo == null) return;

            if (!await DisplayAlert("Confirm delete"
                , $"Are you sure you want to delete tray # {SelectedTrayInfo.TrayNumber}?"
                , "Yes", "No")) return;

            TrayInfo trayInfo = SelectedTrayInfo;
            trayInfo = await PostAsync("Stockdispatch_v2/deletetrayinfo", trayInfo, new Dictionary<string, string?>()
                            {
                                { "TrayInfoID", trayInfo.TrayInfoId.ToString() },
                                { "UserID", User.UserId.ToString() }
                            });

            if (trayInfo.Exception != null) return;

            StockDispatchModel.TrayInfoList.Remove(trayInfo);
            SelectedTrayInfo = null;
        }

        partial void OnSelectedTrayInfoChanged(TrayInfo value)
        {
            if(value == null) return;
            StockDispatchModel.LastKnownTrayNumber = value;
            StockDispatchDetailModel.TrayInfoId = value?.TrayInfoId ?? 0;
            StockDispatchDetailModel.TrayNumber = value?.TrayNumber ?? 0;
        }

        private void ClearData()
        {
            StockDispatchDetailModel.ItemPriceId = 0;
            StockDispatchDetailModel.ItemCode = string.Empty;
            StockDispatchDetailModel.SkuCode = string.Empty;
            StockDispatchDetailModel.ItemName = string.Empty;
            StockDispatchDetailModel.MRP = 0;
            StockDispatchDetailModel.SalePrice = 0;
            StockDispatchDetailModel.DispatchQuantity = 0;
            StockDispatchDetailModel.WeightInKGs = 0.00;
        }
    }
}
