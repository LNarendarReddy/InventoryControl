using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NSRetailLiteApp.Models;
using NSRetailLiteApp.Views.Billing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.Billing
{
    public partial class CustomerRefundViewModel : BaseViewModel
    {

        [ObservableProperty]
        public Bill _currentBill;
        private readonly int branchCounterId;

        public IAsyncRelayCommand FinishRefundCommand { get; }

        public IAsyncRelayCommand<BillDetail> EditQuantityCommand { get; }

        public IAsyncRelayCommand<BillDetail> DeleteQuantityCommand { get; }

        public CustomerRefundViewModel(Bill billData, int branchCounterId)
        {
            CurrentBill = billData;
            this.branchCounterId = branchCounterId;
            FinishRefundCommand = new AsyncRelayCommand(FinishRefund);
            EditQuantityCommand = new AsyncRelayCommand<BillDetail>(EditQuantity);
            DeleteQuantityCommand = new AsyncRelayCommand<BillDetail>(DeleteQuantity);

            if (CurrentBill != null)
            {
                CurrentBill.CR_BillDetailList.ToList().ForEach(x =>
                    {
                        x.RefundQuantity = 0;
                        //x.PropertyChanged += BillDetail_PropertyChanged;
                        if (x.DeletedDate != null)
                            CurrentBill.CR_BillDetailList.Remove(x);
                    });

                UpdateTotals();
            }
        }

        //private void BillDetail_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == "RefundAmount") UpdateTotals();
        //}

        private async Task FinishRefund()
        {
            if (CurrentBill.TotalRefundAmount == 0)
            {
                DisplayErrorMessage("No items to refund");
                return;
            }

            if (!await DisplayAlert("Confirm", "Are you sure to finish refund?", "Yes", "No")) return;

            await RedirectToPage(CurrentBill, new CustomerRefundInfoPage(new CustomerRefundInfoViewModel(CurrentBill, branchCounterId)));
        }

        private async Task EditQuantity(BillDetail billDetail)
        {
            if (billDetail.Quantity == 0)
            {
                await DisplayAlert("Not allowed", "No quantity exists for refund, the operation is cancelled.", "OK");
                return;
            }

            string newQuantity = await Application.Current?.MainPage?.DisplayPromptAsync($"{billDetail.ItemName} - MRP : {billDetail.MRP}", "Enter the refund quantity:", "OK");

            if (int.TryParse(newQuantity, out int qty))
            {
                if (qty > billDetail.Quantity)
                {
                    await DisplayAlert("Not allowed", "Refund quantity cannot be greater than billed quantity, the operation is cancelled.", "OK");
                    return;
                }

                billDetail.RefundQuantity = qty;
                UpdateTotals();
            }
        }

        private async Task DeleteQuantity(BillDetail billDetail)
        {
            if (billDetail.RefundQuantity == 0
                || !await DisplayAlert("Confirm", "Are you sure to delete refund quantity?", "Yes", "No")) 
                return;

            billDetail.RefundQuantity = 0;
            UpdateTotals();
        }

        private void UpdateTotals()
        {
            CurrentBill.TotalRefundAmount = CurrentBill.CR_BillDetailList.Sum(x => x.RefundAmount);
        }
    }
}
