using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevExpress.Utils.About;
using DevExpress.XtraReports.UI;
using Newtonsoft.Json;
using NSRetailLiteApp.Helpers;
using NSRetailLiteApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.ViewModels.Billing
{
    public partial class DayClosureViewModel : BaseViewModel
    {
        private readonly int branchCounterId;
        private readonly int daySequenceId;

        public IAsyncRelayCommand DayCloseCommand { get; }

        public DayClosureViewModel(ObservableCollection<Denomination> denominations
            , ObservableCollection<MOP> mOPs, ObservableCollection<Refund> refunds
            , int branchCounterId, int daySequenceId)
        {
            Denominations = denominations;
            MOPs = mOPs;
            this.branchCounterId = branchCounterId;
            this.daySequenceId = daySequenceId;
            RefundAmount = refunds.FirstOrDefault()?.RefundAmount ?? 0;

            Denominations.ToList().ForEach(x => x.PropertyChanged += DenominationsPropertyChanged);

            UpdateTotals();

            DayCloseCommand = new AsyncRelayCommand(DayClose);
        }

        private void DenominationsPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "ClosureValue") return;

            UpdateTotals();
        }

        public ObservableCollection<Denomination> Denominations { get; }
        public ObservableCollection<MOP> MOPs { get; }
        public decimal RefundAmount { get; }

        [ObservableProperty]
        public decimal _totalAmount;

        private void UpdateTotals()
        {
            TotalAmount = Denominations.Sum(x => x.ClosureValue) + MOPs.Sum(x => x.MOPValue) - RefundAmount;
        }

        private async Task DayClose()
        {
            SaveDayClosure saveDayClosure = new()
            {
                DaySequenceID = daySequenceId,
                RefundAmount = this.RefundAmount,
                MopValues = MOPs,
                BranchCounterId = branchCounterId,
                UserId = HomePageViewModel.User.UserId,
                Denominations = this.Denominations.Select(x =>
                    new MOP() { MOPId = x.DenominationId, MOPValue = x.ClosureValue }).ToObservableCollection()
            };

            if (!await DisplayAlert("Confirm", $"Are you sure you want to day close with total amount: {TotalAmount}?", "Yes", "No"))
                return;

            HolderClass holder = new();
            PostAsync("billing/savedayclosure", ref holder, new Dictionary<string, string?>()
            {
                { "jsonString", JsonConvert.SerializeObject(saveDayClosure) }
            });

            if (holder.Exception != null) return;

            await DisplayAlert("Completed", "Day closure is completed", "OK");

            DayClosure dayClosure = new();
            GetAsync("Billing/getdayclosureforreport", ref dayClosure,
                new Dictionary<string, string?>() {
                    { "dayClosureID", Convert.ToString(holder.GenericID) },
                    { "counterID", Convert.ToString(branchCounterId) }
                });

            if (dayClosure.Exception != null) return;
            XtraReport xtraReport = new DayClosureHelper(dayClosure).GetReport();
            xtraReport.Parameters["Address"].Value = dayClosure.Address;
            xtraReport.Parameters["Phone"].Value = dayClosure.PhoneNo;
            xtraReport.Parameters["BranchName"].Value = dayClosure.BranchName;
            xtraReport.Parameters["CounterName"].Value = dayClosure.CounterName;
            xtraReport.Parameters["UserName"].Value = dayClosure.UserName;

#if ANDROID
            await PrintHelper.PrintAsync(xtraReport);
#endif

            //if (!PrintHelper.PrintReport(xtraReport).Result)
            //{
            //    DisplayErrorMessage("Print operation failed");
            //}
            await this.Pop();
        }
    }
}
