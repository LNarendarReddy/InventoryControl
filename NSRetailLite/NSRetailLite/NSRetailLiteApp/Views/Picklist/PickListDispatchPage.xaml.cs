using NSRetailLiteApp.ViewModels.PickList;

namespace NSRetailLiteApp.Views.Picklist;

public partial class PickListDispatchPage : ContentPage
{
	public PickListDispatchPage(PickListDispatchViewModel dispatchViewModel)
	{
		InitializeComponent();
        DispatchViewModel = dispatchViewModel;
        BindingContext = dispatchViewModel;
        NavigationPage.SetHasNavigationBar(this, false);
    }

    public PickListDispatchViewModel DispatchViewModel { get; }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        DispatchViewModel.Reload();
    }
}