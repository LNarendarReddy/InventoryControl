﻿using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using NSRetailLiteApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels
{
    public abstract class BaseViewModel : ObservableObject
    {
        private static readonly HttpClient httpClient = new()
        {
            Timeout = new TimeSpan(0, 0, 5)
        };

        protected void PostAsync<T>(string path, ref T callingObject, bool displayAlert = true) where T : BaseObservableObject
        {
            PostAsync(path, ref callingObject, new Dictionary<string, string?> { { "jsonstring", JsonConvert.SerializeObject(callingObject) } }, displayAlert);
        }

        protected void PostAsync<T>(string path, ref T callingObject, Dictionary<string, string?> values, bool displayAlert = true) where T : BaseObservableObject
        {
            try
            {
                HttpResponseMessage responseMessage = httpClient.PostAsync(QueryHelpers.AddQueryString("https://nsoftsol.com:6002/api/" + path, values), null).Result;
                ProcessResponse(responseMessage, ref callingObject, displayAlert);
            }
            catch (Exception ex)
            {
                ProcessResponse(ex, callingObject);
            }
        }

        protected void GetAsync<T>(string path, ref T callingObject, Dictionary<string, string?> values, bool displayAlert = true) where T : BaseObservableObject
        {
            try
            {
                HttpResponseMessage responseMessage = httpClient.GetAsync(QueryHelpers.AddQueryString("https://nsoftsol.com:6002/api/" + path, values)).Result;
                ProcessResponse(responseMessage, ref callingObject, displayAlert);
            }
            catch (Exception ex)
            {
                ProcessResponse(ex, callingObject);
            }
        }

        public void ProcessResponse<T>(HttpResponseMessage message, ref T callingObject, bool displayAlert = true) where T : BaseObservableObject
        {
            ReadResponse(message, ref callingObject);
            DisplayErrorMessage(callingObject, displayAlert);
        }

        public void ProcessResponse(string message, BaseObservableObject callingObject, bool displayAlert = true)
        {
            callingObject.Exception = new Exception(message);
            DisplayErrorMessage(callingObject, displayAlert);
        }

        public void ProcessResponse(Exception exception, BaseObservableObject callingObject, bool displayAlert = true)
        {
            callingObject.Exception = exception.InnerException ?? exception;
            DisplayErrorMessage(callingObject, displayAlert);
        }


        private void ReadResponse<T>(HttpResponseMessage message, ref T callingObject) where T : BaseObservableObject
        {
            if (message == null)
            {
                callingObject.Exception = new Exception("http null response message");
                return;
            }

            string responeMessage = message.Content.ReadAsStringAsync().Result;

            if (!message.IsSuccessStatusCode)
            {
                string exceptionMessage = string.IsNullOrEmpty(responeMessage) ? (message.ReasonPhrase ?? "Empty response") : responeMessage;
                callingObject.Exception = new Exception(exceptionMessage);
                return;
            }

            if (string.IsNullOrEmpty(responeMessage))
            {
                callingObject.Exception = new Exception("empty http response message");
                return;
            }

            var jsonSerializer = new JsonSerializer();


            RootClass root = new RootClass();
            jsonSerializer.Populate(new StringReader(responeMessage), root);

            if (callingObject.GetType() == typeof(LoggedInUser))
                callingObject = root.Holder.User as T;
            else if (callingObject.GetType() == typeof(StockCountingModel))
                callingObject = root.Holder.Counting as T;
            else if (callingObject.GetType() == typeof(ObservableCollection<Branch>))
                callingObject = root.Holder.Branch as T;
            else if (callingObject.GetType() == typeof(HolderClass))
                callingObject = root.Holder as T;
        }

        private void DisplayErrorMessage(BaseObservableObject callingObject, bool displayAlert = true)
        {
            if (callingObject.Exception == null || !displayAlert) return;

            Application.Current?.MainPage?.DisplayAlert("Error", callingObject.Exception.Message, "OK");
        }

        protected void DisplayErrorMessage(string error)
        {
            Application.Current?.MainPage?.DisplayAlert("Error", error, "OK");
        }

        protected void RedirectToPage(BaseObservableObject callingObject, ContentPage redirectToPage)
        {
            if (callingObject.Exception == null && Application.Current != null && Application.Current.MainPage != null)
            {
                Application.Current.MainPage.Navigation.PushAsync(redirectToPage);
            }
        }
    }
}