using NSRetailLiteApp.Models;
using NSRetailLiteApp.ViewModels.StockCounting;

namespace NSRetailLiteApp.Views.StockCounting;

public partial class AddItem : ContentPage
{
	private StockCountingDetailViewModel stockCountingDetailViewModel;

    public AddItem(StockCountingDetailViewModel stockCountingDetailViewModel)
	{
		InitializeComponent();
        this.stockCountingDetailViewModel = stockCountingDetailViewModel;
        BindingContext = stockCountingDetailViewModel;
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        //txtItemCode.Focus();
    }
}