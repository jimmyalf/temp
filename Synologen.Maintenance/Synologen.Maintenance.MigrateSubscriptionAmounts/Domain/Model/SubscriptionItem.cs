using System.Data;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model
{
    public class SubscriptionItem
    {
        public int Id { get; set; }
        public int SubscriptionId { get; set; }
		public SubscriptionAmount Amount { get; set; }

        public static SubscriptionItem Parse(IDataRecord record)
        {
            return Data.Parse<SubscriptionItem>(record, parser =>
            {
            	parser.Parse(x => x.Id);
            	parser.ParseComponent(x => x.Amount, map =>
            	{
            		map.Parse(x => x.Taxed, "ProductPrice");
            		map.Parse(x => x.TaxFree, "FeePrice");
            	});
            	parser.Parse(x => x.SubscriptionId);
            });
        }
    }
}
