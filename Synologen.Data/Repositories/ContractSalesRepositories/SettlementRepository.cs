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
			var contractSaleItemsForShop = settlement.ContractSales.Where(x => x.Shop.Id.Equals(shopId));
			var transactionsForShop = settlement.LensSubscriptionTransactions.Where(x => x.Subscription.Customer.Shop.Id.Equals(shopId));
			var shopSettlement = new ShopSettlement
			{
				Id = settlement.Id,
				CreatedDate = settlement.CreatedDate,
				Shop = Session.Get<Shop>(shopId),
				LensSubscriptionTransactions = transactionsForShop.OrderByDescending(x => x.CreatedDate).ToList(),
				SaleItems = contractSaleItemsForShop.SelectMany(x => x.SaleItems).ToList(),
				ContractSalesValueIncludingVAT = contractSaleItemsForShop.Sum(x => x.TotalAmountIncludingVAT),
				LensSubscriptionsValueIncludingVAT = transactionsForShop.Sum(x => x.Amount),
				AllContractSalesHaveBeenMarkedAsPayed = contractSaleItemsForShop.All(x => x.MarkedAsPayed)
			};
			return shopSettlement;
		}
	}
}