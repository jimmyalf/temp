using System;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription
{
	public class CreateLensSubscriptionPresenter : Presenter<ICreateLensSubscriptionView>
	{
		private readonly ICustomerRepository _customerRepository;

		public CreateLensSubscriptionPresenter(ICreateLensSubscriptionView view, ICustomerRepository customerRepository) : base(view)
		{
			_customerRepository = customerRepository;
			View.Load += View_Load;
		}

		public void View_Load(object sender, EventArgs e)
		{
			var customerId = HttpContext.Request.Params["customer"].ToIntOrDefault();
			var customer = _customerRepository.Get(customerId);
			View.Model.CustomerName = customer.ParseName(x => x.FirstName, x => x.LastName);
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
		}
	}
}