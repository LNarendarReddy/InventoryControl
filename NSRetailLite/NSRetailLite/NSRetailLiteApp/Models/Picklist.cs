using CommunityToolkit.Mvvm.ComponentModel;
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
        private ObservableCollection<PickListItemModel> _pickListItemList;
    }
}
