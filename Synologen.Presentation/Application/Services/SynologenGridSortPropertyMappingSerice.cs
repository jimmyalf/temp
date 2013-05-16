using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Controllers;
using Spinit.Wpc.Synologen.Presentation.Models;
using Spinit.Wpc.Synologen.Presentation.Models.Deviation;
using Spinit.Wpc.Synologen.Presentation.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Models.Order;
using Subscription = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Subscription;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public class SynologenGridSortPropertyMappingSerice : BaseGridSortPropertyMappingService
	{
		public SynologenGridSortPropertyMappingSerice()
		{
			Map<FrameController, FrameListItemView, Frame>(x => x.Color, x => x.Color.Name);
			Map<FrameController, FrameListItemView, Frame>(x => x.Brand, x => x.Brand.Name);
            Map<FrameController, FrameListItemView, Frame>(x => x.Supplier, x => x.Supplier.Name);

			Map<FrameController, FrameOrderListItemView, FrameOrder>(x => x.Frame, x => x.Frame.Name);
			Map<FrameController, FrameOrderListItemView, FrameOrder>(x => x.GlassType, x => x.GlassType.Name);
			Map<FrameController, FrameOrderListItemView, FrameOrder>(x => x.Shop, x => x.OrderingShop.Name);
            Map<FrameController, FrameOrderListItemView, FrameOrder>(x => x.Supplier, x => x.Supplier.Name);

			Map<LensSubscriptionController, SubscriptionListItemView, Subscription>(x => x.SubscriptionId, x => x.Id);
			Map<LensSubscriptionController, SubscriptionListItemView, Subscription>(x => x.CustomerName, x => x.Customer.LastName);
			Map<LensSubscriptionController, SubscriptionListItemView, Subscription>(x => x.ShopName, x => x.Customer.Shop.Name);
			Map<LensSubscriptionController, SubscriptionListItemView, Subscription>(x => x.Status, x => x.Active ? SubscriptionStatus.Started.GetEnumDisplayName() : SubscriptionStatus.Stopped.GetEnumDisplayName());

			Map<LensSubscriptionController, TransactionArticleListItem, TransactionArticle>(x => x.ArticleId, x => x.Id);
			Map<LensSubscriptionController, TransactionArticleListItem, TransactionArticle>(x => x.Name, x => x.Name);
			Map<LensSubscriptionController, TransactionArticleListItem, TransactionArticle>(x => x.Active, x => x.Active);
			Map<LensSubscriptionController, TransactionArticleListItem, TransactionArticle>(x => x.NumberOfConnectedTransactions, x => x.NumberOfConnectedTransactions);

			Map<OrderController, OrderListItem, Order>(x => x.OrderId, x => x.Id);
			Map<OrderController, OrderListItem, Order>(x => x.CreatedDate, x => x.Created);
			Map<OrderController, OrderListItem, Order>(x => x.CustomerName, x => x.Customer.FirstName);
			Map<OrderController, OrderListItem, Order>(x => x.PersonalIDNumber, x => x.Customer.PersonalIdNumber);
			Map<OrderController, OrderListItem, Order>(x => x.ShopName, x => x.Shop.Name);
                                 
			Map<OrderController, CategoryListItem, ArticleCategory>(x => x.CategoryId, x => x.Id);
			Map<OrderController, CategoryListItem, ArticleCategory>(x => x.Name, x => x.Name);
                                                   
			Map<OrderController, SupplierListItem, ArticleSupplier>(x => x.SupplierId, x => x.Id);
			Map<OrderController, SupplierListItem, ArticleSupplier>(x => x.Name, x => x.Name);
			Map<OrderController, SupplierListItem, ArticleSupplier>(x => x.OrderEmail, x => x.OrderEmailAddress);
                                 
			Map<OrderController, ArticleTypeListItem, ArticleType>(x => x.ArticleTypeId, x => x.Id);
			Map<OrderController, ArticleTypeListItem, ArticleType>(x => x.Name, x => x.Name);
			Map<OrderController, ArticleTypeListItem, ArticleType>(x => x.CategoryName, x => x.Category.Name);

			For<OrderController, ArticleListItem, Article>(mapper =>
			{
				mapper.Map(x => x.ArticleId, x => x.Id);
				mapper.Map(x => x.Name, x => x.Name);
				mapper.Map(x => x.Supplier, x => x.ArticleSupplier.Name);
				mapper.Map(x => x.Type, x => x.ArticleType.Name);
			});

			For<OrderController, SubscriptionListItem, Core.Domain.Model.Orders.Subscription>(mapper =>
			{
				mapper.Map(x => x.AccountNumber, x => x.BankAccountNumber);
				mapper.Map(x => x.Customer, x => x.Customer.FirstName);
				mapper.Map(x => x.Shop, x => x.Shop.Name);
				mapper.Map(x => x.SubscriptionId, x => x.Id);
				mapper.Map(x => x.PaymentNumber, x => x.AutogiroPayerId);
			});

            For<DeviationController, DeviationListItemView, Deviation>(mapper =>
            {
                mapper.Map(x => x.Id, x => x.Id);
                mapper.Map(x => x.CategoryName, x => x.Category.Name);
                mapper.Map(x => x.CreatedDate, x => x.CreatedDate);
                mapper.Map(x => x.Type, x => x.Type);
                mapper.Map(x => x.ShopId, x => x.ShopId);
                mapper.Map(x => x.SupplierName, x => x.Supplier.Name);
            });

            For<DeviationController, CategoryListItemView, DeviationCategory>(mapper =>
            {
                mapper.Map(x => x.Id, x => x.Id);
                mapper.Map(x => x.Name, x => x.Name);
                mapper.Map(x => x.Active, x => x.Active);
            });

            For<DeviationController, SupplierListItemView, DeviationSupplier>(mapper =>
            {
                mapper.Map(x => x.Id, x => x.Id);
                mapper.Map(x => x.Name, x => x.Name);
            });
		}
	}
}