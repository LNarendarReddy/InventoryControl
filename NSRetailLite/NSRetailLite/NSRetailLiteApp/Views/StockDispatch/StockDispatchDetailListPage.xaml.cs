using NSRetailLiteApp.ViewModels.StockDispatch;

namespace NSRetailLiteApp.Views.StockDispatch;

public partial class StockDispatchDetailListPage : ContentPage
{
    public StockDispatchDetailListPage(StockDispatchDetailListViewModel stockDispatchDetailListViewModel)
    {
        InitializeComponent();
        //NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = stockDispatchDetailListViewModel;
        StockCountingDetailListViewModel = stockDispatchDetailListViewModel;
    }

    public StockDispatchDetailListViewModel StockCountingDetailListViewModel { get; }
}