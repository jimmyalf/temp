using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.Orders
{
    public class ArticlesBySupplierAndArticleTypeCriteriaConverter : NHibernateActionCriteriaConverter<ArticlesBySupplierAndArticleType, Article>
    {
        public ArticlesBySupplierAndArticleTypeCriteriaConverter(ISession session) : base(session)
        {
        }

        public override ICriteria Convert(ArticlesBySupplierAndArticleType source)
        {
            return Session.CreateCriteriaOf<Article>()
                .FilterEqual(x => x.ArticleSupplier.Id, source.SupplierId)
                .FilterEqual(x => x.ArticleType.Id, source.ArticleTypeId);
        }
    }
}