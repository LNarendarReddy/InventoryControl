using NSRetailLiteApp.ViewModels;

namespace NSRetailLiteApp.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = new LoginPageViewModel();
    }
}