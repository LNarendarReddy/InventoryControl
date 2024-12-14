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
    public partial class Bill : BaseObservableObject
    {
        [JsonIgnore]
        [ObservableProperty]
        public int _billId;

        [JsonIgnore]
        [ObservableProperty]
        public string _billNumber;

        [JsonIgnore]
        [ObservableProperty]
        public ObservableCollection<BillDetail> _billDetailList;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _totalAmount;

        [JsonIgnore]
        [ObservableProperty]
        public string _customerName;

        [JsonIgnore]
        [ObservableProperty]
        public string _customerMobile;

        [JsonIgnore]
        [ObservableProperty]
        public string _customerGST;

        [JsonIgnore]
        [ObservableProperty]
        public bool _isDoorDelivery;

        [JsonIgnore]
        [ObservableProperty]
        public string _paymentModeId;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _rounding;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _paidTotalAmount;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _remainingAmount;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _tenderedCash;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _tenderedChange;

        [JsonIgnore]
        [ObservableProperty]
        public ObservableCollection<MOP> _mOPValueList;

        [JsonIgnore]
        [ObservableProperty]
        public int _userId;

        [JsonIgnore]
        [ObservableProperty]
        public int _branchCounterId;

        [JsonIgnore]
        [ObservableProperty]
        public int _daySequenceId;

        public Bill() 
        {
            BillDetailList = [];
            MOPValueList = [];
        }
    }

    public partial class BillDetail : BaseObservableObject
    {
        [JsonIgnore]
        [ObservableProperty]
        public int _billDetailId;

        [JsonIgnore]
        [ObservableProperty]
        public int _billId;

        [JsonIgnore]
        [ObservableProperty]
        public int _itemPriceId;

        [JsonIgnore]
        [ObservableProperty]
        public int _sNO;

        [JsonIgnore]
        [ObservableProperty]
        public string _itemName;

        [JsonIgnore]
        [ObservableProperty]
        public string _itemCode;

        [JsonIgnore]
        [ObservableProperty]
        public string _hSNCode;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _mRP;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _salePrice;

        [JsonIgnore]
        [ObservableProperty]
        public string _gSTCode;

        [JsonIgnore]
        [ObservableProperty]
        public int _quantity;

        [JsonIgnore]
        [ObservableProperty]
        public double _weightInKGs;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _billedAmount;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _cGST;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _sGST;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _iGST;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _cESS;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _gSTValue;

        [JsonIgnore]
        [ObservableProperty]
        public int _gSTId;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _cGSTDesc;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _sGSTDesc;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _cESSDesc;

        [JsonIgnore]
        [ObservableProperty]
        public bool _isOpenItem;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _discount;

        [JsonIgnore]
        [ObservableProperty]
        public int _offerId;

        [JsonIgnore]
        [ObservableProperty]
        public string _offerTypeCode;

        [JsonIgnore]
        [ObservableProperty]
        public DateTime? _deletedDate;

        [JsonIgnore]
        [ObservableProperty]
        public int _branchCounterId;

        [JsonIgnore]
        [ObservableProperty]
        public int _userId;

        [JsonIgnore]
        public Color BackColor
        {
            get
            {
                if (DeletedDate == null || Application.Current == null)
                {
                    return Color.FromArgb("#00FFFFFF"); // transparent
                }

                return Application.Current.RequestedTheme == AppTheme.Light
                    ? Color.FromArgb("#B22222") // flame red
                    : Color.FromArgb("#65000b"); // rose wood
            }
        }

        [JsonIgnore]
        [ObservableProperty]
        public int _branchId;

        [JsonIgnore]
        [ObservableProperty]
        public ObservableCollection<BillDetailSNo> _snos;

        [JsonIgnore]
        [ObservableProperty]
        public bool _isBillOfferItem;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _billOfferPrice;

        public BillDetail()
        {
            Snos = [];
        }
    }

    public partial class BillDetailSNo : BaseObservableObject
    {
        [ObservableProperty]
        public int _billDetailId;

        [ObservableProperty]
        public int _sNo;
    }

    public partial class DaySequence : BaseObservableObject
    {
        [ObservableProperty]
        public int _branchCounterId;

        [ObservableProperty]
        public int _daySequenceId;

        [ObservableProperty]
        public ObservableCollection<Bill> _billList;

        public DaySequence()
        {
            BillList = new ObservableCollection<Bill>();
        }
    }

    public partial class MOP : BaseObservableObject
    {
        [JsonIgnore]
        [ObservableProperty]
        public int _mOPId;

        [JsonIgnore]
        [ObservableProperty]
        public string _mOPName;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _mOPValue;
    }

    public partial class BillOffer : BaseObservableObject
    {
        [JsonIgnore]
        [ObservableProperty]
        public int _itemPriceId;

        [JsonIgnore]
        [ObservableProperty]
        public string _sKUCode;

        [JsonIgnore]
        [ObservableProperty]
        public string _itemName;

        [JsonIgnore]
        [ObservableProperty]
        public string _itemCode;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _mRP;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _salePrice;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _actualSalePrice;
    }

    public partial class Denomination : BaseObservableObject
    {
        [JsonIgnore]
        [ObservableProperty]
        public int _denominationId;

        [JsonIgnore]
        [ObservableProperty]
        public string _displayValue;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _multiplier;

        [JsonIgnore]
        [ObservableProperty]
        public int _quantity;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _closureValue;

        partial void OnQuantityChanged(int value)
        {
            ClosureValue = Multiplier * value;
        }
    }

    public partial class Refund :BaseObservableObject
    {
        [JsonIgnore]
        [ObservableProperty]
        public decimal _refundAmount;
    }

    public partial class SaveDayClosure : BaseObservableObject
    {
        [JsonIgnore]
        [ObservableProperty]
        public decimal _refundAmount;

        [JsonIgnore]
        [ObservableProperty]
        public int _daySequenceID;

        [JsonIgnore]
        [ObservableProperty]
        public int _branchCounterId;

        [JsonIgnore]
        [ObservableProperty]
        public int _userId;

        [JsonIgnore]
        [ObservableProperty]
        public ObservableCollection<MOP> _mopValues;

        [JsonIgnore]
        [ObservableProperty]
        public ObservableCollection<MOP> _denominations;

    }
}
