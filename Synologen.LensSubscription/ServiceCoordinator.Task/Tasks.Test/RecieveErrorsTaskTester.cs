using System;
using System.Linq;
using System.ServiceModel;
using FakeItEasy;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Synologen.LensSubscription.ServiceCoordinator.Task.Test.Factories;
using Synologen.LensSubscription.ServiceCoordinator.Task.Test.TestHelpers;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test
{
	[TestFixture, Category("RecieveErrorsTaskTests")]
	public class When_executing_recieve_errors_task : RecieveErrorsTaskTestBase
	{
		private RecievedError[] expectedErrors;
		private Subscription expectedSubscription;

		public When_executing_recieve_errors_task()
		{
			Context = () =>
			{
				expectedErrors = ErrorFactory.GetList().ToArray();
				expectedSubscription = SubscriptionFactory.Get(3);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(expectedSubscription);
				MockedWebServiceClient.Setup(x => x.GetErrors(AutogiroServiceType.LensSubscription)).Returns(expectedErrors);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Task_fetches_errors_from_webservice()
		{
			MockedWebServiceClient.Verify(x => x.GetErrors(AutogiroServiceType.LensSubscription));
		}

		[Test]
		public void Task_stores_subscription_errors_in_repository()
		{
			expectedErrors.Each(recievedError => MockedSubscriptionErrorRepository.Verify(x => 
                 x.Save(It.Is<SubscriptionError>( error =>
					  Equals(error.CreatedDate.Date, DateTime.Today) &&
					  Equals(error.HandledDate, null) &&
					  Equals(error.IsHandled, false) &&
					  Equals(error.Subscription.Id, expectedSubscription.Id) &&
					  Equals(error.BGErrorId, recievedError.ErrorId) &&
					  ExpectedErrorTypeConversionMatches(error.Type, recievedError.CommentCode)
			))));
		}

		[Test]
		public void Task_fetches_matching_subscriptions_from_repository()
		{
			expectedErrors.Each(recievedError => MockedSubscriptionRepository.Verify(x => 
				x.GetByBankgiroPayerId(It.Is<int>( id => id.Equals(recievedError.PayerNumber))
			)));
		}

		[Test]
		public void Task_loggs_start_and_stop_info_messages()
		{
			LoggingService.AssertInfo("Started");
			LoggingService.AssertInfo("Finished");
		}

		[Test]
		public void Task_sets_payment_as_handled_to_webservice()
		{
			expectedErrors.Each(error => 
				MockedWebServiceClient.Verify(x => x.SetErrorHandled(
						It.Is<RecievedError>(errorItem => errorItem.Equals(error))
			)));
		}
	}

	[TestFixture, Category("RecieveErrorsTaskTests")]
	public class When_receiving_errors_gets_exception_from_web_service : RecieveErrorsTaskTestBase
	{
		private RecievedError[] expectedErrors;
		private Subscription expectedSubscription;
		public When_receiving_errors_gets_exception_from_web_service()
		{
			Context = () =>
			{
				expectedErrors = ErrorFactory.GetList().ToArray();
				expectedSubscription = SubscriptionFactory.Get(3);

				MockedWebServiceClient.Setup(x => x.GetErrors(It.IsAny<AutogiroServiceType>())).Returns(expectedErrors);
				MockedWebServiceClient.Setup(x => x.SetErrorHandled(It.IsAny<RecievedError>())).Throws(new Exception("Test exception"));
				MockedWebServiceClient.SetupGet(x => x.State).Returns(CommunicationState.Faulted);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Task_logs_error_for_each_exception()
		{
			LoggingService.AssertError(messages => messages.Count == expectedErrors.Count());
		}

		[Test]
		public void Task_fetches_new_webclient_for_each_exception()
		{
		    A.CallTo(() => TaskRepositoryResolver.GetRepository<IBGWebServiceClient>()).MustHaveHappened(Repeated.Times(expectedErrors.Count()));
		}
	}
}