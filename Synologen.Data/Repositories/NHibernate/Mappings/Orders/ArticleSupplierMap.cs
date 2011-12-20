using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Orders
{
    public class ArticleSupplierMap : ClassMap<ArticleSupplier>
    {
        public ArticleSupplierMap()
        {
            Table("SynologenOrderArticleSupplier");
            Id(x => x.Id);
            Map(x => x.Name);
            HasMany(x => x.Articles).Inverse().KeyColumn("ArticleSupplierId").Cascade.AllDeleteOrphan();
        }
    }
}