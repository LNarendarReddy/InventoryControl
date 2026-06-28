using NSRetailLiteApp.ViewModels.StockEntry;

namespace NSRetailLiteApp.Views.StockEntry;

public partial class StockEntryDetailListPage : ContentPage
{
	public StockEntryDetailListPage(StockEntryDetailListViewModel stockEntryDetailListViewModel)
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = stockEntryDetailListViewModel;
        StockEntryDetailListViewModel = stockEntryDetailListViewModel;
    }

    public StockEntryDetailListViewModel StockEntryDetailListViewModel { get; }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        StockEntryDetailListViewModel.Reload();
    }
}