using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevExpress.CodeParser;
using DevExpress.XtraRichEdit.Commands;
using Newtonsoft.Json;
using NSRetailLiteApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NSRetailLiteApp.ViewModels.StockDispatch.Indent
{
    public partial class StockDispatchByIndentViewModel : BaseViewModel
    {

        [ObservableProperty]
        public StockDispatchModel _stockDispatchModel;

        public ObservableCollection<ItemGroup> ItemsData { get; } = new ObservableCollection<ItemGroup>();

        private readonly int userID;

        public IAsyncRelayCommand StartDispatchCommand { get; }

        public bool AllowStart => (StockDispatchModel?.SubCategoryId ?? 0) != 0;

        public bool IsNew => (StockDispatchModel?.StockDispatchId ?? 0) <= 0;

        public StockDispatchByIndentViewModel(StockDispatchModel stockDispatchModel, int UserID)
        {
            StockDispatchModel = stockDispatchModel;
            userID = UserID;
            StockDispatchModel.UserId = UserID;

            StockDispatchModel.BranchIndentDetailList
                .GroupBy(x => x.SubCategoryName)
                .Select(x => new ItemGroup(x.Key, x.ToList()))
                .OrderBy(x => x.Name.Length)
                .ToList().ForEach(ItemsData.Add);

            StartDispatchCommand = new AsyncRelayCommand(StartDispatch);
            //SubmitCommand = new AsyncRelayCommand(Submit);
            //DiscardCommand = new AsyncRelayCommand(Discard);
            //EditCommand = new AsyncRelayCommand<StockCountingDetailModel>(Edit);
            //DeleteCommand = new AsyncRelayCommand<StockCountingDetailModel>(Delete);
        }

        private async Task StartDispatch()
        {
            try
            {
                if (!await DisplayAlert("Confirm", "Are you sure? this operation cannot be reversed", "Yes", "No"))
                    return;

                StockDispatchModel = await PostAsyncAsContent("Stockdispatch_v2/savebranchindent", StockDispatchModel);
            }
            catch (Exception ex) { DisplayErrorMessage(ex.StackTrace); }
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
