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
                    innerMap.Map(x => x.Increment).Column("AxisIncrement");
                    innerMap.Map(x => x.Max).Column("AxisMax");
                    innerMap.Map(x => x.Min).Column("AxisMin");
                });
                map.Component(innerComponent => innerComponent.BaseCurve, innerMap =>
                {
                    innerMap.Map(x => x.Increment).Column("BaseCurveIncrement");
                    innerMap.Map(x => x.Max).Column("BaseCurveMax");
                    innerMap.Map(x => x.Min).Column("BaseCurveMin");
                });
                map.Component(innerComponent => innerComponent.Cylinder, innerMap =>
                {
                    innerMap.Map(x => x.Increment).Column("CylinderIncrement");
                    innerMap.Map(x => x.Max).Column("CylinderMax");
                    innerMap.Map(x => x.Min).Column("CylinderMin");
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
            });

            References(x => x.ArticleType).Column("ArticleTypeId");
        }
    }
}