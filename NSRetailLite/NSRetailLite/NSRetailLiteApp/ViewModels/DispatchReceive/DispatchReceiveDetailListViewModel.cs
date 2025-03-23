using CommunityToolkit.Mvvm.Input;
using NSRetailLiteApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.DispatchReceive
{
    public partial class DispatchReceiveDetailListViewModel : BaseViewModel
    {
        private readonly LoggedInUser loggedInUser;

        public IAsyncRelayCommand<DispatchReceiveDetail?> EditReceivedQuantityCommand { get; }
        public IAsyncRelayCommand SubmitCommand { get; }


        public DispatchReceiveDetailListViewModel(
            ObservableCollection<DispatchReceiveDetail> dispatchReceiveDetailList
            , string trayNumber
            , LoggedInUser loggedInUser) 
        {
            DispatchReceiveDetailList = dispatchReceiveDetailList;
            Title = "Tray #: " + trayNumber;
            TrayNumber = trayNumber;
            this.loggedInUser = loggedInUser;
            EditReceivedQuantityCommand = new AsyncRelayCommand<DispatchReceiveDetail?>(EditReceivedQuantity);
            SubmitCommand = new AsyncRelayCommand(Submit);
        }

        public ObservableCollection<DispatchReceiveDetail?> DispatchReceiveDetailList { get; }
        public string TrayNumber { get; }
        public string Title { get; }

        private async Task EditReceivedQuantity(DispatchReceiveDetail? selected)
        {
            if (selected == null) return;

            string newQuantityString = await Application.Current?.MainPage?.DisplayPromptAsync(
                    $"{selected.ItemName}", "Enter received quantity:", "OK", keyboard: Keyboard.Numeric);

            if (string.IsNullOrEmpty(newQuantityString)) return;

            HolderClass holder = new HolderClass();
            await PostAsync("StockDispatch_In/updatedispatchquantity", holder, new Dictionary<string, string?>
            {
                { "StockDispatchDetailID", selected.StockDispatchDetailId.ToString() },
                { "ReceivedQuantity", newQuantityString },
                { "WeightInKgs", 0.ToString() }
            });

            if (holder.Exception != null) return;

            selected.ReceivedQuantity = Convert.ToInt32(newQuantityString);
        }

        private async Task Submit()
        {
            if (!await DisplayAlert("Confirm", "Are you sure you want to submit tray items? This operation cannot be undone", "Yes", "No")) return;

            HolderClass holder = new HolderClass();
            await PostAsync("StockDispatch_In/updatetrayinfo", holder, new Dictionary<string, string?>
            {
                { "StockDispatchID", 0.ToString() },
                { "TrayNumber", TrayNumber },
                { "UserID", loggedInUser.UserId.ToString() }
            });

            if (holder.Exception != null) return;

            await Pop();
        }
    }
}
