using CommunityToolkit.Maui.Views;
using NSRetailLiteApp.ViewModels.Common;

namespace NSRetailLiteApp.Views.Common;

public partial class CategorySelectionPage : Popup
{
	public CategorySelectionPage(CategorySelectionViewModel categorySelectionViewModel)
	{
		InitializeComponent();
        BindingContext = categorySelectionViewModel;
        CategorySelectionViewModel = categorySelectionViewModel;
    }

    public CategorySelectionViewModel CategorySelectionViewModel { get; }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        this.CloseAsync();
    }
}