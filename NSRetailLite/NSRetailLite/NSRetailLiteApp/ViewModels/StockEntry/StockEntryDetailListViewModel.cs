using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DevExpress.Drawing.Internal.Fonts.Interop;
using NSRetailLiteApp.Models;
using NSRetailLiteApp.Views.StockEntry;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.StockEntry
{
    public partial class StockEntryDetailListViewModel : BaseViewModel
    {
        [ObservableProperty]
        private StockEntryModel _stockEntryModel;

        [ObservableProperty]
        private ObservableCollection<StockEntryDetailModel> _filteredStockEntryDetails;

        public IAsyncRelayCommand AddStockEntryDetailCommand { get; }

        public IAsyncRelayCommand SubmitStockEntryCommand { get; }

        public IAsyncRelayCommand DiscardStockEntryCommand { get; }

        public IAsyncRelayCommand EditInvoiceNoCommand { get; }

        public IAsyncRelayCommand<StockEntryDetailModel> EditStockEntryDetailCommand { get; }

        public IAsyncRelayCommand<StockEntryDetailModel> DeleteStockEntryDetailCommand { get; }

        public StockEntryDetailListViewModel(StockEntryModel stockEntryModel)
        {
            StockEntryModel = stockEntryModel;
            StockEntryModel.UserID = HomePageViewModel.User.UserId;
            FilteredStockEntryDetails = stockEntryModel.StockEntryDetailList;

            AddStockEntryDetailCommand = new AsyncRelayCommand(AddStockEntryDetail);
            SubmitStockEntryCommand = new AsyncRelayCommand(SubmitStockEntry);
            DiscardStockEntryCommand = new AsyncRelayCommand(DiscardStockEntry);
            EditStockEntryDetailCommand = new AsyncRelayCommand<StockEntryDetailModel>(EditStockEntryDetail);
            DeleteStockEntryDetailCommand = new AsyncRelayCommand<StockEntryDetailModel>(DeleteStockEntryDetail);

            EditInvoiceNoCommand = new AsyncRelayCommand(EditInvoiceNo);
        }

        private async Task EditInvoiceNo()
        {
            StockEntryModel stockEntryModel = new()
            {
                SupplierId = StockEntryModel.SupplierId,
                SupplierName = StockEntryModel.SupplierName,
                CategoryId = StockEntryModel.CategoryId,
                CategoryName = StockEntryModel.CategoryName,
                SupplierIndentId = StockEntryModel.SupplierIndentId,
                SupplierIndentNo = StockEntryModel.SupplierIndentNo,
                SupplierGSTIN = StockEntryModel.SupplierGSTIN,
                InvoiceDate = StockEntryModel.InvoiceDate,
                StockEntryId = StockEntryModel.StockEntryId
            };
            var stockEntryViewModel = new StockEntryViewModel(stockEntryModel);
            stockEntryViewModel.SaveComplete += StockEntryViewModel_SaveComplete;
            await ShowPopup(stockEntryModel, new InvoiceDetailsPage(stockEntryViewModel));
        }

        private void StockEntryViewModel_SaveComplete(object? sender, EventArgs e)
        {
            Reload();
        }

        [RelayCommand]
        public void PerformSearch(string search = null)
        {
            if (string.IsNullOrEmpty(search))
            {
                FilteredStockEntryDetails = StockEntryModel.StockEntryDetailList;
                return;
            }

            search = search.ToLower();
            FilteredStockEntryDetails
                = new ObservableCollection<StockEntryDetailModel>(
                    StockEntryModel.StockEntryDetailList
                    .Where(x => x.ItemName.Contains(search, StringComparison.CurrentCultureIgnoreCase)
                    || x.ItemCode.Contains(search, StringComparison.CurrentCultureIgnoreCase)
                    || x.SKUCode.Contains(search, StringComparison.CurrentCultureIgnoreCase)));
        }

        private async Task DeleteStockEntryDetail(StockEntryDetailModel? model)
        {
            if (model == null) return;

            string confirmMessage = "Are you sure you want to delete invoice entry the following item?";
            confirmMessage += $"\n\n\t {model.ItemName}";
            confirmMessage += $"\n\n\t\t EAN : {model.ItemCode}";
            confirmMessage += $"\n\t\t MRP : {model.MRP}";
            confirmMessage += $"\n\t\t Qty\\Wght : {(model.IsOpenItem ? model.WeightInKGs : model.Quantity)}";

            if (!await DisplayAlert("Confirm", confirmMessage, "Yes", "No")) return;

            model = await PostAsync("StockEntry_v2/deleteinvoicedetail", model
                    , new Dictionary<string, string?>()
                    {
                        { "StockEntryDetailID", model.StockEntryDetailId.ToString() },
                        { "UserID", HomePageViewModel.User.UserId.ToString() }
                    }, displayAlert: true, showResponse: true);

            if (model.Exception == null)
            {
                await Reload(); //to refresh serial #s
            }
        }

        private async Task EditStockEntryDetail(StockEntryDetailModel? model)
        {
            if (model == null) return;

            await RedirectToPage(StockEntryModel, new StockEntryDetailPage(new StockEntryDetailViewModel(model)), false);
        }

        private async Task DiscardStockEntry()
        {
            if (!await DisplayAlert("Confirm", "Are you sure you want to discard stock entry invoice?", "Yes", "No")) return;

            StockEntryModel stockEntry = StockEntryModel;
            stockEntry = await PostAsync("StockEntry_v2/discardinvoice", stockEntry
                , new Dictionary<string, string?>()
                {
                    { "StockEntryID", stockEntry.StockEntryId.ToString() },
                    { "UserID", HomePageViewModel.User.UserId.ToString() }
                }, displayAlert: true, showResponse: true);

            if (stockEntry.Exception == null)
            {
                Pop();
            }
        }

        private async Task SubmitStockEntry()
        {
            if (!StockEntryModel.StockEntryDetailList.Any())
            {
                DisplayErrorMessage("No items to submit");
                return;
            }

            if (!await DisplayAlert("Confirm", "Are you sure you want to submit stock invoice entry?", "Yes", "No")) return;

            StockEntryModel stockEntry = StockEntryModel;
            stockEntry = await PostAsync("StockEntry_v2/updateinvoice", stockEntry
                , new Dictionary<string, string?>() { { "StockEntryID", stockEntry.StockEntryId.ToString() } }
                , displayAlert: true, showResponse: true);

            if (stockEntry.Exception == null)
            {
                Pop();
            }
        }

        private async Task AddStockEntryDetail()
        {
            try
            {
                StockEntryDetailModel stockEntryDetailModel = new() { StockEntryId = StockEntryModel.StockEntryId };
                await RedirectToPage(StockEntryModel, new StockEntryDetailPage(new StockEntryDetailViewModel(stockEntryDetailModel)), false);
            }
            catch (Exception ex) { DisplayErrorMessage(ex.StackTrace); }
        }

        public async Task Reload()
        {
            var stockEntryModel = StockEntryModel;
            stockEntryModel = await GetAsync("stockentry_v2/getinvoice", stockEntryModel, new Dictionary<string, string?>() { { "UserID", HomePageViewModel.User.UserId.ToString() } }, true);

            StockEntryModel = stockEntryModel;
            PerformSearch();
        }
    }
}
