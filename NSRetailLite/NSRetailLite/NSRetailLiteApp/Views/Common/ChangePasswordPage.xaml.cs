using NSRetailLiteApp.ViewModels.Common;

namespace NSRetailLiteApp.Views.Common;

public partial class ChangePasswordPage : ContentPage
{
	public ChangePasswordPage(ChangePasswordViewModel changePasswordViewModel)
	{
		InitializeComponent(); 
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = changePasswordViewModel;
        ChangePasswordViewModel = changePasswordViewModel;
    }

    public ChangePasswordViewModel ChangePasswordViewModel { get; }
}