using CommunityToolkit.Mvvm.Input;
using NSRetailLiteApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.StockDispatch
{
    public class StockDispatchDetailListViewModel : BaseViewModel
    {
        private readonly LoggedInUser user;

        public StockDispatchDetailListViewModel(BranchIndentDetailModel branchIndentDetailModel
            , StockDispatchModel stockDispatchModel
            , LoggedInUser user)
        {
            BranchIndentDetailModel = branchIndentDetailModel;
            StockDispatchModel = stockDispatchModel;
            this.user = user;
            //SaveCommand = new AsyncRelayCommand(Save);
            //LoadItemCommand = new AsyncRelayCommand(LoadItem);
        }

        public BranchIndentDetailModel BranchIndentDetailModel { get; }

        public StockDispatchModel StockDispatchModel { get; }
    }
}
