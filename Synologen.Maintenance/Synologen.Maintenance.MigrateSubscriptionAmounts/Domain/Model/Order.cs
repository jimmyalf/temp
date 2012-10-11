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
		public decimal? OrderTotalWithdrawalAmount { get; set; }
		public decimal? TaxedWithdrawalAmount { get; set; }
		public decimal? TaxFreeWithdrawalAmount { get; set; }
		public DateTime Created { get; set; }

        public static Order Parse(IDataRecord record)
        {
            return Data.CreateParser<Order>(record)
                .Parse(x => x.Id)
				.Parse(x => x.SubscripitonId, "PaymentOptionSubscripitonId")
				.Parse(x => x.SubscriptionItemId)
				.Parse(x => x.OrderTotalWithdrawalAmount)
				.Parse(x => x.TaxedWithdrawalAmount)
				.Parse(x => x.TaxFreeWithdrawalAmount)
				.Parse(x => x.Created)
                .GetValue();
        }

		public override string ToString()
		{
			return "{Id = " + Id + ", Subscription = " + SubscripitonId + "}";
		}
	}
}