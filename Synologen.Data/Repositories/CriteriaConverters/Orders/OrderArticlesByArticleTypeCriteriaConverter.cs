using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.Orders
{
    public class OrderArticlesByArticleTypeCriteriaConverter : NHibernateActionCriteriaConverter<OrderArticlesByArticleType, Article>
    {
        public OrderArticlesByArticleTypeCriteriaConverter(ISession session) : base(session)
        {
        }

        public override ICriteria Convert(OrderArticlesByArticleType source)
        {
            return Criteria.FilterEqual(x => x.ArticleType.Id, source.ArticleTypeId);
        }
    }
}