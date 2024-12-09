using CommunityToolkit.Mvvm.ComponentModel;
using NSRetailLiteApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.Common
{
    public partial class BranchCounterSelectionViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<BranchCounter> _branchCounters;

        [ObservableProperty]
        public BranchCounter _selectedBranchCounter;

        public BranchCounterSelectionViewModel(ObservableCollection<BranchCounter> branchCounters)
        {
            _branchCounters = branchCounters;
        }
    }
}
