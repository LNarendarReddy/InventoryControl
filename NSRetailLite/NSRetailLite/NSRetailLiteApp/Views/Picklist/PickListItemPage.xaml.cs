using NSRetailLiteApp.ViewModels.PickList;
using static NSRetailLiteApp.ViewModels.PickList.PickListItemViewModel;

namespace NSRetailLiteApp.Views.Picklist;

public partial class PickListItemPage : TabbedPage
{
    private readonly PickListItemViewModel pickListItemViewModel;

    public PickListItemPage(PickListItemViewModel pickListItemViewModel)
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);
        BindingContext = pickListItemViewModel;
        this.pickListItemViewModel = pickListItemViewModel;
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        pickListItemViewModel.LoadTrayWiseData();
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
         if (cmbTrayWisePicker.SelectedItem == null) return;

        TrayWiseGroup findGroupToScroll =
            pickListItemViewModel.TrayWiseData.FirstOrDefault(x => x.Name == (cmbTrayWisePicker.SelectedItem as TrayWiseGroup).Name.ToString());

        if (findGroupToScroll != null) cvTrayWiseData.ScrollTo(findGroupToScroll[0], findGroupToScroll, ScrollToPosition.Center);
    }

    private void ContentPage_Loaded_1(object sender, EventArgs e)
    {
        pickListItemViewModel.LoadAvailableItems();
    }
}