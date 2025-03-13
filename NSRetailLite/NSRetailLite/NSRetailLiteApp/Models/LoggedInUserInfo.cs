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
        private string _userName;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private int _roleId;

        [ObservableProperty]
        private string _roleName;

        [ObservableProperty]
        private int _categoryId;

        [ObservableProperty]
        private int _subCategoryId;

        [ObservableProperty]
        private string _categoryName;

        [ObservableProperty]
        private string _fullName;

        [ObservableProperty]
        private int _userId;

        [ObservableProperty]
        private int _branchId;

        [ObservableProperty]
        private string _branchName;

        [ObservableProperty]
        private ObservableCollection<FeatureAccessData> _featureAccess;

        [ObservableProperty]
        private string _appVersion;

        [ObservableProperty]
        private string _appURL;

        [ObservableProperty]
        private decimal? _multiEditThreshold;

        [ObservableProperty]
        private bool _filterMRPByStock;

        [ObservableProperty]
        private decimal? _enableDraftBills;

        [ObservableProperty]
        private string? _address;

        [ObservableProperty]
        private string? _phoneNo;
    }

    public partial class FeatureAccessData : BaseObservableObject
    {
        [ObservableProperty]
        private int _userId;

        [ObservableProperty]
        private int _featureAccess;

        [ObservableProperty]
        private bool _accessAvailable;

    }
    
    public partial class BranchCounter : BaseObservableObject
    {
        [ObservableProperty]
        private int _counterId;

        [ObservableProperty]
        private string _counterName;
    }
}
