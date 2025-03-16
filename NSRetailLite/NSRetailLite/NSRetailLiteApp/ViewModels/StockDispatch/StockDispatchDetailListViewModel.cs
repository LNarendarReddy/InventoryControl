using CommunityToolkit.Mvvm.Input;
using NSRetailLiteApp.Models;
using NSRetailLiteApp.Views.StockDispatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NSRetailLiteApp.ViewModels.StockDispatch
{
    public class StockDispatchDetailListViewModel : BaseViewModel
    {
        private readonly LoggedInUser user;

        public IAsyncRelayCommand<StockDispatchDetailModel?> EditCommand { get; }
        public IAsyncRelayCommand<StockDispatchDetailModel?> DiscardCommand { get; }
        public IAsyncRelayCommand AddCommand { get; }

        public StockDispatchDetailListViewModel(BranchIndentDetailModel branchIndentDetailModel
            , StockDispatchModel stockDispatchModel
            , LoggedInUser user)
        {
            BranchIndentDetailModel = branchIndentDetailModel;
            StockDispatchModel = stockDispatchModel;
            this.user = user;
            //SaveCommand = new AsyncRelayCommand(Save);
            //LoadItemCommand = new AsyncRelayCommand(LoadItem);

            EditCommand = new AsyncRelayCommand<StockDispatchDetailModel?>(Edit);
            DiscardCommand = new AsyncRelayCommand<StockDispatchDetailModel?>(Discard);
            AddCommand = new AsyncRelayCommand(Add);
        }

        public BranchIndentDetailModel BranchIndentDetailModel { get; }

        public StockDispatchModel StockDispatchModel { get; }

        private async Task Add()
        {
            StockDispatchDetailModel stockDispatchDetailModel = new StockDispatchDetailModel()
            {
                StockDispatchId = StockDispatchModel.StockDispatchId,
                ItemCode = BranchIndentDetailModel.SkuCode,
                IsNew = true,
            };

            await RedirectToPage(stockDispatchDetailModel
                , new StockDispatchDetailPage(
                    new StockDispatchDetailViewModel(stockDispatchDetailModel, null, StockDispatchModel, user)));
        }

        private async Task Edit(StockDispatchDetailModel? selected)
        {
            if (selected == null) return;

            await RedirectToPage(selected
                , new StockDispatchDetailPage(
                    new StockDispatchDetailViewModel(selected, null, StockDispatchModel, user)));
        }

        private async Task Discard(StockDispatchDetailModel? selected)
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

            BranchIndentDetailModel.StockDispatchDetailIndentList.Remove(selected);
            BranchIndentDetailModel.RecalculateDispatchQuantity();
        }
    }
}
