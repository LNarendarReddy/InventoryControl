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
        BranchSelectionViewModel = branchSelectionViewModel;
    }

    public BranchSelectionViewModel BranchSelectionViewModel { get; }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        this.CloseAsync();        
    }
}