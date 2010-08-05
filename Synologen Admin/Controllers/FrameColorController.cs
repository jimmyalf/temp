using System.Web.Mvc;
using MvcContrib.Sorting;
using MvcContrib.UI.Grid;
using Spinit.Wpc.Synologen.Core.Domain.Model;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Persistence;
using Spinit.Wpc.Synologen.Presentation.Helpers.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
	public class FrameColorController : Controller
	{
		private readonly IFrameColorRepository _frameColorRepository;
		private const int DefaultPageSize = 10;

		public FrameColorController(IFrameColorRepository frameColorRepository)
		{
			_frameColorRepository = frameColorRepository;
		}

		[HttpGet]
		public ActionResult Index(int? page, int? pageSize, GridSortOptions sortOptions) 
		{
			var criteria = new PageOfFrameColorsMatchingCriteria
			{
				Page = page ?? 1, 
				PageSize = pageSize ?? DefaultPageSize, 
				OrderBy = ViewModelExtensions.GetTranslatedPropertyNameOrDefault<FrameColorListItemView,FrameColor>(sortOptions.Column), 
				SortAscending = (sortOptions.Direction == SortDirection.Ascending)
			};

			var list = _frameColorRepository.FindBy(criteria);
			var viewList = ((ISortedPagedList<FrameColor>)list).ToFrameColorViewList();
			return View(new FrameColorListView {List = viewList});
		}

		[HttpPost]
		public ActionResult Index(FrameColorListView inModel)
		{
			//var list = new []{new FrameColorListItemView {Id = 1, Name = inModel.SelectedFrameColorName}};
			//inModel.List = new SortedPagedList<FrameColorListItemView>(list, 0, 1, 10);
			return View(inModel);
		}

	}
}