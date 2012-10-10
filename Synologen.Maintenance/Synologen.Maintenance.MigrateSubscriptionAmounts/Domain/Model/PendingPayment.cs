using System.Data;
using Spinit.Data.FluentParameters;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model
{
    class PendingPayment
    {
        public int Id { get; set; }
		public decimal TaxedAmount { get; set; }
		public decimal UntaxedAmount { get; set; }
		
        public static PendingPayment Parse(IDataRecord record)
        {
            return new FluentDataParser<PendingPayment>(record)
                .Parse(x => x.Id)
                .Parse(x => x.TaxedAmount, "cTaxedAmount")
                .Parse(x => x.UntaxedAmount, "cTaxFreeAmount")
                .GetValue();
        }
    }
}
