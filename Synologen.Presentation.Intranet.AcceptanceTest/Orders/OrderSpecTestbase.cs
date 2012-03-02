using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Utility;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
	public abstract class OrderSpecTestbase<TPresenter,TView>: GeneralOrderSpecTestbase<TPresenter,TView>
		where TPresenter : Presenter<TView> 
		where TView : class, IView, ICommonOrderView
	{
		protected void SetupNavigationEvents(string previousPageUrl = null, string abortPageUrl = null, string nextPageUrl = null)
		{
			if(!previousPageUrl.IsNullOrEmpty())
			{
				View.PreviousPageId = 54;
				RoutingService.AddRoute(View.PreviousPageId, previousPageUrl);
			}
			if(!abortPageUrl.IsNullOrEmpty())
			{
				View.AbortPageId = 55;
				RoutingService.AddRoute(View.AbortPageId, abortPageUrl);
			}
			if(!nextPageUrl.IsNullOrEmpty())
			{
				View.NextPageId = 56;
				RoutingService.AddRoute(View.NextPageId, nextPageUrl);
			}
		}
	}


	public abstract class GeneralOrderSpecTestbase<TPresenter,TView>: SpecTestbase<TPresenter,TView>
		where TPresenter : Presenter<TView> 
		where TView : class, IView
	{
		protected Order CreateOrder(Shop shop, OrderCustomer customer = null)
		{
			customer = customer ?? CreateCustomer(shop);
			return CreateWithRepository<IOrderRepository, Order>(() => OrderFactory.GetOrder(shop, customer));
		}

        protected Order CreateOrderWithSubscription(Shop shop, Article article = null, OrderCustomer customer = null, PaymentOptionType paymentOptionType = PaymentOptionType.Subscription_Autogiro_New)
        {
            article = article ?? CreateArticle();
            customer = customer ?? CreateCustomer(shop);
            var lensRecipe = CreateLensRecipe(article);
            var subscription = CreateSubscription(shop, active: paymentOptionType == PaymentOptionType.Subscription_Autogiro_Existing);
            var subscriptionItem = CreateSubscriptionItem(subscription);

            return CreateWithRepository<IOrderRepository, Order>(() => OrderFactory.GetOrder(shop, customer, lensRecipe, subscriptionItem, paymentOptionType));
        }

	    private LensRecipe CreateLensRecipe(Article article)
	    {
	        return CreateWithRepository<ILensRecipeRepository, LensRecipe>(() =>OrderFactory.GetLensRecipe(article));
	    }

	    protected SubscriptionItem CreateSubscriptionItem(Subscription subscription)
	    {
	        return
	            CreateWithRepository<ISubscriptionItemRepository, SubscriptionItem>(
	                () => OrderFactory.GetSubscriptionItem(subscription));
	    }

	    protected OrderCustomer CreateCustomer(Shop shop, Func<Shop,OrderCustomer> factoryMethod = null)
		{
			var customer = (factoryMethod == null) ? OrderFactory.GetCustomer(shop) : factoryMethod(shop);
			return CreateWithRepository<IOrderCustomerRepository, OrderCustomer>(() => customer);
		}

		protected Article CreateArticle(ArticleCategory category = null, ArticleType articleType = null, ArticleSupplier supplier = null)
		{
			category = category ?? CreateWithRepository<IArticleCategoryRepository, ArticleCategory>(() => OrderFactory.GetCategory());
			articleType = articleType ?? CreateWithRepository<IArticleTypeRepository, ArticleType>(() => OrderFactory.GetArticleType(category));
			supplier = supplier ?? CreateWithRepository<IArticleSupplierRepository, ArticleSupplier>(() =>OrderFactory.GetSupplier());
			return CreateWithRepository<IArticleRepository, Article>(() => OrderFactory.GetArticle(articleType, supplier));
		}

		protected IEnumerable<Subscription> CreateSubscriptions(Shop shop, OrderCustomer customer = null , Func<Shop,OrderCustomer,IEnumerable<Subscription>> factoryMethod = null)
		{
			customer = customer ?? CreateCustomer(shop);
			var getSubscriptions = factoryMethod ?? OrderFactory.GetSubscriptions;
			return CreateItemsWithRepository<ISubscriptionRepository, Subscription>(() => getSubscriptions(shop, customer));
		}

		protected Subscription CreateSubscription(Shop shop, OrderCustomer customer = null, bool active = false, DateTime? consentedDate = null, SubscriptionConsentStatus? consentStatus = null)
		{
			customer = customer ?? CreateCustomer(shop);
			return CreateWithRepository<ISubscriptionRepository, Subscription>(() => OrderFactory.GetSubscription(shop, customer, active: active, consentedDate: consentedDate, consentStatus: consentStatus));
		}

		protected decimal GetExpectedCurrentBalance(IList<SubscriptionTransaction> transactions)
		{
			Func<SubscriptionTransaction, bool> isWithdrawal = transaction => (transaction.Reason == TransactionReason.Withdrawal || transaction.Reason == TransactionReason.Correction) && transaction.Type == TransactionType.Withdrawal;
			Func<SubscriptionTransaction, bool> isDeposit = transaction => (transaction.Reason == TransactionReason.Payment || transaction.Reason == TransactionReason.Correction) && transaction.Type == TransactionType.Deposit;
			var withdrawals = transactions.Where(isWithdrawal).Sum(x => x.Amount);
			var deposits = transactions.Where(isDeposit).Sum(x => x.Amount);
			return deposits - withdrawals;
		}

		protected string GetStatusMessage(Subscription subscriptionInput)
		{
			return Switch.On(subscriptionInput, "Okänd status")
				.Case(s => !s.Active, "Inaktivt")
				.Case(s => s.Errors != null && s.Errors.Any(e => !e.IsHandled), "Har ohanterade fel")
				.Case(s => s.ConsentStatus == SubscriptionConsentStatus.Accepted, "Aktivt")
				.Case(s => s.ConsentStatus == SubscriptionConsentStatus.Denied, "Ej medgivet")
				.Case(s => s.ConsentStatus == SubscriptionConsentStatus.NotSent, "Medgivande ej skickat")
				.Case(s => s.ConsentStatus == SubscriptionConsentStatus.Sent, "Skickat för medgivande")
				.Evaluate();
		}
	}
}