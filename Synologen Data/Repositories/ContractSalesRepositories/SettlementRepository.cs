using System.Linq;
using NHibernate;
using NHibernate.Criterion;
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
			var settlement = Session.CreateCriteria<Settlement>()
				.Add(Restrictions.Eq("Id", id))
				.CreateAlias("LensSubscriptionTransactions.Subscription", "TransactionSubscription")
				.CreateAlias("TransactionSubscription.Customer", "SubscriptionCustomer")
				.CreateAlias("SubscriptionCustomer.Shop", "CustomerShop")
				.SetFetchMode("LensSubscriptionTransactions", FetchMode.Eager)
				.Add(Restrictions.Eq("CustomerShop.Id", shopId))
			    .UniqueResult<Settlement>();
			var shopSettlement = new ShopSettlement
			{
			    Id = settlement.Id,
			    CreatedDate = settlement.CreatedDate,
			    Shop = Session.Get<Shop>(id),
			    LensSubscriptionTransactions = settlement.LensSubscriptionTransactions.Where(x => x.Subscription.Customer.Shop.Id.Equals(shopId)).OrderBy(x => x.Id).ToList(),
			    SaleItems = settlement.ContractSales.Where(x => x.Shop.Id.Equals(shopId)).SelectMany(x => x.SaleItems).ToList()
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