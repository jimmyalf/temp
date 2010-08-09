using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings
{
	public class FrameColorMap : ClassMap<FrameColor>
	{
		public FrameColorMap()
		{
			Table("SynologenFrameColor");
			Id(x => x.Id);
			Map(x => x.Name).Not.Nullable();
		}
		
	}
}