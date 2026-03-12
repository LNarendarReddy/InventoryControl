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
    public partial class SupplierSelectionViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<Supplier> _suppliers;

        [ObservableProperty]
        public ObservableCollection<Supplier> _filteredSuppliers;

        [ObservableProperty]
        public Supplier _selectedSupplier;

        public SupplierSelectionViewModel(ObservableCollection<Supplier> suppliers)
        {
            _suppliers = suppliers;
            _filteredSuppliers = suppliers;
        }

        [RelayCommand]
        public void PerformSearch(string search = null)
        {
            if (string.IsNullOrEmpty(search))
            {
                FilteredSuppliers = Suppliers;
                return;
            }

            search = search.ToLower();
            FilteredSuppliers
                = new ObservableCollection<Supplier>(
                    Suppliers.Where(x => x.SupplierName.Contains(search, StringComparison.CurrentCultureIgnoreCase)));
        }
    }
}
