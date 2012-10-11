using System;
using System.Data;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model
{
	public class Order
	{
		public int Id { get; set; }
		public int SubscripitonId { get; set; }
		public int SubscriptionItemId { get; set; }
		public decimal? OldAmount { get; set; }
		public SubscriptionAmount NewAmount { get; set; }
		public DateTime Created { get; set; }

        public static Order Parse(IDataRecord record)
        {
            return Data.Parse<Order>(record, parser =>
            {
            	parser.Parse(x => x.Id);
            	parser.Parse(x => x.SubscripitonId, "PaymentOptionSubscripitonId");
            	parser.Parse(x => x.SubscriptionItemId);
            	parser.Parse(x => x.OldAmount,"OrderTotalWithdrawalAmount");
            	parser.ParseComponent(x => x.NewAmount, map =>
            	{ 
					map.Parse(x => x.Taxed, "TaxedWithdrawalAmount");
					map.Parse(x => x.TaxFree, "TaxFreeWithdrawalAmount");
				});
            	parser.Parse(x => x.Created);
            });
        }

		public override string ToString()
		{
			return "{Id = " + Id + ", Subscription = " + SubscripitonId + "}";
		}
	}
}