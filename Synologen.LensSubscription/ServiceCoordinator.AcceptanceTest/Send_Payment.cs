using System;
using System.Linq;
using NUnit.Framework;
using ServiceCoordinator.AcceptanceTest.TestHelpers;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using SendPaymentTask = Synologen.LensSubscription.ServiceCoordinator.Task.SendPayments.Task;

namespace ServiceCoordinator.AcceptanceTest
{
	[TestFixture, Category("Feature: Sending Payment")]
	public class When_sending_a_payment : TaskBase
	{
		private ITaskRunnerService taskRunnerService;
		private SendPaymentTask task;
		private Customer customer;
		private Subscription subscription;
		private int bankGiroPayerNumber;

		public When_sending_a_payment()
		{
			Context = () =>
			{
				InvokeWebService(service =>
				{
					bankGiroPayerNumber = service.RegisterPayer("Test payer", AutogiroServiceType.LensSubscription);
				});

				var countryToUse = countryRepository.Get(SwedenCountryId);
				var shopToUse = shopRepository.Get(TestShopId);
				customer = Factory.CreateCustomer(countryToUse, shopToUse);
				customerRepository.Save(customer);
				subscription = Factory.CreateSubscriptionReadyForPayment(customer, bankGiroPayerNumber);
				subscriptionRepository.Save(subscription);
				task = ResolveTask<SendPaymentTask>();
				taskRunnerService = GetTaskRunnerService(task);
			};

			Because = () => taskRunnerService.Run();
		}

		[Test]
		public void Webservice_stores_payment()
		{
			var lastPayment = bgPaymentRepository.GetAll().Last();
			lastPayment.Amount.ShouldBe(subscription.PaymentInfo.MonthlyAmount);
			lastPayment.HasBeenSent.ShouldBe(false);
			lastPayment.Payer.Id.ShouldBe(bankGiroPayerNumber);
			lastPayment.PaymentDate.Date.ShouldBe(DateTime.Now.Date); //FIX: Figure out how to config an expected payment date
			lastPayment.PaymentPeriodCode.ShouldBe(PaymentPeriodCode.PaymentOnceOnSelectedDate);
			lastPayment.Reference.ShouldBe(null);
			lastPayment.SendDate.ShouldBe(null);
			lastPayment.Type.ShouldBe(PaymentType.Debit);
		}

		[Test]
		public void Task_updates_payment_date()
		{
			var fetchedSubscription = ResolveRepository<ISubscriptionRepository>(GetWPCSession).Get(subscription.Id);
			fetchedSubscription.PaymentInfo.PaymentSentDate.Value.Date.ShouldBe(DateTime.Now.Date);
		}
	}
}