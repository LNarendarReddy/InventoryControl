using CommunityToolkit.Maui.Views;
using NSRetailLiteApp.ViewModels;

namespace NSRetailLiteApp.Views.StockEntry;

public partial class SupplierIndentSelectionPage : Popup
{
	public SupplierIndentSelectionPage(SupplierIndentSelectionViewModel supplierIndentSelectionViewModel)
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = supplierIndentSelectionViewModel;
        SupplierIndentSelectionViewModel = supplierIndentSelectionViewModel;        
    }

    public SupplierIndentSelectionViewModel SupplierIndentSelectionViewModel { get; }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        this.CloseAsync();
    }
}