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

    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		this.CloseAsync();
    }
}