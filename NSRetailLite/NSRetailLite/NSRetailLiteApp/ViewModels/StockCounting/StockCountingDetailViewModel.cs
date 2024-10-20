using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NSRetailLiteApp.Models;
using NSRetailLiteApp.Views.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.StockCounting
{
    public partial class StockCountingDetailViewModel : BaseViewModel
    {
        [ObservableProperty]
        public StockCountingDetailModel _stockCountingDetailModel;

        public IAsyncRelayCommand SaveCommand { get; }
        public IAsyncRelayCommand LoadItemCommand { get; }
        public IAsyncRelayCommand ScanCommand { get; }

        private StockCountingDetailListViewModel _stockCountingDetailListViewModel;

        public delegate void SaveCompleted();

        public event SaveCompleted SaveComplete; 

        public StockCountingDetailViewModel(StockCountingDetailListViewModel stockCountingDetailListViewModel
            , StockCountingDetailModel stockCountingDetailModel)
        {
            _stockCountingDetailListViewModel = stockCountingDetailListViewModel;
            StockCountingDetailModel = stockCountingDetailModel;
            SaveCommand = new AsyncRelayCommand(Save);
            LoadItemCommand = new AsyncRelayCommand(LoadItem);
            ScanCommand = new AsyncRelayCommand(Scan);
        }

        private async Task Save()
        {
            List<string> errors = new List<string>();

            if (StockCountingDetailModel.ItemPriceId <= 0)
                errors.Add("Item or price not selected");

            if (!StockCountingDetailModel.IsOpenItem && StockCountingDetailModel.Quantity <= 0)
                errors.Add("Quantity cannot be empty");

            if (StockCountingDetailModel.IsOpenItem && StockCountingDetailModel.WeightInKGs <= 0)
                errors.Add("Weight cannot be empty");

            if(errors.Any())
            {
                await DisplayAlert("Error"
                    , "Fix the following errors: \n\n"
                    + string.Join("", errors.Select(x => x = $"\r * {x}.\n"))
                    , "OK");
                return;
            }
                        
            var stockCountingDetailModel = StockCountingDetailModel;
            PostAsync("stockcounting/savecountingdetail", ref stockCountingDetailModel
                , new Dictionary<string, string?>()
                {
                    { "StockCountingID", _stockCountingDetailListViewModel.StockCountingModel.StockCountingId.ToString() },
                    { "StockCountingDetailID", StockCountingDetailModel.StockCountingDetailId.ToString() },
                    { "ItemPriceID", StockCountingDetailModel.ItemPriceId.ToString() },
                    { "Quantity", StockCountingDetailModel.Quantity.ToString() },
                    { "WeightInKgs", StockCountingDetailModel.WeightInKGs.ToString() }
                }, displayAlert: true, showResponse: true);

            if (StockCountingDetailModel.Exception == null)
            {
                ClearData();
                SaveComplete?.Invoke();
            }
        }

        private async Task LoadItem()
        {
            if (string.IsNullOrEmpty(StockCountingDetailModel.ItemCode))
            {
                ClearData();
                return;
            }

            Item item = new();

            GetAsync("stockcounting/getitem", ref item
                , new Dictionary<string, string?>()
                {
                    { "ItemCode", StockCountingDetailModel.ItemCode },
                    {"isNested", "true" }
                }, displayAlert: true);

            if (item.Exception != null) return;

            Tuple<ItemCodeData, ItemPrice> returnData = await new ItemPriceSelectionUtility().GetSelectedItemPrice(item);

            ItemCodeData itemCode = returnData.Item1;
            ItemPrice itemPrice = returnData.Item2;

            if(itemCode == null || itemPrice == null) return;

            StockCountingDetailModel.ItemPriceId = itemPrice.ItemPriceID;
            StockCountingDetailModel.ItemCode = itemCode.ItemCode;
            StockCountingDetailModel.SKUCode = item.SKUCode;
            StockCountingDetailModel.ItemName = item.ItemName;
            StockCountingDetailModel.MRP = "MRP : " + itemPrice.MRP.ToString();
            StockCountingDetailModel.SalePrice = "Sale price : " + itemPrice.SalePrice.ToString();
        }

        private async Task Scan()
        {
            StockCountingDetailModel.ItemCode = await new ItemPriceSelectionUtility().ScanBarCodeWithCamera();
            LoadItem();
        }

        private void ClearData()
        {
            StockCountingDetailModel.StockCountingDetailId = 0;
            //StockCountingDetailModel.StockCountingId = string.Empty;
            StockCountingDetailModel.ItemPriceId = 0;
            StockCountingDetailModel.ItemCode = string.Empty;
            StockCountingDetailModel.SKUCode = string.Empty;
            StockCountingDetailModel.ItemName = string.Empty;
            StockCountingDetailModel.MRP = string.Empty;
            StockCountingDetailModel.SalePrice = string.Empty;
            StockCountingDetailModel.Quantity = 0;
            StockCountingDetailModel.WeightInKGs = 0.00;
            StockCountingDetailModel.SNo = 0;
        }
    }
}
