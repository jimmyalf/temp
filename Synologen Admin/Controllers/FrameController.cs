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
	public class FrameController : Controller
	{
		private readonly IFrameRepository _frameRepository;
		private readonly IFrameColorRepository _frameColorRepository;
		private readonly IFrameBrandRepository _frameBrandRepository;
		private const int DefaultPageSize = 8;

		public FrameController(IFrameRepository frameRepository, IFrameColorRepository frameColorRepository, IFrameBrandRepository frameBrandRepository)
		{
			_frameRepository = frameRepository;
			_frameColorRepository = frameColorRepository;
			_frameBrandRepository = frameBrandRepository;
		}

		#region Index

		[HttpGet]
		public ActionResult Index(string search, int? page, int? pageSize, GridSortOptions sortOptions) 
		{
			var criteria = new PageOfFramesMatchingCriteria
			{
				NameLike = search, 
				Page = page ?? 1, 
				PageSize = pageSize ?? DefaultPageSize, 
				OrderBy = ViewModelExtensions.GetTranslatedPropertyNameOrDefault<FrameListItemView,Frame>(sortOptions.Column), 
				SortAscending = (sortOptions.Direction == SortDirection.Ascending)
			};

			var list = _frameRepository.FindBy(criteria);
			var viewList = ((ISortedPagedList<Frame>)list).ToFrameViewList();
			return View(new FrameListView {List = viewList, SearchWord = search});
		}

		[HttpPost]
		public ActionResult Index(FrameListView inModel, int? pageSize)
		{
			var list = _frameRepository.FindBy(new PageOfFramesMatchingCriteria { NameLike = inModel.SearchWord, Page = 1, PageSize = pageSize ?? DefaultPageSize });
			var viewList = ((ISortedPagedList<Frame>)list).ToFrameViewList();
			return View(new FrameListView {List = viewList, SearchWord = inModel.SearchWord});
		}

		#endregion

		#region Edit

		public ActionResult Edit(int id)
		{
			var selectableFrameColors = _frameColorRepository.GetAll();
			var selectableFrameBrands = _frameBrandRepository.GetAll();
			var frame = _frameRepository.Get(id);
			var viewModel = frame.ToFrameEditView(selectableFrameBrands, selectableFrameColors, "Redigera båge");
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(FrameEditView inModel)
		{
			if (ModelState.IsValid)
			{
				var brand = _frameBrandRepository.Get(inModel.BrandId);
				var color = _frameColorRepository.Get(inModel.ColorId);
				var entity = _frameRepository.Get(inModel.Id);
				var frame = inModel.FillFrame(entity, brand, color);
				_frameRepository.Save(frame);
				return RedirectToAction("Index");
			}
			return View(inModel);
		}

		#endregion

		#region Add

		public ActionResult Add()
		{
			var selectableFrameColors = _frameColorRepository.GetAll();
			var selectableFrameBrands = _frameBrandRepository.GetAll();
			return View(FrameEditView.GetDefaultInstance(selectableFrameColors, selectableFrameBrands, "Skapa ny båge"));
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

		#endregion
	}
}