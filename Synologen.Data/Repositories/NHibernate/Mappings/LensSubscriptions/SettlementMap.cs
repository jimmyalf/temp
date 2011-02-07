using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.LensSubscriptions
{
	public class SettlementMap : ClassMap<Settlement>
	{
		public SettlementMap()
		{
			Table("tblSynologenSettlement");
			Id(x => x.Id).Column("cId");
		}
	}
}