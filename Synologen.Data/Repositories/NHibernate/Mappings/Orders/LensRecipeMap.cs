using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Orders
{
    public class LensRecipeMap : ClassMap<LensRecipe>
    {
        public LensRecipeMap()
        {
            Table("SynologenOrderLensRecipe");
            Id(x => x.Id);

            Component(x => x.Power, m =>
            {
                m.Map(x => x.Left).Column("PowerLeft");
                m.Map(x => x.Right).Column("PowerRight");
            });
            Component(x => x.BaseCurve, m =>
            {
                m.Map(x => x.Left).Column("BaseCurveLeft");
                m.Map(x => x.Right).Column("BaseCurveRight");
            });
            Component(x => x.Diameter, m =>
            {
                m.Map(x => x.Left).Column("DiameterLeft");
                m.Map(x => x.Right).Column("DiameterRight");
            });
            Component(x => x.Axis, m =>
            {
                m.Map(x => x.Left).Column("AxisLeft");
                m.Map(x => x.Right).Column("AxisRight");
            });
            Component(x => x.Cylinder, m =>
            {
                m.Map(x => x.Left).Column("CylinderLeft");
                m.Map(x => x.Right).Column("CylinderRight");
            });
            Component(x => x.Addition, m =>
            {
                m.Map(x => x.Left).Column("AdditionLeft");
                m.Map(x => x.Right).Column("AdditionRight");
            });
            Component(x => x.Quantity, m =>
            {
                m.Map(x => x.Left).Column("QuantityLeft");
                m.Map(x => x.Right).Column("QuantityRight");
            });
        	Component(x => x.Article, map =>
        	{
        		map.References(x => x.Left).Column("ArticleLeftId");
				map.References(x => x.Right).Column("ArticleRightId");
        	});
        }
    }
}