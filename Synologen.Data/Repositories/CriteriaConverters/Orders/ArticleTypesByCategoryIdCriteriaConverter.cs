using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.Orders
{
    public class ArticleTypesByCategoryIdCriteriaConverter : NHibernateActionCriteriaConverter<ArticleTypesByCategory, ArticleType>
    {
        public ArticleTypesByCategoryIdCriteriaConverter(ISession session) : base(session)
        {
        }

        public override ICriteria Convert(ArticleTypesByCategory source)
        {
            return Criteria.FilterEqual(x => x.Category.Id, source.SelectedCategoryId);
        }
    }
}