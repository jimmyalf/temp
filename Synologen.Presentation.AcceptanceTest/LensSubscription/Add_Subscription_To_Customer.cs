using System;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.Presentation.AcceptanceTest.LensSubscription.TestHelpers;

namespace Synologen.Presentation.AcceptanceTest.LensSubscription
{
	[TestFixture, Category("Feature: Adding subscription")]
	public class When_adding_a_subscription_to_customer : PresenterBase
	{
		private ICreateLensSubscriptionView view;
		private CreateLensSubscriptionPresenter presenter;
		private SaveSubscriptionEventArgs saveSubscriptionEventArgs;
		private Customer customer;
		private const int SwedenCountryId = 1;

		public When_adding_a_subscription_to_customer()
		{
			Context = () =>
			{
				var countryToUse = countryRepository.Get(SwedenCountryId);
				var shopToUse = shopRepository.Get(testShopId);
				customer = CreateCustomer(countryToUse, shopToUse);
				customerRepository.Save(customer);
				view = A.Fake<ICreateLensSubscriptionView>();
				httpContext.SetupSingleQuery("customer", customer.Id.ToString());
				presenter = new CreateLensSubscriptionPresenter(view, customerRepository, subscriptionRepository, synologenMemberService)
				{
					HttpContext = httpContext.Object
				};
				saveSubscriptionEventArgs = new SaveSubscriptionEventArgs
				{
					AccountNumber = "123456",
                    ClearingNumber = "7894",
                    MonthlyAmount = "345",
                    Notes = "Notes..."
				};
			};
			Because = () =>
			{
				presenter.View_Submit(this, saveSubscriptionEventArgs);
			};
		}

		[Test]
		public void A_subscription_is_added()
		{
			var lastSubscription = new SubscriptionRepository(GetSession()).GetAll().Last();
			lastSubscription.ActivatedDate.ShouldBe(null);
			lastSubscription.Active.ShouldBe(false);
			lastSubscription.BankgiroPayerNumber.ShouldBe(null);
			lastSubscription.ConsentStatus.ShouldBe(SubscriptionConsentStatus.NotSent);
			lastSubscription.CreatedDate.Date.ShouldBe(DateTime.Now.Date);
			lastSubscription.Customer.Id.ShouldBe(customer.Id);
			lastSubscription.Errors.Count().ShouldBe(0);
			lastSubscription.Notes.ShouldBe(saveSubscriptionEventArgs.Notes);
			lastSubscription.PaymentInfo.AccountNumber.ShouldBe(saveSubscriptionEventArgs.AccountNumber);
			lastSubscription.PaymentInfo.ClearingNumber.ShouldBe(saveSubscriptionEventArgs.ClearingNumber);
			lastSubscription.PaymentInfo.MonthlyAmount.ShouldBe(saveSubscriptionEventArgs.MonthlyAmount.ToDecimal());
			lastSubscription.PaymentInfo.PaymentSentDate.ShouldBe(null);
			lastSubscription.Transactions.Count().ShouldBe(0);
		}
	}
}