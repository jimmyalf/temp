using System.Web.Mvc;
using MvcContrib.Sorting;
using Spinit.Wpc.Synologen.Core.Domain.Model;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Persistence;
using Spinit.Wpc.Synologen.Presentation.Helpers;
using Spinit.Wpc.Synologen.Presentation.Helpers.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
	public class FrameColorController : Controller
	{
		private readonly IFrameColorRepository _frameColorRepository;
		private const int DefaultPageSize = 10;

		public FrameColorController(IFrameColorRepository frameColorRepository)
		{
			_frameColorRepository = frameColorRepository;
		}

		[HttpGet]
		public ActionResult Index(GridPageSortParameters gridParameters, string error)
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
				SortAscending = (gridParameters.Direction == SortDirection.Ascending)
			};

			var list = (ISortedPagedList<FrameColor>) _frameColorRepository.FindBy(criteria);
			var viewList = list.ToFrameColorViewList();
			return View(new FrameColorListView {List = viewList});
		}

		#region Edit

		public ActionResult Edit(int id)
		{
			var frameColor = _frameColorRepository.Get(id);
			var viewModel = frameColor.ToFrameColorEditView("Redigera bågfärg");
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(FrameColorEditView inModel)
		{
			if (ModelState.IsValid)
			{
				var entity = _frameColorRepository.Get(inModel.Id);
				var frameColor = inModel.FillFrameColor(entity);
				_frameColorRepository.Save(frameColor);
				return RedirectToAction("Index");
			}
			return View(inModel);
		}

		#endregion

		#region Add

		public ActionResult Add()
		{
			return View(new FrameColorEditView {FormLegend = "Skapa ny bågfärg"});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Add(FrameColorEditView inModel)
		{
			if (ModelState.IsValid)
			{
				var frameColor = inModel.ToFrameColor();
				_frameColorRepository.Save(frameColor);
				return RedirectToAction("Index");
			}
			return View(inModel);
		}

		#endregion

		[HttpPost]
		[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
			var frameColor = _frameColorRepository.Get(id);
			try{
			    _frameColorRepository.Delete(frameColor);
			}
			catch
			{
				const string errorMessage = "Färgen kunde inte raderas då den existerar på en eller fler bågar";
				return RedirectToAction("Index", new {error = errorMessage});
			}
			return RedirectToAction("Index");
        }

	}
}