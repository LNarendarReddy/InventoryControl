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
    }
}