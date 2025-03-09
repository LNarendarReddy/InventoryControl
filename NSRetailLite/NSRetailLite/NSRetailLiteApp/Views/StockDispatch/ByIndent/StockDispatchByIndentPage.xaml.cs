using CommunityToolkit.Maui.Core.Extensions;
using NSRetailLiteApp.Models;
using NSRetailLiteApp.ViewModels.StockDispatch.Indent;
using System.Collections.ObjectModel;

namespace NSRetailLiteApp.Views.StockDispatch.ByIndent;

public partial class StockDispatchByIndentPage : TabbedPage
{
	public StockDispatchByIndentPage(StockDispatchByIndentViewModel stockDispatchByIndentViewModel)
	{
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);
        BindingContext = stockDispatchByIndentViewModel;
        StockDispatchByIndentViewModel = stockDispatchByIndentViewModel;

    }

    public StockDispatchByIndentViewModel StockDispatchByIndentViewModel { get; }

   
}
