using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NSRetailLiteApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.PickList
{
    public partial class PickListTrayViewModel : BaseViewModel
    {
        private readonly int pickListId;
        private readonly LoggedInUser user;

        [ObservableProperty]
        private string _trayNumber;

        [ObservableProperty]
        private int _pickedQuantity;

        public event EventHandler SaveComplete;

        public PickListTrayViewModel(int pickListId, PickListItemModel pickListItemModel, LoggedInUser user) 
        {
            this.pickListId = pickListId;
            PickListItemModel = pickListItemModel;
            this.user = user;
            PickedQuantity = pickListItemModel.AvailableQuantity;
            SaveCommand = new AsyncRelayCommand(Save);
        }

        public PickListItemModel PickListItemModel { get; }

        public IAsyncRelayCommand SaveCommand { get; }

        private async Task Save()
        {
            List<string> errors = [];

            if (string.IsNullOrEmpty(TrayNumber))
                errors.Add("Tray # cannot be empty");

            if (PickedQuantity == 0)
                errors.Add("Tray Quantity cannot be empty");

            if (PickedQuantity > PickListItemModel.AvailableQuantity)
                errors.Add($"Tray Quantity cannot be more than available quantity ({PickListItemModel.AvailableQuantity})");

            if (errors.Any())
            {
                await DisplayAlert("Error"
                    , "Fix the following errors: \n\n"
                    + string.Join("", errors.Select(x => x = $"\r * {x}.\n"))
                    , "OK");
                return;
            }

            string message = $"Are you sure you want to save?";

            if (!await DisplayAlert("Confirm", message, "Yes", "No")) return;

            var pickListItemModel = PickListItemModel;

            Dictionary<string, string?> parameters = new()
            {
                { "pickListID", pickListId.ToString() },
                { "pickListItemID", pickListItemModel.PickListItemID.ToString() },
                { "quantity", PickedQuantity.ToString() },
                { "trayNumber", TrayNumber },
                { "userID", user.UserId.ToString() },
            };

            pickListItemModel = await PostAsync("picklist/addtray", pickListItemModel, parameters, displayAlert: true, showResponse: false);

            if (pickListItemModel.Exception != null)
            {
                pickListItemModel.Exception = null; // clear exception
                return;
            }

            PickListItemModel.AvailableQuantity -= PickedQuantity;
            SaveComplete?.Invoke(this, EventArgs.Empty);
        }
    }
}
