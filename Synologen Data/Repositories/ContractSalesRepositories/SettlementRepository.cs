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
				Shop = Session.Get<Shop>(id),
				LensSubscriptionTransactions = transactionsForShop.OrderBy(x => x.Id).ToList(),
				SaleItems = contractSaleItemsForShop.SelectMany(x => x.SaleItems).ToList(),
				ContractSalesValueIncludingVAT = contractSaleItemsForShop.Sum(x => x.TotalAmountIncludingVAT),
				AllContractSalesHaveBeenMarkedAsPayed = contractSaleItemsForShop.All(x => x.MarkedAsPayed)
			};
			return shopSettlement;

			/*
            Session.EnableFilter("ShopFilter").SetParameter("shopId", shopId);
            return Session
                .CreateQuery(@"from Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales.Settlement as Settlement
					where Id = :id")
                .SetParameter("id", id)
                .UniqueResult<Settlement>();
			 */


			/*
			 				.ApplyFilter<ShopFilter>("Shop.Id = shopId");
			HasMany(x => x.LensSubscriptionTransactions).KeyColumn("SettlementId")
				.ApplyFilter<ShopFilter>("Subscription.Customer.Shop.Id = shopId");
			 */


			/*    
			 * criteria

       .SetFetchMode ("Names", FetchMode.Eager)
       .CreateCriteria ("Names", JoinType.InnerJoin)
       .Add (Restrictions.And (
			Restrictions.Like ("ShortString", name, MatchMode.Anywhere),
			Restrictions.Eq ("Language", new Language { LanguageId = (int) languageId })))
       .AddOrder (orderByAsc ? Order.Asc ("ShortString") : Order.Desc ("ShortString"));

*/
		}
	}
}