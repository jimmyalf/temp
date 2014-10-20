using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings
{
	public class FrameBrandMap : ClassMap<FrameBrand>
	{
		public FrameBrandMap()
		{
			Table("SynologenFrameBrand");
			Id(x => x.Id);
			Map(x => x.Name).Not.Nullable();
			Map(x => x.NumberOfFramesWithThisBrand)
				.Formula("(Select Count('') from SynologenFrame Where SynologenFrame.BrandId = Id)");
		}
	}
}