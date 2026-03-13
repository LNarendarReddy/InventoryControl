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

        public IAsyncRelayCommand SaveCommand { get; }

        public event EventHandler SaveComplete;

        public StockEntryViewModel(StockEntryModel stockEntryModel)
        {
            _stockEntryModel = stockEntryModel;
            StockEntryModel.UserID = HomePageViewModel.User.UserId;
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

            string message = StockEntryModel.StockEntryId > 0
                ? $"Are you sure you want to save?"
                : $"Are you sure you want to create invoice for {StockEntryModel.SupplierInvoiceNo} ({StockEntryModel.SupplierName})?";

            if (!await DisplayAlert("Confirm", message , "Yes", "No")) return;

            var stockEntryModel = StockEntryModel;
            stockEntryModel = await PostAsyncAsContent("StockEntry_v2/saveinvoice", stockEntryModel, displayAlert: true, showResponse: false);

            if (stockEntryModel.Exception != null)
            {
                stockEntryModel.Exception = null; // clear exception
                return;
            }

            if (StockEntryModel.StockEntryId == 0)
            {
                stockEntryModel.StockEntryId = stockEntryModel.ReturnId;
                await RedirectToPage(stockEntryModel, new StockEntryDetailListPage(new StockEntryDetailListViewModel(stockEntryModel)));
            }

            SaveComplete?.Invoke(this, new EventArgs());
        }
    }
}
