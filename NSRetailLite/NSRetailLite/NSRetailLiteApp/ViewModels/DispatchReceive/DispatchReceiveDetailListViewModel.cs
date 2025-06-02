using CommunityToolkit.Mvvm.ComponentModel;
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
        public IAsyncRelayCommand<DispatchReceiveDetail?> MarkVerifyCommand { get; }
        public IAsyncRelayCommand SubmitCommand { get; }
        public int ItemCount { get; private set; }
        public int DispatchPieceCount { get; private set; }

        [ObservableProperty]
        private int _receivedPieceCount;

        public DispatchReceiveDetailListViewModel(
            Models.DispatchReceive dispatchReceive
            , LoggedInUser loggedInUser) 
        {
            DispatchReceive = dispatchReceive;
            this.loggedInUser = loggedInUser;
            EditReceivedQuantityCommand = new AsyncRelayCommand<DispatchReceiveDetail?>(EditReceivedQuantity);
            MarkVerifyCommand = new AsyncRelayCommand<DispatchReceiveDetail?>(MarkVerify);
            SubmitCommand = new AsyncRelayCommand(Submit);

            BuildModel();
        }

        public Models.DispatchReceive DispatchReceive { get; }

        public string Title { get; private set; }

        private async Task EditReceivedQuantity(DispatchReceiveDetail? selected)
        {
            if (selected == null) return;

            string newQuantityString = await Application.Current?.MainPage?.DisplayPromptAsync(
                    $"{selected.ItemName}", "Enter received quantity:", "OK", keyboard: Keyboard.Numeric);

            if (string.IsNullOrEmpty(newQuantityString)) return;

            if(!int.TryParse(newQuantityString, out int newQuantityValue) || newQuantityValue <= 0)
            {
                DisplayAlert("Error", $"Invalid value {newQuantityString}", "Ok");
                return;
            }

            HolderClass holder = new HolderClass();
            await PostAsync("StockDispatch_In/updatedispatchquantity", holder, new Dictionary<string, string?>
            {
                { "StockDispatchDetailID", selected.StockDispatchDetailId.ToString() },
                { "ReceivedQuantity", newQuantityString },
                { "WeightInKgs", 0.ToString() }
            });

            if (holder.Exception != null) return;

            selected.ReceivedQuantity = Convert.ToInt32(newQuantityString);
            BuildModel();
        }

        private async Task Submit()
        {
            if (!await DisplayAlert("Confirm", "Are you sure you want to submit tray items? This operation cannot be undone", "Yes", "No")) return;

            HolderClass holder = new HolderClass();
            await PostAsync("StockDispatch_In/updatetrayinfo", holder, new Dictionary<string, string?>
            {
                { "StockDispatchID", DispatchReceive.StockDispatchId.ToString() },
                { "TrayNumber", DispatchReceive.TrayNumber },
                { "UserID", loggedInUser.UserId.ToString() }
            });

            if (holder.Exception != null) return;

            await Pop();
        }

        private async Task MarkVerify(DispatchReceiveDetail? selected)
        {
            if (selected == null) return;

            HolderClass holder = new HolderClass();
            await PostAsync("StockDispatch_In/verifystockdispatchdetail", holder, new Dictionary<string, string?>
            {
                { "StockDispatchDetailID", selected.StockDispatchDetailId.ToString() },
                { "IsVerified", (!selected.IsVerified).ToString() }
            });

            if (holder.Exception != null) return;

            selected.IsVerified = !selected.IsVerified;
        }

        private void BuildModel()
        {
            if(DispatchReceive == null) return;

            Title = "Tray #: " + DispatchReceive.TrayNumber;
            ItemCount = DispatchReceive.DispatchReceiveDetailList?.Count ?? 0;
            DispatchPieceCount = DispatchReceive?.DispatchReceiveDetailList?.Sum(x => x.DispatchQuantity) ?? 0;
            ReceivedPieceCount = DispatchReceive?.DispatchReceiveDetailList?.Sum(x => x.ReceivedQuantity) ?? 0;
        }
    }
}
