using CommunityToolkit.Maui.Views;

namespace NSRetailLiteApp.Views.DispatchReceive;

public partial class DispatchReceiveTypeSelectionPage : Popup
{
	public DispatchReceiveTypeSelectionPage()
	{
		InitializeComponent();
	}

    public string SelectedDispatchRecieveType { get; private set; }

    private void Button_Clicked(object sender, EventArgs e)
    {
        SelectedDispatchRecieveType = (sender as Button)?.Text ?? string.Empty;
        this.CloseAsync();
    }
}