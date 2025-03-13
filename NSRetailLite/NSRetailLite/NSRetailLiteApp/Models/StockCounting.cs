using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
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
        private int _stockCountingId;

        [ObservableProperty]
        private int _branchId;

        [ObservableProperty]
        private string _branchName;

        [ObservableProperty]
        private ObservableCollection<StockCountingDetailModel> _countingDetailList;

        public StockCountingModel()
        {
            CountingDetailList = [];
        }
    }

    public partial class StockCountingDetailModel : BaseObservableObject
    {
        [ObservableProperty]
        private int _stockCountingDetailId;

        [ObservableProperty]
        private int _stockCountingId;

        [ObservableProperty]
        private int _itemPriceId;

        [ObservableProperty]
        private string _itemCode;

        [ObservableProperty]
        private string _sKUCode;

        [ObservableProperty]
        private string _itemName;

        [ObservableProperty]
        private string _mRP;

        [ObservableProperty]
        private string _salePrice;

        [ObservableProperty]
        private int _quantity;

        [ObservableProperty]
        private double _weightInKGs;

        [ObservableProperty]
        private int _sNo;

        [ObservableProperty]
        private bool _isOpenItem;
    }
}