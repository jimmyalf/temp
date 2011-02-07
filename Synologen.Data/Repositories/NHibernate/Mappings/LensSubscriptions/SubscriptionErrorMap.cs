using System;
using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.LensSubscriptions
{
	public class SubscriptionErrorMap : ClassMap<SubscriptionError> 
	{
		public SubscriptionErrorMap()
		{
			Table("SynologenLensSubscriptionError");
			Id(x => x.Id);
			Map(x => x.Type)
				.CustomType(typeof(SubscriptionErrorType));
			Map(x => x.Code)
				.Nullable()
				.CustomType(typeof (ConsentInformationCode));
			Map(x => x.CreatedDate);
			Map(x => x.HandledDate)
				.Nullable();
			Map(x => x.IsHandled);
			References(x => x.Subscription)
				.Cascade.None()
				.Column("SubscriptionId")
				.Not.Nullable();
		}

	}
}
