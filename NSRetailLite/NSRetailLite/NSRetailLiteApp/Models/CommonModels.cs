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

    public class HolderClass : BaseObservableObject
    {
        public LoggedInUser User;

        public StockCountingModel Counting;

        public Item Item;

        public ItemCodeData ItemCode;

        public Branch Branch;

        public DaySequence DaySequence;

        public ObservableCollection<Branch> BranchList;

        public ObservableCollection<MOP> MOPList;

        public int GenericID;

        public Bill Bill;

        public Bill CR_Bill;

        public ObservableCollection<BillOffer> OfferList;

        public ObservableCollection<Denomination> DenominationList;

        public ObservableCollection<Refund> RefundList;

        public HolderClass Holder;

        public DayClosure DayClosure;

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

        [ObservableProperty]
        public ObservableCollection<BranchCounter> _branchCounterList;

        public Branch()
        {
            BranchCounterList = [];
        }
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
        public ObservableCollection<ItemCodeData> _itemCodeList;

        public Item()
        {
            ItemCodeList = [];
        }
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
        public ObservableCollection<ItemPrice> _itemPriceList;

        [ObservableProperty]
        public ObservableCollection<ItemOffer> _OfferList;

        public ItemCodeData()
        {
            ItemPriceList = [];
            OfferList = [];
        }
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

        [ObservableProperty]
        public double _qtyOrWeightInKGs;
    }

    public partial class ItemOffer : BaseObservableObject
    {
        [ObservableProperty]
        public string _offerTypeName;

        [ObservableProperty]
        public string _offerCode;

        [ObservableProperty]
        public double _offerValue;
    }
}
