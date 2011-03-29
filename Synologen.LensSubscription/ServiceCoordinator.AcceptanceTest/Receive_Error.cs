using System;
using System.Linq;
using NUnit.Framework;
using ServiceCoordinator.AcceptanceTest;
using ServiceCoordinator.AcceptanceTest.TestHelpers;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.BGData.Repositories;
using ReceiveErrorsTask = Synologen.LensSubscription.ServiceCoordinator.Task.ReceiveErrors.Task;

namespace Synologen.LensSubscription.ServiceCoordinator.AcceptanceTest
{
	[TestFixture, Category("Feature: Receiving Error")]
	public class When_receiveing_an_error : ReceiveErrorTaskbase
	{
		private ReceiveErrorsTask task;
		private ITaskRunnerService taskRunnerService;
		private int bankGiroPayerNumber;
		private Subscription subscription;
		private BGReceivedError error;

		public When_receiveing_an_error()
		{
			Context = () =>
			{
				bankGiroPayerNumber = RegisterPayerWithWebService();
				subscription = StoreSubscription(customer => Factory.CreateSubscriptionReadyForPayment(customer, bankGiroPayerNumber), bankGiroPayerNumber);
				error = StoreBGError(Factory.CreateError, bankGiroPayerNumber);

				task = ResolveTask<ReceiveErrorsTask>();
				taskRunnerService = GetTaskRunnerService(task);
			};
			Because = () =>
			{
				taskRunnerService.Run();
			};
		}

		[Test]
		public void Task_creates_a_subscription_error()
		{
			var lastError = subscriptionErrorRepository.GetAll().Last();
			lastError.BGErrorId.ShouldBe(error.Id);
			lastError.Code.ShouldBe(null);
			lastError.CreatedDate.Date.ShouldBe(DateTime.Now.Date);
			lastError.HandledDate.ShouldBe(null);
			lastError.IsHandled.ShouldBe(false);
			lastError.Subscription.Id.ShouldBe(subscription.Id);
			lastError.Type.ShouldBe(SubscriptionErrorType.NotDebitable);
		}

		[Test]
		public void Task_updates_recieved_error_as_handled()
		{
			var receivedError = new BGReceivedErrorRepository(GetBGSession()).Get(error.Id);
			receivedError.Handled.ShouldBe(true);
		}
	}
}