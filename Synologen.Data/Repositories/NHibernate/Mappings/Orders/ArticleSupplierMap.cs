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
			Map(x => x.OrderEmailAddress);
            Map(x => x.ShippingOptions).CustomType<int>();
            HasMany(x => x.Articles).Inverse().KeyColumn("ArticleSupplierId").Cascade.AllDeleteOrphan();
			Map(x => x.Active);
        }
    }
}