using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Orders
{
    public class ArticleTypeMap : ClassMap<ArticleType>
    {
        public ArticleTypeMap()
        {
            Table("SynologenOrderArticleType");
            Id(x => x.Id);
            Map(x => x.Name);
            References(x => x.Category).Column("ArticleCategoryId");
        }
    }
}