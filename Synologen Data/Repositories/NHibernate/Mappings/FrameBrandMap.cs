using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings
{
	public class FrameBrandMap : ClassMap<FrameBrand>
	{
		public FrameBrandMap()
		{
			Table("SynologenFrameBrand");
			Id(x => x.Id);
			Map(x => x.Name);
		}
		
	}
}