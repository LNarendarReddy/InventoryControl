using NSRetailLiteApp.ViewModels.Billing;

namespace NSRetailLiteApp.Views.Billing;

public partial class BillInfoPage : ContentPage
{
	public BillInfoPage(BillInfoViewModel billInfoViewModel)
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = billInfoViewModel;
        BillInfoViewModel = billInfoViewModel;
    }

    public BillInfoViewModel BillInfoViewModel { get; }
}