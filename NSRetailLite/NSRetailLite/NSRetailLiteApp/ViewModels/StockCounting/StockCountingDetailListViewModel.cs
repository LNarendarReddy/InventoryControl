using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NSRetailLiteApp.Models;
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

        public StockCountingDetailListViewModel(StockCountingModel stockCountingModel) 
        {
            StockCountingModel = stockCountingModel;
            FilteredStockCountingDetails = stockCountingModel.CountingDetail;
        }

        [RelayCommand]
        public void PerformSearch(string search = null)
        {
            if(string.IsNullOrEmpty(search))
            {
                FilteredStockCountingDetails = StockCountingModel.CountingDetail;
                return;
            }

            search = search.ToLower();
            FilteredStockCountingDetails 
                = new ObservableCollection<StockCountingDetailModel>(
                    StockCountingModel.CountingDetail
                    .Where(x => x.ItemName.ToLower().Contains(search) || x.ItemCode.ToLower().Contains(search)));
        }
    }
}
