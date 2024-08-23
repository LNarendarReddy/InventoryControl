using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.Models
{
    public partial class Branch : BaseObservableObject
    {
        [ObservableProperty]
        public int _branchID;

        [ObservableProperty]
        public string _branchName;

        [ObservableProperty]
        public string _branchCode;

        [ObservableProperty]
        public string _address;

        [ObservableProperty]
        public string _phoneNo;

        [ObservableProperty]
        public string _landLine;

        [ObservableProperty]
        public string _emailID;
    }
}
