using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Synologen.LensSubscription.BGData.Repositories.NHibernate.Mappings
{
	public class ReceivedFileSectionMap : ClassMap<ReceivedFileSection>
	{
		public ReceivedFileSectionMap()
		{
			Table("ReceivedFileSections");
			Id(x => x.Id);
			Map(x => x.SectionData);
			Map(x => x.Type)
				.CustomType(typeof(SectionType));
			Map(x => x.TypeName)
				.Nullable();
			Map(x => x.CreatedDate);
			Map(x => x.HandledDate)
				.Nullable();
		}
	}
}