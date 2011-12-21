using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
	public abstract class OrderSpecTestbase<TPresenter,TView> : SpecTestbase<TPresenter,TView> 
		where TPresenter : Presenter<TView> 
		where TView : class, IView
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

		protected IEnumerable<Subscription> GetSubscriptions(OrderCustomer customer = null, Func<OrderCustomer,IEnumerable<Subscription>> factoryMethod = null)
		{
			customer = customer ?? CreateCustomer();
			var getSubscriptions = factoryMethod ?? OrderFactory.GetSubscriptions;
			return CreateItemsWithRepository<ISubscriptionRepository, Subscription>(() => getSubscriptions(customer));
		}
	}
}