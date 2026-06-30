using NSRetailLiteApp.ViewModels.PickList;

namespace NSRetailLiteApp.Views.Picklist;

public partial class PickListDispatchTrayDetailsPage : ContentPage
{
	public PickListDispatchTrayDetailsPage(PickListDipatchTrayDetailsViewModel pickListDipatchTrayDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = pickListDipatchTrayDetailsViewModel;
	}
}