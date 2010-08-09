using System.Web.Mvc;
using Spinit.Wpc.Synologen.Core.Domain.Model;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Helpers;
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
		public ActionResult Index(string search, GridPageSortParameters gridPageSortParameters ) 
		{
			var criteria = new PageOfFramesMatchingCriteria
			{
				NameLike = search, 
				Page = gridPageSortParameters.Page, 
				PageSize = gridPageSortParameters.PageSize ?? DefaultPageSize, 
				OrderBy = ViewModelExtensions.GetTranslatedPropertyNameOrDefault<FrameListItemView,Frame>(gridPageSortParameters.Column), 
				SortAscending = gridPageSortParameters.SortAscending
			};

			var list = _frameRepository.FindBy(criteria);
			var viewList = list
				.ToSortedPagedList()
				.ToFrameViewList();
			return View(new FrameListView {List = viewList, SearchWord = search});
		}

		[HttpPost]
		public ActionResult Index(FrameListView inModel, GridPageSortParameters gridPageSortParameters)
		{
			var list = _frameRepository.FindBy(new PageOfFramesMatchingCriteria
			{
				NameLike = inModel.SearchWord,
				Page = gridPageSortParameters.Page,
				PageSize = gridPageSortParameters.PageSize ?? DefaultPageSize
			});
			var viewList = list
				.ToSortedPagedList()
				.ToFrameViewList();
			return View(new FrameListView {List = viewList, SearchWord = inModel.SearchWord});
		}

		#endregion

		#region Edit

		public ActionResult Edit(int id)
		{
			var selectableFrameColors = _frameColorRepository.GetAll();
			var selectableFrameBrands = _frameBrandRepository.GetAll();
			var frame = _frameRepository.Get(id);
			var viewModel = frame.ToFrameEditView(selectableFrameBrands, selectableFrameColors, "Redigera b�ge");
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
			return View(FrameEditView.GetDefaultInstance(selectableFrameColors, selectableFrameBrands, "Skapa ny b�ge"));
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
			inModel.AvailableFrameColors = _frameColorRepository.GetAll();
			inModel.AvailableFrameBrands = _frameBrandRepository.GetAll();
			inModel.FormLegend = "Skapa ny b�ge";
			return View(inModel);
		}

		#endregion
	}
}