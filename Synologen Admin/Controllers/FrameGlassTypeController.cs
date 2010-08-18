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
		public ActionResult GlassTypes(GridPageSortParameters gridParameters)
		{
			var criteria = new PageOfFrameGlassTypesMatchingCriteria
			{
				Page = gridParameters.Page, 
				PageSize = gridParameters.PageSize ?? DefaultPageSize, 
				OrderBy = gridParameters.Column, 
				SortAscending = gridParameters.SortAscending
			};

			var list = _frameGlassTypeRepository.FindBy(criteria);
			var viewList = list.ToSortedPagedList().ToFrameGlassTypeViewList();
			return View(viewList);
		}

		public ActionResult AddGlassType()
		{
			return View(new FrameGlassTypeEditView {FormLegend = "Skapa ny glastyp"});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddGlassType(FrameGlassTypeEditView inModel)
		{
			if (ModelState.IsValid)
			{
				var frameGlassType = inModel.ToFrameGlassType();
				_frameGlassTypeRepository.Save(frameGlassType);
				this.AddSuccessMessage("Glastypen har sparats");
				return RedirectToAction("GlassTypes");
			}
			return View(inModel);
		}

		public ActionResult EditGlassType(int id)
		{
			var frameGlassType = _frameGlassTypeRepository.Get(id);
			var viewModel = frameGlassType.ToFrameGlassTypeEditView("Redigera glastyp");
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditGlassType(FrameGlassTypeEditView inModel)
		{
			if (ModelState.IsValid)
			{
				var entity = _frameGlassTypeRepository.Get(inModel.Id);
				var frameGlassType = inModel.FillFrameGlassType(entity);
				_frameGlassTypeRepository.Save(frameGlassType);
				this.AddSuccessMessage("Glastypen har sparats");
				return RedirectToAction("GlassTypes");
			}
			return View(inModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
        public ActionResult DeleteGlassType(int id)
        {
			var frameGlassType = _frameGlassTypeRepository.Get(id);
			try{
			    _frameGlassTypeRepository.Delete(frameGlassType);
			}
			catch(SynologenDeleteItemHasConnectionsException)
			{
				this.AddErrorMessage("Glastypen kunde inte raderas då den är knuten till en eller fler beställningar");
				return RedirectToAction("GlassTypes");
			}
			this.AddSuccessMessage("Glastypen har raderats");
			return RedirectToAction("GlassTypes");
        }	
	}
}