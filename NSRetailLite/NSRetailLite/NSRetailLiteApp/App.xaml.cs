﻿using NSRetailLiteApp.Views;
using System.Runtime.ExceptionServices;

namespace NSRetailLiteApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;

            //MainPage = new AppShell();
            MainPage = new NavigationPage(new LoginPage());
        }

        private void CurrentDomain_FirstChanceException(object sender, FirstChanceExceptionEventArgs e)
        {
            //Current?.MainPage?.DisplayAlert("Error", e.Exception.StackTrace, "OK");
        }
    }
}
