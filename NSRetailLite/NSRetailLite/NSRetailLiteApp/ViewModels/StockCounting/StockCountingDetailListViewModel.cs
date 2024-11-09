using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NSRetailLiteApp.Models;
using NSRetailLiteApp.Views.StockCounting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.StockCounting
{
    public partial class StockCountingDetailListViewModel : BaseViewModel
    {
        [ObservableProperty]
        public StockCountingModel _stockCountingModel;

        [ObservableProperty]
        public ObservableCollection<StockCountingDetailModel> _filteredStockCountingDetails;
        
        private readonly int userID;

        public IAsyncRelayCommand AddItemCommand { get; }

        public IAsyncRelayCommand SubmitCommand { get; }

        public IAsyncRelayCommand DiscardCommand { get; }
        
        public IAsyncRelayCommand<StockCountingDetailModel> EditCommand { get; }

        public IAsyncRelayCommand<StockCountingDetailModel> DeleteCommand { get; }


        public StockCountingDetailListViewModel(StockCountingModel stockCountingModel, int UserID)
        {
            StockCountingModel = stockCountingModel;
            userID = UserID;
            FilteredStockCountingDetails = stockCountingModel.CountingDetail;

            AddItemCommand = new AsyncRelayCommand(AddItem);
            SubmitCommand = new AsyncRelayCommand(Submit);
            DiscardCommand = new AsyncRelayCommand(Discard);
            EditCommand = new AsyncRelayCommand<StockCountingDetailModel>(Edit);
            DeleteCommand = new AsyncRelayCommand<StockCountingDetailModel>(Delete);
        }

        [RelayCommand]
        public void PerformSearch(string search = null)
        {
            if (string.IsNullOrEmpty(search))
            {
                FilteredStockCountingDetails = StockCountingModel.CountingDetail;
                return;
            }

            search = search.ToLower();
            FilteredStockCountingDetails
                = new ObservableCollection<StockCountingDetailModel>(
                    StockCountingModel.CountingDetail
                    .Where(x => x.ItemName.Contains(search, StringComparison.CurrentCultureIgnoreCase)
                    || x.ItemCode.Contains(search, StringComparison.CurrentCultureIgnoreCase)));
        }

        private async Task AddItem()
        {
            try
            {
                await RedirectToPage(StockCountingModel, new AddItem(new StockCountingDetailViewModel(this, new StockCountingDetailModel())), false);
            }
            catch (Exception ex) { DisplayErrorMessage(ex.StackTrace); }
        }

        private async Task Submit()
        {
            if (!StockCountingModel.CountingDetail.Any())
            {
                DisplayErrorMessage("No items to submit");
                return;
            }

            if (!await DisplayAlert("Confirm", "Are you sure you want to submit stock counting?", "Yes", "No")) return;

            StockCountingModel stockCounting = StockCountingModel;
            PostAsync("stockcounting/updatecounting", ref stockCounting
                , new Dictionary<string, string?>()
                {
                    { "StockCountingID", StockCountingModel.StockCountingId.ToString() },
                    { "UserID", HomePageViewModel.User.UserId.ToString() }
                }, displayAlert: true, showResponse: true);

            if (stockCounting.Exception == null)
            {
                Pop();
            }
        }

        private async Task Discard()
        {
            if (!await DisplayAlert("Confirm", "Are you sure you want to discard stock counting?", "Yes", "No")) return;

            StockCountingModel stockCounting = StockCountingModel;
            PostAsync("stockcounting/discardcounting", ref stockCounting
                , new Dictionary<string, string?>()
                {
                    { "StockCountingID", StockCountingModel.StockCountingId.ToString() },
                    { "UserID", HomePageViewModel.User.UserId.ToString() }
                }, displayAlert: true, showResponse: true);

            if (stockCounting.Exception == null)
            {
                Pop();
            }
        }

        private async Task Edit(StockCountingDetailModel selected) 
        {
            await RedirectToPage(StockCountingModel, new AddItem(new StockCountingDetailViewModel(this, selected)), false);
        }

        private async Task Delete(StockCountingDetailModel selected)
        {
            string confirmMessage = "Are you sure you want to delete stock counting the following item?";
            confirmMessage += $"\n\n\t {selected.ItemName}";
            confirmMessage += $"\n\n\t\t EAN : {selected.ItemCode}";
            confirmMessage += $"\n\t\t MRP : {selected.MRP}";
            confirmMessage += $"\n\t\t Sale price : {selected.SalePrice}";

            if (!await DisplayAlert("Confirm", confirmMessage, "Yes", "No")) return;

            PostAsync("stockcounting/deletecountingdetail", ref selected
                    , new Dictionary<string, string?>()
                    {
                        { "StockCountingDetailID", selected.StockCountingDetailId.ToString() }
                    }, displayAlert: true, showResponse: true);

            if (selected.Exception == null)
            {
                //StockCountingModel.CountingDetail.Remove(selected);
                Reload(); //to refresh serial #s
            }
        }

        public void Reload()
        {
            var stockCountingModel = StockCountingModel;
            GetAsync("stockcounting/getcounting", ref stockCountingModel
                , new Dictionary<string, string?>()
                {
                    { "UserID", userID.ToString() },
                    { "isNested", "True" }
                }, true);

            StockCountingModel = stockCountingModel;
            PerformSearch();
        }
    }
}
