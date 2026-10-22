using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetailPOS.Gateway
{
    public sealed class CounterPaymentMode
    {
        public int MopID { get; set; }
        public string Name { get; set; }
        public bool RequiresGateway { get; set; }
        public int GatewayID { get; set; }
        public string GatewayType { get; set; }
        public string BaseSettings { get; set; }
        public string AdditionalSettings { get; set; }
        public PaymentGatewayBase Gateway { get; set; }
    }

    public sealed class CounterPaymentConfiguration
    {
        readonly Dictionary<int, CounterPaymentMode> modes = new Dictionary<int, CounterPaymentMode>();
        public IEnumerable<CounterPaymentMode> Modes => modes.Values;
        public CounterPaymentMode Find(int mopID) => modes.TryGetValue(mopID, out var mode) ? mode : null;

        public void Load(DataTable table)
        {
            modes.Clear();
            foreach (DataRow row in table.Rows)
            {
                var mode = new CounterPaymentMode
                {
                    MopID = Convert.ToInt32(row["MOPID"]), Name = Convert.ToString(row["MOPNAME"]),
                    RequiresGateway = Convert.ToBoolean(row["ISGATEWAYCONFIGURABLE"]),
                    GatewayID = row.IsNull("PAYMENTGATEWAYINFOID") ? 0 : Convert.ToInt32(row["PAYMENTGATEWAYINFOID"]),
                    GatewayType = Convert.ToString(row["PAYMENTGATEWAYINFOTYPE"]),
                    BaseSettings = Convert.ToString(row["PAYMENTGATEWAYCONFIGDATA"]),
                    AdditionalSettings = Convert.ToString(row["PAYMENTGATEWAYADDITIONALCONFIG"])
                };
                if (mode.RequiresGateway && mode.GatewayID > 0)
                {
                    try { mode.Gateway = PaymentGatewayBase.Create(mode.GatewayType, mode.GatewayID, mode.BaseSettings, mode.AdditionalSettings); }
                    catch { mode.Gateway = null; }
                }
                modes.Add(mode.MopID, mode);
            }
        }
    }

    public sealed class PaymentAttempt
    {
        public Guid AttemptID { get; set; }
        public int BillID { get; set; }
        public int MopID { get; set; }
        public decimal Amount { get; set; }
        public int GatewayID { get; set; }
        public string GatewayType { get; set; }
        public string BaseSettings { get; set; }
        public string AdditionalSettings { get; set; }
        public string RequestReference { get; set; }
        public string ProviderReference { get; set; }
        public bool RefundRequested { get; set; }
        public PaymentGatewayBase CreateGateway() => PaymentGatewayBase.Create(GatewayType, GatewayID, BaseSettings, AdditionalSettings);
    }
}
