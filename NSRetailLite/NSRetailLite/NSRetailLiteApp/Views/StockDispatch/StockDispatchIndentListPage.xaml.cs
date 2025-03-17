using NSRetailLiteApp.ViewModels.StockDispatch;

namespace NSRetailLiteApp.Views.StockDispatch;

public partial class StockDispatchIndentListPage : ContentPage
{
	public StockDispatchIndentListPage(StockDispatchIndentListViewModel stockDispatchIndentListViewModel)
	{
		InitializeComponent();
        StockDispatchIndentListViewModel = stockDispatchIndentListViewModel;
        BindingContext = stockDispatchIndentListViewModel;
    }

    public StockDispatchIndentListViewModel StockDispatchIndentListViewModel { get; }
}