using CommunityToolkit.Maui.Views;

namespace NSRetailLiteApp.Views.StockDispatch;

public partial class StockDispatchTypeSelectionPage : Popup
{
	public StockDispatchTypeSelectionPage()
	{
		InitializeComponent();
	}

	public string SelectedDispatchType { get; private set; }

    private void Button_Clicked(object sender, EventArgs e)
    {
		SelectedDispatchType = (sender as Button)?.Text ?? string.Empty;
        this.CloseAsync();
    }
}