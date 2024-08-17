using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.Models
{
    internal abstract partial class BaseObservableObject : ObservableObject
    {
        [ObservableProperty]
        public int _id;

        [ObservableProperty]
        public Exception _exception;

        public void ProcessResponse(HttpResponseMessage message)
        {
            ReadResponse(message);
            DisplayErrorMessage();
        }

        public void ProcessResponse(string message)
        {
            Exception = new Exception(message);
            DisplayErrorMessage();
        }

        public void ProcessResponse(Exception exception)
        {
            Exception = exception.InnerException ?? exception;
            DisplayErrorMessage();
        }


        private void ReadResponse(HttpResponseMessage message)
        {
            if (message == null)
            {
                Exception = new Exception("Null http response message");
                return;
            }

            string responeMessage = message.Content.ReadAsStringAsync().Result;

            if (!message.IsSuccessStatusCode)
            {
                Exception = new Exception(responeMessage);
                return;
            }

            if (string.IsNullOrEmpty(responeMessage))
            {
                Exception = new Exception("empty http response message");
                return;
            }

            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Populate(new StringReader(responeMessage), this);
        }

        private void DisplayErrorMessage()
        {
            if (Exception == null) return;

            Application.Current?.MainPage?.DisplayAlert("Error", Exception.Message, "OK");
        }
    }
}
