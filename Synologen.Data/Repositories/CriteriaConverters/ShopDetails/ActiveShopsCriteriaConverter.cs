using NHibernate;
using NHibernate.Criterion;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ShopDetails;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.ShopDetails
{
    public class ActiveShopsCriteriaConverter : NHibernateActionCriteriaConverter<ActiveShopsCriteria,Shop>
    {
        public ActiveShopsCriteriaConverter(ISession session) : base(session) { }

        public override ICriteria Convert(ActiveShopsCriteria source)
        {
            return Session.CreateCriteriaOf<Shop>()
                .FilterEqual(x => x.Active, true)
                .FilterEqual(x => x.CategoryId, source.CategoryId)
                .Add(Restrictions.IsNotNull("Coordinates.Latitude"))
                .AddOrder(Order.Asc("Name"));
        }
    }
}
