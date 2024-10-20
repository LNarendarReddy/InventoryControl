using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.Common
{
    public partial class BarcodeScannerViewModel : BaseViewModel
    {
        [ObservableProperty]
        public string _scannedCode;
    }
}
