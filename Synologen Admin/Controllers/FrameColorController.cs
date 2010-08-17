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
				OrderBy = gridParameters.Column, 
				SortAscending = gridParameters.SortAscending
			};

			var list = _frameColorRepository.FindBy(criteria);
			var viewList = list.ToSortedPagedList().ToFrameColorViewList();
			return View(viewList);
		}


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
	
	}
}