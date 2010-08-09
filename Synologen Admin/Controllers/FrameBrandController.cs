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
	public class FrameBrandController : Controller {
		private readonly IFrameBrandRepository _frameBrandRepository;
		private const int DefaultPageSize = 10;

		public FrameBrandController(IFrameBrandRepository frameColorRepository) {
			_frameBrandRepository = frameColorRepository;
		}

		[HttpGet]
		public ActionResult Index(GridPageSortParameters gridPageSortParameters, string error) {
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

		#region Edit

		public ActionResult Edit(int id) {
			var frameBrand = _frameBrandRepository.Get(id);
			var viewModel = frameBrand.ToFrameBrandEditView("Redigera bågmärke");
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(FrameBrandEditView inModel) {
			if (ModelState.IsValid) {
				var entity = _frameBrandRepository.Get(inModel.Id);
				var frameBrand = inModel.FillFrameBrand(entity);
				_frameBrandRepository.Save(frameBrand);
				return RedirectToAction("Index");
			}
			return View(inModel);
		}

		#endregion

		#region Add

		public ActionResult Add() {
			return View(new FrameBrandEditView { FormLegend = "Skapa nytt bågmärke" });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Add(FrameBrandEditView inModel) {
			if (ModelState.IsValid) {
				var frameBrand = inModel.ToFrameBrand();
				_frameBrandRepository.Save(frameBrand);
				return RedirectToAction("Index");
			}
			return View(inModel);
		}

		#endregion

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id) {
			var frameColor = _frameBrandRepository.Get(id);
			try {
				_frameBrandRepository.Delete(frameColor);
			}
			catch {
				const string errorMessage = "Bågmärket kunde inte raderas då är knutet till en eller fler bågar";
				return RedirectToAction("Index", new { error = errorMessage });
			}
			return RedirectToAction("Index");
		}
	}
}