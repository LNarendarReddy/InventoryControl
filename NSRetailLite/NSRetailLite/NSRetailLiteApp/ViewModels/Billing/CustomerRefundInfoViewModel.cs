using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevExpress.Utils.About;
using DevExpress.XtraReports.UI;
using Newtonsoft.Json;
using NSRetailLiteApp.Helpers;
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
            holder = await PostAsync("CRefund/savecrefund", holder, new Dictionary<string, string?>()
            {
                {"jsonString", JsonConvert.SerializeObject(CurrentBill) }
            });

            if (holder.Exception != null) return;

            PrintRefundSlip(CurrentBill);
            await Pop();
            await Pop();
        }

        private async Task PrintRefundSlip(Bill CurrentBill)
        {
            XtraReport report = new CRefundHelper(CurrentBill).GetPrint();
            report.Parameters["GSTIN"].Value = "37AAICV7240C1ZC";
            report.Parameters["CIN"].Value = "U51390AP2022PTC121579";
            report.Parameters["FSSAI"].Value = "10114004000548";
            report.Parameters["Address"].Value = HomePageViewModel.User.Address;
            report.Parameters["BillDate"].Value = DateTime.Now;
            report.Parameters["BillNumber"].Value = CurrentBill.BillNumber;
            report.Parameters["BranchName"].Value = HomePageViewModel.User.BranchName;
            report.Parameters["CounterName"].Value = CurrentBill.CounterName;
            report.Parameters["Phone"].Value = HomePageViewModel.User.PhoneNo;
            report.Parameters["UserName"].Value = HomePageViewModel.User.FullName;
            report.Parameters["IsWithBill"].Value = true;

#if ANDROID
            await PrintHelper.PrintAsync(report);
#endif

            //if (!PrintHelper.PrintReport(report).Result)
            //{
            //    DisplayErrorMessage("Print operation failed");
            //}
        }
    }
}
