using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.ContractSales
{
	public class OldCustomerMap : ClassMap<OldCustomer>
	{
		public OldCustomerMap()
		{
			Table("SynologenLensSubscriptionCustomer");
			Id(x => x.Id);
			Map(x => x.FirstName);
			Map(x => x.LastName);
			References(x => x.Shop)
					.Fetch.Join()
					.Cascade.None()
					.Column("ShopId")
					.Not.Nullable();
		}
	}

	public class NewCustomerMap : ClassMap<NewCustomer>
	{
		public NewCustomerMap()
		{
			Table("SynologenOrderCustomer");
			Id(x => x.Id);
			Map(x => x.FirstName);
			Map(x => x.LastName);
		}
	}
}