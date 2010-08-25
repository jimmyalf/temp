using System.Web.Mvc;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Helpers.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
	public partial class FrameController
	{
		[HttpGet]
		public ActionResult FrameOrders(FrameOrderListView viewModel)
		{
			var criteria = new PageOfFrameOrdersMatchingCriteria
			{
				OrderBy = viewModel.Column,
				Page = viewModel.Page,
                PageSize = viewModel.PageSize ?? DefaultPageSize,
                SortAscending = viewModel.SortAscending
			};
			viewModel.List = _frameOrderRepository.FindBy(criteria)
				.ToSortedPagedList()
				.ToFrameOrderViewList();
			return View(viewModel);
		}
		
	}
}