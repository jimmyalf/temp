using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Synologen.LensSubscription.BGData.Mappings
{
	public class FileSectionToSendMap : ClassMap<FileSectionToSend>
	{
		public FileSectionToSendMap()
		{
			Id(x => x.Id);
			Map(x => x.CreatedDate).Not.Nullable();
			Map(x => x.SectionData).Not.Nullable().Length(4000);
			Map(x => x.SentDate).Nullable();
			Map(x => x.Type).CustomType(typeof (SectionType)).Not.Nullable();
			Map(x => x.TypeName).Not.Nullable();
		}
	}
}