﻿using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Orders
{
	public class SubscriptionItemMap : ClassMap<SubscriptionItem>
	{
		public SubscriptionItemMap()
		{
			Table("SynologenOrderSubscriptionItem");
			Id(x => x.Id);
			Map(x => x.Description).Length(512).Nullable();
			Map(x => x.Notes).Length(4000).Nullable();
			Map(x => x.WithdrawalsLimit).Nullable();
			Map(x => x.PerformedWithdrawals).Not.Nullable();
			References(x => x.Subscription).Column("SubscriptionId");
			Map(x => x.TaxFreeAmount);
			Map(x => x.TaxedAmount);
		}
	}
}