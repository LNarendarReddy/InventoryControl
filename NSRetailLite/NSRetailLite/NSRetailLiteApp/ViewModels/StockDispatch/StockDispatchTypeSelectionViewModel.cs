using CommunityToolkit.Mvvm.ComponentModel;
using NSRetailLiteApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.StockDispatch
{
    public partial class StockDispatchTypeSelectionViewModel : BaseViewModel
    {
        [ObservableProperty]
        private bool _isCategoryUser;

        [ObservableProperty]
        private bool _isSubCategoryUser;

        public StockDispatchTypeSelectionViewModel(LoggedInUser user)
        {
            User = user;
            IsCategoryUser = User.SubCategoryId == 0;
            IsSubCategoryUser = User.SubCategoryId > 0;
        }

        public LoggedInUser User { get; }
    }
}
