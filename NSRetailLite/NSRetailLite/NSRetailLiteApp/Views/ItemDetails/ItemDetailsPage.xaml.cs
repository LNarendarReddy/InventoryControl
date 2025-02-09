using NSRetailLiteApp.ViewModels.ItemDetails;

namespace NSRetailLiteApp.Views.ItemDetails;

public partial class ItemDetailsPage : ContentPage
{
	public ItemDetailsPage(ItemDetailsViewModel itemDetailsViewModel)
	{
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = itemDetailsViewModel;
        ItemDetailsViewModel = itemDetailsViewModel;
    }

    public ItemDetailsViewModel ItemDetailsViewModel { get; }
}