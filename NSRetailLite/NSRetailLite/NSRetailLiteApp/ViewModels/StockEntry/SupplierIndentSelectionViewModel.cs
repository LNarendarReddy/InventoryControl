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
    public partial class SupplierIndentSelectionViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<SupplierIndent> _supplierIndents;

        [ObservableProperty]
        public ObservableCollection<SupplierIndent> _filteredSupplierIndents;

        [ObservableProperty]
        public SupplierIndent _selectedSupplierIndent;

        public SupplierIndentSelectionViewModel(ObservableCollection<SupplierIndent> supplierIndents)
        {
            _supplierIndents = supplierIndents;
            _filteredSupplierIndents = supplierIndents;
        }

        [RelayCommand]
        public void PerformSearch(string search = null)
        {
            if (string.IsNullOrEmpty(search))
            {
                FilteredSupplierIndents = SupplierIndents;
                return;
            }

            search = search.ToLower();
            FilteredSupplierIndents
            = new ObservableCollection<SupplierIndent>(
                    SupplierIndents.Where(x => !string.IsNullOrEmpty(x.SupplierIndentNo) && x.SupplierIndentNo.Contains(search, StringComparison.CurrentCultureIgnoreCase)));
        }
    }
}
