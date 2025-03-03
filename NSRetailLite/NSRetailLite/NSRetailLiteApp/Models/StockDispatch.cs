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
        [ObservableProperty]
        public int _stockDispatchId;

        [ObservableProperty]
        public ObservableCollection<BranchIndentDetailModel> _branchIndentDetailList;

        [ObservableProperty]
        public ObservableCollection<StockDispatchDetailModel> _stockDispatachDetailIndentList;

        [ObservableProperty]
        public ObservableCollection<StockDispatchDetailModel> _stockDispatachDetailManualList;
    }

    public partial class StockDispatchDetailModel : BaseObservableObject
    {
        [ObservableProperty]
        public int _stockDispatchDetailId;
    }

    public partial class BranchIndentDetailModel : BaseObservableObject
    {
        [ObservableProperty]
        public int _branchIndentDetailId;

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

        [ObservableProperty]
        public double _branchStock;

        [ObservableProperty]
        public double _avgSales;

        [ObservableProperty]
        public double _noOfDaySales;

        [ObservableProperty]
        public double _indentQuantity;

        [ObservableProperty]
        public DateTime _lastDispatchDate;
    }
}
