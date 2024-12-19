using NSRetailLiteApp.ViewModels.Billing;

namespace NSRetailLiteApp.Views.Billing;

public partial class CustomerRefundInfoPage : ContentPage
{
	public CustomerRefundInfoPage(CustomerRefundInfoViewModel customerRefundInfoViewModel)
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = customerRefundInfoViewModel;
        CustomerRefundInfoViewModel = customerRefundInfoViewModel;
    }

    public CustomerRefundInfoViewModel CustomerRefundInfoViewModel { get; }
}