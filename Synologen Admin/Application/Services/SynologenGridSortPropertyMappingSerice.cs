using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Controllers;
using Spinit.Wpc.Synologen.Presentation.Models;
using Spinit.Wpc.Synologen.Presentation.Models.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public class SynologenGridSortPropertyMappingSerice : BaseGridSortPropertyMappingService
	{
		public SynologenGridSortPropertyMappingSerice()
		{
			Map<FrameController, FrameListItemView, Frame>(x => x.Color, x => x.Color.Name);
			Map<FrameController, FrameListItemView, Frame>(x => x.Brand, x => x.Brand.Name);
			Map<FrameController, FrameOrderListItemView, FrameOrder>(x => x.Frame, x => x.Frame.Name);
			Map<FrameController, FrameOrderListItemView, FrameOrder>(x => x.GlassType, x => x.GlassType.Name);
			Map<FrameController, FrameOrderListItemView, FrameOrder>(x => x.Shop, x => x.OrderingShop.Name);

			Map<LensSubscriptionController, SubscriptionListItemView, Subscription>(x => x.SubscriptionId, x => x.Id);
			Map<LensSubscriptionController, SubscriptionListItemView, Subscription>(x => x.CustomerName, x => x.Customer.LastName);
			Map<LensSubscriptionController, SubscriptionListItemView, Subscription>(x => x.ShopName, x => x.Customer.Shop.Name);
			Map<LensSubscriptionController, SubscriptionListItemView, Subscription>(x => x.Status, x => x.Status);

			Map<LensSubscriptionController, TransactionArticleListItem, TransactionArticle>(x => x.ArticleId, x => x.Id);
			Map<LensSubscriptionController, TransactionArticleListItem, TransactionArticle>(x => x.Name, x => x.Name);
			Map<LensSubscriptionController, TransactionArticleListItem, TransactionArticle>(x => x.Active, x => x.Active);
			Map<LensSubscriptionController, TransactionArticleListItem, TransactionArticle>(x => x.NumberOfConnectedTransactions, x => x.NumberOfConnectedTransactions);
		}
	}
}