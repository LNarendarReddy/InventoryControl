using CommunityToolkit.Maui.Views;
using NSRetailLiteApp.ViewModels;

namespace NSRetailLiteApp.Views.StockCounting;

public partial class BranchSelectionPage : Popup
{
	public BranchSelectionPage(BranchSelectionViewModel branchSelectionViewModel)
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = branchSelectionViewModel;
    }

    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        this.CloseAsync();
    }
}