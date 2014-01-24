using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Orders
{
    public class OrderCustomerMap : ClassMap<OrderCustomer>
    {
        public OrderCustomerMap()
        {
            Table("SynologenOrderCustomer");
            Id(x => x.Id);
            Map(x => x.AddressLineOne);
            Map(x => x.AddressLineTwo);
            Map(x => x.City);
            Map(x => x.Email);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.MobilePhone);
            Map(x => x.Notes);
            Map(x => x.PersonalIdNumber);
            Map(x => x.Phone);
            Map(x => x.PostalCode);
			References(x => x.Shop).Column("ShopId").Not.Nullable();
        }
    }
}
