using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.Models
{

    internal class RootClass
    {
        public HolderClass Holder;
    }

    internal class HolderClass : BaseObservableObject
    {
        public LoggedInUser User;

        public StockCountingModel Counting;

        public Item Item;

        public ObservableCollection<Branch> Branch;

        public int GenericID;
    }

    
    public partial class Branch : BaseObservableObject
    {
        [ObservableProperty]
        public int _branchID;

        [ObservableProperty]
        public string _branchName;

        [ObservableProperty]
        public string _branchCode;

        [ObservableProperty]
        public string _address;

        [ObservableProperty]
        public string _phoneNo;

        [ObservableProperty]
        public string _landLine;

        [ObservableProperty]
        public string _emailID;
    }

    public partial class Item : BaseObservableObject
    {
        [ObservableProperty]
        public int _itemID;

        [ObservableProperty]
        public string _itemName;

        [ObservableProperty]
        public string _sKUCode;

        [ObservableProperty]
        public bool _isOpenItem;

        [ObservableProperty]
        public int _parentItemID;

        [ObservableProperty]
        public ObservableCollection<ItemCodeData> _itemCodes;
    }

    public partial class ItemCodeData : BaseObservableObject
    {
        [ObservableProperty]
        public int _itemID;

        [ObservableProperty]
        public string _itemCodeID;

        [ObservableProperty]
        public string _itemCode;

        [ObservableProperty]
        public string _hSNCode;

        [ObservableProperty]
        public ObservableCollection<ItemPrice> _itemPrices;
    }

    public partial class ItemPrice : BaseObservableObject
    {
        [ObservableProperty]
        public int _itemCodeID;

        [ObservableProperty]
        public int _itemPriceID;

        [ObservableProperty]
        public double _mRP;

        [ObservableProperty]
        public double _salePrice;
    }
}
