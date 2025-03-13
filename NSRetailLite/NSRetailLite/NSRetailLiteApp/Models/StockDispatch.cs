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
        public int _branchIndentId;

        [JsonIgnore]
        [ObservableProperty]
        public int _fromBranchId;

        [JsonIgnore]
        [ObservableProperty]
        public int _toBranchId;

        [JsonIgnore]
        [ObservableProperty]
        public string _toBranchName;

        [JsonIgnore]
        [ObservableProperty]
        public int _userId;

        [JsonIgnore]
        [ObservableProperty]
        public int _noOfDays;

        [JsonIgnore]
        [ObservableProperty]
        public int _subCategoryId;

        [JsonIgnore]
        [ObservableProperty]
        public int _categoryId;

        [JsonIgnore]
        [ObservableProperty]
        public string _subCategoryName;

        [JsonIgnore]
        [ObservableProperty]
        public string _categoryName;

        [JsonIgnore]
        [ObservableProperty]
        public ObservableCollection<BranchIndentDetailModel> _branchIndentDetailList;

        [JsonIgnore]
        [ObservableProperty]
        public ObservableCollection<StockDispatchDetailModel> _stockDispatchDetailManualList;

        public StockDispatchModel()
        {
            BranchIndentDetailList = new ObservableCollection<BranchIndentDetailModel>();
            StockDispatchDetailManualList = new ObservableCollection<StockDispatchDetailModel>();
        }
    }

    public partial class StockDispatchDetailModel : BaseObservableObject
    {
        public static string LastKnownTrayNumber { get; private set; }

        [JsonIgnore]
        [ObservableProperty]
        public int _stockDispatchDetailId;

        [JsonIgnore]
        [ObservableProperty]
        public int _stockDispatchId;

        [JsonIgnore]
        [ObservableProperty]
        public int _branchIndentId;

        [JsonIgnore]
        [ObservableProperty]
        public int _itemId;

        [JsonIgnore]
        [ObservableProperty]
        public int _itemPriceId;

        [JsonIgnore]
        [ObservableProperty]
        public string _skuCode;

        [JsonIgnore]
        [ObservableProperty]
        public string _itemName;

        [JsonIgnore]
        [ObservableProperty]
        public string _itemCode;

        [JsonIgnore]
        [ObservableProperty]
        public string _trayNumber;

        [JsonIgnore]
        [ObservableProperty]
        public double _mRP;

        [JsonIgnore]
        [ObservableProperty]
        public double _salePrice;

        [JsonIgnore]
        [ObservableProperty]
        public bool _isOpenItem;

        [JsonIgnore]
        [ObservableProperty]
        public int _dispatchQuantity;

        [JsonIgnore]
        [ObservableProperty]
        public double _weightInKGs;

        [JsonIgnore]
        [ObservableProperty]
        public bool _isNew;

        partial void OnTrayNumberChanged(string value)
        {
            if (string.IsNullOrEmpty(value)) return;
            LastKnownTrayNumber = value;
        }

        public StockDispatchDetailModel()
        {
            TrayNumber = LastKnownTrayNumber;
        }
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
        public double _dispatchQuantity;

        [JsonIgnore]
        [ObservableProperty]
        public DateTime? _lastDispatchDate;

        [JsonIgnore]
        [ObservableProperty]
        public ObservableCollection<StockDispatchDetailModel> _stockDispatchDetailIndentList;

        public BranchIndentDetailModel()
        {
            StockDispatchDetailIndentList = new ObservableCollection<StockDispatchDetailModel>();
        }

        public void RecalculateDispatchQuantity()
        {
            DispatchQuantity = StockDispatchDetailIndentList.Sum(x => x.IsOpenItem ? x.WeightInKGs : x.DispatchQuantity);
        }
    }
}
