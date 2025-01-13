using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
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
    public abstract partial class BaseViewModel : ObservableObject
    {

        [ObservableProperty]
        public bool _isLoading;

        private static readonly HttpClient httpClient = new()
        {
            Timeout = new TimeSpan(0, 0, 5)
        };

        protected void PostAsync<T>(string path, ref T callingObject, bool displayAlert = true) where T : BaseObservableObject
        {
            PostAsync(path, ref callingObject, new Dictionary<string, string?> { { "jsonstring", JsonConvert.SerializeObject(callingObject) } }, displayAlert);
        }

        protected void PostAsync<T>(string path, ref T callingObject, Dictionary<string, string?> values
            , bool displayAlert = true, bool showResponse = false) where T : BaseObservableObject
        {
            IsLoading = true;
            try
            {
                HttpResponseMessage responseMessage = httpClient.PostAsync(QueryHelpers.AddQueryString("https://nsoftsol.com:6002/api/" + path, values), null).Result;
                ProcessResponse(responseMessage, ref callingObject, displayAlert, showResponse);
            }
            catch (Exception ex)
            {
                ProcessResponse(ex, callingObject);
            }
            finally { IsLoading = false; }
        }

        protected void GetAsync<T>(string path, ref T callingObject, Dictionary<string, string?> values, bool displayAlert = true) where T : BaseObservableObject
        {
            IsLoading = true;
            try
            {
                HttpResponseMessage responseMessage = httpClient.GetAsync(QueryHelpers.AddQueryString("https://nsoftsol.com:6002/api/" + path, values)).Result;
                ProcessResponse(responseMessage, ref callingObject, displayAlert);
            }
            catch (Exception ex)
            {
                ProcessResponse(ex, callingObject);
            }
            finally { IsLoading = false; }
        }

        public void ProcessResponse<T>(HttpResponseMessage message, ref T callingObject, bool displayAlert = true
            , bool showResponse = false) where T : BaseObservableObject
        {
            ReadResponse(message, ref callingObject, showResponse);
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


        private void ReadResponse<T>(HttpResponseMessage message, ref T callingObject, bool showResponse) where T : BaseObservableObject
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

            if(showResponse)
            {
                DisplayAlert("Response", responeMessage, "OK");
                return;
            }

            if (int.TryParse(responeMessage, out int ID) && callingObject.GetType() == typeof(HolderClass))
            {
                (callingObject as HolderClass).GenericID = ID;
                return;
            }

            var jsonSerializer = new JsonSerializer();

            RootClass root = new RootClass();
            jsonSerializer.Populate(new StringReader(responeMessage), root);

            if (callingObject.GetType() == typeof(LoggedInUser))
                callingObject = root.Holder.User as T;
            else if (callingObject.GetType() == typeof(StockCountingModel))
                callingObject = root.Holder.Counting as T;
            else if (callingObject.GetType() == typeof(Item))
                callingObject = root.Holder.Item as T;
            else if (callingObject.GetType() == typeof(ObservableCollection<Branch>))
                callingObject = root.Holder.BranchList as T;
            else if (callingObject.GetType() == typeof(Branch))
                callingObject = root.Holder.Branch as T;
            else if (callingObject.GetType() == typeof(DaySequence))
                callingObject = root.Holder.DaySequence as T;
            else if (callingObject.GetType() == typeof(HolderClass))
                callingObject = root.Holder as T;
            else if (callingObject.GetType() == typeof(DayClosure))
                callingObject = root.Holder.DayClosure as T;
        }

        private void DisplayErrorMessage(BaseObservableObject callingObject, bool displayAlert = true)
        {
            if (callingObject.Exception == null || !displayAlert) return;

            Application.Current?.MainPage?.DisplayAlert("Error", callingObject.Exception.Message, "OK");
        }

        protected void DisplayErrorMessage(string error)
        {
            //Application.Current?.MainPage?.DisplayAlert("Error", error, "OK");
            DisplayAlert("Error", error, "OK");
        }

        protected async Task RedirectToPage(BaseObservableObject callingObject, ContentPage redirectToPage, bool isModal = false)
        {
            if (callingObject?.Exception == null && Application.Current != null && Application.Current.MainPage != null)
            {
                if (isModal)
                    await Application.Current.MainPage.Navigation.PushModalAsync(redirectToPage);
                else
                    await Application.Current.MainPage.Navigation.PushAsync(redirectToPage);
            }
        }

        protected async Task ShowPopup(BaseObservableObject callingObject, Popup popupPage)
        {
            if (callingObject?.Exception == null && Application.Current != null && Application.Current.MainPage != null)
            {
                await Application.Current.MainPage.ShowPopupAsync(popupPage);                
            }
        }

        protected async Task<bool> DisplayAlert(string caption, string message, string accept, string cancel = "")
        {
            if (Application.Current != null && Application.Current.MainPage != null)
            {
                if (string.IsNullOrEmpty(cancel))
                {
                    await Application.Current.MainPage.DisplayAlert(caption, message, accept);
                    return true;
                }
                else
                {
                    return await Application.Current.MainPage.DisplayAlert(caption, message, accept, cancel);
                }
            }

            return false;
        }

        protected async Task Pop(bool isModal = false)
        {
            if (Application.Current != null && Application.Current.MainPage != null)
            {
                if (isModal)
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                else
                    await Application.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}
