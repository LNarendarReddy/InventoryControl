using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NSRetailLiteApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.Common
{
    public partial class ItemCodeSelectionViewModel : BaseViewModel
    {
        [ObservableProperty]
        public Item _item;

        [ObservableProperty]
        public ItemCodeData _selectedItemCode;

        [ObservableProperty]
        public string _scanItemCode;

        public IRelayCommand ItemCodeScannedCommand { get; }

        public ItemCodeSelectionViewModel(Item item, bool showScanCodeTextBox = false)
        {
            Item = item;
            ShowScanCodeTextBox = showScanCodeTextBox;
            ScanItemCode = string.Empty;
            ItemCodeScannedCommand = new RelayCommand(ItemCodeScanned);
        }

        public bool ShowScanCodeTextBox { get; }

        private void ItemCodeScanned()
        {
            if(string.IsNullOrEmpty(ScanItemCode) || Item == null)  return; 

            ItemCodeData? itemCodeData = Item.ItemCodeList.FirstOrDefault(x => x.ItemCode.Equals(ScanItemCode));
            if (itemCodeData == null)
            {
                DisplayAlert("Error", "Item code not found in SKU", "OK");
                ScanItemCode = string.Empty;
                return;
            }
            SelectedItemCode = itemCodeData;
        }
    }
}
