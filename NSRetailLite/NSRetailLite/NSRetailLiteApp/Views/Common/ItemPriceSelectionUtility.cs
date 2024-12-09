using NSRetailLiteApp.Models;
using NSRetailLiteApp.ViewModels;
using NSRetailLiteApp.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.Views.Common
{
    public class ItemPriceSelectionUtility : BaseViewModel
    {
        public async Task<Tuple<ItemCodeData, ItemPrice>> GetSelectedItemPrice(Item item)
        {
            if (item == null || item.ItemCodeList == null || !item.ItemCodeList.Any()) return new Tuple<ItemCodeData, ItemPrice>(null, null);

            ItemCodeData selectedItemCode;

            if (item.ItemCodeList.Count > 1)
            {
                ItemCodeSelectionViewModel itemCodeSelectionViewModel = new ItemCodeSelectionViewModel(item);
                await base.ShowPopup(item, new ItemCodeSelectionPage(itemCodeSelectionViewModel));
                selectedItemCode = itemCodeSelectionViewModel.SelectedItemCode;
            }
            else
            {
                selectedItemCode = item.ItemCodeList[0];
            }

            if (selectedItemCode == null) return new Tuple<ItemCodeData, ItemPrice>(null, null); ;

            if (selectedItemCode.ItemPriceList.Count > 1)
            {
                ItemPriceSelectionViewModel itemPriceSelectionViewModel = new ItemPriceSelectionViewModel(selectedItemCode);
                await base.ShowPopup(selectedItemCode, new ItemPriceSelectionPage(itemPriceSelectionViewModel));

                return new Tuple<ItemCodeData, ItemPrice>(selectedItemCode, itemPriceSelectionViewModel.SelectedItemPrice);
            }
            else 
                return new Tuple<ItemCodeData, ItemPrice>(selectedItemCode, selectedItemCode.ItemPriceList[0]);
        }

        public async Task<string> ScanBarCodeWithCamera()
        {
            BarcodeScannerViewModel barcodeScannerViewModel = new BarcodeScannerViewModel();
            await base.ShowPopup(null, new BarcodeScannerPopup(barcodeScannerViewModel));
            return barcodeScannerViewModel.ScannedCode;

        }
    }
}
