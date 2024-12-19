using NSRetailLiteApp.ViewModels.Billing;

namespace NSRetailLiteApp.Views.Billing;

public partial class CustomerRefundPage : ContentPage
{
	public CustomerRefundPage(CustomerRefundViewModel customerRefundViewModel)
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = customerRefundViewModel;
        CustomerRefundViewModel = customerRefundViewModel;
    }

    public CustomerRefundViewModel CustomerRefundViewModel { get; }
}