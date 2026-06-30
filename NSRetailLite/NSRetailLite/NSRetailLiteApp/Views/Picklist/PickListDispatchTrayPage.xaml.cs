using NSRetailLiteApp.ViewModels.PickList;

namespace NSRetailLiteApp.Views.Picklist;

public partial class PickListDispatchTrayPage : ContentPage
{
	public PickListDispatchTrayPage(PickListDispatchTrayViewModel pickListDispatchTrayViewModel)
	{
		InitializeComponent();
		BindingContext = pickListDispatchTrayViewModel;
	}
}