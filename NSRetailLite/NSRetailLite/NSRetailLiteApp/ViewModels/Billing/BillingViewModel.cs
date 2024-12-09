using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using NSRetailLiteApp.Models;
using NSRetailLiteApp.Views.Billing;
using NSRetailLiteApp.Views.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.Billing
{
    public partial class BillingViewModel : BaseViewModel
    {
        private readonly int branchCounterId;

        [ObservableProperty]
        public Bill _currentBill;

        [ObservableProperty]
        public string _itemCode;

        public IAsyncRelayCommand FinishBillCommand { get; }
        public IAsyncRelayCommand LoadItemCommand { get; }

        public delegate void LoadCompletedHandler();

        public event LoadCompletedHandler LoadCompleted;

        public BillingViewModel(Bill bill, int branchCounterId) 
        { 
            _currentBill = bill;
            this.branchCounterId = branchCounterId;
            FinishBillCommand = new AsyncRelayCommand(FinishBill);
            LoadItemCommand = new AsyncRelayCommand(LoadItem);
            UpdateTotals();
        }

        private async Task FinishBill()
        {
            if (CurrentBill.TotalAmount <= 0)
            {
                DisplayErrorMessage("No items to bill");
                return;
            }


            HolderClass holder = new();

            GetAsync("billing/getmop", ref holder, []);

            ObservableCollection<MOP> MOPItemsList = holder.Holder.MOPList;
            MOPItemsList.Add(new MOP() { MOPId = 0, MOPName = "Multiple" });
            
            await RedirectToPage(holder, new BillInfoPage(new BillInfoViewModel(CurrentBill, MOPItemsList)));

            //List<string> errors = new List<string>();

            //if (StockCountingDetailModel.ItemPriceId <= 0)
            //    errors.Add("Item or price not selected");

            //if (!StockCountingDetailModel.IsOpenItem && StockCountingDetailModel.Quantity <= 0)
            //    errors.Add("Quantity cannot be empty");

            //if (StockCountingDetailModel.IsOpenItem && StockCountingDetailModel.WeightInKGs <= 0)
            //    errors.Add("Weight cannot be empty");

            //if (!StockCountingDetailModel.IsOpenItem && StockCountingDetailModel.Quantity > 9999)
            //    errors.Add("Quantity cannot be more than 4 digits");

            //if (StockCountingDetailModel.IsOpenItem && StockCountingDetailModel.WeightInKGs >= 10000)
            //    errors.Add("Weight cannot be more than 4 digits");

            //if (errors.Any())
            //{
            //    await DisplayAlert("Error"
            //        , "Fix the following errors: \n\n"
            //        + string.Join("", errors.Select(x => x = $"\r * {x}.\n"))
            //        , "OK");
            //    return;
            //}

            //var stockCountingDetailModel = StockCountingDetailModel;
            //PostAsync("stockcounting/savecountingdetail", ref stockCountingDetailModel
            //    , new Dictionary<string, string?>()
            //    {
            //        { "StockCountingID", _stockCountingDetailListViewModel.StockCountingModel.StockCountingId.ToString() },
            //        { "StockCountingDetailID", StockCountingDetailModel.StockCountingDetailId.ToString() },
            //        { "ItemPriceID", StockCountingDetailModel.ItemPriceId.ToString() },
            //        { "Quantity", StockCountingDetailModel.Quantity.ToString() },
            //        { "WeightInKgs", StockCountingDetailModel.WeightInKGs.ToString() }
            //    }, displayAlert: true, showResponse: true);

            //if (StockCountingDetailModel.Exception == null)
            //{
            //    ClearData();
            //    SaveComplete?.Invoke();
            //}
        }

        private async Task LoadItem()
        {
            if (string.IsNullOrEmpty(ItemCode))
            {
                return;
            }

            string inputItemCode = string.Empty;
            double weightInKGs = 0;
            if(ItemCode.Contains('?'))
            {
                var input = ItemCode.Split('?');
                inputItemCode = input[0];
                if (!double.TryParse(input[1], out weightInKGs))
                {
                    DisplayErrorMessage("Invalid weight value, operation has been cancelled");
                    UpdateTotals();
                    return;
                }
            }
            else
            {
                inputItemCode = ItemCode;
            }

            Item item = new();

            GetAsync("billing/getitem", ref item
                , new Dictionary<string, string?>()
                {
                    { "ItemCode", inputItemCode }
                }, displayAlert: true);

            if (item.Exception != null) return;

            if(item.IsOpenItem && weightInKGs <= 0)
            {
                DisplayErrorMessage("Invalid weight value, operation has been cancelled");
                UpdateTotals();
                return;
            }

            Tuple<ItemCodeData, ItemPrice> returnData = await new ItemPriceSelectionUtility().GetSelectedItemPrice(item);

            ItemCodeData itemCode = returnData.Item1;
            ItemPrice itemPrice = returnData.Item2;

            if (itemCode == null || itemPrice == null) return;

            BillDetail billDetail = new()
            {
                ItemPriceId = itemPrice.ItemPriceID,
                Quantity = 1,
                BillId = CurrentBill.BillId,
                WeightInKGs = weightInKGs,
                BranchCounterId = branchCounterId,
                UserId = HomePageViewModel.User.UserId
            };

            HolderClass holder = new();
            PostAsync("billing/savebilldetail", ref holder
                , new Dictionary<string, string?>()
                {
                    { "jsonstring", JsonConvert.SerializeObject(billDetail) }
                });

            if(holder.Exception != null) return;

            CurrentBill = holder.Bill;
            UpdateTotals();
        }

        private void UpdateTotals()
        {
            CurrentBill.TotalAmount = CurrentBill.BillDetailList.Where(x => x.DeletedDate == null).Sum(x => x.BilledAmount);
            LoadCompleted?.Invoke();
        }
    }
}
