using NSRetailLiteApp.ViewModels.PickList;

namespace NSRetailLiteApp.Views.Picklist;

public partial class PickListDispatchDetailPage : ContentPage
{
    private readonly PickListDispatchDetailViewModel pickListDispatchDetailViewModel;

    public PickListDispatchDetailPage(PickListDispatchDetailViewModel pickListDispatchDetailViewModel)
	{
		InitializeComponent();
		BindingContext = pickListDispatchDetailViewModel;
        pickListDispatchDetailViewModel.SaveComplete += PickListDispatchDetailViewModel_SaveComplete;
        this.pickListDispatchDetailViewModel = pickListDispatchDetailViewModel;
    }

    private void PickListDispatchDetailViewModel_SaveComplete()
    {
        txtItemCode.Focus();
    }

    private void pickListDispatchDetailPage_Loaded(object sender, EventArgs e)
    {
        if (pickListDispatchDetailViewModel == null) return;

        if (string.IsNullOrEmpty(pickListDispatchDetailViewModel.PickListDispatchViewModel.LastKnownTrayNumber))
            txtTrayNumber.Focus();
        else
            txtItemCode.Focus();
    }
}