using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NSRetailLiteApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.PickList
{
    public partial class PickListDispatchDetailViewModel : BaseViewModel
    {
        public IAsyncRelayCommand SaveCommand { get; }
        public IAsyncRelayCommand LoadItemCommand { get; }
        public PickListDispatchViewModel PickListDispatchViewModel { get; }

        [ObservableProperty]
        private PickListDispatchDetailModel _pickListDispatchDetailModelObj;
        
        public delegate void SaveCompleted();
        
        public event SaveCompleted SaveComplete;

        public PickListDispatchDetailViewModel(PickListDispatchViewModel pickListDispatchViewModel)
        {
            SaveCommand = new AsyncRelayCommand(Save);
            LoadItemCommand = new AsyncRelayCommand(LoadItem);
            PickListDispatchViewModel = pickListDispatchViewModel;
            PickListDispatchDetailModelObj = new PickListDispatchDetailModel()
            {
                TrayNumber = PickListDispatchViewModel.LastKnownTrayNumber
            };
        }

        private async Task Save()
        {
            List<string> errors = [];

            if (PickListDispatchDetailModelObj.ItemCodeId <= 0)
                errors.Add("Item code not selected");

            if (string.IsNullOrEmpty(PickListDispatchDetailModelObj.TrayNumber))
                errors.Add("Tray # cannot be empty");

            if (PickListDispatchDetailModelObj.MRP <= 0)
                errors.Add("MRP cannot be empty");

            if (PickListDispatchDetailModelObj.Quantity <= 0)
                errors.Add("Quantity cannot be empty");
            
            if (PickListDispatchDetailModelObj.Quantity > 9999)
                errors.Add("Quantity cannot be more than 4 digits");

            if (errors.Any())
            {
                await DisplayAlert("Error"
                    , "Fix the following errors: \n\n"
                    + string.Join("", errors.Select(x => x = $"\r * {x}.\n"))
                    , "OK");
                return;
            }

            var pickListDispatchDetailModelObj = PickListDispatchDetailModelObj;
            pickListDispatchDetailModelObj = await PostAsync("PickList/savedispatchdetail", pickListDispatchDetailModelObj
                , new Dictionary<string, string?>()
                {
                    { "itemCodeID", pickListDispatchDetailModelObj.ItemCodeId.ToString() },
                    { "picklistDispatchID", PickListDispatchViewModel.PickListDispatchModelObj.PickListDispatchId.ToString() },
                    { "Quantity", pickListDispatchDetailModelObj.Quantity.ToString() },
                    { "mRP", pickListDispatchDetailModelObj.MRP.ToString() },
                    { "trayNumber", pickListDispatchDetailModelObj.TrayNumber.ToString() }
                }, displayAlert: true);

            if (pickListDispatchDetailModelObj.Exception == null)
            {
                ClearData();
                SaveComplete?.Invoke();
            }
            
            pickListDispatchDetailModelObj.Exception = null;
            PickListDispatchViewModel.LastKnownTrayNumber = PickListDispatchDetailModelObj.TrayNumber;
        }

        private async Task LoadItem()
        {
            if (string.IsNullOrEmpty(PickListDispatchDetailModelObj.ItemCode))
            {
                ClearData();
                return;
            }

            HolderClass holderClass = new();
            holderClass = await GetAsync("PickList/getdispatchitemdetail", holderClass, new Dictionary<string, string?>
            {
                { "itemCode", PickListDispatchDetailModelObj.ItemCode },
                { "pickListDispatchID", PickListDispatchViewModel.PickListDispatchModelObj.PickListDispatchId.ToString() }
            });

            if (holderClass == null || holderClass.Exception != null) return;

            ObservableCollection<PickListDispatchDetailModel>? availableItems = holderClass.Holder?.PickListDispatchItemList;

            if(availableItems == null || availableItems.Count == 0) return;

            PickListDispatchDetailModel? pickListDispatchDetailModel = null;

            if (availableItems.Count == 1)
            {
                pickListDispatchDetailModel = availableItems[0];
            }
            else
            {
                string selectedMRP =
                await DisplayActionSheet($"Select MRP:", [.. availableItems.Select(x => x.MRP.ToString())]);

                if (string.IsNullOrEmpty(selectedMRP) || selectedMRP.ToLower() == "cancel") return;

                pickListDispatchDetailModel = availableItems.First(x => x.MRP.ToString() == selectedMRP);
            }

            if (pickListDispatchDetailModel == null) return;

            PickListDispatchDetailModelObj.ItemId = pickListDispatchDetailModel.ItemId;
            PickListDispatchDetailModelObj.ItemCodeId = pickListDispatchDetailModel.ItemCodeId;
            PickListDispatchDetailModelObj.ItemName = pickListDispatchDetailModel.ItemName;
            PickListDispatchDetailModelObj.SKUCode = pickListDispatchDetailModel.SKUCode;
            PickListDispatchDetailModelObj.ItemCode = pickListDispatchDetailModel.ItemCode;
            PickListDispatchDetailModelObj.MRP = pickListDispatchDetailModel.MRP;
            PickListDispatchDetailModelObj.Quantity = pickListDispatchDetailModel.AvailableQty;
            PickListDispatchDetailModelObj.TotalPickListQty = PickListDispatchDetailModelObj.TotalPickListQty;
            PickListDispatchDetailModelObj.AllocatedQty = pickListDispatchDetailModel.AvailableQty;            
        }

        private void ClearData()
        {
            PickListDispatchDetailModelObj.SKUCode = string.Empty;
            PickListDispatchDetailModelObj.ItemName = string.Empty;
            PickListDispatchDetailModelObj.ItemCode = string.Empty;
            PickListDispatchDetailModelObj.MRP = 0;
            PickListDispatchDetailModelObj.ItemId = 0;
            PickListDispatchDetailModelObj.ItemCodeId = 0;
            PickListDispatchDetailModelObj.Quantity = 0;
        }
    }
}
