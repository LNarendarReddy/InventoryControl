using NSRetailLiteApp.ViewModels.StockDispatch;

namespace NSRetailLiteApp.Views.StockDispatch;

public partial class StockDispatchDetailPage : ContentPage
{
	public StockDispatchDetailPage(StockDispatchDetailViewModel stockDispatchDetailViewModel)
	{
		InitializeComponent();
        StockDispatchDetailViewModel = stockDispatchDetailViewModel;
        BindingContext = stockDispatchDetailViewModel;
    }

    public StockDispatchDetailViewModel StockDispatchDetailViewModel { get; }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        if (!StockDispatchDetailViewModel.StockDispatchDetailModel.IsNew)
            return;

        StockDispatchDetailViewModel.LoadItemCommand.Execute(null);

        if (string.IsNullOrEmpty(StockDispatchDetailViewModel.StockDispatchDetailModel.TrayNumber)) { txtTrayNumber?.Focus(); return; }

        if (string.IsNullOrEmpty(StockDispatchDetailViewModel.StockDispatchDetailModel.ItemCode)) { txtItemCode?.Focus(); return; }

        (StockDispatchDetailViewModel.StockDispatchDetailModel.IsOpenItem ? txtWeightInKGs: txtQuantity)?.Focus();    
    }
}