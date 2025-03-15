using CommunityToolkit.Maui.Views;
using NSRetailLiteApp.ViewModels.StockDispatch;

namespace NSRetailLiteApp.Views.StockDispatch;

public partial class StockDispatchTypeSelectionPage : Popup
{
	public StockDispatchTypeSelectionPage(StockDispatchTypeSelectionViewModel stockDispatchTypeViewModel)
	{
		InitializeComponent();
        StockDispatchTypeViewModel = stockDispatchTypeViewModel;
        BindingContext = stockDispatchTypeViewModel;
    }

	public string SelectedDispatchType { get; private set; }
    public StockDispatchTypeSelectionViewModel StockDispatchTypeViewModel { get; }

    private void Button_Clicked(object sender, EventArgs e)
    {
		SelectedDispatchType = (sender as Button)?.Text ?? string.Empty;
        this.CloseAsync();
    }
}