using CommunityToolkit.Maui.Views;
using NSRetailLiteApp.Models;
using NSRetailLiteApp.ViewModels.StockEntry;

namespace NSRetailLiteApp.Views.StockEntry;

public partial class InvoiceDetailsPage : Popup
{
    public InvoiceDetailsPage(StockEntryViewModel stockEntryViewModel)
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = stockEntryViewModel;
        stockEntryViewModel.SaveComplete += StockEntryViewModel_SaveComplete;
    }

    private void StockEntryViewModel_SaveComplete(object? sender, EventArgs e)
    {
        CloseAsync();
    }

    private void txtSupplierInvoiceNo_Loaded(object sender, EventArgs e)
    {
        txtSupplierInvoiceNo.Focus();
    }
}