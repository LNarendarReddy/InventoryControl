using NSRetailLiteApp.ViewModels.DispatchReceive;

namespace NSRetailLiteApp.Views.DispatchReceive;

public partial class DispatchReceiveListPage : ContentPage
{
	public DispatchReceiveListPage(DispatchReceiveListViewModel dispatchReceiveListViewModel)
	{
		InitializeComponent();
        DispatchReceiveListViewModel = dispatchReceiveListViewModel;
        BindingContext = DispatchReceiveListViewModel;
    }

    public DispatchReceiveListViewModel DispatchReceiveListViewModel { get; }
}