using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevExpress.Maui.Mvvm;
using NSRetailLiteApp.Models;
using NSRetailLiteApp.Views.Picklist;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.PickList
{
    public partial class PickListItemViewModel : BaseViewModel
    {
        [ObservableProperty]
        private Branch _branch;

        [ObservableProperty]
        private string _title;

        private readonly LoggedInUser loggedInUser;

        public ObservableCollection<PickListItemModel> PickListItemsModel { get; }

        public ObservableCollection<TrayWiseGroup> TrayWiseData { get; }

        public PickListItemViewModel(Branch selectedBranch, LoggedInUser loggedInUser) 
        {
            Branch = selectedBranch;
            this.loggedInUser = loggedInUser;
            PickListItemsModel = [];

            Title = $"{Branch.BranchName} ( {Branch.BranchCode} )";
            SubmitCommand = new AsyncRelayCommand(Submit);
            AddTrayQuantityCommand = new AsyncRelayCommand<PickListItemModel?>(AddTrayQuantity);
            DeleteItemDetailCommand = new AsyncRelayCommand<PickListItemModel?>(DeleteItemDetail);
            TrayWiseData = new();
        }

        public IAsyncRelayCommand SubmitCommand { get; }

        public IAsyncRelayCommand<PickListItemModel> AddTrayQuantityCommand { get; }

        public IAsyncRelayCommand<PickListItemModel> DeleteItemDetailCommand { get; }

        private async Task Submit()
        {
            if (!await DisplayAlert("Confirm", "Are you sure you want to submit picklist tray data? this operation cannot be reversed", "Yes", "No"))
                return;

            HolderClass holderClass = new HolderClass();
            holderClass = await PostAsync("picklist/submitpicklist", holderClass, new Dictionary<string, string?>
            {
                { "PickListID", Branch.PickListID.ToString() },
                { "userID", loggedInUser.UserId.ToString() }
            });

            if (holderClass?.Exception == null) await Pop();
        }

        private async Task AddTrayQuantity(PickListItemModel? selected)
        {
            if (selected == null) return;

            await ShowPopup(selected, new AddTrayPage(new PickListTrayViewModel(Branch.PickListID, selected, loggedInUser)));
        }

        private async Task DeleteItemDetail(PickListItemModel? selected)
        {
            if (selected == null) return;
            Dictionary<string, string?> paremeters = new Dictionary<string, string?>
            {
                { "pickListItemDetailID", selected.PickListItemDetailID.ToString() },
                { "userID", loggedInUser.UserId.ToString() }
            };
            await DeleteAsync("picklist/deleteitemdetail", selected, paremeters);

            if (selected.Exception != null)
            {
                selected.Exception = null;
                return;
            }

            TrayWiseData.Where(x => x.Contains(selected)).ToList().ForEach(x => x.Remove(selected));
            var removableTrays = TrayWiseData.Where(x => x.Count == 0).ToList();
            removableTrays.ForEach(x => TrayWiseData.Remove(x));
        }

        public async Task LoadTrayWiseData()
        {
            IsLoading = true;
            HolderClass holderClass = new();
            holderClass = await GetAsync("picklist/gettraywisedata", holderClass, new Dictionary<string, string?>()
                {
                    { "PicklistID", Branch.PickListID.ToString() }
                }, true);

            if (holderClass == null || holderClass.Exception != null) return;

            TrayWiseData.Clear();
            holderClass.Holder.PickListTrayList.ToList().ForEach(x => TrayWiseData.Add(new TrayWiseGroup(x.TrayNumber, x.PickListItemList?.ToList() ?? [])));
            IsLoading = false;
        }

        public async Task LoadAvailableItems()
        {
            IsLoading = true;
            HolderClass holderClass = new();
            holderClass = await GetAsync("picklist/getitemdata", holderClass, new Dictionary<string, string?>()
                {
                    { "PicklistID", Branch.PickListID.ToString() }
                }, true);

            PickListItemsModel.Clear();
            holderClass.Holder.PickListItemList.ForEach(PickListItemsModel.Add);
            IsLoading = false;
        }

        public class TrayWiseGroup : ObservableCollection<PickListItemModel>
        {
            public string Name { get; private set; }

            public int PieceCount { get; private set; }


            public TrayWiseGroup(string name, List<PickListItemModel> pickListItems) : base(pickListItems)
            {
                Name = name;
                PieceCount = pickListItems?.Sum(x => x.Quantity) ?? 0;
            }
        }
    }
}
