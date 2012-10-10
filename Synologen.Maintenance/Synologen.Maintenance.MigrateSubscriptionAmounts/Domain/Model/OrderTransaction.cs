using System.Data;
using Spinit.Data.FluentParameters;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model
{
    class OrderTransaction
    {
        public int Id { get; set; }
        public int SubscriptionId { get; set; }
        public int? PendingPaymentId { get; set; }
        public int Reason { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxedAmount { get; set; }
        public decimal UntaxedAmount { get; set; }

        public static OrderTransaction Parse(IDataRecord record)
        {
            return new FluentDataParser<OrderTransaction>(record)
                .Parse(x => x.Id)
                .Parse(x => x.SubscriptionId, "cSubscriptionId")
                .Parse(x => x.PendingPaymentId, "cPendingPaymentId")
                .Parse(x => x.Reason, "cReason")
                .Parse(x => x.Amount, "cAmount")
                .GetValue();
        }
    }
}
