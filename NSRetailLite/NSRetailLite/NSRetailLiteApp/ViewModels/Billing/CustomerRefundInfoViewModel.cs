using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using NSRetailLiteApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.Billing
{
    public partial class CustomerRefundInfoViewModel : BaseViewModel
    {
        public IAsyncRelayCommand FinishRefundCommand { get; }

        [ObservableProperty]
        public int _noOfItems;

        [ObservableProperty]
        public int _noOfPieces;

        [ObservableProperty]
        public ObservableCollection<BillDetail> _refundDetailList;
        private readonly int branchCounterId;

        public CustomerRefundInfoViewModel(Bill bill, int branchCounterId) 
        {
            CurrentBill = bill;
            this.branchCounterId = branchCounterId;
            FinishRefundCommand = new AsyncRelayCommand(FinishRefund);

            RefundDetailList = CurrentBill.CR_BillDetailList.Where(x => x.RefundQuantity != 0).ToObservableCollection();
            //CurrentBill.CR_BillDetailList.Where(x => x.RefundQuantity == 0).ToList().ForEach(x => CurrentBill.CR_BillDetailList.Remove(x));
            NoOfItems = RefundDetailList.Count(x => x.RefundQuantity > 0);
            NoOfPieces = RefundDetailList.Sum(x => x.RefundQuantity);
        }

        public Bill CurrentBill { get; }

        private async Task FinishRefund()
        {
            List<string> errors = [];

            if (string.IsNullOrEmpty(CurrentBill.CustomerName))
                errors.Add("Customer name cannot be empty");

            if (string.IsNullOrEmpty(CurrentBill.CustomerMobile))
                errors.Add("Customer mobile # cannot be empty");

            if (!string.IsNullOrEmpty(CurrentBill.CustomerMobile)
               && CurrentBill.CustomerMobile.Length != 10)
            {
                errors.Add("Customer mobile # should be 10 digits");
            }

            if (errors.Any())
            {
                await DisplayAlert("Error"
                    , "Fix the following errors: \n\n"
                    + string.Join("", errors.Select(x => x = $"\r * {x}.\n"))
                    , "OK");
                return;
            }

            if (!await DisplayAlert("Confirm", "Are you sure to finish refund?", "Yes", "No")) return;

            CurrentBill.BillDetailList = RefundDetailList.ToObservableCollection();
            CurrentBill.CR_BillDetailList = [];

            CurrentBill.BranchCounterId = branchCounterId;
            CurrentBill.UserId = HomePageViewModel.User.UserId;
            //CurrentBill.BranchId = HomePageViewModel.User.BranchId;

            CurrentBill.BillDetailList.ToList().ForEach(x =>
            {
                x.BranchCounterId = branchCounterId;
                x.UserId = HomePageViewModel.User.UserId;
                x.BranchId = HomePageViewModel.User.BranchId;
            });

            HolderClass holder = new();
            PostAsync("CRefund/savecrefund", ref holder, new Dictionary<string, string?>()
            {
                {"jsonString", JsonConvert.SerializeObject(CurrentBill) }
            });

            if (holder.Exception != null) return;
            await Pop();
            await Pop();
        }
    }
}
