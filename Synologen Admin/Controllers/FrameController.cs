using System;
using System.Web.Mvc;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Helpers;
using Spinit.Wpc.Synologen.Presentation.Helpers.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
	public partial class FrameController : Controller
	{
		private readonly IFrameRepository _frameRepository;
		private readonly IFrameColorRepository _frameColorRepository;
		private readonly IFrameBrandRepository _frameBrandRepository;
		private readonly IFrameGlassTypeRepository _frameGlassTypeRepository;
		private readonly IFrameOrderRepository _frameOrderRepository;
		private readonly int DefaultPageSize;
		public FrameController(IFrameRepository frameRepository, IFrameColorRepository frameColorRepository, IFrameBrandRepository frameBrandRepository, IFrameGlassTypeRepository frameGlassTypeRepository, IFrameOrderRepository frameOrderRepository, IAdminSettingsService adminSettingsSetvice)
		{
			_frameRepository = frameRepository;
			_frameColorRepository = frameColorRepository;
			_frameBrandRepository = frameBrandRepository;
			_frameGlassTypeRepository = frameGlassTypeRepository;
			_frameOrderRepository = frameOrderRepository;
			DefaultPageSize = adminSettingsSetvice.GetDefaultPageSize();
		}

		[HttpGet]
		public ActionResult Index(string search, GridPageSortParameters gridPageSortParameters ) 
		{
			var decodedSearchTerm = search.UrlDecode();
			var criteria = new PageOfFramesMatchingCriteria
			{
				NameLike = decodedSearchTerm,
				Page = gridPageSortParameters.Page, 
				PageSize = gridPageSortParameters.PageSize ?? DefaultPageSize, 
				OrderBy = gridPageSortParameters.Column,
				SortAscending = gridPageSortParameters.Direction == SortDirection.Ascending
			};
			//HttpContext.Items.Add("MVC", "Use flag to determine MVC or Webform masterpage");
			var list = _frameRepository.FindBy(criteria);
			var viewList = list
				.ToFrameViewList();
			var viewModel = new FrameListView {List = viewList, SearchTerm = decodedSearchTerm};
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Index(FrameListView inModel)
		{
			var routeValues = ControllerContext.HttpContext.Request.QueryString
				.ToRouteValueDictionary()
				.BlackList("controller", "action");
			if(String.IsNullOrEmpty(inModel.SearchTerm))
			{
			    routeValues.TryRemoveRouteValue("search");
			}
			else
			{
			    routeValues.AddOrReplaceRouteValue("search", inModel.SearchTerm.UrlEncode());
			}
			return RedirectToAction("Index", routeValues);
		}

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
				this.AddSuccessMessage("Bågen har sparats");
				return RedirectToAction("Index");
			}
			var selectableFrameColors = _frameColorRepository.GetAll();
			var selectableFrameBrands = _frameBrandRepository.GetAll();
			inModel.AvailableFrameBrands = selectableFrameBrands;
			inModel.AvailableFrameColors = selectableFrameColors;
			return View(inModel);
		}

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
				this.AddSuccessMessage("Bågen har sparats");
				return RedirectToAction("Index");
			}
			inModel.AvailableFrameColors = _frameColorRepository.GetAll();
			inModel.AvailableFrameBrands = _frameBrandRepository.GetAll();
			inModel.FormLegend = "Skapa ny båge";
			return View(inModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id) {
			var frame = _frameRepository.Get(id);
			//TODO: Check for connected orders during delete
			try
			{
				_frameRepository.Delete(frame);
			}
			catch(SynologenDeleteItemHasConnectionsException)
			{
				this.AddErrorMessage("Bågen kunde inte raderas då den är knuten till en eller fler beställningar");
				return RedirectToAction("Index");
			}
			this.AddSuccessMessage("Bågen har raderats");
			return RedirectToAction("Index");
		}
	}


}