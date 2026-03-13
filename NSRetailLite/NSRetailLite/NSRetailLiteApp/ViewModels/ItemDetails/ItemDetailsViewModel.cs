using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NSRetailLiteApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.ItemDetails
{
    public partial class ItemDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        private Item _itemData;

        [ObservableProperty]
        private ItemPrice _selectedItemPrice;

        [ObservableProperty]
        private ItemOffer _selectedItemOffer;

        [ObservableProperty]
        private ObservableCollection<ItemOffer> _filteredItemOfferList;

        [ObservableProperty]
        private double _finalPrice;

        [ObservableProperty]
        private double? _totalQuantity;

        [ObservableProperty]
        private bool _showFinalPrice;

        [ObservableProperty]
        private string _itemCode;

        public Branch SelectedBranch { get; }

        public IAsyncRelayCommand LoadItemCommand { get; }

        public ItemDetailsViewModel(Branch selectedBranch)
        {
            LoadItemCommand = new AsyncRelayCommand(LoadItem);
            SelectedBranch = selectedBranch;
        }

        private async Task LoadItem()
        {
            if (string.IsNullOrEmpty(ItemCode)) return;

            Item item = new();
            item = await GetAsync("item/getitem", item
                , new Dictionary<string, string?>()
                {
                    { "ItemCode", ItemCode },
                    {"BranchId", SelectedBranch.BranchID.ToString() }
                }, displayAlert: true);

            ItemData = item;
            FillData();
        }

        partial void OnSelectedItemPriceChanged(ItemPrice value)
        {
            FillData();            
        }

        partial void OnSelectedItemOfferChanged(ItemOffer value)
        {
            FillData();
        }

        private void FillData()
        {
            TotalQuantity = ItemData?.ItemPriceList.Sum(x => x.QtyOrWeightInKGs);
            FilteredItemOfferList = [];

            if (ItemData == null) return;

            if (SelectedItemPrice != null && ItemData.ItemOfferList != null)
                FilteredItemOfferList = ItemData.ItemOfferList
                    .Where(x => x.ItemCodeId == SelectedItemPrice.ItemCodeID)
                    .ToObservableCollection();

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

//        holderClass = new ();
//            holderClass = await GetAsync("Item/getItemdata", holderClass, new Dictionary<string, string?>()
//            {
//                { "ItemCodeID", selectedItemCode.ItemCodeID
//    },
//                { "BranchID", HomePageViewModel.User.BranchId.ToString()
//},
//                { "useWHConnection", true.ToString() }
//            });

    }
}
