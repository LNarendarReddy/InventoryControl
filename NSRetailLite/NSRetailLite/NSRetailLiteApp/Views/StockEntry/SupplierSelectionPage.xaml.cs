using CommunityToolkit.Maui.Views;
using NSRetailLiteApp.ViewModels;

namespace NSRetailLiteApp.Views.StockEntry;

public partial class SupplierSelectionPage : Popup
{
	public SupplierSelectionPage(SupplierSelectionViewModel supplierSelectionViewModel)
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = supplierSelectionViewModel;
        SupplierSelectionViewModel = supplierSelectionViewModel;
    }

    public SupplierSelectionViewModel SupplierSelectionViewModel { get; }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        this.CloseAsync();        
    }
}