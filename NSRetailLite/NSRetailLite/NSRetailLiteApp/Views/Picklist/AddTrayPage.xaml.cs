using CommunityToolkit.Maui.Views;
using NSRetailLiteApp.ViewModels.PickList;

namespace NSRetailLiteApp.Views.Picklist;

public partial class AddTrayPage : Popup
{
	public AddTrayPage(PickListTrayViewModel pickListTrayViewModel)
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = pickListTrayViewModel;
        pickListTrayViewModel.SaveComplete += PickListTrayViewModel_SaveComplete;
    }

    private void PickListTrayViewModel_SaveComplete(object? sender, EventArgs e)
    {
        CloseAsync();
    }

    private void TrayNumber_Loaded(object sender, EventArgs e)
    {
        txtTrayNumber.Focus();
    }
}