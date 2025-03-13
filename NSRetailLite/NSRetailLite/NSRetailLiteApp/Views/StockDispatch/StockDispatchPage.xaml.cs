using CommunityToolkit.Maui.Core.Extensions;
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
}
