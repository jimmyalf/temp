using System;
using System.Collections.Generic;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
	public abstract class OrderSpecTestbase<TPresenter,TView>: SpecTestbase<TPresenter,TView>
		where TPresenter : Presenter<TView> 
		where TView : class, IView, ICommonOrderView
	{
		protected Order CreateOrder(Shop shop, Article article = null, OrderCustomer customer = null)
		{
			article = article ?? CreateArticle();
			customer = customer ?? CreateCustomer(shop);
			return CreateWithRepository<IOrderRepository, Order>(() => OrderFactory.GetOrder(shop, article, customer));
		}

        protected Order CreateOrderWithSubscription(Shop shop, Article article = null, OrderCustomer customer = null)
        {
            article = article ?? CreateArticle();
            customer = customer ?? CreateCustomer(shop);
            var lensRecipe = CreateLensRecipe();
            var subscription = CreateSubscription(shop);
            var subscriptionItem = CreateSubscriptionItem(subscription);

            return CreateWithRepository<IOrderRepository, Order>(() => OrderFactory.GetOrder(shop, article, customer, lensRecipe, subscriptionItem));
        }

	    private LensRecipe CreateLensRecipe()
	    {
	        return CreateWithRepository<ILensRecipeRepository, LensRecipe>(OrderFactory.GetLensRecipe);
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

		protected Subscription CreateSubscription(Shop shop, OrderCustomer customer = null)
		{
			customer = customer ?? CreateCustomer(shop);
			return CreateWithRepository<ISubscriptionRepository, Subscription>(() => OrderFactory.GetSubscription(shop, customer));
		}

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
}