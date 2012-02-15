using System;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.Orders
{
    public class ArticleSuppliersBySelectedArticleTypeCriteriaConverter : NHibernateActionCriteriaConverter<ArticleSuppliersBySelectedArticleType, ArticleSupplier>
    {
        public ArticleSuppliersBySelectedArticleTypeCriteriaConverter(ISession session) : base(session)
        {
        }

        public override ICriteria Convert(ArticleSuppliersBySelectedArticleType source)
        {
			throw new NotImplementedException("Nothing is implemented here!");
            return Session.CreateCriteriaOf<ArticleSupplier>();
        }
    }
}