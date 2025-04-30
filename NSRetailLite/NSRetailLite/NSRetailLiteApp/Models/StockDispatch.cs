using CommunityToolkit.Mvvm.ComponentModel;
using DevExpress.Maui.Pdf.Internal;
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
        public static TrayInfo LastKnownTrayNumber { get; set; }

        [ObservableProperty]
        private int _stockDispatchId;

        [ObservableProperty]
        private int _branchIndentId;

        [ObservableProperty]
        private int _fromBranchId;

        [ObservableProperty]
        private int _toBranchId;

        [ObservableProperty]
        private string _toBranchName;

        [ObservableProperty]
        private int _userId;

        [ObservableProperty]
        private int _noOfDays;

        [ObservableProperty]
        private int _subCategoryId;

        [ObservableProperty]
        private int _categoryId;

        [ObservableProperty]
        private string _subCategoryName;

        [ObservableProperty]
        private string _categoryName;

        [ObservableProperty]
        private ObservableCollection<BranchIndentDetailModel> _branchIndentDetailList;

        [ObservableProperty]
        private ObservableCollection<StockDispatchDetailModel> _stockDispatchDetailManualList;

        [ObservableProperty]
        private ObservableCollection<TrayInfo> _trayInfoList;

        public StockDispatchModel()
        {
            BranchIndentDetailList = new ObservableCollection<BranchIndentDetailModel>();
            StockDispatchDetailManualList = new ObservableCollection<StockDispatchDetailModel>();
            TrayInfoList = new ObservableCollection<TrayInfo>();
        }
    }

    public partial class StockDispatchDetailModel : BaseObservableObject
    {
        [ObservableProperty]
        private int _stockDispatchDetailId;

        [ObservableProperty]
        private int _stockDispatchId;

        [ObservableProperty]
        private int _branchIndentId;

        [ObservableProperty]
        private int _itemId;

        [ObservableProperty]
        private int _itemPriceId;

        [ObservableProperty]
        private string _skuCode;

        [ObservableProperty]
        private string _itemName;

        [ObservableProperty]
        private string _itemCode;

        [ObservableProperty]
        private int _trayInfoId;

        [ObservableProperty]
        private int _trayNumber;

        [ObservableProperty]
        private double _mRP;

        [ObservableProperty]
        private double _salePrice;

        [ObservableProperty]
        private bool _isOpenItem;

        [ObservableProperty]
        private int _dispatchQuantity;

        [ObservableProperty]
        private double _weightInKGs;

        [ObservableProperty]
        private bool _isNew;

        [ObservableProperty]
        private bool _isTrayVerified;

        [ObservableProperty]
        private Color _TrayVerifiedColor;

        public StockDispatchDetailModel()
        {
            OnIsTrayVerifiedChanged(IsTrayVerified);
        }

        partial void OnIsTrayVerifiedChanged(bool value)
        {
            TrayVerifiedColor = (value ? System.Drawing.Color.Green 
                : Application.Current.RequestedTheme == AppTheme.Light
                    ? System.Drawing.Color.Black
                    : System.Drawing.Color.White).ToMauiColor();
        }
    }

    public partial class BranchIndentDetailModel : BaseObservableObject
    {
        [ObservableProperty]
        private int _branchIndentDetailId;

        [ObservableProperty]
        private int _itemId;
                
        [ObservableProperty]
        private string _skuCode;

        [ObservableProperty]
        private string _itemName;

        [ObservableProperty]
        private int _subCategoryId;

        [ObservableProperty]
        private string _subCategoryName;

        [ObservableProperty]
        private double _branchStock;

        [ObservableProperty]
        private double _wHStock;

        [ObservableProperty]
        private double _avgSales;

        [ObservableProperty]
        private double _noOfDaySales;

        [ObservableProperty]
        private double _indentQuantity;

        [ObservableProperty]
        private double _dispatchQuantity;

        [ObservableProperty]
        private DateTime? _lastDispatchDate;

        [ObservableProperty]
        private ObservableCollection<StockDispatchDetailModel> _stockDispatchDetailIndentList;

        [ObservableProperty]
        private bool _isDenominatorVisible;

        [ObservableProperty]
        private Color _quantityColor;

        public BranchIndentDetailModel()
        {
            StockDispatchDetailIndentList = new ObservableCollection<StockDispatchDetailModel>();
        }

        public void RecalculateDispatchQuantity()
        {
            DispatchQuantity = StockDispatchDetailIndentList.Sum(x => x.IsOpenItem ? x.WeightInKGs : x.DispatchQuantity);
        }

        partial void OnDispatchQuantityChanged(double value)
        {
            IsDenominatorVisible = value > 0;

            if (value == 0)
            {
                QuantityColor = Application.Current.RequestedTheme == AppTheme.Light 
                    ? System.Drawing.Color.Black.ToMauiColor()
                    : System.Drawing.Color.White.ToMauiColor();
                return;
            }
            if (value != IndentQuantity)
            {
                QuantityColor = System.Drawing.Color.Orange.ToMauiColor();
            }
            if (value == IndentQuantity)
            {
                QuantityColor = System.Drawing.Color.Green.ToMauiColor();
            }            
        }

        partial void OnIndentQuantityChanged(double value)
        {
            OnDispatchQuantityChanged(DispatchQuantity);
        }
    }

    public partial class TrayInfo : BaseObservableObject
    {
        [ObservableProperty]
        private int _trayInfoId;

        [ObservableProperty]
        private int _trayNumber;
    }

    public partial class BranchIndent : BaseObservableObject
    {
        [ObservableProperty]
        private int _branchIndentID;

        [ObservableProperty]
        private int _branchID;

        [ObservableProperty]
        private int _categoryID;

        [ObservableProperty]
        private int _subCategoryID;

        [ObservableProperty]
        private int _stockDispatchID;

        [ObservableProperty]
        private string _branchName;

        [ObservableProperty]
        private string _categoryName;

        [ObservableProperty]
        private string _subCategoryName;

        [ObservableProperty]
        private string _indentStatus;

        [ObservableProperty]
        private string _dispatchBy;

        [ObservableProperty]
        private DateTime? _indentDate;
    }

}