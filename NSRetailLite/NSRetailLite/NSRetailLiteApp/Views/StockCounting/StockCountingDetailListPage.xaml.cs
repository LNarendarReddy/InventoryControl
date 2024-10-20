using NSRetailLiteApp.ViewModels;
using NSRetailLiteApp.ViewModels.StockCounting;

namespace NSRetailLiteApp.Views.StockCounting;

public partial class StockCountingDetailListPage : ContentPage
{
	public StockCountingDetailListPage(StockCountingDetailListViewModel stockCountingDetailListViewModel)
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = stockCountingDetailListViewModel;
        StockCountingDetailListViewModel = stockCountingDetailListViewModel;
    }

    public StockCountingDetailListViewModel StockCountingDetailListViewModel { get; }

    private void stockCountingListPage_Loaded(object sender, EventArgs e)
    {
        StockCountingDetailListViewModel.Reload();
    }
}