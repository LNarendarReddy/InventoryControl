using NSRetailLiteApp.ViewModels.Billing;

namespace NSRetailLiteApp.Views.Billing;

public partial class DayClosurePage : ContentPage
{

    public DayClosurePage(DayClosureViewModel dayClosureViewModel)
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = dayClosureViewModel;
        DayClosureViewModel = dayClosureViewModel;
    }

    public DayClosureViewModel DayClosureViewModel { get; }
}