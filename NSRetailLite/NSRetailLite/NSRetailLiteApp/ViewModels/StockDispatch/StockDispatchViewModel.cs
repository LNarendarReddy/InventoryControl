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

        private readonly LoggedInUser user;

        public IAsyncRelayCommand StartDispatchCommand { get; }
        public IAsyncRelayCommand SubmitCommand { get; }
        public IAsyncRelayCommand DiscardCommand { get; }
        public IAsyncRelayCommand AddManualCommand { get; }
        public IAsyncRelayCommand<BranchIndentDetailModel> AddIndentQuantityCommand { get; }

        public IAsyncRelayCommand<BranchIndentDetailModel> EditIndentQuantityCommand { get; }

        public IAsyncRelayCommand<StockDispatchDetailModel> EditManualQuantityCommand { get; }

        public IAsyncRelayCommand<StockDispatchDetailModel> DeleteManualQuantityCommand { get; }

        [ObservableProperty]
        private bool _allowStart;

        [ObservableProperty]
        private bool _isNew;

        [ObservableProperty]
        private bool _isManual;

        [ObservableProperty]
        private string _title;

        public StockDispatchViewModel(StockDispatchModel stockDispatchModel, LoggedInUser User)
        {
            StockDispatchModel = stockDispatchModel;
            user = User;

            BuildModelData();

            StartDispatchCommand = new AsyncRelayCommand(StartDispatch);
            SubmitCommand = new AsyncRelayCommand(Submit);
            DiscardCommand = new AsyncRelayCommand(Discard);
            AddManualCommand = new AsyncRelayCommand(AddManual);
            AddIndentQuantityCommand = new AsyncRelayCommand<BranchIndentDetailModel?>(AddIndentQuantity);
            EditIndentQuantityCommand = new AsyncRelayCommand<BranchIndentDetailModel?>(EditIndentQuantity);
            EditManualQuantityCommand = new AsyncRelayCommand<StockDispatchDetailModel?>(EditManualQuantity);
            DeleteManualQuantityCommand = new AsyncRelayCommand<StockDispatchDetailModel?>(DeleteManualQuantity);
        }

        private async Task StartDispatch()
        {
            try
            {
                if (!await DisplayAlert("Confirm", "Are you sure to start dispatch? this operation cannot be reversed", "Yes", "No"))
                    return;

                StockDispatchModel = await PostAsyncAsContent("Stockdispatch_v2/savebranchindent", StockDispatchModel);
                BuildModelData();                
            }
            catch (Exception ex) { DisplayErrorMessage(ex.StackTrace); }
        }

        private async Task Submit()
        {
            if (!await DisplayAlert("Confirm", "Are you sure you want to submit dispatch? this operation cannot be reversed", "Yes", "No"))
                return;

            HolderClass holderClass = new HolderClass();
            holderClass = await PostAsync("Stockdispatch_v2/updatedispatch", holderClass, new Dictionary<string, string?>
            {
                { "StockDispatchID", StockDispatchModel.StockDispatchId.ToString() }
            });

            if (holderClass?.Exception == null) Pop();
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

            if (holderClass?.Exception == null) Pop();
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
                    new StockDispatchDetailViewModel(stockDispatchDetailModel, null, StockDispatchModel, user)));
        }

        private async Task AddIndentQuantity(BranchIndentDetailModel? selected)
        {
            if(selected == null) return;

            StockDispatchDetailModel stockDispatchDetailModel = new StockDispatchDetailModel()
            {
                ItemCode = selected.SkuCode,
                StockDispatchId = StockDispatchModel.StockDispatchId,
                IsNew = true
            };

            await RedirectToPage(stockDispatchDetailModel
                , new StockDispatchDetailPage(
                    new StockDispatchDetailViewModel(stockDispatchDetailModel, selected, StockDispatchModel, user)));
        }

        private async Task EditIndentQuantity(BranchIndentDetailModel? selected)
        {
            if (selected == null) return;

            await RedirectToPage(selected, new StockDispatchDetailListPage(
                new StockDispatchDetailListViewModel(selected, StockDispatchModel, user)));
        }

        private async Task EditManualQuantity(StockDispatchDetailModel? selected)
        {
            if (selected == null) return;

            await RedirectToPage(selected
                , new StockDispatchDetailPage(
                    new StockDispatchDetailViewModel(selected, null, StockDispatchModel, user)));
        }

        private async Task DeleteManualQuantity(StockDispatchDetailModel? selected)
        {
            if (selected == null) return;

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

        private void BuildModelData()
        {
            if(StockDispatchModel == null) return;

            AllowStart = (StockDispatchModel?.SubCategoryId ?? 0) != 0;
            IsNew = (StockDispatchModel?.StockDispatchId ?? 0) <= 0;
            IsManual = !IsNew && StockDispatchModel?.BranchIndentId == 0;
            string filler = IsManual ? "Manual" : $" {StockDispatchModel.NoOfDays} days"; 
            Title = $"{StockDispatchModel.ToBranchName} - ( {filler} )";
            StockDispatchModel.UserId = user.UserId;

            if (StockDispatchModel.BranchIndentDetailList == null) return;

            ItemsData.Clear();
            StockDispatchModel.BranchIndentDetailList
                .GroupBy(x => x.SubCategoryName ?? string.Empty)
                .Select(x => new ItemGroup(x.Key, x.ToList()))
                .OrderBy(x => x.Name.Length)
                .ToList().ForEach(ItemsData.Add);
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
}
