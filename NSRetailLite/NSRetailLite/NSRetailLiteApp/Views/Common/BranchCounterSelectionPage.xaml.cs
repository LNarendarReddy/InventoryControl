using CommunityToolkit.Maui.Views;
using NSRetailLiteApp.ViewModels.Common;

namespace NSRetailLiteApp.Views.Common;

public partial class BranchCounterSelectionPage : Popup
{
	public BranchCounterSelectionPage(BranchCounterSelectionViewModel branchCounterSelectionViewModel)
	{
		InitializeComponent(); 
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = branchCounterSelectionViewModel;
        BranchCounterSelectionViewModel = branchCounterSelectionViewModel;
    }

    public BranchCounterSelectionViewModel BranchCounterSelectionViewModel { get; }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        this.CloseAsync();
    }
}