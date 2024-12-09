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
        public double _totalAmount;

        [ObservableProperty]
        public string _customerName;

        [ObservableProperty]
        public string _customerMobile;

        [ObservableProperty]
        public string _customerGST;

        [ObservableProperty]
        public string _saleType;

        [ObservableProperty]
        public string _paymentModeId;

        public Bill() 
        {
            BillDetailList = new ObservableCollection<BillDetail>();
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
        public double _mRP;

        [ObservableProperty]
        public double _salePrice;

        [ObservableProperty]
        public string _gSTCode;

        [ObservableProperty]
        public int _quantity;

        [ObservableProperty]
        public double _weightInKGs;

        [ObservableProperty]
        public double _billedAmount;

        [ObservableProperty]
        public double _cGST;

        [ObservableProperty]
        public double _sGST;

        [ObservableProperty]
        public double _iGST;

        [ObservableProperty]
        public double _cESS;

        [ObservableProperty]
        public double _gSTValue;

        [ObservableProperty]
        public int _gSTId;

        [ObservableProperty]
        public double _cGSTDesc;

        [ObservableProperty]
        public double _sGSTDesc;

        [ObservableProperty]
        public double _cESSDesc;

        [ObservableProperty]
        public bool _isOpenItem;

        [ObservableProperty]
        public double _discount;

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
