using System.Web.Mvc;
using Spinit.Wpc.Synologen.Business;
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
		private readonly int DefaultPageSize = Globals.DefaultAdminPageSize;

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
			var selectableFrameColors = _frameColorRepository.GetAll();
			var selectableFrameBrands = _frameBrandRepository.GetAll();
			inModel.AvailableFrameBrands = selectableFrameBrands;
			inModel.AvailableFrameColors = selectableFrameColors;
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
			inModel.AvailableFrameColors = _frameColorRepository.GetAll();
			inModel.AvailableFrameBrands = _frameBrandRepository.GetAll();
			inModel.FormLegend = "Skapa ny båge";
			return View(inModel);
		}

		#endregion

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id) {
			var frame = _frameRepository.Get(id);
			//TODO: Check for connected orders during delete
			_frameRepository.Delete(frame);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Colors(GridPageSortParameters gridParameters, string error)
		{
			if(!string.IsNullOrEmpty(error))
			{
				ModelState.AddModelError(string.Empty, error);
			}

			var criteria = new PageOfFrameColorsMatchingCriteria
			{
				Page = gridParameters.Page, 
				PageSize = gridParameters.PageSize ?? DefaultPageSize, 
				OrderBy = ViewModelExtensions.GetTranslatedPropertyNameOrDefault<FrameColorListItemView,FrameColor>(gridParameters.Column), 
				SortAscending = gridParameters.SortAscending
			};

			var list = _frameColorRepository.FindBy(criteria);
			var viewList = list.ToSortedPagedList().ToFrameColorViewList();
			return View(viewList);
		}

		#region Edit Color

		public ActionResult EditColor(int id)
		{
			var frameColor = _frameColorRepository.Get(id);
			var viewModel = frameColor.ToFrameColorEditView("Redigera bågfärg");
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditColor(FrameColorEditView inModel)
		{
			if (ModelState.IsValid)
			{
				var entity = _frameColorRepository.Get(inModel.Id);
				var frameColor = inModel.FillFrameColor(entity);
				_frameColorRepository.Save(frameColor);
				return RedirectToAction("Colors");
			}
			return View(inModel);
		}

		#endregion

		#region Add Color

		public ActionResult AddColor()
		{
			return View(new FrameColorEditView {FormLegend = "Skapa ny bågfärg"});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddColor(FrameColorEditView inModel)
		{
			if (ModelState.IsValid)
			{
				var frameColor = inModel.ToFrameColor();
				_frameColorRepository.Save(frameColor);
				return RedirectToAction("Colors");
			}
			return View(inModel);
		}

		#endregion

		[HttpPost]
		[ValidateAntiForgeryToken]
        public ActionResult DeleteColor(int id)
        {
			var frameColor = _frameColorRepository.Get(id);
			try{
			    _frameColorRepository.Delete(frameColor);
			}
			catch
			{
				const string errorMessage = "Färgen kunde inte raderas då den existerar på en eller fler bågar";
				return RedirectToAction("Colors", new {error = errorMessage});
			}
			return RedirectToAction("Colors");
        }


		[HttpGet]
		public ActionResult Brands(GridPageSortParameters gridPageSortParameters, string error) {
			if (!string.IsNullOrEmpty(error)) {
				ModelState.AddModelError("", error);
			}

			var criteria = new PageOfFrameBrandsMatchingCriteria {
				Page = gridPageSortParameters.Page,
				PageSize = gridPageSortParameters.PageSize ?? DefaultPageSize,
				OrderBy = ViewModelExtensions.GetTranslatedPropertyNameOrDefault<FrameBrandListItemView, FrameBrand>(gridPageSortParameters.Column),
				SortAscending = gridPageSortParameters.SortAscending
			};

			var list = _frameBrandRepository.FindBy(criteria);
			var viewList = list.ToSortedPagedList().ToFrameBrandViewList();
			return View(viewList);
		}

		#region Edit Brand

		public ActionResult EditBrand(int id) {
			var frameBrand = _frameBrandRepository.Get(id);
			var viewModel = frameBrand.ToFrameBrandEditView("Redigera bågmärke");
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditBrand(FrameBrandEditView inModel) {
			if (ModelState.IsValid) {
				var entity = _frameBrandRepository.Get(inModel.Id);
				var frameBrand = inModel.FillFrameBrand(entity);
				_frameBrandRepository.Save(frameBrand);
				return RedirectToAction("Brands");
			}
			return View(inModel);
		}

		#endregion

		#region Add Brand

		public ActionResult AddBrand() {
			return View(new FrameBrandEditView { FormLegend = "Skapa nytt bågmärke" });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddBrand(FrameBrandEditView inModel) {
			if (ModelState.IsValid) {
				var frameBrand = inModel.ToFrameBrand();
				_frameBrandRepository.Save(frameBrand);
				return RedirectToAction("Brands");
			}
			return View(inModel);
		}

		#endregion

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteBrand(int id) {
			var frameColor = _frameBrandRepository.Get(id);
			try {
				_frameBrandRepository.Delete(frameColor);
			}
			catch {
				const string errorMessage = "Bågmärket kunde inte raderas då är knutet till en eller fler bågar";
				return RedirectToAction("Brands", new { error = errorMessage });
			}
			return RedirectToAction("Brands");
		}
	}
}