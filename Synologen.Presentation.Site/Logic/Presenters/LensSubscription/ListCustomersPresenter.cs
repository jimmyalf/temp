using System;
using System.Linq;
using System.Web;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription
{
	public class ListCustomersPresenter : Presenter<IListCustomersView>
	{
		private readonly ICustomerRepository _customerRepository;
		private readonly ISynologenMemberService _synologenMemberService;
		private const string FirstNameColumn = "FirstName";
		private const string LastNameColumn = "LastName";
		private const string PersonalIdNumberColumn = "PersonalIdNumber";

		public ListCustomersPresenter(IListCustomersView view, ICustomerRepository customerRepository, ISynologenMemberService synologenMemberService) : base(view)
		{
			_customerRepository = customerRepository;
			_synologenMemberService = synologenMemberService;
			View.Load += View_Load;
			View.SearchList += SearchList;
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
			View.SearchList -= SearchList;
		}

		public void View_Load(object sender, EventArgs e)
		{
			UpdateModel(null, false);
		}

		public void SearchList(object o, SearchEventArgs args)
		{
			UpdateModel(args.SearchTerm, true);
		}

		private static string SetSortOrder(string thisColumn, string orderColumn, string previousSortOrder)
		{
			string newSortOrder = "Asc";
			if (String.IsNullOrEmpty(orderColumn) || String.IsNullOrEmpty(previousSortOrder))
				return newSortOrder;

			if (thisColumn == orderColumn)
				newSortOrder = (previousSortOrder == "Asc") ? "Desc" : "Asc";
			return newSortOrder;
		}

		private void UpdateModel(string searchTerm, bool fromEvent)
		{

			string firstNameSortUrl;
			string lastNameSortUrl;
			string personNumberSortUrl;
			var editUrl = View.EditPageId == 0 ? "#" : _synologenMemberService.GetPageUrl(View.EditPageId);
			var currentpage = HttpContext.Request.Url.AbsolutePath;
			var orderColumn = HttpContext.Request.Params["order"];
			var sortOrder = HttpContext.Request.Params["sort"];

			CheckOrderParam(ref orderColumn);

			if (string.IsNullOrEmpty(searchTerm) && !fromEvent)
				searchTerm = HttpContext.Request.Params["search"];
			View.Model.SearchTerm = searchTerm;

			if (string.IsNullOrEmpty(searchTerm))
			{
				firstNameSortUrl = String.Format(currentpage + "?order=" + FirstNameColumn + "&sort={0}", SetSortOrder(FirstNameColumn, orderColumn, sortOrder));
				lastNameSortUrl = String.Format(currentpage + "?order=" + LastNameColumn + "&sort={0}", SetSortOrder(LastNameColumn, orderColumn, sortOrder));
				personNumberSortUrl = String.Format(currentpage + "?order=" + PersonalIdNumberColumn + "&sort={0}", SetSortOrder(PersonalIdNumberColumn, orderColumn, sortOrder));
			}
			else
			{
				firstNameSortUrl = String.Format(currentpage + "?order=" + FirstNameColumn + "&sort={0}&search={1}", SetSortOrder(FirstNameColumn, orderColumn, sortOrder), HttpUtility.UrlEncode(searchTerm));
				lastNameSortUrl = String.Format(currentpage + "?order=" + LastNameColumn + "&sort={0}&search={1}", SetSortOrder(LastNameColumn, orderColumn, sortOrder), HttpUtility.UrlEncode(searchTerm));
				personNumberSortUrl = String.Format(currentpage + "?order=" + PersonalIdNumberColumn + "&sort={0}&search={1}", SetSortOrder(PersonalIdNumberColumn, orderColumn, sortOrder), HttpUtility.UrlEncode(searchTerm));
			}
			View.Model.FirstNameSortUrl = firstNameSortUrl;
			View.Model.LastNameSortUrl = lastNameSortUrl;
			View.Model.PersonNumberSortUrl = personNumberSortUrl;

			Func<Customer, ListCustomersItemModel> converter = x => new ListCustomersItemModel
			{
				FirstName = x.FirstName,
				LastName = x.LastName,
				PersonalIdNumber = x.PersonalIdNumber,
				EditPageUrl = String.Format("{0}?customer={1}", editUrl, x.Id)
			};
			var shopId = _synologenMemberService.GetCurrentShopId();
			var criteria = new CustomersForShopMatchingCriteria { ShopId = shopId, SearchTerm = searchTerm, OrderBy = orderColumn, SortAscending = (sortOrder != null) ? (sortOrder == "Asc"): true};
			View.Model.List = _customerRepository.FindBy(criteria).Select(converter);
		}

		private static void CheckOrderParam(ref string order)
		{
			if (String.IsNullOrEmpty(order))
				return;
			switch (order)
			{
				case FirstNameColumn:
					break;
				case LastNameColumn:
					break;
				case PersonalIdNumberColumn:
					break;
				default:
					order = FirstNameColumn;
					break;
			}
		}
	}
}
