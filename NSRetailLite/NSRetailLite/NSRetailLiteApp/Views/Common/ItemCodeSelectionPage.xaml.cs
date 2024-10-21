using CommunityToolkit.Maui.Views;
using NSRetailLiteApp.ViewModels.Common;

namespace NSRetailLiteApp.Views.Common;

public partial class ItemCodeSelectionPage : Popup
{
	public ItemCodeSelectionPage(ItemCodeSelectionViewModel itemCodeSelectionViewModel)
	{
		InitializeComponent();
		BindingContext = itemCodeSelectionViewModel;
	}

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        this.CloseAsync();
    }
}