using CommunityToolkit.Mvvm.ComponentModel;
using DevExpress.Maui.Pdf.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.Models
{
    public partial class Dispatch : BaseObservableObject
    {
        [ObservableProperty]
        private int _stockDispatchId;

        [ObservableProperty]
        private int _categoryId;

        [ObservableProperty]
        private string _categoryName;

        [ObservableProperty]
        private string _subCategoryName;

        [ObservableProperty]
        private string _dispatchNumber;

        [ObservableProperty]
        private DateTime? _dispatchDate;

        [ObservableProperty]
        private int _status;

        [ObservableProperty]
        private string _statusText;

        [ObservableProperty]
        private int _totalTrays;

        [ObservableProperty]
        private int _pendingTrays;

        [ObservableProperty]
        private Color _pendingTraysColor;

        partial void OnPendingTraysChanged(int value)
        {
            PendingTraysColor = (value == 0 ? System.Drawing.Color.Green : System.Drawing.Color.Orange).ToMauiColor();
        }
    }


    public partial class DispatchReceive : BaseObservableObject
    {
        [ObservableProperty]
        private int _stockDispatchId;

        [ObservableProperty]
        private string _dispatchNumber;

        [ObservableProperty]
        private string _trayNumber;

        [ObservableProperty]
        private ObservableCollection<DispatchReceiveDetail> _dispatchReceiveDetailList;

        public DispatchReceive()
        {
            DispatchReceiveDetailList = new ObservableCollection<DispatchReceiveDetail>();
        }
    }

    public partial class DispatchReceiveDetail : BaseObservableObject
    {
        [ObservableProperty]
        private int _stockDispatchDetailId;

        [ObservableProperty]
        private string _itemName;

        [ObservableProperty]
        private string _itemCode;

        [ObservableProperty]
        private string _sKUCode;

        [ObservableProperty]
        private double _mRP;

        [ObservableProperty]
        private double _salePrice;

        [ObservableProperty]
        private int _dispatchQuantity;

        [ObservableProperty]
        private int _receivedQuantity;

        [ObservableProperty]
        private double _weightInKGs;

        [ObservableProperty]
        private bool _isOpenItem;

        [ObservableProperty]
        private double _sentQuantity;

        [ObservableProperty]
        private bool _isDenominatorVisible;

        [ObservableProperty]
        private Color _quantityColor;

        [ObservableProperty]
        private bool _isVerified;

        [ObservableProperty]
        private Color _textColor;

        partial void OnDispatchQuantityChanged(int value)
        {
            SetValues();
        }

        partial void OnIsOpenItemChanged(bool value)
        {
            SetValues();    
        }

        partial void OnReceivedQuantityChanged(int value)
        {
            SetValues();
        }

        partial void OnIsVerifiedChanged(bool value)
        {
            SetValues();
        }

        private void SetValues()
        {
            SentQuantity = IsOpenItem ? WeightInKGs : DispatchQuantity;
            IsDenominatorVisible = IsOpenItem ? false : DispatchQuantity != ReceivedQuantity;

            System.Drawing.Color normalTextColor = Application.Current.RequestedTheme == AppTheme.Light
                    ? System.Drawing.Color.Black
                    : System.Drawing.Color.White;

            QuantityColor = (IsDenominatorVisible ? System.Drawing.Color.Orange : normalTextColor).ToMauiColor();
            TextColor = (IsDenominatorVisible ? System.Drawing.Color.Orange
                : IsVerified ? System.Drawing.Color.Green : normalTextColor).ToMauiColor();
        }
    }
}
