using NSRetailLiteApp.ViewModels.DispatchReceive;

namespace NSRetailLiteApp.Views.DispatchReceive;

public partial class DispatchReceiveDetailListPage : ContentPage
{
	public DispatchReceiveDetailListPage(DispatchReceiveDetailListViewModel dispatchReceiveDetailListViewModel)
	{
		InitializeComponent();
        DispatchReceiveDetailListViewModel = dispatchReceiveDetailListViewModel;
        BindingContext = dispatchReceiveDetailListViewModel;
    }

    public DispatchReceiveDetailListViewModel DispatchReceiveDetailListViewModel { get; }
}