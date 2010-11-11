using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Repositories.ContractSalesRepositories
{
	public class ContractSaleRepository : NHibernateRepository<ContractSale>, IContractSaleRepository
	{
		public ContractSaleRepository(ISession session) : base(session) {}
	}
}