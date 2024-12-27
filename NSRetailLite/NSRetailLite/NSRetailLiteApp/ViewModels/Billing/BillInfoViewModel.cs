using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevExpress.Maui.Core.Internal;
using DevExpress.Maui.Mvvm;
using DevExpress.Maui.Pdf;
using DevExpress.XtraReports.UI;
using Newtonsoft.Json;
using NSRetailLiteApp.Helpers;
using NSRetailLiteApp.Models;
using NSRetailLiteApp.Views.Billing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.Billing
{
    public partial class BillInfoViewModel : BaseViewModel
    {
        [ObservableProperty]
        public Bill _currentBill;
        
        public ObservableCollection<MOP> MopList { get; }

        public ObservableCollection<MOPViewModel> MopValueList { get; }

        public MOPViewModel CashMOP { get; }

        public MOPViewModel B2BMOP { get; }

        [ObservableProperty]
        public MOP _selectedMOP;

        private readonly int branchCounterId;

        [ObservableProperty]
        private bool _isBillOfferApplied;

        [ObservableProperty]
        private decimal _roundedValue;

        public IAsyncRelayCommand PayBillCommand { get; }

        public delegate void OnBillFinishedHandler(Bill bill);

        public event OnBillFinishedHandler OnBillFinished;

        public BillInfoViewModel(Bill bill, ObservableCollection<MOP> mopList, int branchCounterId)
        {
            CurrentBill = bill;
            MopList = mopList;
            this.branchCounterId = branchCounterId;
            MopValueList = mopList.Select(x => new MOPViewModel(x, CurrentBill)).ToList().ToObservableCollection();
            MopValueList.ToList().ForEach(x => x.MOPValueChanged += UpdateTotals);

            CashMOP = MopValueList.First(x => x.MOPName.Equals("CASH"));
            B2BMOP = MopValueList.First(x => x.MOPName.Equals("B2B Credit"));

            PayBillCommand = new AsyncRelayCommand(PayBill);
            MopList.Add(new MOP() { MOPId = 0, MOPName = "Multiple" });

            UpdateTotals();
        }

        private void UpdateTotals()
        {
            CurrentBill.TotalAmount = CurrentBill.BillDetailList.Where(x => x.DeletedDate == null).Sum(x => x.BilledAmount);
            CurrentBill.PaidTotalAmount = MopValueList.Sum(x => x.MOPValue);
            CurrentBill.RemainingAmount = CurrentBill.TotalAmount - CurrentBill.PaidTotalAmount;
            RoundedValue = CurrentBill.RemainingAmount < 0 ? 0 : Math.Round(CurrentBill.RemainingAmount);

            if (CashMOP.MOPValue > 0)
            {
                CurrentBill.Rounding = Math.Round(CurrentBill.TotalAmount) - CurrentBill.TotalAmount;
                CurrentBill.TenderedCash = CashMOP.MOPValue;
                CurrentBill.TenderedChange = CurrentBill.RemainingAmount + Math.Round(CurrentBill.TotalAmount) - CurrentBill.TotalAmount;
            }
            else
            {
                CurrentBill.Rounding = 0;
                CurrentBill.TenderedCash = 0;
                CurrentBill.TenderedChange = 0;
            }
        }

        private async Task PayBill()
        {
            List<string> errors = [];

            //if (string.IsNullOrEmpty(CurrentBill.IsDoorDelivery))
            //    errors.Add("Sale type not selected");

            if (SelectedMOP == null)
                errors.Add("Payment mode not selected");

            if (!string.IsNullOrEmpty(CurrentBill.CustomerMobile) 
                && CurrentBill.CustomerMobile.Length != 10)
            {
                errors.Add("Customer mobile # should be 10 digits");
            }

            if (!string.IsNullOrEmpty(CurrentBill.CustomerGST)
                && CurrentBill.CustomerGST.Length != 15)
            {
                errors.Add("Customer GST # should be 15 characters");
            }

            if (B2BMOP.MOPValue > 0)
            {
                if (string.IsNullOrEmpty(CurrentBill.CustomerName))
                    errors.Add("Customer name cannot be empty for B2B credit bill");

                if (string.IsNullOrEmpty(CurrentBill.CustomerMobile))
                    errors.Add("Customer mobile # cannot be empty for B2B credit bill");

                if (string.IsNullOrEmpty(CurrentBill.CustomerGST))
                    errors.Add("Customer GST # cannot be empty for B2B credit bill");
            }

            if(CurrentBill.RemainingAmount > 0)
            {
                errors.Add($"Amount not complete, please fill respective payment mode for remaining amount {CurrentBill.RemainingAmount}");
            }

            if (errors.Any())
            {
                await DisplayAlert("Error"
                    , "Fix the following errors: \n\n"
                    + string.Join("", errors.Select(x => x = $"\r * {x}.\n"))
                    , "OK");
                return;
            }

            if (!await DisplayAlert("Confirm", "Are you sure you want to pay bill? This operation cannot be reversed", "Yes", "No")) return;
                        
            HolderClass holder = new();

            if (!IsBillOfferApplied) // since bill offer is already checked and applied
            {
                GetAsync("Billing/getbilloffers", ref holder, new Dictionary<string, string?>
                {
                    { "BillID", CurrentBill.BillId.ToString() },
                    { "BranchId", HomePageViewModel.User.BranchId.ToString() },
                    { "CounterId", branchCounterId.ToString() }
                });
            }

            if (holder.Holder?.OfferList != null && holder.Holder.OfferList.Any())
            {
                BillOffer offerItem = holder.Holder.OfferList[0];
                if (await DisplayAlert("Add free item", $"Add item {offerItem.ItemName} ({offerItem.SKUCode}) for Rs.{offerItem.ActualSalePrice} to the bill?", "Yes", "No"))
                {
                    holder = new();
                    BillDetail billDetail = new BillDetail()
                    {
                        BranchCounterId = branchCounterId,
                        UserId = HomePageViewModel.User.UserId,
                        BranchId = HomePageViewModel.User.BranchId,
                        ItemPriceId = offerItem.ItemPriceId,
                        Quantity = 1,
                        BillId = CurrentBill.BillId,
                        WeightInKGs = 0,
                        IsBillOfferItem = true,
                        BillOfferPrice = offerItem.ActualSalePrice
                    };

                    PostAsync($"billing/savebilldetail", ref holder
                        , new Dictionary<string, string?>()
                        {
                            { "jsonstring", JsonConvert.SerializeObject(billDetail) }
                        });

                    if (holder.Exception != null) return;

                    if (offerItem.ActualSalePrice > 0)
                    {
                        CurrentBill = holder.Bill;
                        MopValueList.ToList().ForEach(item => item.MOPValue = 0);
                        IsBillOfferApplied = true;
                        UpdateTotals();
                        await DisplayAlert("Verification", "Bill amount updated, please verify amount", "OK");
                        SelectedMOP = null;
                        return;
                    }
                }
            }

            CurrentBill.MOPValueList.Clear();

            MopValueList.Where(x => x.MOPValue > 0).ToList().ForEach(x => CurrentBill.MOPValueList.Add(new MOP() { MOPId = x.MOPId,MOPName =x.MOPName , MOPValue = x.MOPValue }));
            CurrentBill.UserId = HomePageViewModel.User.UserId;

            //reset object
            holder = new();
            ObservableCollection<BillDetail> billDetails = CurrentBill.BillDetailList.ToObservableCollection();
            CurrentBill.BillDetailList.Clear(); // no need to resend
            PostAsync("billing/finishbill", ref holder, new Dictionary<string, string?>
            {
                { "jsonString", JsonConvert.SerializeObject(CurrentBill) }
            });

            if (holder.Exception != null) return;

            CurrentBill.BillDetailList = billDetails;
            try
            {
                IsLoading = true;
                await PrintBill(CurrentBill);
            }
            catch (Exception ex)
            {
                DisplayErrorMessage("Print error : " + ex.Message);
            }
            finally
            {
                IsLoading = false;
            }

            // Report logic to go here

            CurrentBill = holder.Bill;
            await Pop();
            OnBillFinished?.Invoke(CurrentBill);
        }

        partial void OnSelectedMOPChanged(MOP value)
        {
            foreach (var mop in MopValueList) 
            {
                mop.IsEnabled = value != null && (value.MOPId == 0 || value.MOPId == mop.MOPId);
                mop.MOPValue = value != null && value.MOPId == mop.MOPId && value.MOPId != CashMOP.MOPId ? CurrentBill.TotalAmount : 0;
            }
        }

        private async Task PrintBill(Bill CurrentBill)
        {
            XtraReport report = new BillHelper(CurrentBill).GetBillPrint();
            report.Parameters["CIN"].Value = "U51390AP2022PTC121579";
            report.Parameters["GSTIN"].Value = "37AAICV7240C1ZC";
            report.Parameters["FSSAI"].Value = "10114004000548";
            report.Parameters["Address"].Value = HomePageViewModel.User.Address;
            report.Parameters["Phone"].Value = HomePageViewModel.User.PhoneNo;
            report.Parameters["BillNumber"].Value = CurrentBill.BillNumber;
            report.Parameters["BillDate"].Value = DateTime.Now;
            report.Parameters["CounterName"].Value = CurrentBill.CounterName;
            report.Parameters["UserName"].Value =  HomePageViewModel.User.FullName;
            report.Parameters["BranchName"].Value = HomePageViewModel.User.BranchName;
            report.Parameters["RoundingFactor"].Value = CurrentBill.Rounding;
            report.Parameters["IsDuplicate"].Value = false;
            report.Parameters["CustomerName"].Value = CurrentBill.CustomerName;
            report.Parameters["CustomerNumber"].Value = CurrentBill.CustomerMobile;
            report.Parameters["TenderedCash"].Value = CurrentBill.TenderedCash;
            report.Parameters["TenderedChange"].Value = CurrentBill.TenderedChange;
            report.Parameters["IsDoorDelivery"].Value = CurrentBill.IsDoorDelivery;
            report.Parameters["CustomerGST"].Value = CurrentBill.CustomerGST;

            if (!PrintHelper.PrintReport(report).Result)
            {
                DisplayErrorMessage("Print operation failed");
            }

            //narendar test code
            //// Export the report to a PDF file
            //string resultFile = Path.Combine(FileSystem.Current.AppDataDirectory, report.Name + ".pdf");
            //report.ExportToPdf(resultFile);

            //await Share.Default.RequestAsync(new ShareFileRequest
            //{
            //    Title = "Share PDF file",
            //    File = new ShareFile(resultFile)

            //});
        }
    }

    public partial class MOPViewModel : BaseViewModel
    {
        [ObservableProperty]
        public int _mOPId;

        [ObservableProperty]
        public string _mOPName;

        [ObservableProperty]
        public decimal _mOPValue;

        [ObservableProperty]
        public bool _isEnabled;

        public MOPViewModel(MOP mopValue, Bill bill)
        {
            MOPId = mopValue.MOPId;
            MOPName = mopValue.MOPName;
            MOPValue = mopValue.MOPValue;
            IsEnabled = false;
            Bill = bill;
        }

        public Bill Bill { get; }

        public delegate void MOPValueChangedHandler();

        public event MOPValueChangedHandler MOPValueChanged;

        partial void OnMOPValueChanged(decimal value)
        {
            MOPValueChanged?.Invoke();
        }
    }
}
