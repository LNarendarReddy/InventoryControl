using CommunityToolkit.Mvvm.ComponentModel;
using NSRetailLiteApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.Common
{
    public partial class ItemPriceSelectionViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ItemCodeData _itemCode;

        [ObservableProperty]
        public ItemPrice _selectedItemPrice;

        public ItemPriceSelectionViewModel(ItemCodeData itemCode)
        {
            ItemCode = itemCode;
        }
    }
}
