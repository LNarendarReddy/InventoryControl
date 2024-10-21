using CommunityToolkit.Maui.Views;
using NSRetailLiteApp.ViewModels.Common;

namespace NSRetailLiteApp.Views.Common;

public partial class ItemPriceSelectionPage : Popup
{
	public ItemPriceSelectionPage(ItemPriceSelectionViewModel itemPriceSelectionViewModel)
	{
		InitializeComponent();
		BindingContext = itemPriceSelectionViewModel;
	}

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        this.CloseAsync();
    }
}