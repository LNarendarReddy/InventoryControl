using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

        public IAsyncRelayCommand PayBillCommand { get; }

        public BillInfoViewModel(Bill bill, ObservableCollection<MOP> mopList)
        {
            CurrentBill = bill;
            MopList = mopList;
            PayBillCommand = new AsyncRelayCommand(PayBill);
        }

        private async Task PayBill()
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(CurrentBill.SaleType))
                errors.Add("Sale type not selected");

            if (string.IsNullOrEmpty(CurrentBill.PaymentModeId))
                errors.Add("Payment mode not selected");

            if (errors.Any())
            {
                await DisplayAlert("Error"
                    , "Fix the following errors: \n\n"
                    + string.Join("", errors.Select(x => x = $"\r * {x}.\n"))
                    , "OK");
                return;
            }


        }
    }
}
