using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevExpress.PivotGrid.PivotTable;
using NSRetailLiteApp.Models;
using NSRetailLiteApp.Views.StockEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.StockEntry
{
    public partial class StockEntryViewModel : BaseViewModel
    {
        [ObservableProperty]
        private StockEntryModel _stockEntryModel;

        private readonly LoggedInUser user;
        public IAsyncRelayCommand SaveCommand { get; }

        public event EventHandler SaveComplete;

        public StockEntryViewModel(StockEntryModel stockEntryModel, LoggedInUser User)
        {
            _stockEntryModel = stockEntryModel;
            user = User;
            StockEntryModel.UserID = user.UserId;
            SaveCommand = new AsyncRelayCommand(Save);            
        }

        private async Task Save()
        {
            List<string> errors = [];

            if (string.IsNullOrEmpty(StockEntryModel.SupplierInvoiceNo))
                errors.Add("Supplier invoice # cannot be empty");

            if (StockEntryModel.InvoiceDate == DateTime.MinValue)
                errors.Add("Invoice date cannot be empty");

            if (errors.Any())
            {
                await DisplayAlert("Error"
                    , "Fix the following errors: \n\n"
                    + string.Join("", errors.Select(x => x = $"\r * {x}.\n"))
                    , "OK");
                return;
            }
            if (!await DisplayAlert("Confirm"
                       , $"Do you create a new invoice for {StockEntryModel.SupplierInvoiceNo} ({StockEntryModel.SupplierName})?"
                       , "Yes", "No")) return;

            var stockEntryModel = StockEntryModel;
            stockEntryModel = await PostAsyncAsContent("StockEntry_v2/saveinvoice", stockEntryModel, displayAlert: true, showResponse: false);

            if (stockEntryModel.Exception != null)
            {
                stockEntryModel.Exception = null; // clear exception
                return;
            }

            stockEntryModel.StockEntryId = stockEntryModel.ReturnId;
            await RedirectToPage(stockEntryModel, new StockEntryDetailListPage(new StockEntryDetailListViewModel(stockEntryModel)));
            SaveComplete?.Invoke(this, new EventArgs());
        }
    }
}
