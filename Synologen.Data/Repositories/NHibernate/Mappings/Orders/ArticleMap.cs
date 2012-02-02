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
                map.Component(innerComponent => innerComponent.Axis, innerMap =>
                {
                    innerMap.Map(x => x.Increment).Column("AxisIncrement").Nullable();
                    innerMap.Map(x => x.Max).Column("AxisMax").Nullable();
                    innerMap.Map(x => x.Min).Column("AxisMin").Nullable();
					innerMap.Map(x => x.DisableDefinition).Column("AxisDisableDefinition");
                });
                map.Component(innerComponent => innerComponent.BaseCurve, innerMap =>
                {
                    innerMap.Map(x => x.Increment).Column("BaseCurveIncrement");
                    innerMap.Map(x => x.Max).Column("BaseCurveMax");
                    innerMap.Map(x => x.Min).Column("BaseCurveMin");
                });
                map.Component(innerComponent => innerComponent.Cylinder, innerMap =>
                {
                    innerMap.Map(x => x.Increment).Column("CylinderIncrement").Nullable();;
                    innerMap.Map(x => x.Max).Column("CylinderMax").Nullable();;
                    innerMap.Map(x => x.Min).Column("CylinderMin").Nullable();
					innerMap.Map(x => x.DisableDefinition).Column("CylinderDisableDefinition");
                });
                map.Component(innerComponent => innerComponent.Diameter, innerMap =>
                {
                    innerMap.Map(x => x.Increment).Column("DiameterIncrement");
                    innerMap.Map(x => x.Max).Column("DiameterMax");
                    innerMap.Map(x => x.Min).Column("DiameterMin");
                });
                map.Component(innerComponent => innerComponent.Power, innerMap =>
                {
                    innerMap.Map(x => x.Increment).Column("PowerIncrement");
                    innerMap.Map(x => x.Max).Column("PowerMax");
                    innerMap.Map(x => x.Min).Column("PowerMin");
                });
                map.Component(innerComponent => innerComponent.Addition, innerMap =>
                {
                    innerMap.Map(x => x.Increment).Column("AdditionIncrement").Nullable();
                    innerMap.Map(x => x.Max).Column("AdditionMax").Nullable();
                    innerMap.Map(x => x.Min).Column("AdditionMin").Nullable();
					innerMap.Map(x => x.DisableDefinition).Column("AdditionDisableDefinition");
                });
            });
            References(x => x.ArticleType).Column("ArticleTypeId");
            References(x => x.ArticleSupplier).Column("ArticleSupplierId");
			Map(x => x.Active);
        }
    }
}