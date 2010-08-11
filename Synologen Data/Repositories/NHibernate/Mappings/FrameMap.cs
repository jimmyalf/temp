using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings
{
	public class FrameMap : ClassMap<Frame>
	{
		public FrameMap()
		{
			Table("SynologenFrame");
			Id(x => x.Id);
			Map(x => x.Name).Not.Nullable();;
			Map(x => x.AllowOrders).Not.Nullable();;
			Map(x => x.ArticleNumber).Not.Nullable();;
			References(x => x.Brand)
				.Cascade.SaveUpdate()
				.Column("BrandId")
				.Not.Nullable();
			References(x => x.Color)
				.Cascade.SaveUpdate()
				.Column("ColorId")
				.Not.Nullable();
			Map(x => x.Axis);
			Component(x => x.Index, m =>
			{
			  m.Map(x => x.Min).Column("IndexMin").Not.Nullable();;
			  m.Map(x => x.Max).Column("IndexMax").Not.Nullable();;
			  m.Map(x => x.Increment).Column("IndexIncrement").Not.Nullable();;
			});
			Component(x => x.Sphere, m =>
			{
			  m.Map(x => x.Min).Column("SphereMin").Not.Nullable();;
			  m.Map(x => x.Max).Column("SphereMax").Not.Nullable();;
			  m.Map(x => x.Increment).Column("SphereIncrement").Not.Nullable();;
			});
			Component(x => x.PupillaryDistance, m =>
			{
			  m.Map(x => x.Min).Column("PupillaryDistanceMin").Not.Nullable();;
			  m.Map(x => x.Max).Column("PupillaryDistanceMax").Not.Nullable();;
			  m.Map(x => x.Increment).Column("PupillaryDistanceIncrement").Not.Nullable();;
			});
			Component(x => x.Cylinder, m =>
			{
			  m.Map(x => x.Min).Column("CylinderMin").Not.Nullable();;
			  m.Map(x => x.Max).Column("CylinderMax").Not.Nullable();;
			  m.Map(x => x.Increment).Column("CylinderIncrement").Not.Nullable();;
			});
		}
	}
}