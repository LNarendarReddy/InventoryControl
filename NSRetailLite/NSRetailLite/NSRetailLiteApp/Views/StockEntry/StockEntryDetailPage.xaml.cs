using NSRetailLiteApp.ViewModels.StockEntry;

namespace NSRetailLiteApp.Views.StockEntry;

public partial class StockEntryDetailPage : ContentPage
{
	public StockEntryDetailPage(StockEntryDetailViewModel stockEntryDetailViewModel)
	{
		InitializeComponent();		
        stockEntryDetailViewModel.SaveComplete += stockEntryDetailViewModel_SaveComplete;
        StockEntryDetailViewModel = stockEntryDetailViewModel;
        BindingContext = StockEntryDetailViewModel;
        stockEntryDetailViewModel.FocusMRP += StockEntryDetailViewModel_FocusMRP;
        stockEntryDetailViewModel.FocusQuantity += StockEntryDetailViewModel_FocusQuantity;
        stockEntryDetailViewModel.FocusWeight += StockEntryDetailViewModel_FocusWeight;
    }

    private void StockEntryDetailViewModel_FocusWeight()
    {
        txtWeightInKGs.Focus();
    }

    private void StockEntryDetailViewModel_FocusQuantity()
    {
        txtQuantity.Focus();
    }

    private void StockEntryDetailViewModel_FocusMRP()
    {
        txtMRP.Focus();
    }

    public StockEntryDetailViewModel StockEntryDetailViewModel { get; }

    private void stockEntryDetailViewModel_SaveComplete()
    {
        txtItemCode.Focus();
    }

    private void txtItemCode_Loaded(object sender, EventArgs e)
    {
        txtItemCode.Focus();
    }

    private void StockEntryDetail_Loaded(object sender, EventArgs e)
    {
        Title = (StockEntryDetailViewModel.StockEntryDetailModel.StockEntryDetailId == 0 ? "Add" : "Edit") + " invoice item";
    }
}