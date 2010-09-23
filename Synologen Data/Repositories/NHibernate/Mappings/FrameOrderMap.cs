using FluentNHibernate.Mapping;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings
{
	public class FrameOrderMap : ClassMap<Core.Domain.Model.FrameOrder.FrameOrder>
	{
		public FrameOrderMap()
		{
			Table("SynologenFrameOrder");
			Id(x => x.Id);
			Map(x => x.Sent).Nullable();
			Map(x => x.Created).Not.Nullable();
			Map(x => x.Reference).Nullable().Length(255);
			Component(x => x.Addition, m =>
			{
				m.Map(x => x.Left).Column("AdditionLeft").Nullable();
				m.Map(x => x.Right).Column("AdditionRight").Nullable();
			});
			Component(x => x.Axis, m =>
			{
				m.Map(x => x.Left).Column("AxisLeft");
				m.Map(x => x.Right).Column("AxisRight");
			});
			Component(x => x.Sphere, m =>
			{
				m.Map(x => x.Left).Column("SphereLeft");
				m.Map(x => x.Right).Column("SphereRight");
			});
			Component(x => x.PupillaryDistance, m =>
			{
				m.Map(x => x.Left).Column("PupillaryDistanceLeft");
				m.Map(x => x.Right).Column("PupillaryDistanceRight");
			});
			Component(x => x.Cylinder, m =>
			{
				m.Map(x => x.Left).Column("CylinderLeft");
				m.Map(x => x.Right).Column("CylinderRight");
			});
			Component(x => x.Height, m =>
			{
				m.Map(x => x.Left).Column("HeightLeft").Nullable();
				m.Map(x => x.Right).Column("HeightRight").Nullable();
			});
			References(x => x.OrderingShop)
				.Cascade.None()
				.Column("OrderingShopId")
				.Not.Nullable();
			References(x => x.GlassType)
				.Cascade.None()
				.Column("GlassTypeId")
				.Not.Nullable();
			References(x => x.Frame)
				.Cascade.None()
				.Column("FrameId")
				.Not.Nullable();
			
		}
	}
}