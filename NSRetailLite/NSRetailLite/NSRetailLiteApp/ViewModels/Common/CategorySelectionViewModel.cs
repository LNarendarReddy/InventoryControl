using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NSRetailLiteApp.Models;
using System.Collections.ObjectModel;

namespace NSRetailLiteApp.ViewModels.Common
{
    public partial class CategorySelectionViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<Category> _categories;

        [ObservableProperty]
        public ObservableCollection<Category> _filteredCategories;

        [ObservableProperty]
        public Category _selectedCategory;

        public CategorySelectionViewModel(ObservableCollection<Category> categories)
        {
            _categories = categories;
            _filteredCategories = categories;
        }

        [RelayCommand]
        public void PerformSearch(string search = null)
        {
            if (string.IsNullOrEmpty(search))
            {
                FilteredCategories = Categories;
                return;
            }

            search = search.ToLower();
            FilteredCategories
            = new ObservableCollection<Category>(
                    Categories.Where(x => x.CategoryName.Contains(search, StringComparison.CurrentCultureIgnoreCase)));
        }
    }
}
