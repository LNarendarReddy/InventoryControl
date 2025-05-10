using NSRetailPOS.Gateway.PineLabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NSRetailPOS.Gateway
{
    public abstract class PaymentGatewayBase
    {
        public Action<string> StatusUpdate;
        public Action<Exception> Error;

        public bool IsInProgress { get; protected set; }

        public abstract string GatewayTpye { get; }

        protected abstract Task<bool> CancelRequest(ICancelRequest cancelRequest);

        protected abstract Task<IStatusResponse> CheckRequestStatus(IPaymentRequest paymentRequest, IStatusRequest statusRequest, CancellationToken token, int forceCheckCount = 0);

        protected abstract Task<IPaymentResponse> SendRequest(IPaymentRequest paymentRequest, CancellationToken token);

        protected abstract IPaymentRequest GetPaymentRequest(params object[] parameters);

        public abstract Task<CompletedTransactionData> ReceivePayment(int mopID, CancellationToken token, params object[] parameters);

        public static PaymentGatewayBase Create(string gatewayType, string baseSettings, string additionalSettings)
        {
            if (string.IsNullOrEmpty(baseSettings) || string.IsNullOrEmpty(additionalSettings)) return null;


            PaymentGatewayBase gateway = null;
            switch (gatewayType)
            {
                case "PineLabs":
                    gateway = new PineLabsGateway(baseSettings, additionalSettings);
                    break;
            }
            return gateway;
        }
    }

    public interface IPaymentRequest
    {
    }

    public interface IStatusRequest
    {

    }

    public interface ICancelRequest
    {

    }

    public interface IPaymentResponse
    {

    }

    public interface IStatusResponse
    {

    }

    public class CompletedTransactionData
    {
        public IPaymentRequest PaymentRequest { get; set; }

        public IStatusResponse StatusResponse { get; set; }

        public decimal Amount {  get; set; }

        public int MopID { get; set; }
    }
}
