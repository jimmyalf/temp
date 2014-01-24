using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings
{
	public class FrameColorMap : ClassMap<FrameColor>
	{
		public FrameColorMap()
		{
			Table("SynologenFrameColor");
			Id(x => x.Id);
			Map(x => x.Name).Not.Nullable();
			Map(x => x.NumberOfFramesWithThisColor)
				.Formula("(Select Count('') from SynologenFrame Where SynologenFrame.ColorId = Id)");
		}
		
	}
}