using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.LensSubscriptions
{
	public class SubscriptionMap : ClassMap<Subscription>
	{
		public SubscriptionMap()
		{
			Table("SynologenLensSubscription");
			Id(x => x.Id);
			Map(x => x.CreatedDate);
			Map(x => x.ActivatedDate)
				.Nullable();
			Component(x => x.PaymentInfo, m =>
			{
				m.Map(x => x.AccountNumber);
				m.Map(x => x.ClearingNumber);
				m.Map(x => x.MonthlyAmount);
			});
			References(x => x.Customer)
				.Cascade.None()
				.Column("CustomerId")
				.Not.Nullable();
			Map(x => x.Status)
				.CustomType(typeof (SubscriptionStatus));
		}
	}
}