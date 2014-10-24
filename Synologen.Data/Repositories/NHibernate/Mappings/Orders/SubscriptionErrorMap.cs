using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Orders
{
	public class SubscriptionErrorMap : ClassMap<SubscriptionError> 
	{
		public SubscriptionErrorMap()
		{
			Table("SynologenOrderSubscriptionError");
			Id(x => x.Id);
			Map(x => x.Type).CustomType<SubscriptionErrorType>();
			Map(x => x.Code).Nullable().CustomType<ConsentInformationCode>();
			Map(x => x.CreatedDate);
			Map(x => x.HandledDate).Nullable();
			References(x => x.Subscription)
				.Cascade.None()
				.Column("SubscriptionId")
				.Not.Nullable();
			Map(x => x.BGConsentId).Nullable();
			Map(x => x.BGErrorId).Nullable();
			Map(x => x.BGPaymentId).Nullable();
		}

	}
}
