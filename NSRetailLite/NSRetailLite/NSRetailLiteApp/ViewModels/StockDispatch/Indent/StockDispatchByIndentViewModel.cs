using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevExpress.CodeParser;
using DevExpress.XtraRichEdit.Commands;
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

        public StockDispatchByIndentViewModel(StockDispatchModel stockDispatchModel, int UserID)
        {
            StockDispatchModel = stockDispatchModel;
            userID = UserID;

            StockDispatchModel.BranchIndentDetailList
                .GroupBy(x => x.SubCategoryName)
                .Select(x => new ItemGroup(x.Key, x.ToList()))
                .OrderBy(x => x.Name.Length)
                .ToList().ForEach(ItemsData.Add);

            //AddItemCommand = new AsyncRelayCommand(AddItem);
            //SubmitCommand = new AsyncRelayCommand(Submit);
            //DiscardCommand = new AsyncRelayCommand(Discard);
            //EditCommand = new AsyncRelayCommand<StockCountingDetailModel>(Edit);
            //DeleteCommand = new AsyncRelayCommand<StockCountingDetailModel>(Delete);
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
