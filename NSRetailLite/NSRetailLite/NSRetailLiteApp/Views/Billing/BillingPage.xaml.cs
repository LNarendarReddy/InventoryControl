using NSRetailLiteApp.Models;
using NSRetailLiteApp.ViewModels.Billing;

namespace NSRetailLiteApp.Views.Billing;

public partial class BillingPage : ContentPage
{
	public BillingPage(BillingViewModel billingViewModel)
	{
		InitializeComponent(); 
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = billingViewModel;
        BillingViewModel = billingViewModel;
        billingViewModel.LoadCompleted += BillingViewModel_LoadCompleted;
    }

    private void BillingViewModel_LoadCompleted()
    {
        txtItemCode.Focus();
    }

    public BillingViewModel BillingViewModel { get; }
}