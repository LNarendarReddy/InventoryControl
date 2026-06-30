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
    public partial class PickListDispatchTrayViewModel : BaseViewModel
    {
        private readonly LoggedInUser loggedInUser;

        public Branch Branch { get; }

        public IAsyncRelayCommand<PickListTrayModel> ViewTrayCommand { get; }

        public IAsyncRelayCommand<PickListTrayModel> VerifyTrayCommand { get; }

        public IAsyncRelayCommand SubmitCommand { get; }

        public PickListDispatchTrayViewModel(Branch branch, ObservableCollection<PickListTrayModel> pickListTrayModels, LoggedInUser loggedInUser)
        {
            this.Branch = branch;
            PickListTrayModels = pickListTrayModels;
            this.loggedInUser = loggedInUser;
            ViewTrayCommand = new AsyncRelayCommand<PickListTrayModel?>(ViewTray);
            VerifyTrayCommand = new AsyncRelayCommand<PickListTrayModel?>(VerifyTray);
            SubmitCommand = new AsyncRelayCommand(Submit);
        }

        public ObservableCollection<PickListTrayModel> PickListTrayModels { get; }

        private async Task ViewTray(PickListTrayModel? selected)
        {
            if (selected == null) return;

            await RedirectToPage(selected, new PickListDispatchTrayDetailsPage(new PickListDipatchTrayDetailsViewModel(selected)));
        }

        private async Task VerifyTray(PickListTrayModel? selected)
        {
            if (selected == null) return;

            bool trayVerified = !selected.IsTrayVerified;
            await PostAsync("picklist/verifytray", selected, new Dictionary<string, string?>()
            {
                { "pickListTrayID", selected.PickListTrayID.ToString() },
                { "IsTrayVerified", trayVerified.ToString() },
                { "userID", loggedInUser.UserId.ToString() }
            });

            if (selected.Exception != null)
            {
                selected.Exception = null;
                return;
            }

            selected.IsTrayVerified = trayVerified;
        }

        private async Task Submit()
        {
            if (!await DisplayAlert("Confirm", "Are you sure you want to dispatch? this operation cannot be reversed", "Yes", "No"))
                return;

            List<PickListTrayModel> pickListTrays = PickListTrayModels.ToList();
            string stats = $"Review the values before submitting, this operation cannot be reversed {Environment.NewLine}";
            stats += $"{Environment.NewLine}    * Total trays seleceted: {pickListTrays.Count(x => x.IsTrayVerified)}";
            stats += $"{Environment.NewLine}    * Total tray items : {pickListTrays.Sum(x => x.PickListItemList.Count)}";
            stats += $"{Environment.NewLine}    * Total tray pieces: {pickListTrays.Sum(x => x.PickListItemList.Sum(y => y.Quantity))}";
            stats += Environment.NewLine;
            stats += $"{Environment.NewLine}    * Total skipped trays : {pickListTrays.Count(x => !x.IsTrayVerified)}";

            if (!await DisplayAlert("Confirm", stats, "Yes", "No"))
                return;

            HolderClass holderClass = new();
            holderClass = await PostAsync("picklist/dispatchbranch", holderClass, new Dictionary<string, string?>
            {
                { "branchID", Branch.BranchID.ToString() },
                { "pickListTrayIDs", string.Join(",", pickListTrays.Select(x => x.PickListTrayID)) },
                { "userID", loggedInUser.UserId.ToString() }
            });

            if (holderClass?.Exception == null) await Pop();
        }
    }
}
