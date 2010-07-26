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
		}
	}
}