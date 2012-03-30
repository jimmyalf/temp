using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.ContractSales
{
	public class NewSubscriptionMap : ClassMap<NewSubscription>
	{
		public NewSubscriptionMap()
		{
			Table("SynologenOrderSubscription");
			Id(x => x.Id);
			References(x => x.Shop)
				.Fetch.Join()
				.Cascade.None()
				.Column("ShopId")
				.Not.Nullable();
		}
	}
}