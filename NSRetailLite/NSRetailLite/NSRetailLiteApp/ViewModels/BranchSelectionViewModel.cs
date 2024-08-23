using CommunityToolkit.Mvvm.ComponentModel;
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
        public Branch _selectedBranch;

        public BranchSelectionViewModel(ObservableCollection<Branch> branches)
        {
            _branches = branches;
        }
    }
}
