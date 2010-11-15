using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.ContractSales
{
	public class TransactionMap : ClassMap<Transaction>
	{
		public TransactionMap()
		{
			Table("SynologenLensSubscriptionTransaction");
			Id(x => x.Id);
			Map(x => x.Amount);
		}
	}
}