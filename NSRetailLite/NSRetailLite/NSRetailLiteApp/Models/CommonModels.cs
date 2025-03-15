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

        public StockDispatchModel StockDispatch;
    }
    
    public partial class Branch : BaseObservableObject
    {
        [ObservableProperty]
        private int _branchID;

        [ObservableProperty]
        private string _branchName;

        [ObservableProperty]
        private string _branchCode;

        [ObservableProperty]
        private string _address;

        [ObservableProperty]
        private string _phoneNo;

        [ObservableProperty]
        private string _landLine;

        [ObservableProperty]
        private string _emailID;

        [ObservableProperty]
        private int _branchIndentID;

        [ObservableProperty]
        private ObservableCollection<BranchCounter> _branchCounterList;

        public Branch()
        {
            BranchCounterList = [];
        }
    }

    public partial class Item : BaseObservableObject
    {
        [ObservableProperty]
        private int _itemID;

        [ObservableProperty]
        private string _itemName;

        [ObservableProperty]
        private string _sKUCode;

        [ObservableProperty]
        private bool _isOpenItem;

        [ObservableProperty]
        private int _parentItemID;

        [ObservableProperty]
        private ObservableCollection<ItemCodeData> _itemCodeList;

        public Item()
        {
            ItemCodeList = [];
        }
    }

    public partial class ItemCodeData : BaseObservableObject
    {
        [ObservableProperty]
        private int _itemID;

        [ObservableProperty]
        private string _itemCodeID;

        [ObservableProperty]
        private string _itemCode;

        [ObservableProperty]
        private string _hSNCode;

        [ObservableProperty]
        private ObservableCollection<ItemPrice> _itemPriceList;

        [ObservableProperty]
        private ObservableCollection<ItemOffer> _OfferList;

        public ItemCodeData()
        {
            ItemPriceList = [];
            OfferList = [];
        }
    }

    public partial class ItemPrice : BaseObservableObject
    {
        [ObservableProperty]
        private int _itemCodeID;

        [ObservableProperty]
        private int _itemPriceID;

        [ObservableProperty]
        private double _mRP;

        [ObservableProperty]
        private double _salePrice;

        [ObservableProperty]
        private double _qtyOrWeightInKGs;
    }

    public partial class ItemOffer : BaseObservableObject
    {
        [ObservableProperty]
        private string _offerTypeName;

        [ObservableProperty]
        private string _offerCode;

        [ObservableProperty]
        private double _offerValue;
    }
}
