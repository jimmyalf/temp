using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Synologen.LensSubscription.BGData.Mappings
{
	public class BGFtpPasswordMap : ClassMap<BGFtpPassword>
	{
		public BGFtpPasswordMap()
		{
			Id(x => x.Id);
			Map(x => x.Password).Not.Nullable().Length(8);
			Map(x => x.Created).Not.Nullable();
		}
	}
}