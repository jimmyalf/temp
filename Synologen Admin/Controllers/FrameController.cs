using System.Web.Mvc;
using Spinit.Wpc.Synologen.Core.Domain.Model;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Persistence;
using Spinit.Wpc.Synologen.Presentation.App.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
	public class FrameController : Controller
	{
		private readonly IFrameRepository _frameRepository;

		public FrameController(IFrameRepository frameRepository)
		{
			_frameRepository = frameRepository;
		}

		[HttpGet]
		public ActionResult Index(string search, int? page, int? pageSize)
		{
			var list = _frameRepository.FindBy(new PageOfFramesMatchingCriteria { NameLike = search, Page = page ?? 1, PageSize = pageSize ?? 3 });
			var viewList = ((IPagedList<Frame>)list).ToFrameViewList();
			return View(new FrameListView {List = viewList, SearchWord = search});
		}

		[HttpPost]
		public ActionResult Index(FrameListView inModel, int? pageSize)
		{
			var list = _frameRepository.FindBy(new PageOfFramesMatchingCriteria { NameLike = inModel.SearchWord, Page = 1, PageSize = pageSize ?? 3 });
			var viewList = ((IPagedList<Frame>)list).ToFrameViewList();
			return View(new FrameListView {List = viewList, SearchWord = inModel.SearchWord});
		}

		public ActionResult Add()
		{
			return View(new FrameEditView());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Add(FrameEditView inModel)
		{
			if (ModelState.IsValid)
			{
				var frame = inModel.ToFrame();
				_frameRepository.Save(frame);
				return RedirectToAction("Index");
			}
			return View(inModel);
		}
	}
}