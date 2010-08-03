using System.Linq;
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
		private readonly IFrameColorRepository _frameColorRepository;
		private readonly IFrameBrandRepository _frameBrandRepository;

		public FrameController(IFrameRepository frameRepository, IFrameColorRepository frameColorRepository, IFrameBrandRepository frameBrandRepository)
		{
			_frameRepository = frameRepository;
			_frameColorRepository = frameColorRepository;
			_frameBrandRepository = frameBrandRepository;
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
			var selectableFrameColors = _frameColorRepository.GetAll();
			var selectableFrameBrands = _frameBrandRepository.GetAll();
			return View(FrameEditView.GetDefaultInstance(selectableFrameColors, selectableFrameBrands));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Add(FrameEditView inModel)
		{
			if (ModelState.IsValid)
			{
				var brand = _frameBrandRepository.Get(inModel.BrandId);
				var color = _frameColorRepository.Get(inModel.ColorId);
				var frame = inModel.ToFrame(brand, color);
				_frameRepository.Save(frame);
				return RedirectToAction("Index");
			}
			return View(inModel);
		}
	}
}