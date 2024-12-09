using CommunityToolkit.Mvvm.ComponentModel;
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
        [ObservableProperty]
        public int _billId;

        [ObservableProperty]
        public string _billNumber;

        [ObservableProperty]
        public ObservableCollection<BillDetail> _billDetailList;

        [ObservableProperty]
        public decimal _totalAmount;

        [ObservableProperty]
        public string _customerName;

        [ObservableProperty]
        public string _customerMobile;

        [ObservableProperty]
        public string _customerGST;

        [ObservableProperty]
        public bool _isDoorDelivery;

        [ObservableProperty]
        public string _paymentModeId;

        [ObservableProperty]
        public decimal _rounding;

        [ObservableProperty]
        public decimal _paidTotalAmount;

        [ObservableProperty]
        public decimal _remainingAmount;

        [ObservableProperty]
        public decimal _tenderedCash;

        [ObservableProperty]
        public decimal _tenderedChange;

        [ObservableProperty]
        public ObservableCollection<MOP> _mOPValueList;

        [ObservableProperty]
        public int _userId;

        [ObservableProperty]
        public int _branchCounterId;

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
        [ObservableProperty]
        public int _billDetailId;

        [ObservableProperty]
        public int _billId;

        [ObservableProperty]
        public int _itemPriceId;

        [ObservableProperty]
        public int _sNO;

        [ObservableProperty]
        public string _itemName;

        [ObservableProperty]
        public string _itemCode;

        [ObservableProperty]
        public string _hSNCode;

        [ObservableProperty]
        public decimal _mRP;

        [ObservableProperty]
        public decimal _salePrice;

        [ObservableProperty]
        public string _gSTCode;

        [ObservableProperty]
        public int _quantity;

        [ObservableProperty]
        public double _weightInKGs;

        [ObservableProperty]
        public decimal _billedAmount;

        [ObservableProperty]
        public decimal _cGST;

        [ObservableProperty]
        public decimal _sGST;

        [ObservableProperty]
        public decimal _iGST;

        [ObservableProperty]
        public decimal _cESS;

        [ObservableProperty]
        public decimal _gSTValue;

        [ObservableProperty]
        public int _gSTId;

        [ObservableProperty]
        public decimal _cGSTDesc;

        [ObservableProperty]
        public decimal _sGSTDesc;

        [ObservableProperty]
        public decimal _cESSDesc;

        [ObservableProperty]
        public bool _isOpenItem;

        [ObservableProperty]
        public decimal _discount;

        [ObservableProperty]
        public int _offerId;

        [ObservableProperty]
        public string _offerTypeCode;

        [ObservableProperty]
        public DateTime? _deletedDate;

        [ObservableProperty]
        public int _branchCounterId;

        [ObservableProperty]
        public int _userId;

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

        [ObservableProperty]
        public int _branchId;
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
        [ObservableProperty]
        public int _mOPId;

        [ObservableProperty]
        public string _mOPName;

        [ObservableProperty]
        public decimal _mOPValue;
    }
}
