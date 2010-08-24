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
			Map(x => x.Name)
				.Not.Nullable();
			Map(x => x.IncludeAdditionParametersInOrder)
				.Not.Nullable();
			Map(x => x.IncludeHeightParametersInOrder)
				.Not.Nullable();
		}
	}
}