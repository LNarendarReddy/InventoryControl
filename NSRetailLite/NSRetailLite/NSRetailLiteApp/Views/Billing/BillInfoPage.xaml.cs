using NSRetailLiteApp.ViewModels.Billing;

namespace NSRetailLiteApp.Views.Billing;

public partial class BillInfoPage : ContentPage
{
	public BillInfoPage(BillInfoViewModel billInfoViewModel)
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);
        BindingContext = billInfoViewModel;
        BillInfoViewModel = billInfoViewModel;
    }

    public BillInfoViewModel BillInfoViewModel { get; }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
            BillInfoViewModel.CurrentBill.PaymentModeId = Convert.ToString((sender as RadioButton)?.Value);
    }

    protected override bool OnBackButtonPressed()
    {
        return BillInfoViewModel.IsBillOfferApplied ? true : base.OnBackButtonPressed();
    }
}