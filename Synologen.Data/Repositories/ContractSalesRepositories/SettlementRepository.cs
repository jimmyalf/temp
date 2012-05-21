using System.Linq;
using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

using Spinit.Wpc.Synologen.Core.Domain.Persistence.ContractSales;

namespace Spinit.Wpc.Synologen.Data.Repositories.ContractSalesRepositories
{
	public class SettlementRepository : NHibernateRepository<Settlement>, ISettlementRepository 
	{
		public SettlementRepository(ISession session) : base(session) {}

		public ShopSettlement GetForShop(int id, int shopId)
		{
			var settlement = Get(id);
			var contractSaleItemsForShop = settlement.ContractSales.Where(x => x.Shop.Id.Equals(shopId)).ToList();
			var oldTransactionsForShop = settlement.OldTransactions.Where(x => x.Subscription.Customer.Shop.Id.Equals(shopId)).OrderByDescending(x => x.CreatedDate).ToList();
			var newTransactionsForShop = settlement.NewTransactions.Where(x => x.Subscription.Shop.Id.Equals(shopId)).OrderByDescending(x => x.CreatedDate).ToList();
			var shopSettlement = new ShopSettlement
			{
				Id = settlement.Id,
				CreatedDate = settlement.CreatedDate,
				Shop = Session.Get<Shop>(shopId),
				OldTransactions = oldTransactionsForShop,
				NewTransactions = newTransactionsForShop,
				SaleItems = contractSaleItemsForShop.SelectMany(x => x.SaleItems),
				ContractSalesValueIncludingVAT = contractSaleItemsForShop.Sum(x => x.TotalAmountIncludingVAT),
				OldTransactionValueIncludingVAT = oldTransactionsForShop.Sum(x => x.Amount),
				NewTransactionValueIncludingVAT = newTransactionsForShop.Sum(x => x.Amount),
				AllContractSalesHaveBeenMarkedAsPayed = contractSaleItemsForShop.All(x => x.MarkedAsPayed)
			};
			return shopSettlement;
		}
	}
}