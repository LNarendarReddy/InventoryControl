using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public partial class PickListDispatchViewModel : BaseViewModel
    {
        private readonly LoggedInUser loggedInUser;

        [ObservableProperty]
        public ObservableCollection<PickListDispatchDetailModel> _filteredPickListDispatchDetails;

        public PickListDispatchViewModel(LoggedInUser loggedInUser)
        {
            this.loggedInUser = loggedInUser;
            SubmitCommand = new AsyncRelayCommand(Submit);
            DiscardCommand = new AsyncRelayCommand(Discard);
            AddDispatchDetailCommand = new AsyncRelayCommand(AddDispatchDetail);
            DeleteDispatchDetailCommand = new AsyncRelayCommand<PickListDispatchDetailModel>(DeleteDispatchDetail);
        }

        [ObservableProperty]
        private PickListDispatchModel _pickListDispatchModelObj;

        [ObservableProperty]
        private string _headerText;


        [ObservableProperty]
        private string _lastKnownTrayNumber;

        public IAsyncRelayCommand SubmitCommand { get; }

        public IAsyncRelayCommand DiscardCommand { get; }

        public IAsyncRelayCommand AddDispatchDetailCommand { get; }

        public IAsyncRelayCommand<PickListDispatchDetailModel> DeleteDispatchDetailCommand { get; }

        private async Task Submit()
        {
            if (!await DisplayAlert("Confirm", "Are you sure you want to submit FT dispatch?", "Yes", "No")) return;

            HolderClass holderClass = new();
            holderClass = await PostAsync("picklist/updatedispatch", holderClass, new Dictionary<string, string?>
            {
                { "picklistDispatchID", PickListDispatchModelObj.PickListDispatchId.ToString() }
            });

            if (holderClass == null || holderClass.Exception != null
                || holderClass.GenericID == 0) return;

            DisplayAlert("Success", "Dispatch submitted", "OK");
            await Pop();
        }

        private async Task Discard()
        {
            if (!await DisplayAlert("Confirm", "Are you sure you want to discard FT dispatch?", "Yes", "No")) return;

            HolderClass holderClass = new();
            holderClass = await PostAsync("picklist/deletedispatch", holderClass, new Dictionary<string, string?>
            {
                { "picklistDispatchID", PickListDispatchModelObj.PickListDispatchId.ToString() }
            });

            if (holderClass == null || holderClass.Exception != null 
                || holderClass.GenericID != PickListDispatchModelObj.PickListDispatchId) return;

            await DisplayAlert("Success", "Dispatch discarded", "OK");
            await Pop();
        }

        private async Task AddDispatchDetail()
        {
            await RedirectToPage(new HolderClass(), new PickListDispatchDetailPage(new PickListDispatchDetailViewModel(this)));
        }

        public async Task Reload()
        {
            HolderClass holderClass = new();

            holderClass = await GetAsync("picklist/getdispatch", holderClass
                   , new Dictionary<string, string?>()
                    {
                        { "userID", loggedInUser.UserId.ToString() }
                    }, displayAlert: true);

            if (holderClass == null || holderClass.Exception != null) return;

            PickListDispatchModelObj = holderClass.PickListDispatch;
            HeaderText = $"{PickListDispatchModelObj.BranchName} ( {PickListDispatchModelObj.LocationDivisionName} )";

            PerformSearch();
        }

        [RelayCommand]
        public void PerformSearch(string search = null)
        {
            if (string.IsNullOrEmpty(search))
            {
                FilteredPickListDispatchDetails = PickListDispatchModelObj.PicklistDispatchDetailList;
                return;
            }

            search = search.ToLower();
            FilteredPickListDispatchDetails
                = new ObservableCollection<PickListDispatchDetailModel>(
                    PickListDispatchModelObj.PicklistDispatchDetailList
                    .Where(x => x.ItemName.Contains(search, StringComparison.CurrentCultureIgnoreCase)
                    || x.ItemCode.Contains(search, StringComparison.CurrentCultureIgnoreCase)));
        }

        private async Task DeleteDispatchDetail(PickListDispatchDetailModel? selected)
        {
            if (selected == null) return;

            string confirmMessage = "Are you sure you want to delete stock counting the following item?";
            confirmMessage += $"\n\n\t {selected.ItemName}";
            confirmMessage += $"\n\n\t\t EAN : {selected.ItemCode}";
            confirmMessage += $"\n\t\t MRP : {selected.MRP}";
            confirmMessage += $"\n\t\t Tray # : {selected.TrayNumber}";

            if (!await DisplayAlert("Confirm", confirmMessage, "Yes", "No")) return;

            selected = await PostAsync("picklist/deletedispatchdetail", selected
                    , new Dictionary<string, string?>()
                    {
                        { "pickListDispatchDetailID", selected.PickListDispatchDetailId.ToString() },
                        { "picklistDispatchID", selected.PickListDispatchId.ToString() }
                    }, displayAlert: true, showResponse: true);

            if (selected.Exception != null) return;

            DisplayAlert("Success", "dispatch item deleted successfully", "ok");
            await Reload();
        }
    }
}
