using System.Data;
using Spinit.Data.FluentParameters;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model
{
    public class OrderTransaction
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
            return Data.CreateParser<OrderTransaction>(record)
                .Parse(x => x.Id)
                .Parse(x => x.SubscriptionId)
                .Parse(x => x.PendingPaymentId)
                .Parse(x => x.Reason)
                .Parse(x => x.Amount)
                .GetValue();
        }
    }
}
