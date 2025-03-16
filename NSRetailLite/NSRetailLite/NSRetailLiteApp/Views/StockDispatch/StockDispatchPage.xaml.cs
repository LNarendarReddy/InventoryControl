using CommunityToolkit.Maui.Core.Extensions;
using DevExpress.Data.Extensions;
using NSRetailLiteApp.Models;
using NSRetailLiteApp.ViewModels.StockDispatch;
using NSRetailLiteApp.ViewModels.StockDispatch.Indent;
using System.Collections.ObjectModel;

namespace NSRetailLiteApp.Views.StockDispatch;

public partial class StockDispatchPage : TabbedPage
{
	public StockDispatchPage(StockDispatchViewModel stockDispatchViewModel)
	{
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);
        BindingContext = stockDispatchViewModel;
        StockDispatchViewModel = stockDispatchViewModel;
    }

    public StockDispatchViewModel StockDispatchViewModel { get; }

    private void TabbedPage_Loaded(object sender, EventArgs e)
    {
        if (StockDispatchViewModel.IsManual)
            this.CurrentPage = ManualDipstachContent;
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbTrayWisePicker.SelectedItem == null) return;

        TrayWiseGroup findGroupToScroll = 
            StockDispatchViewModel.TrayWiseData.FirstOrDefault(x => x.Name == (cmbTrayWisePicker.SelectedItem as TrayInfo).TrayNumber.ToString());

        if (findGroupToScroll != null) cvTrayWiseData.ScrollTo(findGroupToScroll[0], findGroupToScroll, ScrollToPosition.Center);

    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        StockDispatchViewModel.BuildTrayWiseData();
    }
}
