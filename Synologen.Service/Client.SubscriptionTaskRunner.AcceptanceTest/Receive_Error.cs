using System;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.ServiceCoordinator.Task.ReceiveErrors;
using Synologen.Service.Client.SubscriptionTaskRunner.AcceptanceTest.TestHelpers;

namespace Synologen.Service.Client.SubscriptionTaskRunner.AcceptanceTest
{
	[TestFixture, Category("Feature: Receiving Error")]
	public class When_receiveing_an_error : TaskBase
	{
		private Task _task;
		private ITaskRunnerService _taskRunnerService;
		private int _bankGiroPayerNumber;
		private BGReceivedError _error;
		private Subscription _subscription;

		public When_receiveing_an_error()
		{
			Context = () =>
			{
				var shop = CreateShop<Shop>();
				_bankGiroPayerNumber = RegisterPayerWithWebService();
				_subscription = StoreSubscription(customer => Factory.CreateSubscription(customer, shop, _bankGiroPayerNumber, SubscriptionConsentStatus.Accepted, new DateTime(2011,01,01)), shop.Id);
				_error = StoreBGError(Factory.CreateError, _bankGiroPayerNumber);

				_task = ResolveTask<Task>();
				_taskRunnerService = GetTaskRunnerService(_task);
			};
			Because = () => _taskRunnerService.Run();
		}

		[Test]
		public void Task_creates_a_subscription_error()
		{
			var subscriptionError = GetAll<SubscriptionError>(GetWPCSession).Single();
			subscriptionError.BGErrorId.ShouldBe(_error.Id);
			subscriptionError.Code.ShouldBe(null);
			subscriptionError.CreatedDate.Date.ShouldBe(DateTime.Now.Date);
			subscriptionError.HandledDate.ShouldBe(null);
			subscriptionError.IsHandled.ShouldBe(false);
			subscriptionError.Subscription.Id.ShouldBe(_subscription.Id);
			subscriptionError.Type.ShouldBe(SubscriptionErrorType.NotDebitable);
		}

		[Test]
		public void Task_updates_recieved_error_as_handled()
		{
			var receivedError = Get<BGReceivedError>(GetBGSession, _error.Id);
			receivedError.Handled.ShouldBe(true);
		}
	}
}