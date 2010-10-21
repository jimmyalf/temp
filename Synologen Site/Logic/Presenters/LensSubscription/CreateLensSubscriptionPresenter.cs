using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription
{
	public class CreateLensSubscriptionPresenter : Presenter<ICreateLensSubscriptionView>
	{
		private readonly ICustomerRepository _customerRepository;
		private readonly ISubscriptionRepository _subscriptionRepository;

		public CreateLensSubscriptionPresenter(ICreateLensSubscriptionView view, ICustomerRepository customerRepository, ISubscriptionRepository subscriptionRepository) : base(view)
		{
			_customerRepository = customerRepository;
			_subscriptionRepository = subscriptionRepository;
			View.Load += View_Load;
			View.Submit += View_Submit;
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
			View.Submit -= View_Submit;
		}

		public void View_Submit(object sender, SaveSubscriptionEventArgs args)
		{
			var customerId = HttpContext.Request.Params["customer"].ToIntOrDefault();
			Func<Customer, SaveSubscriptionEventArgs, Subscription> converter = (customer,eventArgs) => new Subscription
			{
				CreatedDate = DateTime.Now,
				Customer = customer,
				PaymentInfo = new SubscriptionPaymentInfo
				{
					AccountNumber = eventArgs.AccountNumber, 
					ClearingNumber = eventArgs.ClearingNumber, 
					MonthlyAmount = eventArgs.MonthlyAmount
				},
				Status = SubscriptionStatus.Created,
			};
			var customerToSave = _customerRepository.Get(customerId);
			var subscriptionToSave = converter.Invoke(customerToSave,args);
			_subscriptionRepository.Save(subscriptionToSave);
		}
	}
}