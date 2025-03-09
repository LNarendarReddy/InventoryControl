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

        public IAsyncRelayCommand<BillDetail> EditItemCommand { get; }

        public IAsyncRelayCommand<BillDetail> DeleteItemCommand { get; }
        public int DaySequenceId { get; }

        public delegate void LoadCompletedHandler();

        public event LoadCompletedHandler LoadCompleted;

        public BillingViewModel(Bill bill, int branchCounterId, int daySequenceId) 
        { 
            this.branchCounterId = branchCounterId;
            DaySequenceId = daySequenceId;
            FinishBillCommand = new AsyncRelayCommand(FinishBill);
            LoadItemCommand = new AsyncRelayCommand(LoadItem);
            EditItemCommand = new AsyncRelayCommand<BillDetail>(EditItem);
            DeleteItemCommand = new AsyncRelayCommand<BillDetail>(DeleteItem);

            UpdateTotals(bill);
        }

        private async Task FinishBill()
        {
            if (CurrentBill.TotalAmount <= 0)
            {
                DisplayErrorMessage("No items to bill");
                return;
            }

            if (!await DisplayAlert("Confirm", "Are you sure you want to finish current bill?", "Yes", "No")) return;

            HolderClass holder = new();

            holder = await GetAsync("billing/getmop", holder, []);

            BillInfoViewModel billInfoViewModel = new(CurrentBill, holder.Holder.MOPList, branchCounterId);
            billInfoViewModel.OnBillFinished += BillInfoViewModel_OnBillFinished;
            await RedirectToPage(holder, new BillInfoPage(billInfoViewModel));
            
        }

        private void BillInfoViewModel_OnBillFinished(Bill bill)
        {
            CurrentBill = bill;
            UpdateTotals();
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

            item = await GetAsync("billing/getitem", item
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
                WeightInKGs = weightInKGs                
            };

            await UpdateBillDetail(billDetail, "save");
        }

        private async Task EditItem(BillDetail billDetail)
        {
            if (billDetail.DeletedDate != null)
            {
                DisplayErrorMessage("Deleted items cannot be edited");
                return;
            }

            if (billDetail.IsOpenItem)
            {
                DisplayErrorMessage("Loose\\open items cannot be edited");
                return;
            }

            if (billDetail.MRP > HomePageViewModel.User.MultiEditThreshold)
            {
                DisplayErrorMessage("Cannot edit quantity as it exceeds threshold limit");
                return;
            }

            string newQuantity = await Application.Current?.MainPage?.DisplayPromptAsync($"{billDetail.ItemName} - MRP : {billDetail.MRP}", "Enter the quantity:", "OK");

            if (int.TryParse(newQuantity, out int qty))
            {
                billDetail.Quantity = qty;
                await UpdateBillDetail(billDetail, "save");
            }
        }

        private async Task DeleteItem(BillDetail billDetail)
        {
            if (billDetail.DeletedDate != null)
            {
                DisplayErrorMessage("Deleted items cannot be edited");
                return;
            }

            if (!await DisplayAlert("Confirm", $"Are you sure you want to delete - {billDetail.ItemName} with MRP - {billDetail.MRP}?", "Yes", "No"))
            {
                return;
            }

            IsLoading = true;

            // add SNOs for remaining items
            int currentIndex = CurrentBill.BillDetailList.IndexOf(billDetail);
            for (int i = currentIndex + 1; i < CurrentBill.BillDetailList.Count; i++)
            {
                BillDetail currentBillDetail = CurrentBill.BillDetailList[i];
                if (currentBillDetail.DeletedDate != null) continue;

                currentBillDetail.SNO--;

                billDetail.Snos.Add(new BillDetailSNo() { BillDetailId = currentBillDetail.BillDetailId, SNo = currentBillDetail.SNO });
            }

            await UpdateBillDetail(billDetail, "delete");
        }

        private void UpdateTotals(Bill bill = null)
        {
            if (bill != null) 
            { 
                CurrentBill = bill;
                CurrentBill.DaySequenceId = DaySequenceId;
            }

            if (CurrentBill != null)
                CurrentBill.TotalAmount = CurrentBill.BillDetailList.Where(x => x.DeletedDate == null).Sum(x => x.BilledAmount);

            LoadCompleted?.Invoke();
            IsLoading = false;
        }

        private async Task UpdateBillDetail(BillDetail billDetail, string urlPrefix)
        {
            IsLoading = true;
            HolderClass holder = new();
            billDetail.BranchCounterId = branchCounterId;
            billDetail.UserId = HomePageViewModel.User.UserId;
            billDetail.BranchId = HomePageViewModel.User.BranchId;

            holder = await PostAsync($"billing/{urlPrefix}billdetail", holder
                , new Dictionary<string, string?>()
                {
                    { "jsonstring", JsonConvert.SerializeObject(billDetail) }
                });


            if (holder.Exception != null) 
            {
                IsLoading = false;
                return; 
            }

            ItemCode = string.Empty;
            UpdateTotals(holder.Bill);
        }
    }
}
