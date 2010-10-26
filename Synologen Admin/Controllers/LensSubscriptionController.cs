using System.Web.Mvc;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Models.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
	public class LensSubscriptionController : Controller
	{
		private readonly ILensSubscriptionViewService _lensSubscriptionViewService;
		public LensSubscriptionController(ILensSubscriptionViewService lensSubscriptionViewService ) { _lensSubscriptionViewService = lensSubscriptionViewService; }

		public ActionResult Index()
		{
			var criteria = new PageOfSubscriptionsMatchingCriteria();
			var model = new SubscriptionListView
			{
				List = _lensSubscriptionViewService.GetSubscriptions(criteria)
			};
			
			return View(model);
		}
	}
}