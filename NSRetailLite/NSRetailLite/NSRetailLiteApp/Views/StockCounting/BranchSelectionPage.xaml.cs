using NSRetailLiteApp.ViewModels;

namespace NSRetailLiteApp.Views.StockCounting;

public partial class BranchSelectionPage : ContentPage
{
	public BranchSelectionPage(BranchSelectionViewModel branchSelectionViewModel)
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = branchSelectionViewModel;
    }
}