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

            Component(x => x.Power).ColumnPrefix("Power");
            Component(x => x.BaseCurve).ColumnPrefix("BaseCurve");
            Component(x => x.Diameter).ColumnPrefix("Diameter");
        }
    }
}