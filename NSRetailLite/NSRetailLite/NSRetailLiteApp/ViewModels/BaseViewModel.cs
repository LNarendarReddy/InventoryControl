using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using NSRetailLiteApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels
{
    internal abstract class BaseViewModel : ObservableObject
    {
        private static readonly HttpClient httpClient = new()
        {
            Timeout = new TimeSpan(0, 0, 5)
        };

        protected void PostAsync(string path, BaseObservableObject callingObject, ContentPage redirectToPage)
        {
            PostAsync(path, callingObject, redirectToPage, new Dictionary<string, string?> { { "jsonstring", JsonConvert.SerializeObject(callingObject) } });
        }

        protected void PostAsync(string path, BaseObservableObject callingObject, ContentPage redirectToPage, Dictionary<string, string?> values)
        {
            try
            {
                HttpResponseMessage responseMessage = httpClient.PostAsync(QueryHelpers.AddQueryString("https://nsoftsol.com:6002/api/" + path, values), null).Result;
                callingObject.ProcessResponse(responseMessage);
            }
            catch (Exception ex)
            {
                callingObject.ProcessResponse(ex);
            }

            if (callingObject.Exception == null && Application.Current != null && Application.Current.MainPage != null)
            {
                Application.Current.MainPage.Navigation.PushAsync(redirectToPage);
            }
        }
    }
}
