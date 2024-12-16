using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using NSRetailLiteApp.Models;
using NSRetailLiteApp.Views.Billing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            CurrentBill.PaidTotalAmount = MopValueList.Sum(x => x.MOPValue);
            CurrentBill.RemainingAmount = CurrentBill.TotalAmount - CurrentBill.PaidTotalAmount;

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
                        await DisplayAlert("Verification", "Bill amount updated, please verify amount", "OK");
                        SelectedMOP = null;

                        MopValueList.ToList().ForEach(item => item.MOPValue = 0);
                        IsBillOfferApplied = true;
                        return;
                    }
                }
            }

            CurrentBill.MOPValueList.Clear();

            MopValueList.Where(x => x.MOPValue > 0).ToList().ForEach(x => CurrentBill.MOPValueList.Add(new MOP() { MOPId = x.MOPId, MOPValue = x.MOPValue }));
            CurrentBill.UserId = HomePageViewModel.User.UserId;

            //reset object
            holder = new();
            CurrentBill.BillDetailList.Clear(); // no need to resend
            PostAsync("billing/finishbill", ref holder, new Dictionary<string, string?>
            {
                { "jsonString", JsonConvert.SerializeObject(CurrentBill) }
            });

            if (holder.Exception != null) return;

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
