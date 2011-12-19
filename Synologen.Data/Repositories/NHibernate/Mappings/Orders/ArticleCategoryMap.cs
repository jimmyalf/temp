using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Orders
{
    public class ArticleCategoryMap : ClassMap<ArticleCategory>
    {
        public ArticleCategoryMap()
        {
            Table("SynologenOrderArticleCategory");
            Id(x => x.Id);

            Map(x => x.Name);
        }
    }
}