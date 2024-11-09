using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.Models
{
    public partial class LoggedInUser : BaseObservableObject
    {
        [ObservableProperty]
        public string _userName;

        [ObservableProperty]
        public string _password;

        [ObservableProperty]
        public int _roleId;

        [ObservableProperty]
        public string _roleName;

        [ObservableProperty]
        public string _categoryId;

        [ObservableProperty]
        public string _categoryName;

        [ObservableProperty]
        public string _fullName;

        [ObservableProperty]
        public int _userId;

        [ObservableProperty]
        public int _branchId;

        [ObservableProperty]
        public string _branchName;

        [ObservableProperty]
        public ObservableCollection<FeatureAccessData> _featureAccess;

        [ObservableProperty]
        public string _appVersion;

        [ObservableProperty]
        public string _appURL;
    }

    public partial class FeatureAccessData : BaseObservableObject
    {
        [ObservableProperty]
        public int _userId;

        [ObservableProperty]
        public int _featureAccess;

        [ObservableProperty]
        public bool _accessAvailable;

    }    
}
