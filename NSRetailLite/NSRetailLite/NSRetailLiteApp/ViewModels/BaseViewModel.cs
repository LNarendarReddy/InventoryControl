using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using NSRetailLiteApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels
{
    public abstract partial class BaseViewModel : ObservableObject
    {
        
        [ObservableProperty]
        public bool _isLoading;

        const string URL = "https://nsoftsol.com:6002/api/";
        //const string URL = "https://nsoftsolcf.com/api/";

        private static readonly HttpClient httpClient = new()
        {
            Timeout = new TimeSpan(0, 0, 120)
        };

        protected async Task<T> PostAsync<T>(string path, T callingObject, Dictionary<string, string?> values
            , bool displayAlert = true, bool showResponse = false) where T : BaseObservableObject
        {
            IsLoading = true;
            try
            {
                HttpResponseMessage responseMessage = await httpClient.PostAsync(QueryHelpers.AddQueryString($"{URL}{path}", values), null);
                ProcessResponse(responseMessage, ref callingObject, displayAlert, showResponse);
            }
            catch (Exception ex)
            {
                ProcessResponse(ex, callingObject);
            }
            finally { IsLoading = false; }
            return callingObject;
        }

        protected async Task<T> PostAsyncAsContent<T>(string path, T callingObject, bool displayAlert = true, bool showResponse = false) where T : BaseObservableObject
        {
            IsLoading = true;
            try
            {
                HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync($"{URL}{path}", callingObject);
                ProcessResponse(responseMessage, ref callingObject, displayAlert, showResponse);
            }
            catch (Exception ex)
            {
                ProcessResponse(ex, callingObject);
            }
            finally { IsLoading = false; }
            return callingObject;
        }

        protected async Task<T> GetAsync<T>(string path, T callingObject, Dictionary<string, string?> values, bool displayAlert = true, int timeOut = 5) where T : BaseObservableObject
        {
            IsLoading = true;
            try
            {
                using (var cts = new CancellationTokenSource(timeOut * 1000))
                {
                    HttpResponseMessage responseMessage = await httpClient.GetAsync(QueryHelpers.AddQueryString($"{URL}{path}", values)
                        , HttpCompletionOption.ResponseContentRead, cts.Token);
                    ProcessResponse(responseMessage, ref callingObject, displayAlert);
                }
            }
            catch (Exception ex)
            {
                ProcessResponse(ex, callingObject);
            }
            finally 
            {
                IsLoading = false; 
            }
            return callingObject;
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

            if (int.TryParse(responeMessage, out int ID))
            {
                if (callingObject.GetType() == typeof(HolderClass))
                {
                    (callingObject as HolderClass).GenericID = ID;
                    return;
                }
                else if (callingObject is BaseObservableObject)
                {
                    callingObject.ReturnId = ID;
                    return;
                }
            }

            var jsonSerializer = new Newtonsoft.Json.JsonSerializer();

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
            else if (callingObject.GetType() == typeof(StockDispatchModel))
                callingObject = root.Holder.StockDispatch as T;
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

        protected async Task RedirectToPage<T>(BaseObservableObject callingObject, T redirectToPage, bool isModal = false) where T : Page
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
