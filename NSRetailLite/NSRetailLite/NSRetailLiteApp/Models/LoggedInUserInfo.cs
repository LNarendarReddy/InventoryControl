using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.Models
{
    internal partial class LoggedInUserInfo : BaseObservableObject
    {
        [ObservableProperty]
        public string _userName;

        [ObservableProperty]
        public string _password;

        [ObservableProperty]
        public string _fullName;

        [ObservableProperty]
        public int _roleId;

        [ObservableProperty]
        public string _roleName;

        [ObservableProperty]
        public string _categoryId;

        [ObservableProperty]
        public string _categoryName;
    }
}
