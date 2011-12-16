using System;
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

		public SearchCustomerPresenter(ISearchCustomerView view, ISynologenMemberService synologenMember, IOrderCustomerRepository orderCustomerRepository) : base(view)
		{
			_synologenMember = synologenMember;
			_orderCustomerRepository = orderCustomerRepository;
			View.Submit += View_Submit;
			View.Abort += View_Abort;
		}

		public void View_Submit(object sender, SearchCustomerEventArgs e)
		{
			var customer = _orderCustomerRepository
				.FindBy(new CustomerDetailsFromPersonalIdNumberCriteria {PersonalIdNumber = e.PersonalIdNumber})
				.FirstOrDefault();
			if(customer == null)
			{
				Redirect(View.NextPageId, "{Url}?personalIdNumber={PersonalIdNumber}", new {e.PersonalIdNumber});
			}
			else
			{
				Redirect(View.NextPageId, "{Url}?customer={CustomerId}", new { CustomerId = customer.Id });
			}
		}

		public void View_Abort(object sender, EventArgs e)
		{
			Redirect(View.AbortPageId, "{Url}");
		}


		private void Redirect(int pageId, string format, object parameters = null)
		{
			var url = _synologenMember.GetPageUrl(pageId);
			var redirectUrl = format.ReplaceWith(parameters ?? new {}).ReplaceWith(new {Url = url});
			HttpContext.Response.Redirect(redirectUrl);
		}

		public override void ReleaseView()
		{
			View.Submit -= View_Submit;
			View.Abort -= View_Abort;
		}
	}
}