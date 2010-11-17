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
			Map(x => x.FirstName);
			Map(x => x.LastName);
			Map(x => x.PersonalIdNumber)
				.Length(12);
			Map(x => x.Notes);
			References(x => x.Shop)
					.Cascade.None()
					.Column("ShopId")
					.Not.Nullable();
			Component(x => x.Address, m =>
			{
				m.Map(x => x.AddressLineOne);
				m.Map(x => x.AddressLineTwo);
				m.Map(x => x.City);
				m.Map(x => x.PostalCode);
				m.References(x => x.Country)
							.Cascade.None()
							.Column("CountryId")
							.Not.Nullable();
			});
			Component(x => x.Contact, m =>
			{
				m.Map(x => x.Email);
				m.Map(x => x.MobilePhone);
				m.Map(x => x.Phone);
			});
			HasMany(x => x.Subscriptions)
			  .Inverse()
			  .Cascade.All();
		}
	}
}