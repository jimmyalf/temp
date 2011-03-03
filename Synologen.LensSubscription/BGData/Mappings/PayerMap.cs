using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Synologen.LensSubscription.BGData.Mappings
{
	public class PayerMap : ClassMap<AutogiroPayer>
	{
		public PayerMap()
		{
			Id(x => x.Id);
			Map(x => x.Name);
			Map(x => x.ServiceType).CustomType(typeof (AutogiroServiceType)).Not.Nullable();
		}
	}
}