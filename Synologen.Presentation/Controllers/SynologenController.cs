using System.Linq;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Transform;
using Spinit.Wpc.Synologen.Core.Domain.Model.Synologen;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Helpers;
using Spinit.Wpc.Synologen.Presentation.Helpers.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models.Order;
using Spinit.Wpc.Synologen.Data.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
	public class SynologenController : BaseController
	{
		public SynologenController(ISession session, IAdminSettingsService adminSettingsService) 
			: base(session, adminSettingsService) {}

		[HttpGet]
		public ActionResult ShopGroups(GridPageSortParameters pageSortParameters)
		{
			var query = GetPagedSortedQuery<ShopGroup>(pageSortParameters, criteria => criteria
				.SetFetchMode(x => x.Shops, FetchMode.Join)
				.SetResultTransformer(Transformers.RootEntity)
			);
			var groups = Query(query);
		 	var viewModel = new ShopGroupListView(groups);
			return View(viewModel);
		}

		[HttpGet]
		public ActionResult ShopGroupForm(int? id)
		{
			var viewModel = new ShopGroupFormView();
			if(id.HasValue)
			{
				var group = WithSession(session => session.Get<ShopGroup>(id.Value));
				viewModel = new ShopGroupFormView(group);
			}
			return View(viewModel);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult ShopGroupForm(ShopGroupFormView form)
		{
			if(!ModelState.IsValid)
			{
				this.AddErrorMessage("Gruppen kunde inte sparas, se validering-meddelanden.");
				return View(form);
			}
			var group = form.IsCreate 
				? new ShopGroup() 
				: WithSession(session => session.Get<ShopGroup>(form.Id));
			group.Name = form.Name;
			WithSession(session => session.Save(group));
			this.AddSuccessMessage("Gruppen har sparats.");
			return RedirectToAction("ShopGroups");
		}

		[HttpPost]
		public ActionResult DeleteShopGroup(int id)
		{
			var group = WithSession(x => x.Get<ShopGroup>(id));
			if(group.Shops.Any())
			{
				this.AddErrorMessage("Gruppen kunde inte raderas då den är knuten till en eller flera butiker");
				return RedirectToAction("ShopGroups");				
			}
			WithSession(x => x.Delete(group));
			this.AddSuccessMessage("Gruppen har raderats");
			return RedirectToAction("ShopGroups");
		}
	}
}