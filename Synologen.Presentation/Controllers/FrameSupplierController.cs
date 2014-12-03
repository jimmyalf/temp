using System.Web.Mvc;
using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Presentation.Helpers;
using Spinit.Wpc.Synologen.Presentation.Helpers.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
    public partial class FrameController
    {
        [HttpGet]
        public ActionResult Suppliers(GridPageSortParameters gridParameters)
        {
            var criteria = new PagedSortedCriteria<FrameSupplier>
            {
                Page = gridParameters.Page,
                PageSize = gridParameters.PageSize ?? DefaultPageSize,
                OrderBy = gridParameters.Column,
                SortAscending = gridParameters.Direction == SortDirection.Ascending
            } as PagedSortedCriteria;

            var list = _frameSupplierRepository.FindBy(criteria);
            var viewList = list.ToFrameSupplierViewList();

            return View(viewList);
        }

        [HttpGet]
        public ActionResult EditSupplier(int id)
        {
            var frameSupplier = _frameSupplierRepository.Get(id);
            var viewModel = frameSupplier.ToFrameSupplierEditView("Redigera leverantör");
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditSupplier(FrameSupplierEditView inModel)
        {
            if (ModelState.IsValid)
            {
                var entity = _frameSupplierRepository.Get(inModel.Id);
                var frameSupplier = inModel.FillFrameSupplier(entity);
                _frameSupplierRepository.Save(frameSupplier);
                this.AddSuccessMessage("Leverantören har sparats");
                return RedirectToAction("Suppliers");
            }
            return View(inModel);
        }


        [HttpGet]
        public ActionResult AddSupplier()
        {
            var viewModel = new FrameSupplier().ToFrameSupplierEditView("Skapa ny leverantör");
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddSupplier(FrameSupplierEditView inModel)
        {
            if (ModelState.IsValid)
            {
                var frameSupplier = inModel.ToFrameSupplier();
                _frameSupplierRepository.Save(frameSupplier);
                this.AddSuccessMessage("Leverantören har sparats");
                return RedirectToAction("Suppliers");
            }
            return View(inModel);
        }


        [HttpPost]
        public ActionResult DeleteSupplier(int id)
        {
            var frameSupplier = _frameSupplierRepository.Get(id);
            _frameSupplierRepository.Delete(frameSupplier);
            this.AddSuccessMessage("Leverantören har raderats");
            return RedirectToAction("Suppliers");
        }
    }
}