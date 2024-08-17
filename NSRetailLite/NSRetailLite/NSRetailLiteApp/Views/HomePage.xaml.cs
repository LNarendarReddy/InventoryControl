using NSRetailLiteApp.ViewModels;

namespace NSRetailLiteApp.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = new HomePageViewModel();
    }
}