using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.Models
{
    public partial class Supplier : BaseObservableObject
    {
        [ObservableProperty]
        private int _supplierId;

        [ObservableProperty]
        private string _supplierName;

        [ObservableProperty]
        private string _gSTIN;
    }

    public partial class SupplierIndent : BaseObservableObject
    {
        [ObservableProperty]
        private int _supplierIndentId;

        [ObservableProperty]
        private string _supplierIndentNo;
    }

    public partial class StockEntryModel : BaseObservableObject
    {
        [ObservableProperty]
        private int _stockEntryId;

        [ObservableProperty]
        private string _supplierInvoiceNo;

        [ObservableProperty]
        private int _supplierId;

        [ObservableProperty]
        private string _supplierName;

        [ObservableProperty]
        private string _supplierGSTIN;

        [ObservableProperty]
        private int _categoryId;

        [ObservableProperty]
        private string _categoryName;

        [ObservableProperty]
        private int _supplierIndentId;

        [ObservableProperty]
        private string _supplierIndentNo;

        [ObservableProperty]
        private DateTime _invoiceDate;

        [ObservableProperty]
        private ObservableCollection<StockEntryDetailModel> _stockEntryDetailList;

        [ObservableProperty]
        private int _userID;

        public StockEntryModel()
        {
            _stockEntryDetailList = [];
        }
    }

    public partial class StockEntryDetailModel : BaseObservableObject
    {
        [ObservableProperty]
        private int _stockEntryDetailId;

        [ObservableProperty]
        private int _stockEntryId;

        [ObservableProperty]
        private int _itemID;

        [ObservableProperty]
        private string _itemName;

        [ObservableProperty]
        private string _sKUCode;

        [ObservableProperty]
        private bool _isOpenItem;

        [ObservableProperty]
        private string _itemCode;

        [ObservableProperty]
        private int _itemCodeID;

        [ObservableProperty]
        private double? _mRP;

        [ObservableProperty]
        private double _quantity;

        [ObservableProperty]
        private double _weightInKGs;

        [ObservableProperty]
        private int _userID;
    }
}
