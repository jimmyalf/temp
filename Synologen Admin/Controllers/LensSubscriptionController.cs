using System.Web.Mvc;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Helpers;
using Spinit.Wpc.Synologen.Presentation.Models.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
	public class LensSubscriptionController : Controller
	{
		private readonly ILensSubscriptionViewService _lensSubscriptionViewService;
		public LensSubscriptionController(ILensSubscriptionViewService lensSubscriptionViewService ) { _lensSubscriptionViewService = lensSubscriptionViewService; }

		public ActionResult Index(GridPageSortParameters pageSortParameters)
		{
			var criteria = new PageOfSubscriptionsMatchingCriteria
			{
				OrderBy = pageSortParameters.Column,
                Page = pageSortParameters.Page,
                PageSize = pageSortParameters.PageSize ?? 40,
                SortAscending = pageSortParameters.Direction == SortDirection.Ascending
			};
			var model = new SubscriptionListView
			{
				List = _lensSubscriptionViewService.GetSubscriptions(criteria)
			};
			
			return View(model);
		}
	}
}