using System;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription
{
	public class ShopSubscriptionErrorListPresenter : Presenter<IShopSubscriptionErrorListView>
	{
		private readonly ISynologenMemberService _synologenMemberService;
		private readonly ISubscriptionErrorRepository _subscriptionErrorRepository;

		public ShopSubscriptionErrorListPresenter(IShopSubscriptionErrorListView view, ISynologenMemberService synologenMemberService, ISubscriptionErrorRepository subscriptionErrorRepository) : base(view)
		{
			_synologenMemberService = synologenMemberService;
			_subscriptionErrorRepository = subscriptionErrorRepository;
			View.Load += View_Load;
		}

		public void View_Load(object sender, EventArgs e) 
		{
			var shopId = _synologenMemberService.GetCurrentShopId();
			var errors = _subscriptionErrorRepository.FindBy(new AllUnhandledSubscriptionErrorsForShopCriteria(shopId));
			var subscriptionPageUrl = SubscriptionPageUrlResolver(View.SubscriptionPageId);
			View.Model.UnhandledErrors = errors.Select( error => SubscriptionListItemConverter(error, subscriptionPageUrl));
		}

		public override void ReleaseView() 
		{ 
			View.Load -= View_Load;
		}

		private static Func<SubscriptionError,string, ShopSubscriptionErrorListItemModel> SubscriptionListItemConverter
		{
			get
			{
				return (error, subscriptionPageUrl) => new ShopSubscriptionErrorListItemModel
				{
					CreatedDate = error.CreatedDate.ToString("yyyy-MM-dd"),
					CustomerName = error.Subscription.Customer.ParseName(x => x.FirstName, x => x.LastName),
                    Reason = error.Type.GetEnumDisplayName(),
                    SubscriptionLink = String.Format("{0}?subscription={1}", subscriptionPageUrl, error.Subscription.Id)
				};
			}
		}

		private Func<int, string> SubscriptionPageUrlResolver
		{
			get
			{
				return pageId => (pageId <= 0) ? "#" : _synologenMemberService.GetPageUrl(pageId);
			}
		}
	}
}