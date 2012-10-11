using System.Data;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model
{
    public class Transaction
    {
        public int Id { get; set; }
        public int SubscriptionId { get; set; }
        public int? PendingPaymentId { get; set; }
        public int Reason { get; set; }
        public decimal OldAmount { get; set; }
		public SubscriptionAmount NewAmount { get; set; }

        public static Transaction Parse(IDataRecord record)
        {
        	return Data.Parse<Transaction>(record, parser =>
        	{
        		parser.Parse(x => x.Id);
        		parser.Parse(x => x.SubscriptionId);
        		parser.Parse(x => x.PendingPaymentId);
        		parser.Parse(x => x.Reason);
        		parser.Parse(x => x.OldAmount, "Amount");
        		parser.ParseComponent(x => x.NewAmount, map =>
        		{
        			map.Parse(x => x.Taxed, "TaxedAmount");
        			map.Parse(x => x.TaxFree, "TaxFreeAmount");
        		});
        	});
        }
    }
}
