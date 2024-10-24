using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NSRetailLiteApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels
{
    public partial class BranchSelectionViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<Branch> _branches;

        [ObservableProperty]
        public ObservableCollection<Branch> _filteredBranches;

        [ObservableProperty]
        public Branch _selectedBranch;

        public BranchSelectionViewModel(ObservableCollection<Branch> branches)
        {
            _branches = branches;
            _filteredBranches = branches;
        }

        [RelayCommand]
        public void PerformSearch(string search = null)
        {
            if (string.IsNullOrEmpty(search))
            {
                FilteredBranches = Branches;
                return;
            }

            search = search.ToLower();
            FilteredBranches
                = new ObservableCollection<Branch>(
                    Branches.Where(x => x.BranchName.Contains(search, StringComparison.CurrentCultureIgnoreCase)));
        }
    }
}
