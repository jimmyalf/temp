using System.Data;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model
{
    class PendingPayment
    {
        public int Id { get; set; }
		public SubscriptionAmount Amount { get; set; }
		
        public static PendingPayment Parse(IDataRecord record)
        {
        	return Data.Parse<PendingPayment>(record, parser =>
        	{
        		parser.Parse(x => x.Id);
        		parser.ParseComponent(x => x.Amount, map =>
        		{
					map.Parse(x => x.Taxed, "TaxedAmount");
					map.Parse(x => x.TaxFree, "TaxFreeAmount");
        		});
        	});
        }
    }
}
