using System.Web.Mvc;
using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
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
			var criteria = new PageOfGlassTypesMatchingCriteria
			{
				Page = gridParameters.Page, 
				PageSize = gridParameters.PageSize ?? DefaultPageSize, 
				OrderBy = gridParameters.Column, 
				SortAscending = gridParameters.Direction == SortDirection.Ascending
			};

			var list = _frameGlassTypeRepository.FindBy(criteria);
			var viewList = list.ToFrameGlassTypeViewList();
			return View(viewList);
		}

		public ActionResult AddGlassType()
		{
		    var allSuppliers = _frameSupplierRepository.GetAll();
			return View(FrameGlassTypeEditView.GetDefaultInstance("Skapa ny glastyp", allSuppliers));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddGlassType(FrameGlassTypeEditView inModel)
		{
			if (ModelState.IsValid)
			{
			    var supplier = _frameSupplierRepository.Get(inModel.SupplierId);
				var frameGlassType = inModel.ToFrameGlassType(supplier);
				_frameGlassTypeRepository.Save(frameGlassType);
				this.AddSuccessMessage("Glastypen har sparats");
				return RedirectToAction("GlassTypes");
			}
		    inModel.FormLegend = "Skapa ny glastyp";
		    inModel.AvailableFrameSuppliers = _frameSupplierRepository.GetAll();
			return View(inModel);
		}

		public ActionResult EditGlassType(int id)
		{
			var frameGlassType = _frameGlassTypeRepository.Get(id);
		    var allSuppliers = _frameSupplierRepository.GetAll();
            var viewModel = frameGlassType.ToFrameGlassTypeEditView(allSuppliers, "Redigera glastyp");
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditGlassType(FrameGlassTypeEditView inModel)
		{
			if (ModelState.IsValid)
			{
				var entity = _frameGlassTypeRepository.Get(inModel.Id);
			    var supplier = _frameSupplierRepository.Get(inModel.SupplierId);
				var frameGlassType = inModel.FillFrameGlassType(entity, supplier);
				_frameGlassTypeRepository.Save(frameGlassType);
				this.AddSuccessMessage("Glastypen har sparats");
				return RedirectToAction("GlassTypes");
			}

		    inModel.FormLegend = "Redigera glastyp";
		    inModel.AvailableFrameSuppliers = _frameSupplierRepository.GetAll();
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