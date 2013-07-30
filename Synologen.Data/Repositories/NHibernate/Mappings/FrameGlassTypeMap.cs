using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings
{
	public class FrameGlassTypeMap : ClassMap<FrameGlassType>
	{
		public FrameGlassTypeMap()
		{
			Table("SynologenFrameGlassType");
			Id(x => x.Id);
			Map(x => x.Name).Not.Nullable();
            References(x => x.Supplier)
                .Cascade.SaveUpdate()
                .Column("FrameSupplierId")
                .Not.Nullable();
			Map(x => x.IncludeAdditionParametersInOrder).Not.Nullable();
			Map(x => x.IncludeHeightParametersInOrder).Not.Nullable();
			Map(x => x.NumberOfConnectedOrdersWithThisGlassType).Formula("(Select Count('') from SynologenFrameOrder Where SynologenFrameOrder.GlassTypeId = Id)");
			Component(x => x.Sphere, m =>
			{
			  m.Map(x => x.Min).Column("SphereMin").Not.Nullable();
			  m.Map(x => x.Max).Column("SphereMax").Not.Nullable();
			  m.Map(x => x.Increment).Column("SphereIncrement").Not.Nullable();
			});
			Component(x => x.Cylinder, m =>
			{
			  m.Map(x => x.Min).Column("CylinderMin").Not.Nullable();
			  m.Map(x => x.Max).Column("CylinderMax").Not.Nullable();
			  m.Map(x => x.Increment).Column("CylinderIncrement").Not.Nullable();
			});
		}
	}
}