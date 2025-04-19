using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using NSRetailLiteApp.ViewModels.Common;

namespace NSRetailLiteApp.Views.Common;

public partial class ItemCodeSelectionPage : Popup
{
    public ItemCodeSelectionPage(ItemCodeSelectionViewModel itemCodeSelectionViewModel)
	{
		InitializeComponent();
		BindingContext = itemCodeSelectionViewModel;
        ItemCodeSelectionViewModel = itemCodeSelectionViewModel;
    }

    public ItemCodeSelectionViewModel ItemCodeSelectionViewModel { get; }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        this.CloseAsync();
    }

    private void Popup_Opened(object sender, CommunityToolkit.Maui.Core.PopupOpenedEventArgs e)
    {
        if(ItemCodeSelectionViewModel.ShowScanCodeTextBox) txtScanItemCode.Focus();
    }
}