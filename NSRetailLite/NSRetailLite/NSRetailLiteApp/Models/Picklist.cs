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
    public partial class PickListItemModel : BaseObservableObject
    {
        [ObservableProperty]
        private int _pickListItemID;

        [ObservableProperty]
        private int _pickListItemDetailID;

        [ObservableProperty]
        private int _pickListTrayID;

        [ObservableProperty]
        private int _itemCodeID;

        [ObservableProperty]
        private string _itemName;

        [ObservableProperty]
        private string _itemCode;

        [ObservableProperty]
        private double _mRP;

        [ObservableProperty]
        private int _quantity;

        [ObservableProperty]
        private int _availableQuantity;
    }

    public partial class PickListTrayModel : BaseObservableObject
    {
        [ObservableProperty]
        private int _pickListTrayID;

        [ObservableProperty]
        private string _trayNumber;

        [ObservableProperty]
        private int _itemCount;

        [ObservableProperty]
        private int _pieceCount;

        [ObservableProperty]
        private bool _isTrayVerified;

        [ObservableProperty]
        private Color _TrayVerifiedColor;

        partial void OnIsTrayVerifiedChanged(bool value)
        {
            TrayVerifiedColor = (value ? System.Drawing.Color.Green
                : Application.Current.RequestedTheme == AppTheme.Light
                    ? System.Drawing.Color.Black
                    : System.Drawing.Color.White).ToMauiColor();
        }

        [ObservableProperty]
        private ObservableCollection<PickListItemModel> _pickListItemList;
    }
}
