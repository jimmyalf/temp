using System.Web.Mvc;
using MvcContrib.UI.Grid;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
	public class FrameBrandController : Controller
	{
		private readonly IFrameBrandRepository _frameBrandRepository;
		private const int DefaultPageSize = 10;

		public FrameBrandController(IFrameBrandRepository frameColorRepository)
		{
			_frameBrandRepository = frameColorRepository;
		}

		[HttpGet]
		public ActionResult Index(int? page, int? pageSize, GridSortOptions sortOptions, string error)
		{
			if(!string.IsNullOrEmpty(error))
			{
				ModelState.AddModelError("", error);
			}

			var criteria = new PageOfFrameBrandsMatchingCriteria
			{
				Page = page ?? 1, 
				PageSize = pageSize ?? DefaultPageSize, 
				OrderBy = ViewModelExtensions.GetTranslatedPropertyNameOrDefault<FrameBrandListItemView,FrameBrand>(sortOptions.Column), 
				SortAscending = (sortOptions.Direction == SortDirection.Ascending)
			};

			var list = (ISortedPagedList<FrameBrand>) _frameBrandRepository.FindBy(criteria);
			var viewList = list.ToFrameBrandViewList();
			return View(new FrameBrandListView {List = viewList});
		}

		#region Edit

		public ActionResult Edit(int id)
		{
			var frameBrand = _frameBrandRepository.Get(id);
			var viewModel = frameBrand.ToFrameBrandEditView("Redigera bågmärke");
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(FrameBrandEditView inModel)
		{
			if (ModelState.IsValid)
			{
				var entity = _frameBrandRepository.Get(inModel.Id);
				var frameBrand = inModel.FillFrameBrand(entity);
				_frameBrandRepository.Save(frameBrand);
				return RedirectToAction("Index");
			}
			return View(inModel);
		}

		#endregion

		#region Add

		public ActionResult Add()
		{
			return View(new FrameBrandEditView(){FormLegend = "Skapa nytt bågmärke"});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Add(FrameBrandEditView inModel)
		{
			if (ModelState.IsValid)
			{
				var frameBrand = inModel.ToFrameBrand();
				_frameBrandRepository.Save(frameBrand);
				return RedirectToAction("Index");
			}
			return View(inModel);
		}

		#endregion

		[HttpPost]
		[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
			var frameColor = _frameBrandRepository.Get(id);
			try{
			    _frameBrandRepository.Delete(frameColor);
			}
			catch
			{
				const string errorMessage = "Färgen kunde inte raderas då den existerar på en eller fler bågar";
				return RedirectToAction("Index", new {error = errorMessage});
			}
			return RedirectToAction("Index");
        }
}