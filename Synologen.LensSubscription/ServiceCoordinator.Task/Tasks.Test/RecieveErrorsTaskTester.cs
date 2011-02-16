using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Synologen.LensSubscription.ServiceCoordinator.Task.Test.Factories;
using Synologen.LensSubscription.ServiceCoordinator.Task.Test.TestHelpers;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test
{
	[TestFixture]
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
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(expectedSubscription);
				MockedWebServiceClient.Setup(x => x.GetNewErrors()).Returns(expectedErrors);
			};
			Because = task => task.Execute();
		}

		[Test]
		public void Task_fetches_errors_from_webservice()
		{
			MockedWebServiceClient.Verify(x => x.GetNewErrors());
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
					  ExpectedErrorTypeConversionMatches(error.Type, recievedError.CommentCode)
			))));
		}

		[Test]
		public void Task_fetches_matching_subscriptions_from_repository()
		{
			expectedErrors.Each(recievedError => MockedSubscriptionRepository.Verify(x => 
				x.Get(It.Is<int>( id => id.Equals(recievedError.PayerNumber))
			)));
		}

		[Test]
		public void Task_loggs_start_and_stop_info_messages()
		{
			MockedLogger.Verify(x => x.Info(It.Is<string>(message => message.Contains("Started"))), Times.Once());
			MockedLogger.Verify(x => x.Info(It.Is<string>(message => message.Contains("Finished"))), Times.Once());
		}

		private static bool ExpectedErrorTypeConversionMatches(SubscriptionErrorType subscriptionErrorType, ErrorType errorType)
		{
			if(errorType ==  ErrorType.ConsentMissing)
			{
				return subscriptionErrorType == SubscriptionErrorType.ConsentMissing;
			}
			if(errorType ==  ErrorType.AccountNotYetApproved)
			{
				return subscriptionErrorType == SubscriptionErrorType.NotApproved;
			}
			if(errorType ==  ErrorType.ConsentStopped)
			{
				return subscriptionErrorType == SubscriptionErrorType.CosentStopped;
			}
			if(errorType ==  ErrorType.NotYetDebitable)
			{
				return subscriptionErrorType == SubscriptionErrorType.NotDebitable;
			}
			throw new AssertionException("No Matching enum");
		}
	}
}