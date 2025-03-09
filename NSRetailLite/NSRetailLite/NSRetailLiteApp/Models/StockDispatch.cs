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
    public partial class StockDispatchModel : BaseObservableObject
    {
        [JsonIgnore]
        [ObservableProperty]
        public int _stockDispatchId;

        [JsonIgnore]
        [ObservableProperty]
        public ObservableCollection<BranchIndentDetailModel> _branchIndentDetailList;

        [JsonIgnore]
        [ObservableProperty]
        public ObservableCollection<StockDispatchDetailModel> _stockDispatachDetailIndentList;

        [JsonIgnore]
        [ObservableProperty]
        public ObservableCollection<StockDispatchDetailModel> _stockDispatachDetailManualList;
    }

    public partial class StockDispatchDetailModel : BaseObservableObject
    {
        [JsonIgnore]
        [ObservableProperty]
        public int _stockDispatchDetailId;
    }

    public partial class BranchIndentDetailModel : BaseObservableObject
    {
        [JsonIgnore]
        [ObservableProperty]
        public int _branchIndentDetailId;

        [JsonIgnore]
        [ObservableProperty]
        public int _itemId;
                
        [JsonIgnore]
        [ObservableProperty]
        public string _skuCode;

        [JsonIgnore]
        [ObservableProperty]
        public string _itemName;

        [JsonIgnore]
        [ObservableProperty]
        public string _subCategoryName;

        [JsonIgnore]
        [ObservableProperty]
        public double _branchStock;

        [JsonIgnore]
        [ObservableProperty]
        public double _avgSales;

        [JsonIgnore]
        [ObservableProperty]
        public double _noOfDaySales;

        [JsonIgnore]
        [ObservableProperty]
        public double _indentQuantity;

        [JsonIgnore]
        [ObservableProperty]
        public DateTime _lastDispatchDate;
    }
}
