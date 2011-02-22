using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Synologen.LensSubscription.BGData.Mappings
{
	public class ReceivedFileSectionMap : ClassMap<ReceivedFileSection>
	{
		public ReceivedFileSectionMap()
		{
			Table("ReceivedFileSections");
			Id(x => x.Id);
			Map(x => x.SectionData).Not.Nullable();
			Map(x => x.Type)
				.CustomType(typeof(SectionType))
                .Not.Nullable();
			Map(x => x.TypeName).Nullable();
			Map(x => x.CreatedDate).Not.Nullable();
			Map(x => x.HandledDate).Nullable();
		}
	}
}