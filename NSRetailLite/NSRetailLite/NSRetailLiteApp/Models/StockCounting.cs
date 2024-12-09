using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.Models
{
   
    public partial class StockCountingModel : BaseObservableObject
    {
        [ObservableProperty]
        public int _stockCountingId;

        [ObservableProperty]
        public int _branchId;

        [ObservableProperty]
        public string _branchName;

        [ObservableProperty]
        public ObservableCollection<StockCountingDetailModel> _countingDetailList;

        public StockCountingModel()
        {
            CountingDetailList = [];
        }
    }

    public partial class StockCountingDetailModel : BaseObservableObject
    {
        [ObservableProperty]
        public int _stockCountingDetailId;

        [ObservableProperty]
        public int _stockCountingId;

        [ObservableProperty]
        public int _itemPriceId;

        [ObservableProperty]
        public string _itemCode;

        [ObservableProperty]
        public string _sKUCode;

        [ObservableProperty]
        public string _itemName;

        [ObservableProperty]
        public string _mRP;

        [ObservableProperty]
        public string _salePrice;

        [ObservableProperty]
        public int _quantity;

        [ObservableProperty]
        public double _weightInKGs;

        [ObservableProperty]
        public int _sNo;

        [ObservableProperty]
        public bool _isOpenItem;
    }
}
