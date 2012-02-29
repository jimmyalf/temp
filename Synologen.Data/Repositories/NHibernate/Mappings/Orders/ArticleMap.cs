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
            Component(component => component.Options, map =>
            {
                map.Component(innerComponent => innerComponent.BaseCurve, innerMap =>
                {
                    innerMap.Map(x => x.Increment).Column("BaseCurveIncrement");
                    innerMap.Map(x => x.Max).Column("BaseCurveMax");
                    innerMap.Map(x => x.Min).Column("BaseCurveMin");
                });
                map.Component(innerComponent => innerComponent.Diameter, innerMap =>
                {
                    innerMap.Map(x => x.Increment).Column("DiameterIncrement");
                    innerMap.Map(x => x.Max).Column("DiameterMax");
                    innerMap.Map(x => x.Min).Column("DiameterMin");
                });
        		map.Map(x => x.EnableAddition).Not.Nullable();
				map.Map(x => x.EnableAxis).Not.Nullable();
				map.Map(x => x.EnableCylinder).Not.Nullable();
            });
            References(x => x.ArticleType).Column("ArticleTypeId");
            References(x => x.ArticleSupplier).Column("ArticleSupplierId");
			Map(x => x.Active);
        }
    }
}