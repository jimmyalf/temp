using System;
using System.Collections.Generic;
using FakeItEasy;
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
		protected Order CreateOrder(Article article = null, OrderCustomer customer = null)
		{
			article = article ?? CreateArticle();
			customer = customer ?? CreateCustomer();
			return CreateWithRepository<IOrderRepository, Order>(() => OrderFactory.GetOrder(article, customer));
		}

		protected OrderCustomer CreateCustomer(Func<OrderCustomer> factoryMethod = null)
		{
			var customer = (factoryMethod == null) ? OrderFactory.GetCustomer() : factoryMethod();
			return CreateWithRepository<IOrderCustomerRepository, OrderCustomer>(() => customer);
		}

		protected Article CreateArticle(ArticleCategory category = null, ArticleType articleType = null, ArticleSupplier supplier = null)
		{
			category = category ?? CreateWithRepository<IArticleCategoryRepository, ArticleCategory>(OrderFactory.GetCategory);
			articleType = articleType ?? CreateWithRepository<IArticleTypeRepository, ArticleType>(() => OrderFactory.GetArticleType(category));
			supplier = supplier ?? CreateWithRepository<IArticleSupplierRepository, ArticleSupplier>(OrderFactory.GetSupplier);
			return CreateWithRepository<IArticleRepository, Article>(() => OrderFactory.GetArticle(articleType, supplier));
		}

		protected IEnumerable<Subscription> CreateSubscriptions(OrderCustomer customer = null, Func<OrderCustomer,IEnumerable<Subscription>> factoryMethod = null)
		{
			customer = customer ?? CreateCustomer();
			var getSubscriptions = factoryMethod ?? OrderFactory.GetSubscriptions;
			return CreateItemsWithRepository<ISubscriptionRepository, Subscription>(() => getSubscriptions(customer));
		}

		protected Subscription CreateSubscription(OrderCustomer customer = null)
		{
			customer = customer ?? CreateCustomer();
			return CreateWithRepository<ISubscriptionRepository, Subscription>(() => OrderFactory.GetSubscription(customer));
		}

		protected void SetupNavigationEvents(string previousPageUrl = null, string abortPageUrl = null, string nextPageUrl = null)
		{
			if(!previousPageUrl.IsNullOrEmpty())
			{
				View.PreviousPageId = 54;
				A.CallTo(() => SynologenMemberService.GetPageUrl(View.PreviousPageId)).Returns(previousPageUrl);
				A.CallTo(() => RoutingService.GetPageUrl(View.PreviousPageId)).Returns(previousPageUrl);
			}
			if(!abortPageUrl.IsNullOrEmpty())
			{
				View.AbortPageId = 55;
				A.CallTo(() => SynologenMemberService.GetPageUrl(View.AbortPageId)).Returns(abortPageUrl);
				A.CallTo(() => RoutingService.GetPageUrl(View.AbortPageId)).Returns(abortPageUrl);
			}
			if(!nextPageUrl.IsNullOrEmpty())
			{
				View.NextPageId = 56;
				A.CallTo(() => SynologenMemberService.GetPageUrl(View.NextPageId)).Returns(nextPageUrl);
				A.CallTo(() => RoutingService.GetPageUrl(View.NextPageId)).Returns(nextPageUrl);
			}
		}
	}
}