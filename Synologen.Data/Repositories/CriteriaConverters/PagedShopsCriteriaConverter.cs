using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters
{
    public class PagedShopsCriteriaConverter : NHibernateActionCriteriaConverter<PagedShopsCriteria,Shop>
    {
        public PagedShopsCriteriaConverter(ISession session) : base(session)
        {
        }

        public override ICriteria Convert(PagedShopsCriteria source)
        {
            return Session.CreateCriteriaOf<Shop>()
                .FilterEqual(x => x.Active, true)
                .Page(source.Page, source.PageSize);
        }
    }
}
