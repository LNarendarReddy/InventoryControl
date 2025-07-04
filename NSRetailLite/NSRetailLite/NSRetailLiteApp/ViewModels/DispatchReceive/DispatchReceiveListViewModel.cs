﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NSRetailLiteApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NSRetailLiteApp.ViewModels.DispatchReceive
{
    public partial class DispatchReceiveListViewModel : BaseViewModel
    {
        private readonly LoggedInUser loggedInUser;

        public DispatchReceiveListViewModel(ObservableCollection<Dispatch> dispatchList, LoggedInUser loggedInUser) 
        {
            DispatchList = dispatchList;
            this.loggedInUser = loggedInUser;
            SubmitCommand = new AsyncRelayCommand<Dispatch>(Submit);
            BuildModel();
        }

        [ObservableProperty]
        private ObservableCollection<Dispatch> _dispatchList;

        public AsyncRelayCommand<Dispatch> SubmitCommand { get; private set; }

        [ObservableProperty]
        public ObservableCollection<CategoryGroup> _categoryGroupList;

        private async Task Submit(Dispatch selected)
        {
            if (selected == null) return;

            if (selected.PendingTrays > 0)
            {
                await DisplayAlert("Info", $"Cannot submit dispatch as {selected.PendingTrays} tray(s) are still pending", "OK");
                return;
            }

            if (!await DisplayAlert("Confirm", $"Are you sure you want to submit dispatch - {selected.DispatchNumber}?"
                , "Yes", "No")) return;

            // add submit api /api/StockDispatch_In/finishstockin

            selected = await PostAsync("StockDispatch_In/finishstockin", selected, new Dictionary<string, string?>
            {
                { "StockDispatchID", selected.StockDispatchId.ToString() },
                { "CounterID", 0.ToString() },
                { "UserID", loggedInUser.UserId.ToString() }
            }, displayAlert: true);

            if (selected.Exception != null) return;

            HolderClass holder = new HolderClass();
            holder = await GetAsync("StockDispatch_In/getdispatchlist", holder, new Dictionary<string, string?>()
                {
                    { "BranchID", loggedInUser.BranchId.ToString() }
                    , { "GetAllDispatches", false.ToString() }
                });

            if (holder == null || holder.Exception != null) return;
            DisplayAlert("Success", "Dispatch receive completed", "Ok");
            DispatchList = holder.Holder.DispatchList;
            BuildModel();
        }

        private void BuildModel()
        {
            if (DispatchList == null) return;

            CategoryGroupList = new ObservableCollection<CategoryGroup>();
            DispatchList.GroupBy(x => x.CategoryName)
                .Select(x => new CategoryGroup(x.Key, x.ToList()))
                .OrderBy(x => x.Name)
                .ToList()
                .ForEach(CategoryGroupList.Add);
        }
    }

    public class CategoryGroup : ObservableCollection<Dispatch>
    {
        public string Name { get; private set; }

        public CategoryGroup(string name, List<Dispatch> dispatches) : base(dispatches)
        {
            Name = name;
        }
    }
}
