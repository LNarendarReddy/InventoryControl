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
        [JsonIgnore]
        [ObservableProperty]
        public int _stockCountingDetailId;

        [JsonIgnore]
        [ObservableProperty]
        public int _stockCountingId;

        [JsonIgnore]
        [ObservableProperty]
        public int _itemPriceId;

        [JsonIgnore]
        [ObservableProperty]
        public string _itemCode;

        [JsonIgnore]
        [ObservableProperty]
        public string _sKUCode;

        [JsonIgnore]
        [ObservableProperty]
        public string _itemName;

        [JsonIgnore]
        [ObservableProperty]
        public string _mRP;

        [JsonIgnore]
        [ObservableProperty]
        public string _salePrice;

        [JsonIgnore]
        [ObservableProperty]
        public int _quantity;

        [JsonIgnore]
        [ObservableProperty]
        public double _weightInKGs;

        [JsonIgnore]
        [ObservableProperty]
        public int _sNo;

        [JsonIgnore]
        [ObservableProperty]
        public bool _isOpenItem;
    }
}
