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
        public ObservableCollection<BranchIndentGroup> IndentData {  get; }
        public StockDispatchIndentListViewModel(ObservableCollection<BranchIndent> branchIndentList)
        {
            BranchIndentList = branchIndentList;
            IndentData = new ObservableCollection<BranchIndentGroup>();
            branchIndentList
                .GroupBy(x => x.BranchName ?? string.Empty)
                .Select(x => new BranchIndentGroup(x.Key, x.OrderBy(x => x.SubCategoryName.Length).ThenBy(x => x.SubCategoryName).ToList()))
                .ToList().ForEach(IndentData.Add);
        }

        public ObservableCollection<BranchIndent> BranchIndentList { get; }
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
