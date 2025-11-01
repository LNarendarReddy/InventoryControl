using NSRetailLiteApp.Views;
using System.Runtime.ExceptionServices;

namespace NSRetailLiteApp
{
    public partial class App : Application
    {

        public static string Version = "0.0.4.6";

        public App()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
            NavigationPage navigationPage = new NavigationPage(new LoginPage());
            NavigationPage.SetHasBackButton(navigationPage, false);
            NavigationPage.SetHasNavigationBar(navigationPage, false);
            //MainPage = new AppShell();
            MainPage = navigationPage; //new NavigationPage(new LoginPage());
        }

        private void CurrentDomain_FirstChanceException(object sender, FirstChanceExceptionEventArgs e)
        {
            //Current?.MainPage?.DisplayAlert("Error", e.Exception.Message, "OK");
            //Current?.MainPage?.DisplayAlert("Error", e.Exception.StackTrace, "OK");
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);

#if ANDROID
            window.Created += (s, e) =>
            {
                var nativeWindow = window.Handler?.PlatformView as Android.App.Activity;
                nativeWindow?.Window?.SetSoftInputMode(Android.Views.SoftInput.AdjustResize);
            };
#endif

            return window;
        }
    }
}
