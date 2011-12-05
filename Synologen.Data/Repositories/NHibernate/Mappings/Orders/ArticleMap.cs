using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Orders
{
    public class ArticleMap : ClassMap<Article>
    {
        public ArticleMap()
        {
            Table("SynologenOrderArticle");
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
}