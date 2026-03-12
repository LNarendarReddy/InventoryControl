using NSRetailLiteApp.ViewModels.StockEntry;

namespace NSRetailLiteApp.Views.StockEntry;

public partial class StockEntryDetailListPage : ContentPage
{
    bool firstTimeLoad = true;

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
        if (firstTimeLoad)
        {
            firstTimeLoad = false;
            return;
        }
        
        StockEntryDetailListViewModel.Reload();
    }
}