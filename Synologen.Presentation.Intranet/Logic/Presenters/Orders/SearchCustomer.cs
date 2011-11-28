using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
	public class SearchCustomerPresenter : Presenter<ISearchCustomerView>
	{
		private readonly ISynologenMemberService _synologenMember;
		private readonly IOrderCustomerRepository _orderCustomerRepository;
		private const string RedirectWithCustomerUrlFormat = "{url}?customer={customerId}";
		private const string RedirectWithoutCustomerUrlFormat = "{url}?personalIdNumber={personalIdNumber}";

		public SearchCustomerPresenter(ISearchCustomerView view, ISynologenMemberService synologenMember, IOrderCustomerRepository orderCustomerRepository) : base(view)
		{
			_synologenMember = synologenMember;
			_orderCustomerRepository = orderCustomerRepository;
			View.SearchCustomer += View_SearchCustomer;
		}

		public void View_SearchCustomer(object sender, SearchCustomerEventArgs e)
		{
			var customer = _orderCustomerRepository
				.FindBy(new CustomerDetailsFromPersonalIdNumberCriteria {PersonalIdNumber = e.PersonalIdNumber})
				.FirstOrDefault();
			var customerId = (customer == null) ? (int?) null : customer.Id;
			Redirect(customerId, e.PersonalIdNumber);
		}

		private void Redirect(int? customerId, string personalIdNumber)
		{
			var url = _synologenMember.GetPageUrl(View.EditCustomerPageId);
			var redirect = customerId == null 
				? RedirectWithoutCustomerUrlFormat.ReplaceWith(new {url, personalIdNumber}) 
				: RedirectWithCustomerUrlFormat.ReplaceWith(new {url, customerId});
			HttpContext.Response.Redirect(redirect);
		}

		public override void ReleaseView()
		{
			View.SearchCustomer -= View_SearchCustomer;
		}
	}
}