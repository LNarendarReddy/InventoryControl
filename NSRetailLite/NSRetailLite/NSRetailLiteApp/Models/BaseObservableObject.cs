using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.Models
{
    public abstract partial class BaseObservableObject : ObservableObject
    {
        [ObservableProperty]
        public Exception _exception;
    }
}
