using System;
using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ContractSales;

namespace Spinit.Wpc.Synologen.Data.Repositories.ContractSalesRepositories
{
	public class SettlementRepository : NHibernateRepository<ShopSettlement>, ISettlementRepository 
	{
		public SettlementRepository(ISession session) : base(session) {}
		public int GetNumberOfOrdersWithGivenStatusAndNoInvoiceNumberSet(int status) { throw new NotImplementedException(); }
	}
}