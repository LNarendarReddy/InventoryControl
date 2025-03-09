using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NSRetailLiteApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.Common
{
    public partial class ChangePasswordViewModel : BaseViewModel
    {
        [ObservableProperty]
        public LoggedInUser _userData;

        [ObservableProperty]
        public string _oldPassword;

        [ObservableProperty]
        public string _newPassword;

        [ObservableProperty]
        public string _confirmPassword;

        [ObservableProperty]
        public bool _passwordMatched;

        public IAsyncRelayCommand ChangePasswordCommand { get; }

        public ChangePasswordViewModel(LoggedInUser loggedInUser)
        {
            UserData = loggedInUser;
            ChangePasswordCommand = new AsyncRelayCommand(ChangePassword);
        }

        partial void OnNewPasswordChanged(string value)
        {
            PasswordMatched = !string.IsNullOrEmpty(NewPassword) && NewPassword == ConfirmPassword;
        }

        partial void OnConfirmPasswordChanged(string value)
        {
            PasswordMatched = !string.IsNullOrEmpty(NewPassword) && NewPassword == ConfirmPassword;
        }

        private async Task ChangePassword()
        {
            HolderClass holder = new();
            holder = await PostAsync("user/changepassword", holder, new Dictionary<string, string?>()
            {
                { "UserId", UserData.UserId.ToString() },
                { "OldPassword", OldPassword },
                { "NewPassword", NewPassword }
            });

            if (holder.Exception != null) return;

            await DisplayAlert("Success", "Password changed successfully, User will be logged out. Please re-login", "OK");
            Application.Current?.MainPage?.Navigation.PopAsync();
            Application.Current?.MainPage?.Navigation.PopAsync();
        }
    }
}
