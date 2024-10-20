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

    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        this.CloseAsync();
    }
}