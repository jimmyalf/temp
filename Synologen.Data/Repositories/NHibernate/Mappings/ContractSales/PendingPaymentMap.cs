using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.ContractSales
{
	public class PendingPaymentMap : ClassMap<PendingPayment>
	{
		public PendingPaymentMap()
		{
			Table("SynologenOrderSubscriptionPendingPayment");
			Id(x => x.Id);
			Map(x => x.TaxedAmount).Not.Nullable();
			Map(x => x.TaxFreeAmount).Not.Nullable();
		}
	}
}