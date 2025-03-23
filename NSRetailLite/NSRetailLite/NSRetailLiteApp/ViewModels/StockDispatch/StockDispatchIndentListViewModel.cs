using CommunityToolkit.Mvvm.Input;
using DevExpress.CodeParser;
using NSRetailLiteApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.StockDispatch
{
    public partial class StockDispatchIndentListViewModel : BaseViewModel
    {
        private readonly LoggedInUser loggedInUser;

        public ObservableCollection<BranchIndentGroup> IndentData {  get; }

        public IAsyncRelayCommand<BranchIndent?> DiscardCommand { get; }

        public StockDispatchIndentListViewModel(ObservableCollection<BranchIndent> branchIndentList
            , LoggedInUser loggedInUser)
        {
            BranchIndentList = branchIndentList;
            this.loggedInUser = loggedInUser;
            IndentData = new ObservableCollection<BranchIndentGroup>();
            DiscardCommand = new AsyncRelayCommand<BranchIndent?>(Discard);
            BuildModel();
        }

        public ObservableCollection<BranchIndent> BranchIndentList { get; }

        private async Task Discard(BranchIndent? selected)
        {
            if (selected == null) return;

            if (!await DisplayAlert("Confirm"
                , $"Are you sure you want to discard indent for {selected.BranchName} ({selected.SubCategoryName})? This operation cannot be undone"
                , "Yes", "No")) return;

            HolderClass holder = new HolderClass();
            await PostAsync("Stockdispatch_v2/discardbranchindent", holder, new Dictionary<string, string?>
            {
                { "BranchIndentID", selected.BranchIndentID.ToString() },
                { "UserID", loggedInUser.UserId.ToString() }
            });

            if (holder.Exception != null) return;

            BranchIndentList.Remove(selected);
            BuildModel();
        }

        private void BuildModel()
        {
            IndentData.Clear();
            BranchIndentList
                .GroupBy(x => x.BranchName ?? string.Empty)
                .Select(x => new BranchIndentGroup(x.Key, x.OrderBy(x => x.SubCategoryName.Length).ThenBy(x => x.SubCategoryName).ToList()))
                .ToList().ForEach(IndentData.Add);
        }
    }

    public class BranchIndentGroup : ObservableCollection<BranchIndent>
    {
        public string Name { get; private set; }

        public BranchIndentGroup(string name, List<BranchIndent> branchIndentModels) : base(branchIndentModels)
        {
            Name = name;
        }
    }
}
