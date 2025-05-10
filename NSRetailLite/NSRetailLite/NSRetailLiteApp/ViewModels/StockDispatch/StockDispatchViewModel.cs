using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevExpress.CodeParser;
using DevExpress.XtraRichEdit.Commands;
using Newtonsoft.Json;
using NSRetailLiteApp.Models;
using NSRetailLiteApp.Views.StockDispatch;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.StockDispatch.Indent
{
    public partial class StockDispatchViewModel : BaseViewModel
    {
        [ObservableProperty]
        public StockDispatchModel _stockDispatchModel;

        public ObservableCollection<ItemGroup> ItemsData { get; } = new ObservableCollection<ItemGroup>();
        public ObservableCollection<TrayWiseGroup> TrayWiseData { get; } = new ObservableCollection<TrayWiseGroup>();

        private readonly LoggedInUser user;

        public IAsyncRelayCommand StartDispatchCommand { get; }

        public IAsyncRelayCommand SubmitCommand { get; }

        public IAsyncRelayCommand DiscardCommand { get; }

        public IAsyncRelayCommand AddManualCommand { get; }

        public IAsyncRelayCommand SearchSKUCommand { get; }

        public IAsyncRelayCommand<BranchIndentDetailModel> AddIndentQuantityCommand { get; }

        public IAsyncRelayCommand<BranchIndentDetailModel> EditIndentQuantityCommand { get; }

        public IAsyncRelayCommand<StockDispatchDetailModel> EditManualQuantityCommand { get; }
        
        public IAsyncRelayCommand<StockDispatchDetailModel> EditManualQuantityOnlyCommand { get; }

        public IAsyncRelayCommand<StockDispatchDetailModel> DeleteManualQuantityCommand { get; }

        public IAsyncRelayCommand<StockDispatchDetailModel> VerifyTrayDispatchCommand { get; }

        [ObservableProperty]
        private bool _allowStart;

        [ObservableProperty]
        private bool _isNew;

        [ObservableProperty]
        private bool _isManual;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string _searchCode;

        [ObservableProperty]
        private Item? _foundItem;

        public StockDispatchViewModel(StockDispatchModel stockDispatchModel, LoggedInUser User)
        {
            StockDispatchModel = stockDispatchModel;
            user = User;

            BuildModelData();

            StartDispatchCommand = new AsyncRelayCommand(StartDispatch);
            SubmitCommand = new AsyncRelayCommand(Submit);
            DiscardCommand = new AsyncRelayCommand(Discard);
            AddManualCommand = new AsyncRelayCommand(AddManual);
            SearchSKUCommand = new AsyncRelayCommand(SearchSKU);
            AddIndentQuantityCommand = new AsyncRelayCommand<BranchIndentDetailModel?>(AddIndentQuantity);
            EditIndentQuantityCommand = new AsyncRelayCommand<BranchIndentDetailModel?>(EditIndentQuantity);
            EditManualQuantityCommand = new AsyncRelayCommand<StockDispatchDetailModel?>(EditManualQuantity);
            EditManualQuantityOnlyCommand = new AsyncRelayCommand<StockDispatchDetailModel?>(EditManualQuantityOnly);
            DeleteManualQuantityCommand = new AsyncRelayCommand<StockDispatchDetailModel?>(DeleteManualQuantity);
            VerifyTrayDispatchCommand = new AsyncRelayCommand<StockDispatchDetailModel?>(VerifyTrayDispatch);
        }

        private async Task StartDispatch()
        {
            try
            {
                if (!await DisplayAlert("Confirm", "Are you sure to start dispatch? this operation cannot be reversed", "Yes", "No"))
                    return;

                StockDispatchModel = await PostAsyncAsContent("Stockdispatch_v2/savebranchindent", StockDispatchModel);
                
                if (StockDispatchModel.Exception != null) return;

                await DisplayAlert("Success", "Branch Indent saved successfully", "OK");
                await Pop();                
            }
            catch (Exception ex) { DisplayErrorMessage(ex.StackTrace); }
        }

        private async Task Submit()
        {
            if (!await DisplayAlert("Confirm", "Are you sure you want to submit dispatch? this operation cannot be reversed", "Yes", "No"))
                return;

            List<BranchIndentDetailModel> branchIndents = StockDispatchModel.BranchIndentDetailList.ToList();
            string stats = $"Review the values before submitting, this operation cannot be reversed {Environment.NewLine}";
            stats += $"{Environment.NewLine}    * Total indent items : {branchIndents.Count}";
            stats += $"{Environment.NewLine}    * Added indent items : {branchIndents.Count(x => x.DispatchQuantity > 0)}";
            stats += $"{Environment.NewLine}    * Added manual items: {StockDispatchModel.StockDispatchDetailManualList.Count()}";
            stats += Environment.NewLine;
            stats += $"{Environment.NewLine}    * Total skipped indent items : {branchIndents.Count(x => x.DispatchQuantity == 0)}";
            stats += $"{Environment.NewLine}    * Skipped items with WH Stock > 0: {branchIndents.Count(x => x.DispatchQuantity == 0 && x.WHStock > 0)}";
            stats += $"{Environment.NewLine}    * Skipped items with WH Stock = 0: {branchIndents.Count(x => x.DispatchQuantity == 0 && x.WHStock == 0)}";

            if (!await DisplayAlert("Confirm", stats, "Yes", "No"))
                return;

            HolderClass holderClass = new HolderClass();
            holderClass = await PostAsync("Stockdispatch_v2/updatedispatch", holderClass, new Dictionary<string, string?>
            {
                { "StockDispatchID", StockDispatchModel.StockDispatchId.ToString() }
            });

            if (holderClass?.Exception == null) await Pop();
        }

        private async Task Discard()
        {
            if (!await DisplayAlert("Confirm", "Are you sure you want to discard dispatch? this operation cannot be reversed", "Yes", "No"))
                return;

            HolderClass holderClass = new HolderClass();
            holderClass = await PostAsync("Stockdispatch_v2/discarddispatch", holderClass, new Dictionary<string, string?>
            {
                { "StockDispatchID", StockDispatchModel.StockDispatchId.ToString() },
                { "UserID", user.UserId.ToString() }
            });

            if (holderClass?.Exception == null) await Pop();
        }

        private async Task AddManual()
        {
            StockDispatchDetailModel stockDispatchDetailModel = new StockDispatchDetailModel()
            {
                StockDispatchId = StockDispatchModel.StockDispatchId,
                IsNew = true,                
            };

            await RedirectToPage(stockDispatchDetailModel
                , new StockDispatchDetailPage(
                    new StockDispatchDetailViewModel(stockDispatchDetailModel, null, StockDispatchModel
                        , user, showItemScanInCodeSelection: true, popAfterSave: false)));
        }

        private async Task AddIndentQuantity(BranchIndentDetailModel? selected)
        {
            if(selected == null || IsNew) return;

            StockDispatchDetailModel stockDispatchDetailModel = new StockDispatchDetailModel()
            {
                ItemCode = selected.SkuCode,
                StockDispatchId = StockDispatchModel.StockDispatchId,
                IsNew = true
            };

            await RedirectToPage(stockDispatchDetailModel
                , new StockDispatchDetailPage(
                    new StockDispatchDetailViewModel(stockDispatchDetailModel, selected, StockDispatchModel
                        , user, showItemScanInCodeSelection: true, cachedItem: FoundItem)));
        }

        private async Task EditIndentQuantity(BranchIndentDetailModel? selected)
        {
            if (selected == null || IsNew) return;

            await RedirectToPage(selected, new StockDispatchDetailListPage(
                new StockDispatchDetailListViewModel(selected, StockDispatchModel, user)));
        }

        private async Task EditManualQuantity(StockDispatchDetailModel? selected)
        {
            if (selected == null || IsNew) return;

            await RedirectToPage(selected
                , new StockDispatchDetailPage(
                    new StockDispatchDetailViewModel(selected, null, StockDispatchModel
                        , user, showItemScanInCodeSelection: true)));
        }

        private async Task EditManualQuantityOnly(StockDispatchDetailModel? selected)
        {
            if (selected == null || IsNew) return;

            await RedirectToPage(selected
                , new StockDispatchDetailPage(
                    new StockDispatchDetailViewModel(selected
                        , StockDispatchModel.BranchIndentDetailList.FirstOrDefault(x=>x.StockDispatchDetailIndentList.Contains(selected))
                        , StockDispatchModel, user, false, showItemScanInCodeSelection: true)));
        }

        private async Task DeleteManualQuantity(StockDispatchDetailModel? selected)
        {
            if (selected == null || IsNew) return;

            if (!await DisplayAlert("Confirm Delete",
                $"Are you sure you want to delete dispatch for item {selected.ItemName} - ({selected.ItemCode})?"
                , "Yes", "No")) return;

            HolderClass holderClass = new HolderClass();
            holderClass = await PostAsync("Stockdispatch_v2/deletedispatchdetail", holderClass, new Dictionary<string, string?>
                                {
                                    { "StockDispatchDetailID", selected.StockDispatchDetailId.ToString() }
                                });

            if (holderClass.Exception != null) return;

            BranchIndentDetailModel parent = StockDispatchModel
                .BranchIndentDetailList.FirstOrDefault(x => 
                        x.StockDispatchDetailIndentList.Any(y => y.StockDispatchDetailId == selected.StockDispatchDetailId));

            if (parent == null)
            {
                StockDispatchModel.StockDispatchDetailManualList.Remove(selected);
            }
            else
            { 
                parent.StockDispatchDetailIndentList.Remove(selected);
                parent.RecalculateDispatchQuantity();
            }
        }

        private async Task VerifyTrayDispatch(StockDispatchDetailModel? selected)
        {
            if (selected == null || IsNew) return;

            HolderClass holderClass = new HolderClass();
            holderClass = await PostAsync("Stockdispatch_v2/trayverifystockdispatchdetail", holderClass, new Dictionary<string, string?>
                                {
                                    { "StockDispatchDetailID", selected.StockDispatchDetailId.ToString() },
                                    { "IsTrayVerified", (!selected.IsTrayVerified).ToString() }
                                });

            if (holderClass.Exception != null) return;

            selected.IsTrayVerified = !selected.IsTrayVerified;
        }

        private async Task SearchSKU()
        {
            if(string.IsNullOrEmpty(SearchCode))
            {
                FilterByItem(null);
                return;
            }

            Item item = new() { SKUCode = SearchCode };

            item = await GetAsync("Stockdispatch_v2/getitem", item
                , new Dictionary<string, string?>()
                {
                    { "ItemCode", SearchCode },
                    { "CategoryID", user.CategoryId.ToString() },
                    { "SubCategoryID", user.SubCategoryId.ToString() }
                }, displayAlert: true);

            if (item.Exception != null)
            {
                SearchCode = string.Empty;
                return;
            }

            FilterByItem(item, true);
        }

        private void BuildModelData()
        {
            if(StockDispatchModel == null) return;

            AllowStart = user.SubCategoryId == 0;
            IsNew = (StockDispatchModel?.StockDispatchId ?? 0) <= 0;
            IsManual = !IsNew && StockDispatchModel?.BranchIndentId == 0;
            string filler = IsManual ? "Manual" : $" {StockDispatchModel.NoOfDays} days"; 
            Title = $"{StockDispatchModel.ToBranchName} - ( {filler} )";
            StockDispatchModel.UserId = user.UserId;

            if (StockDispatchModel.BranchIndentDetailList == null) return;

            FilterByItem(null);
        }

        private void FilterByItem(Item? item, bool showMessage = false)
        {
            ItemsData.Clear();
            FoundItem = null;
            List<BranchIndentDetailModel> branchIndents;

            branchIndents = StockDispatchModel.BranchIndentDetailList.Where(x => item == null || x.ItemId == item.ItemID).ToList();
            if (branchIndents.Count == 0)
            {
                if (showMessage)
                    DisplayAlert("Not found", "Item not found in indent list", "OK");
                branchIndents = StockDispatchModel.BranchIndentDetailList.ToList();
                SearchCode = string.Empty;
            }
            else
            {
                FoundItem = item;
            }

            branchIndents
                .GroupBy(x => x.SubCategoryName ?? string.Empty)
                .Select(x => new ItemGroup(x.Key, x.ToList()))
                .OrderBy(x => x.Name.Length).ThenBy(x => x.Name)
                .ToList().ForEach(ItemsData.Add);
        }

        partial void OnSearchCodeChanged(string value)
        {
            if (string.IsNullOrEmpty(value) && FoundItem != null)
            {
                FilterByItem(null);
            }
        }

        public void BuildTrayWiseData()
        {
            TrayWiseData.Clear();
            List<StockDispatchDetailModel> allDispatchDetails = new List<StockDispatchDetailModel>();

            allDispatchDetails.AddRange(StockDispatchModel.BranchIndentDetailList.SelectMany(x => x.StockDispatchDetailIndentList));
            allDispatchDetails.AddRange(StockDispatchModel.StockDispatchDetailManualList);

            allDispatchDetails
                .GroupBy(x => x.TrayNumber.ToString())
                .Select(x => new TrayWiseGroup(x.Key, x.ToList()))
                .OrderBy(x => x.Name)
                .ToList().ForEach(TrayWiseData.Add);
        }
    }


    public class ItemGroup : ObservableCollection<BranchIndentDetailModel>
    {
        public string Name { get; private set; }

        public ItemGroup(string name, List<BranchIndentDetailModel> branchIndentDetailModels) : base(branchIndentDetailModels)
        {
            Name = name;
        }
    }

    public class TrayWiseGroup : ObservableCollection<StockDispatchDetailModel>
    {
        public string Name { get; private set; }

        public int PieceCount { get; private set; }


        public TrayWiseGroup(string name, List<StockDispatchDetailModel> stockDispatchDetailModels) : base(stockDispatchDetailModels)
        {
            Name = name;
            PieceCount = stockDispatchDetailModels.Sum(x => x.DispatchQuantity);
        }
    }
}
