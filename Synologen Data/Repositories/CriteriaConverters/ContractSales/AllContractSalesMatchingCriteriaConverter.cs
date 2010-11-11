using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ContractSales;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.ContractSales
{
	public class AllContractSalesMatchingCriteriaConverter: NHibernateActionCriteriaConverter<AllContractSalesMatchingCriteria, ContractSale>
	{
		public AllContractSalesMatchingCriteriaConverter(ISession session) : base(session) {}

		public override ICriteria Convert(AllContractSalesMatchingCriteria source)
		{
			return Criteria
				.FilterEqual(x => x.StatusId, source.ContractSaleStatus)
				.FilterEqual(x => x.InvoiceNumber, source.InvoiceNumber);
		}
	}
}