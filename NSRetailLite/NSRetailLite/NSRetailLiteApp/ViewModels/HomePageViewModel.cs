using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels
{
    internal partial class HomePageViewModel : BaseViewModel
    {
        public HomePageViewModel() 
        {
            LogoutCommand = new AsyncRelayCommand(Logout);
        }

        public IAsyncRelayCommand LogoutCommand { get; }

        private async Task Logout()
        {
            if(Application.Current == null || Application.Current.MainPage == null) return;

            bool confirm = await Application.Current.MainPage.DisplayAlert("Confirm", "Are you sure to logout?", "Yes", "No");
            if (confirm)
                Application.Current?.MainPage?.Navigation.PopAsync();
        }
    }
}
