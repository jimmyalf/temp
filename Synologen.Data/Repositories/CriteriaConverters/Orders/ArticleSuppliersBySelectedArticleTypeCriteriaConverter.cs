using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.Orders
{
    public class ArticleSuppliersBySelectedArticleTypeCriteriaConverter : NHibernateActionCriteriaConverter<ArticleSuppliersBySelectedArticleType, ArticleSuppliersBySelectedArticleTypeCriteriaConverter>
    {
        public ArticleSuppliersBySelectedArticleTypeCriteriaConverter(ISession session) : base(session)
        {
        }

        public override ICriteria Convert(ArticleSuppliersBySelectedArticleType source)
        {
            return Session.CreateCriteriaOf<ArticleSupplier>();
        }
    }
}