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
			Map(x => x.Name);
			Map(x => x.AllowOrders);
			Map(x => x.ArticleNumber);
			References(x => x.Brand)
				.Column("BrandId")
				.Cascade.All();
			References(x => x.Color)
				.Column("ColorId")
				.Cascade.All();;
			Map(x => x.Axis);
			Component(x => x.Index, m =>
			{
			  m.Map(x => x.Min).Column("IndexMin");
			  m.Map(x => x.Max).Column("IndexMax");
			  m.Map(x => x.Increment).Column("IndexIncrement");
			});
			Component(x => x.Sphere, m =>
			{
			  m.Map(x => x.Min).Column("SphereMin");
			  m.Map(x => x.Max).Column("SphereMax");
			  m.Map(x => x.Increment).Column("SphereIncrement");
			});
			Component(x => x.PupillaryDistance, m =>
			{
			  m.Map(x => x.Min).Column("PupillaryDistanceMin");
			  m.Map(x => x.Max).Column("PupillaryDistanceMax");
			  m.Map(x => x.Increment).Column("PupillaryDistanceIncrement");
			});
			Component(x => x.Cylinder, m =>
			{
			  m.Map(x => x.Min).Column("CylinderMin");
			  m.Map(x => x.Max).Column("CylinderMax");
			  m.Map(x => x.Increment).Column("CylinderIncrement");
			});
		}
	}
}