using CommunityToolkit.Mvvm.ComponentModel;
using NSRetailLiteApp.Models;
using NSRetailLiteApp.ViewModels;

namespace NSRetailLiteApp.Views;

public partial class HomePage : ContentPage
{   
    public HomePage(LoggedInUser loggedInUser)
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = new HomePageViewModel(loggedInUser);
    }
}