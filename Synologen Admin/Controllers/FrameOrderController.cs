using System;
using System.Web.Mvc;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Helpers;
using Spinit.Wpc.Synologen.Presentation.Helpers.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
	public partial class FrameController
	{
		[HttpGet]
		public ActionResult FrameOrders(string search, FrameOrderListView viewModel)
		{
			var criteria = new PageOfFrameOrdersMatchingCriteria
			{
				Search = search.UrlDecode(),
				OrderBy = viewModel.Column,
				Page = viewModel.Page,
                PageSize = viewModel.PageSize ?? DefaultPageSize,
                SortAscending = viewModel.Direction == SortDirection.Ascending
			};
			viewModel.List = _frameOrderRepository.FindBy(criteria)
				.ToSortedPagedList()
				.ToFrameOrderViewList();
			viewModel.SearchTerm = search.UrlDecode();
			return View(viewModel);
		}

		
		[HttpPost]
		public ActionResult FrameOrders(FrameOrderListView viewModel)
		{
			var routeValues = ControllerContext.HttpContext.Request.QueryString
				.ToRouteValueDictionary()
				.BlackList("action", "controller");
			if(String.IsNullOrEmpty(viewModel.SearchTerm))
			{
				routeValues.TryRemoveRouteValue("search");
			}
			else
			{
				routeValues.AddOrReplaceRouteValue("search", viewModel.SearchTerm.UrlEncode());
			}
			return RedirectToAction("FrameOrders", routeValues);
		}


	
		
	}
}