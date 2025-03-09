using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NSRetailLiteApp.Models;
using NSRetailLiteApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels
{
    internal partial class LoginPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        public LoggedInUser _model;

        public LoginPageViewModel() { Model = new LoggedInUser(); }

        [RelayCommand]
        public async void Login()
        {
            LoggedInUser loggedInUserInfo = new();
            if (string.IsNullOrEmpty(Model.UserName))
            {
                DisplayErrorMessage("User name cannot be empty");
                return;
            }

            if (string.IsNullOrEmpty(Model.Password))
            {
                DisplayErrorMessage("Password cannot be empty");
                return;
            }

            loggedInUserInfo = await PostAsync("user/getuserlogin", loggedInUserInfo
                , new Dictionary<string, string?>
                    {
                        { "UserName", Model.UserName },
                        { "Password", Model.Password },
                        { "AppVersion", "1.5" },
                        { "IsNested", "True" } }
                );

            Model.Password = string.Empty;

            if (loggedInUserInfo.Exception != null)
            {
                return;
            }

            if (loggedInUserInfo.AppVersion == App.Version)
            {
                RedirectToPage(loggedInUserInfo, new HomePage(loggedInUserInfo));
                return;
            }

            if (await DisplayAlert("Update", $"New update available, do you want to download new APK?", "Yes", "No"))
            {
                Launcher.OpenAsync(loggedInUserInfo.AppURL);
                return;
            }

            DisplayErrorMessage("The operation has been cancelled");
        }
    }
}
