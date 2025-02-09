using CommunityToolkit.Mvvm.ComponentModel;
using NSRetailLiteApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.ItemDetails
{
    public partial class ItemDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        public Item _itemData;

        [ObservableProperty]
        public ItemCodeData _itemCodeData;

        [ObservableProperty]
        public ItemPrice _selectedItemPrice;

        [ObservableProperty]
        public ItemOffer _selectedItemOffer;

        [ObservableProperty]
        public double _finalPrice;

        [ObservableProperty]
        public bool _showFinalPrice;

        public ItemDetailsViewModel(Item itemData, ItemCodeData itemCodeData)
        {
            ItemData = itemData;
            ItemCodeData = itemCodeData;
        }

        partial void OnSelectedItemPriceChanged(ItemPrice value)
        {
            RefreshFinalPrice();
        }

        partial void OnSelectedItemOfferChanged(ItemOffer value)
        {
            RefreshFinalPrice();
        }

        private void RefreshFinalPrice()
        {
            ShowFinalPrice = SelectedItemPrice != null && SelectedItemOffer != null;

            if(!ShowFinalPrice) return;

            switch(SelectedItemOffer.OfferTypeName.ToLower())
            {
                case "discount %":
                    FinalPrice = SelectedItemPrice.MRP - (SelectedItemOffer.OfferValue * SelectedItemPrice.MRP / 100);
                    break;
                case "discount flat":
                    FinalPrice = SelectedItemPrice.MRP - SelectedItemOffer.OfferValue;
                    break;
                case "fixed rate":
                    FinalPrice = SelectedItemOffer.OfferValue;
                    break;
                default:
                    FinalPrice = 0;
                    break;
            }
        }
    }
}
