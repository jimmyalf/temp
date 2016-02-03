using NHibernate;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.Orders
{
    public class CustomerDetailsFromPersonalIdNumberCriteriaConverter : NHibernateActionCriteriaConverter<CustomerDetailsFromPersonalIdNumberCriteria, OrderCustomer>, IActionCriteria
    {
        public CustomerDetailsFromPersonalIdNumberCriteriaConverter(ISession session) : base(session) { }

        public override ICriteria Convert(CustomerDetailsFromPersonalIdNumberCriteria source)
        {
            return Criteria
				.CreateAlias(x => x.Shop)
				.FilterEqual(x => x.Shop.Id, source.ShopId)
				.FilterEqual(x => x.PersonalIdNumber, source.PersonalIdNumber);
        }
    }
}