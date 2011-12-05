using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Orders
{
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Table("SynologenOrder");
            Id(x => x.Id);
            References(x => x.Article).Column("ArticleId");
            //Map(x => x.CategoryId);
        	References(x => x.LensRecipe).Column("LensRecipeId").Nullable();
			//Map(x => x.LeftBaseCurve);
			//Map(x => x.LeftDiameter);
			//Map(x => x.LeftPower);
			//Map(x => x.RightBaseCurve);
			//Map(x => x.RightDiameter);
			//Map(x => x.RightPower);
            Map(x => x.ShippingType).CustomType<int>();
			//Map(x => x.SupplierId);
			//Map(x => x.TypeId);
        }
    }
}