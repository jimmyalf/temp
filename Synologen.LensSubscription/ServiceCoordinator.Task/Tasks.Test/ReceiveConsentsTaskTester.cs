using System;
using System.Collections.Generic;
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
	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_executing_receive_consents_task : ReceiveConsentsTaskBase
	{
		private IEnumerable<ReceivedConsent> expectedConsents;
		private Subscription expectedSubscription;
		private static int subscriptionId = 1;

		public When_executing_receive_consents_task()
		{
			Context = () =>
			{
				expectedConsents = ConsentFactory.GetList(subscriptionId);
				expectedSubscription = SubscriptionFactory.Get(subscriptionId);

				MockedWebServiceClient.Setup(x => x.GetConsents(It.IsAny<AutogiroServiceType>())).Returns(expectedConsents.ToArray());
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Task_logs_start_and_stop_info_messages()
		{
			MockedLogger.Verify(x => x.Info(It.Is<string>(message => message.Contains("Started"))), Times.Once());
			MockedLogger.Verify(x => x.Info(It.Is<string>(message => message.Contains("Finished"))), Times.Once());
		}

		[Test]
		public void Task_logs_number_of_received_consents()
		{
			MockedLogger.Verify(x => x.Debug(It.Is<string>(message => message.Contains("Fetched 18 consent replies from bgc server"))), Times.Once());
		}

		[Test]
		public void Task_logs_after_each_handled_consent()
		{
			MockedLogger.Verify(x => x.Debug(It.Is<string>(message => message.Contains("accepted"))), Times.Exactly(1));
			MockedLogger.Verify(x => x.Debug(It.Is<string>(message => message.Contains("denied"))), Times.Exactly(17));
		}

		[Test]
		public void Task_fetches_matching_subscriptions_from_repository()
		{
			expectedConsents.Each(recievedConsent =>
              MockedSubscriptionRepository.Verify(x =>
				x.GetByBankgiroPayerId(It.Is<int>(id => id.Equals(recievedConsent.PayerNumber))
			)));
		}

		[Test]
		public void Task_sends_consenthandled_to_webservice()
		{
			expectedConsents.Each(consent => 
				MockedWebServiceClient.Verify(x => x.SetConsentHandled(
						It.Is<ReceivedConsent>(consentItem => consentItem.ConsentId.Equals(consent.ConsentId))
			)));
		}

		[Test]
		public void Task_updates_subscription_status_and_activateddate_and_saves_subsriptionerrors()
		{
			MockedSubscriptionRepository.Verify(x => x.Save(It.IsAny<Subscription>()), Times.Exactly(18));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.IsAny<SubscriptionError>()), Times.Exactly(17));
		}
	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_consent_accepted : ReceiveConsentsTaskBase
	{
		private ReceivedConsent receivedConsent;
		private Subscription expectedSubscription;
		private int subscriptionId = 1;

		public When_receiving_consent_accepted()
		{
			Context = () =>
			{
				receivedConsent = ConsentFactory.Get(subscriptionId, ConsentCommentCode.NewConsent);
				expectedSubscription = SubscriptionFactory.Get(subscriptionId);
				var consents = new [] { receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.LensSubscription)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void subscription_is_updated_with_consentstatus_and_activateddate()
		{
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(subscription => subscription.ConsentStatus.Equals(SubscriptionConsentStatus.Accepted))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(subscription => subscription.ActivatedDate.Equals(receivedConsent.ConsentValidForDate))));
		}

	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_ConsentTurnedDownByBank : ReceiveConsentsTaskBase
	{
		private ReceivedConsent receivedConsent;
		private Subscription expectedSubscription;
		private int subscriptionId = 1;

		public When_receiving_ConsentTurnedDownByBank()
		{
			Context = () =>
			{
				receivedConsent = ConsentFactory.Get(subscriptionId, ConsentCommentCode.ConsentTurnedDownByBank);
				expectedSubscription = SubscriptionFactory.Get(subscriptionId);
				var consents = new[] { receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.LensSubscription)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Subscription_is_updated_with_consentstatus()
		{
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(subscription => subscription.ConsentStatus.Equals(SubscriptionConsentStatus.Denied))));
		}

		[Test]
		public void task_adds_subscription_error_with_expected_type()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(GetSubscriptionErrorInformationCode(receivedConsent.InformationCode)))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(subscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.ConsentTurnedDownByBank))));			

		}
	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_ConsentTurnedDownByPayer : ReceiveConsentsTaskBase
	{
		private ReceivedConsent receivedConsent;
		private Subscription expectedSubscription;
		private const int subscriptionId = 1;

		public When_receiving_ConsentTurnedDownByPayer()
		{
			Context = () =>
			{
				receivedConsent = ConsentFactory.Get(subscriptionId, ConsentCommentCode.ConsentTurnedDownByPayer);
				expectedSubscription = SubscriptionFactory.Get(subscriptionId);
				var consents = new[] { receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.LensSubscription)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Subscription_is_updated_with_consentstatus()
		{
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(subscription => subscription.ConsentStatus.Equals(SubscriptionConsentStatus.Denied))));
		}

		[Test]
		public void Task_adds_subscription_error_with_expected_type()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(GetSubscriptionErrorInformationCode(receivedConsent.InformationCode)))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(subscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.ConsentTurnedDownByPayer))));			

		}
	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_AccountTypeNotApproved : ReceiveConsentsTaskBase
	{
		private ReceivedConsent receivedConsent;
		private Subscription expectedSubscription;
		private int subscriptionId = 1;

		public When_receiving_AccountTypeNotApproved()
		{
			Context = () =>
			{
				receivedConsent = ConsentFactory.Get(subscriptionId, ConsentCommentCode.AccountTypeNotApproved);
				expectedSubscription = SubscriptionFactory.Get(subscriptionId);
				var consents = new[] { receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.LensSubscription)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Subscription_is_updated_with_consentstatus()
		{
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(subscription => subscription.ConsentStatus.Equals(SubscriptionConsentStatus.Denied))));
		}

		[Test]
		public void Task_adds_subscription_error_with_expected_type()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(GetSubscriptionErrorInformationCode(receivedConsent.InformationCode)))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(subscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.AccountTypeNotApproved))));			

		}
	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_ConsentMissingInBankgiroConsentRegister : ReceiveConsentsTaskBase
	{
		private ReceivedConsent receivedConsent;
		private Subscription expectedSubscription;
		private int subscriptionId = 1;

		public When_receiving_ConsentMissingInBankgiroConsentRegister()
		{
			Context = () =>
			{
				receivedConsent = ConsentFactory.Get(subscriptionId, ConsentCommentCode.ConsentMissingInBankgiroConsentRegister);
				expectedSubscription = SubscriptionFactory.Get(subscriptionId);
				var consents = new[] { receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.LensSubscription)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Subscription_is_updated_with_consentstatus()
		{
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(subscription => subscription.ConsentStatus.Equals(SubscriptionConsentStatus.Denied))));
		}

		[Test]
		public void Task_adds_subscription_error_with_expected_type()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(GetSubscriptionErrorInformationCode(receivedConsent.InformationCode)))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(subscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.ConsentMissingInBankgiroConsentRegister))));			

		}
	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_IncorrectAccountOrPersonalIdNumber : ReceiveConsentsTaskBase
	{
		private ReceivedConsent receivedConsent;
		private Subscription expectedSubscription;
		private int subscriptionId = 1;

		public When_receiving_IncorrectAccountOrPersonalIdNumber()
		{
			Context = () =>
			{
				receivedConsent = ConsentFactory.Get(subscriptionId, ConsentCommentCode.IncorrectAccountOrPersonalIdNumber);
				expectedSubscription = SubscriptionFactory.Get(subscriptionId);
				var consents = new[] { receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.LensSubscription)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Subscription_is_updated_with_consentstatus()
		{
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(subscription => subscription.ConsentStatus.Equals(SubscriptionConsentStatus.Denied))));
		}

		[Test]
		public void Task_adds_subscription_error_with_expected_type()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(GetSubscriptionErrorInformationCode(receivedConsent.InformationCode)))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(subscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.IncorrectAccountOrPersonalIdNumber))));			

		}
	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_ConsentCanceledByBankgiro : ReceiveConsentsTaskBase
	{
		private ReceivedConsent receivedConsent;
		private Subscription expectedSubscription;
		private int subscriptionId = 1;

		public When_receiving_ConsentCanceledByBankgiro()
		{
			Context = () =>
			{
				receivedConsent = ConsentFactory.Get(subscriptionId, ConsentCommentCode.ConsentCanceledByBankgiro);
				expectedSubscription = SubscriptionFactory.Get(subscriptionId);
				var consents = new[] { receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.LensSubscription)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Subscription_is_updated_with_consentstatus()
		{
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(subscription => subscription.ConsentStatus.Equals(SubscriptionConsentStatus.Denied))));
		}

		[Test]
		public void Task_adds_subscription_error_with_expected_type()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(GetSubscriptionErrorInformationCode(receivedConsent.InformationCode)))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(subscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.ConsentCanceledByBankgiro))));			
		}
	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_ConsentCanceledByBankgiroBecauseOfMissingStatement : ReceiveConsentsTaskBase
	{
		private ReceivedConsent receivedConsent;
		private Subscription expectedSubscription;
		private int subscriptionId = 1;

		public When_receiving_ConsentCanceledByBankgiroBecauseOfMissingStatement()
		{
			Context = () =>
			{
				receivedConsent = ConsentFactory.Get(subscriptionId, ConsentCommentCode.ConsentCanceledByBankgiroBecauseOfMissingStatement);
				expectedSubscription = SubscriptionFactory.Get(subscriptionId);
				var consents = new[] { receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.LensSubscription)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Subscription_is_updated_with_consentstatus()
		{
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(subscription => subscription.ConsentStatus.Equals(SubscriptionConsentStatus.Denied))));
		}

		[Test]
		public void Task_adds_subscription_error_with_expected_type()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(GetSubscriptionErrorInformationCode(receivedConsent.InformationCode)))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(subscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.ConsentCanceledByBankgiroBecauseOfMissingStatement))));			
		}
	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsideration : ReceiveConsentsTaskBase
	{
		private ReceivedConsent receivedConsent;
		private Subscription expectedSubscription;
		private int subscriptionId = 1;

		public When_receiving_ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsideration()
		{
			Context = () =>
			{
				receivedConsent = ConsentFactory.Get(subscriptionId, ConsentCommentCode.ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsideration);
				expectedSubscription = SubscriptionFactory.Get(subscriptionId);
				var consents = new[] { receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.LensSubscription)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Subscription_is_updated_with_consentstatus()
		{
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(subscription => subscription.ConsentStatus.Equals(SubscriptionConsentStatus.Denied))));
		}

		[Test]
		public void Task_adds_subscription_error_with_expected_type()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(GetSubscriptionErrorInformationCode(receivedConsent.InformationCode)))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(subscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsederation))));			
		}
	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_ConsentTemporarilyStoppedByPayer : ReceiveConsentsTaskBase
	{
		private ReceivedConsent receivedConsent;
		private Subscription expectedSubscription;
		private int subscriptionId = 1;

		public When_receiving_ConsentTemporarilyStoppedByPayer()
		{
			Context = () =>
			{
				receivedConsent = ConsentFactory.Get(subscriptionId, ConsentCommentCode.ConsentTemporarilyStoppedByPayer);
				expectedSubscription = SubscriptionFactory.Get(subscriptionId);
				var consents = new[] { receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.LensSubscription)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Subscription_is_updated_with_consentstatus()
		{
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(subscription => subscription.ConsentStatus.Equals(SubscriptionConsentStatus.Denied))));
		}

		[Test]
		public void Task_adds_subscription_error_with_expected_type()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(GetSubscriptionErrorInformationCode(receivedConsent.InformationCode)))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(subscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.ConsentTemporarilyStoppedByPayer))));			
		}

	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_TemporaryConsentStopRevoked : ReceiveConsentsTaskBase
	{
		private ReceivedConsent receivedConsent;
		private Subscription expectedSubscription;
		private int subscriptionId = 1;

		public When_receiving_TemporaryConsentStopRevoked()
		{
			Context = () =>
			{
				receivedConsent = ConsentFactory.Get(subscriptionId, ConsentCommentCode.TemporaryConsentStopRevoked);
				expectedSubscription = SubscriptionFactory.Get(subscriptionId);
				var consents = new[] { receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.LensSubscription)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Subscription_is_updated_with_consentstatus()
		{
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(subscription => subscription.ConsentStatus.Equals(SubscriptionConsentStatus.Denied))));
		}

		[Test]
		public void Task_adds_subscription_error_with_expected_type()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(GetSubscriptionErrorInformationCode(receivedConsent.InformationCode)))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(subscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.TemporaryConsentStopRevoked))));			
		}

	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_IncorrectPersonalIdNumber : ReceiveConsentsTaskBase
	{
		private ReceivedConsent receivedConsent;
		private Subscription expectedSubscription;
		private int subscriptionId = 1;

		public When_receiving_IncorrectPersonalIdNumber()
		{
			Context = () =>
			{
				receivedConsent = ConsentFactory.Get(subscriptionId, ConsentCommentCode.IncorrectPersonalIdNumber);
				expectedSubscription = SubscriptionFactory.Get(subscriptionId);
				var consents = new[] { receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.LensSubscription)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Subscription_is_updated_with_consentstatus()
		{
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(subscription => subscription.ConsentStatus.Equals(SubscriptionConsentStatus.Denied))));
		}

		[Test]
		public void Task_adds_subscription_error_with_expected_type()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(GetSubscriptionErrorInformationCode(receivedConsent.InformationCode)))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(subscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.IncorrectPersonalIdNumber))));			
		}

	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_IncorrectAccountNumber : ReceiveConsentsTaskBase
	{
		private ReceivedConsent receivedConsent;
		private Subscription expectedSubscription;
		private int subscriptionId = 1;

		public When_receiving_IncorrectAccountNumber()
		{
			Context = () =>
			{
				receivedConsent = ConsentFactory.Get(subscriptionId, ConsentCommentCode.IncorrectAccountNumber);
				expectedSubscription = SubscriptionFactory.Get(subscriptionId);
				var consents = new[] { receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.LensSubscription)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Subscription_is_updated_with_consentstatus()
		{
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(subscription => subscription.ConsentStatus.Equals(SubscriptionConsentStatus.Denied))));
		}

		[Test]
		public void Task_adds_subscription_error_with_expected_type()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(GetSubscriptionErrorInformationCode(receivedConsent.InformationCode)))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(subscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.IncorrectAccountNumber))));			
		}

	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_MaxAmountNotAllowed : ReceiveConsentsTaskBase
	{
		private ReceivedConsent receivedConsent;
		private Subscription expectedSubscription;
		private int subscriptionId = 1;

		public When_receiving_MaxAmountNotAllowed()
		{
			Context = () =>
			{
				receivedConsent = ConsentFactory.Get(subscriptionId, ConsentCommentCode.MaxAmountNotAllowed);
				expectedSubscription = SubscriptionFactory.Get(subscriptionId);
				var consents = new[] { receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.LensSubscription)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Subscription_is_updated_with_consentstatus()
		{
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(subscription => subscription.ConsentStatus.Equals(SubscriptionConsentStatus.Denied))));
		}

		[Test]
		public void Task_adds_subscription_error_with_expected_type()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(GetSubscriptionErrorInformationCode(receivedConsent.InformationCode)))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(subscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.MaxAmountNotAllowed))));			
		}

	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_IncorrectPaymentReceiverBankgiroNumber : ReceiveConsentsTaskBase
	{
		private ReceivedConsent receivedConsent;
		private Subscription expectedSubscription;
		private int subscriptionId = 1;

		public When_receiving_IncorrectPaymentReceiverBankgiroNumber()
		{
			Context = () =>
			{
				receivedConsent = ConsentFactory.Get(subscriptionId, ConsentCommentCode.IncorrectPaymentReceiverBankgiroNumber);
				expectedSubscription = SubscriptionFactory.Get(subscriptionId);
				var consents = new[] { receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.LensSubscription)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Subscription_is_updated_with_consentstatus()
		{
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(subscription => subscription.ConsentStatus.Equals(SubscriptionConsentStatus.Denied))));
		}

		[Test]
		public void Task_adds_subscription_error_with_expected_type()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(GetSubscriptionErrorInformationCode(receivedConsent.InformationCode)))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(subscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.IncorrectPaymentReceiverBankgiroNumber))));			
		}

	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_PaymentReceiverBankgiroNumberMissing : ReceiveConsentsTaskBase
	{
		private ReceivedConsent receivedConsent;
		private Subscription expectedSubscription;
		private int subscriptionId = 1;

		public When_receiving_PaymentReceiverBankgiroNumberMissing()
		{
			Context = () =>
			{
				receivedConsent = ConsentFactory.Get(subscriptionId, ConsentCommentCode.PaymentReceiverBankgiroNumberMissing);
				expectedSubscription = SubscriptionFactory.Get(subscriptionId);
				var consents = new[] { receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.LensSubscription)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Subscription_is_updated_with_consentstatus()
		{
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(subscription => subscription.ConsentStatus.Equals(SubscriptionConsentStatus.Denied))));
		}

		[Test]
		public void Task_adds_subscription_error_with_expected_type()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(GetSubscriptionErrorInformationCode(receivedConsent.InformationCode)))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(subscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.PaymentReceiverBankgiroNumberMissing))));			
		}

	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_Canceled : ReceiveConsentsTaskBase
	{
		private ReceivedConsent receivedConsent;
		private Subscription expectedSubscription;
		private int subscriptionId = 1;

		public When_receiving_Canceled()
		{
			Context = () =>
			{
				receivedConsent = ConsentFactory.Get(subscriptionId, ConsentCommentCode.Canceled);
				expectedSubscription = SubscriptionFactory.Get(subscriptionId);
				var consents = new[] { receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.LensSubscription)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Subscription_is_updated_with_consentstatus()
		{
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(subscription => subscription.ConsentStatus.Equals(SubscriptionConsentStatus.Denied))));
		}

		[Test]
		public void Task_adds_subscription_error_with_expected_type()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(GetSubscriptionErrorInformationCode(receivedConsent.InformationCode)))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(subscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.Canceled))));			
		}
	}
}