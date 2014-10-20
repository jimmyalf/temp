using NHibernate;
using NHibernate.Criterion;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ShopDetails;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.ShopDetails
{
    public class SearchShopsCriteriaConverter : NHibernateActionCriteriaConverter<SearchShopsCriteria, Shop>
    {
        public SearchShopsCriteriaConverter(ISession session) : base(session) { }

        public override ICriteria Convert(SearchShopsCriteria source)
        {
            return Session.CreateCriteriaOf<Shop>()
                .FilterBy(x => x.Name, source.Search, MatchMode.Anywhere)
                .FilterEqual(x => x.Active, true)
                .FilterEqual(x => x.CategoryId, source.CategoryId)
                .Add(Restrictions.IsNotNull("Coordinates.Latitude"));
        }
    }
}
