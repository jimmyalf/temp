using System.Web.Mvc;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
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
		public ActionResult Brands(GridPageSortParameters gridPageSortParameters) 
		{
			var criteria = new PageOfFrameBrandsMatchingCriteria {
				Page = gridPageSortParameters.Page,
				PageSize = gridPageSortParameters.PageSize ?? DefaultPageSize,
				OrderBy = gridPageSortParameters.Column,
				SortAscending = gridPageSortParameters.SortAscending
			};

			var list = _frameBrandRepository.FindBy(criteria);
			var viewList = list.ToSortedPagedList().ToFrameBrandViewList();
			return View(viewList);
		}


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
				this.AddSuccessMessage("Märket har sparats");
				return RedirectToAction("Brands");
			}
			return View(inModel);
		}


		public ActionResult AddBrand() {
			return View(new FrameBrandEditView { FormLegend = "Skapa nytt bågmärke" });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddBrand(FrameBrandEditView inModel) {
			if (ModelState.IsValid) {
				var frameBrand = inModel.ToFrameBrand();
				_frameBrandRepository.Save(frameBrand);
				this.AddSuccessMessage("Märket har sparats");
				return RedirectToAction("Brands");
			}
			return View(inModel);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteBrand(int id) {
			var frameColor = _frameBrandRepository.Get(id);
			try 
			{
				_frameBrandRepository.Delete(frameColor);
			}
			catch(SynologenDeleteItemHasConnectionsException)
			{
				this.AddErrorMessage("Bågmärket kunde inte raderas då är knutet till en eller fler bågar");
			}
			return RedirectToAction("Brands");
		}		
	}
}