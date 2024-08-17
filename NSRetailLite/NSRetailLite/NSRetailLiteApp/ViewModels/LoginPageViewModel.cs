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
        public LoggedInUserInfo _model;

        public LoginPageViewModel() { Model = new LoggedInUserInfo(); }

        [RelayCommand]
        public void Login()
        {
            LoggedInUserInfo loggedInUserInfo = new();
            if (string.IsNullOrEmpty(Model.UserName))
            {
                loggedInUserInfo.ProcessResponse("User name cannot be empty");
                return;
            }

            if (string.IsNullOrEmpty(Model.Password))
            {
                loggedInUserInfo.ProcessResponse("Password cannot be empty");
                return;
            }

            PostAsync("user/getuserlogin", loggedInUserInfo, new HomePage()
                , new Dictionary<string, string?> { { "UserName", Model.UserName }, { "Password", Model.Password }, { "AppVersion", "1.5" } }
                );

            Model.Password = string.Empty;
        }
    }
}
