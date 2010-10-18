using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.LensSubscriptions
{
	public class CustomerMap : ClassMap<Customer>
	{
		public CustomerMap()
		{
			Table("SynologenLensSubscriptionCustomer");
			Id(x => x.Id);
		}
	}
}