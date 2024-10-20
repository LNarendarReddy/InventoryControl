using CommunityToolkit.Mvvm.ComponentModel;
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

        public ItemCodeSelectionViewModel(Item item)
        {
            Item = item;
        }
    }
}
