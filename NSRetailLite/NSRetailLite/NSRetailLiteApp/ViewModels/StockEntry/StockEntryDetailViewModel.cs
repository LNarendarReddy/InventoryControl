using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NSRetailLiteApp.Models;
using NSRetailLiteApp.Views.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.StockEntry
{
    public partial class StockEntryDetailViewModel : BaseViewModel
    {
        [ObservableProperty]
        private StockEntryDetailModel _stockEntryDetailModel;

        public IAsyncRelayCommand SaveCommand { get; }
        public IAsyncRelayCommand LoadItemCommand { get; }

        public delegate void SaveCompleted();

        public delegate void SetFocus();

        public event SaveCompleted SaveComplete;

        public event SetFocus FocusMRP;
        public event SetFocus FocusQuantity;
        public event SetFocus FocusWeight;


        public StockEntryDetailViewModel(StockEntryDetailModel stockEntryDetailModel)
        {
            StockEntryDetailModel = stockEntryDetailModel;
            SaveCommand = new AsyncRelayCommand(Save);
            LoadItemCommand = new AsyncRelayCommand(LoadItem);
        }

        private async Task Save()
        {
            List<string> errors = [];

            if (StockEntryDetailModel.ItemCodeID <= 0)
                errors.Add("Item not selected");

            if (!StockEntryDetailModel.IsOpenItem && StockEntryDetailModel.Quantity <= 0)
                errors.Add("Quantity cannot be empty");

            if (StockEntryDetailModel.IsOpenItem && StockEntryDetailModel.WeightInKGs <= 0)
                errors.Add("Weight cannot be empty");

            if (!StockEntryDetailModel.IsOpenItem && StockEntryDetailModel.Quantity > 99999)
                errors.Add("Quantity cannot be more than 5 digits");

            if (StockEntryDetailModel.IsOpenItem && StockEntryDetailModel.WeightInKGs > 9999.99)
                errors.Add("Weight cannot be more than 4 digits");

            if (errors.Any())
            {
                await DisplayAlert("Error"
                    , "Fix the following errors: \n\n"
                    + string.Join("", errors.Select(x => x = $"\r * {x}.\n"))
                    , "OK");
                return;
            }

            var stockEntryDetailModel = StockEntryDetailModel;
            stockEntryDetailModel.UserID = HomePageViewModel.User.UserId;
            stockEntryDetailModel = await PostAsyncAsContent("StockEntry_v2/saveinvoicedetail", stockEntryDetailModel
                , displayAlert: true, showResponse: true);

            if (stockEntryDetailModel.Exception == null)
            {
                ClearData();
                SaveComplete?.Invoke();
            }
        }

        private async Task LoadItem()
        {
            if (string.IsNullOrEmpty(StockEntryDetailModel.ItemCode))
            {
                ClearData();
                return;
            }

            Item item = new();

            item = await GetAsync("StockEntry_v2/getitembycode", item
                , new Dictionary<string, string?>()
                {
                    { "ItemCode", StockEntryDetailModel.ItemCode }
                }, displayAlert: true);

            if (item.Exception != null) return;

            Tuple<ItemCodeData, ItemPrice> returnData = await new ItemPriceSelectionUtility().GetSelectedItemPrice(item);

            ItemCodeData itemCode = returnData.Item1;
            ItemPrice itemPrice = returnData.Item2;

            if (itemCode == null) return;

            StockEntryDetailModel.ItemCodeID = int.Parse(itemCode.ItemCodeID);
            StockEntryDetailModel.ItemCode = itemCode.ItemCode;
            StockEntryDetailModel.SKUCode = item.SKUCode;
            StockEntryDetailModel.ItemName = item.ItemName;
            StockEntryDetailModel.MRP = itemPrice?.MRP;
            StockEntryDetailModel.IsOpenItem = item.IsOpenItem;
            StockEntryDetailModel.Quantity = 0;
            StockEntryDetailModel.WeightInKGs = 0;

            if (itemPrice == null)
            {
                FocusMRP?.Invoke();
                return;
            }

            (item.IsOpenItem ? FocusWeight : FocusQuantity)?.Invoke();
        }

        private void ClearData()
        {
            StockEntryDetailModel.StockEntryDetailId = 0;
            StockEntryDetailModel.ItemCodeID = 0;
            StockEntryDetailModel.ItemCode = string.Empty;
            StockEntryDetailModel.SKUCode = string.Empty;
            StockEntryDetailModel.ItemName = string.Empty;
            StockEntryDetailModel.MRP = null;
            StockEntryDetailModel.Quantity = 0;
            StockEntryDetailModel.WeightInKGs = 0;
        }
    }
}
