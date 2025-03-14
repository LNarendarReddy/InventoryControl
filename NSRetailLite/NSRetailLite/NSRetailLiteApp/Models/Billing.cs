﻿using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public ObservableCollection<BillDetail> _cR_BillDetailList;

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
        public string _counterName;

        [JsonIgnore]
        [ObservableProperty]
        public int _daySequenceId;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _totalRefundAmount;

        public Bill() 
        {
            BillDetailList = [];
            CR_BillDetailList = [];
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
        [ObservableProperty]
        public Color _backColor;

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

        [JsonIgnore]
        [ObservableProperty]
        public bool _isDeleted;

        [JsonIgnore]
        [ObservableProperty]
        public int _refundQuantity;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _refundAmount;

        public BillDetail()
        {
            Snos = [];
            RefundQuantity = 0;
        }

        partial void OnDeletedDateChanged(DateTime? value)
        {
            IsDeleted = value != null;

            if (DeletedDate == null || Application.Current == null)
            {
                BackColor = Color.FromArgb("#00FFFFFF"); // transparent
            }
            else
            {
                BackColor = Application.Current.RequestedTheme == AppTheme.Light
                    ? Color.FromArgb("#B22222") // flame red
                    : Color.FromArgb("#65000b"); // rose wood
            }
        }

        partial void OnRefundQuantityChanged(int value)
        {
            RefundAmount = (BilledAmount / Quantity) * RefundQuantity;
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

    public partial class UserMOPBreakDown : BaseObservableObject
    {
        [JsonIgnore]
        [ObservableProperty]
        public string _userName;

        [JsonIgnore]
        [ObservableProperty]
        public string _mopName;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _mopValue;
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

        [JsonIgnore]
        [ObservableProperty]
        public string _quantityString;

        partial void OnQuantityChanged(int value)
        {
            ClosureValue = Multiplier * value;
        }

        partial void OnQuantityStringChanged(string value)
        {
            if (string.IsNullOrEmpty(value)) QuantityString = "0";
            Quantity = int.TryParse(value, out int newValue) ? newValue : 0;
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

    public partial class DayClosure : BaseObservableObject
    {
        [JsonIgnore]
        [ObservableProperty]
        public int _dayClosureID;

        [JsonIgnore]
        [ObservableProperty]
        public DateTime _closureDate;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _openingBalance;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _closingBalance;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _closingDifference;

        [JsonIgnore]
        [ObservableProperty]
        public int _closedBy;

        [JsonIgnore]
        [ObservableProperty]
        public decimal _refundAmount;

        [JsonIgnore]
        [ObservableProperty]
        public DateTime _createdDate;

        [JsonIgnore]
        [ObservableProperty]
        public int _completedBills;

        [JsonIgnore]
        [ObservableProperty]
        public int _draftBills;

        [JsonIgnore]
        [ObservableProperty]
        public int _voidItems;

        [JsonIgnore]
        [ObservableProperty]
        public string _address;

        [JsonIgnore]
        [ObservableProperty]
        public string _phoneNo;

        [JsonIgnore]
        [ObservableProperty]
        public string _branchName;

        [JsonIgnore]
        [ObservableProperty]
        public string _counterName;

        [JsonIgnore]
        [ObservableProperty]
        public string _userName;

        [JsonIgnore]
        [ObservableProperty]
        public ObservableCollection<Denomination> _denominationsList;

        [JsonIgnore]
        [ObservableProperty]
        public ObservableCollection<MOP> _mopValuesList;

        [JsonIgnore]
        [ObservableProperty]
        public ObservableCollection<UserMOPBreakDown> _userWiseMopBreakDownList;

    }
}
