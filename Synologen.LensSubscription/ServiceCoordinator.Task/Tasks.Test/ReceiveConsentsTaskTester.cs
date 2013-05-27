using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using FakeItEasy;
using Moq;
using NUnit.Framework;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Spinit.Wpc.Synologen.Core.Domain.Testing;
using Synologen.LensSubscription.ServiceCoordinator.Task.Test.Factories;
using Synologen.LensSubscription.ServiceCoordinator.Task.Test.TestHelpers;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test
{
	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_executing_receive_consents_task : ReceiveConsentsTaskBase
	{
		private IEnumerable<ReceivedConsent> _expectedConsents;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;

		public When_executing_receive_consents_task()
		{
			Context = () =>
			{
				_expectedConsents = ConsentFactory.GetList(SubscriptionId);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);

				MockedWebServiceClient.Setup(x => x.GetConsents(It.IsAny<AutogiroServiceType>())).Returns(_expectedConsents.ToArray());
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Task_logs_start_and_stop_info_messages()
		{
			LoggingService.AssertInfo("Started");
			LoggingService.AssertInfo("Finished");
		}

		[Test]
		public void Task_logs_number_of_received_consents()
		{
			LoggingService.AssertDebug("Fetched 18 consent replies from bgc server");
		}

		[Test]
		public void Task_logs_after_each_handled_consent()
		{
			LoggingService.AssertDebug("accepted");
			LoggingService.AssertDebug(messages => messages.Count(x => x.Contains("denied")) == 17);
		}

		[Test]
		public void Task_fetches_matching_subscriptions_from_repository()
		{
			_expectedConsents.Each(recievedConsent =>
              MockedSubscriptionRepository.Verify(x =>
				x.GetByBankgiroPayerId(It.Is<int>(id => id.Equals(recievedConsent.PayerNumber))
			)));
		}

		[Test]
		public void Task_sends_consenthandled_to_webservice()
		{
			_expectedConsents.Each(consent => 
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
		private ReceivedConsent _receivedConsent;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;

		public When_receiving_consent_accepted()
		{
			Context = () =>
			{
				_receivedConsent = ConsentFactory.Get(SubscriptionId, ConsentCommentCode.NewConsent);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);
				var consents = new [] { _receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.SubscriptionVersion2)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void subscription_is_updated_with_consentstatus_and_activateddate()
		{
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(subscription => subscription.ConsentStatus.Equals(SubscriptionConsentStatus.Accepted))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(subscription => subscription.ConsentedDate.Equals(_receivedConsent.ConsentValidForDate))));
		}

	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_ConsentTurnedDownByBank : ReceiveConsentsTaskBase
	{
		private ReceivedConsent _receivedConsent;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;

		public When_receiving_ConsentTurnedDownByBank()
		{
			Context = () =>
			{
				_receivedConsent = ConsentFactory.Get(SubscriptionId, ConsentCommentCode.ConsentTurnedDownByBank);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);
				var consents = new[] { _receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.SubscriptionVersion2)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
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
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(_receivedConsent.InformationCode))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(SubscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.ConsentTurnedDownByBank))));			
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.BGConsentId.Equals(_receivedConsent.ConsentId))));

		}
	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_ConsentTurnedDownByPayer : ReceiveConsentsTaskBase
	{
		private ReceivedConsent _receivedConsent;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;

		public When_receiving_ConsentTurnedDownByPayer()
		{
			Context = () =>
			{
				_receivedConsent = ConsentFactory.Get(SubscriptionId, ConsentCommentCode.ConsentTurnedDownByPayer);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);
				var consents = new[] { _receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.SubscriptionVersion2)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
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
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(_receivedConsent.InformationCode))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(SubscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.ConsentTurnedDownByPayer))));			
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.BGConsentId.Equals(_receivedConsent.ConsentId))));
		}
	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_AccountTypeNotApproved : ReceiveConsentsTaskBase
	{
		private ReceivedConsent _receivedConsent;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;

		public When_receiving_AccountTypeNotApproved()
		{
			Context = () =>
			{
				_receivedConsent = ConsentFactory.Get(SubscriptionId, ConsentCommentCode.AccountTypeNotApproved);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);
				var consents = new[] { _receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.SubscriptionVersion2)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
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
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(_receivedConsent.InformationCode))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(SubscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.AccountTypeNotApproved))));			
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.BGConsentId.Equals(_receivedConsent.ConsentId))));
		}
	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_ConsentMissingInBankgiroConsentRegister : ReceiveConsentsTaskBase
	{
		private ReceivedConsent _receivedConsent;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;

		public When_receiving_ConsentMissingInBankgiroConsentRegister()
		{
			Context = () =>
			{
				_receivedConsent = ConsentFactory.Get(SubscriptionId, ConsentCommentCode.ConsentMissingInBankgiroConsentRegister);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);
				var consents = new[] { _receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.SubscriptionVersion2)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
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
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(_receivedConsent.InformationCode))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(SubscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.ConsentMissingInBankgiroConsentRegister))));			
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.BGConsentId.Equals(_receivedConsent.ConsentId))));
		}
	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_IncorrectAccountOrPersonalIdNumber : ReceiveConsentsTaskBase
	{
		private ReceivedConsent _receivedConsent;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;

		public When_receiving_IncorrectAccountOrPersonalIdNumber()
		{
			Context = () =>
			{
				_receivedConsent = ConsentFactory.Get(SubscriptionId, ConsentCommentCode.IncorrectAccountOrPersonalIdNumber);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);
				var consents = new[] { _receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.SubscriptionVersion2)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
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
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(_receivedConsent.InformationCode))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(SubscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.IncorrectAccountOrPersonalIdNumber))));			
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.BGConsentId.Equals(_receivedConsent.ConsentId))));
		}
	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_ConsentCanceledByBankgiro : ReceiveConsentsTaskBase
	{
		private ReceivedConsent _receivedConsent;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;

		public When_receiving_ConsentCanceledByBankgiro()
		{
			Context = () =>
			{
				_receivedConsent = ConsentFactory.Get(SubscriptionId, ConsentCommentCode.ConsentCanceledByBankgiro);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);
				var consents = new[] { _receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.SubscriptionVersion2)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
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
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(_receivedConsent.InformationCode))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(SubscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.ConsentCanceledByBankgiro))));			
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.BGConsentId.Equals(_receivedConsent.ConsentId))));
		}
	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_ConsentCanceledByBankgiroBecauseOfMissingStatement : ReceiveConsentsTaskBase
	{
		private ReceivedConsent _receivedConsent;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;

		public When_receiving_ConsentCanceledByBankgiroBecauseOfMissingStatement()
		{
			Context = () =>
			{
				_receivedConsent = ConsentFactory.Get(SubscriptionId, ConsentCommentCode.ConsentCanceledByBankgiroBecauseOfMissingStatement);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);
				var consents = new[] { _receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.SubscriptionVersion2)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
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
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(_receivedConsent.InformationCode))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(SubscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.ConsentCanceledByBankgiroBecauseOfMissingStatement))));			
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.BGConsentId.Equals(_receivedConsent.ConsentId))));
		}
	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsideration : ReceiveConsentsTaskBase
	{
		private ReceivedConsent _receivedConsent;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;

		public When_receiving_ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsideration()
		{
			Context = () =>
			{
				_receivedConsent = ConsentFactory.Get(SubscriptionId, ConsentCommentCode.ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsideration);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);
				var consents = new[] { _receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.SubscriptionVersion2)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
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
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(_receivedConsent.InformationCode))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(SubscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsederation))));			
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.BGConsentId.Equals(_receivedConsent.ConsentId))));
		}
	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_ConsentTemporarilyStoppedByPayer : ReceiveConsentsTaskBase
	{
		private ReceivedConsent _receivedConsent;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;

		public When_receiving_ConsentTemporarilyStoppedByPayer()
		{
			Context = () =>
			{
				_receivedConsent = ConsentFactory.Get(SubscriptionId, ConsentCommentCode.ConsentTemporarilyStoppedByPayer);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);
				var consents = new[] { _receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.SubscriptionVersion2)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
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
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(_receivedConsent.InformationCode))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(SubscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.ConsentTemporarilyStoppedByPayer))));			
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.BGConsentId.Equals(_receivedConsent.ConsentId))));
		}

	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_TemporaryConsentStopRevoked : ReceiveConsentsTaskBase
	{
		private ReceivedConsent _receivedConsent;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;

		public When_receiving_TemporaryConsentStopRevoked()
		{
			Context = () =>
			{
				_receivedConsent = ConsentFactory.Get(SubscriptionId, ConsentCommentCode.TemporaryConsentStopRevoked);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);
				var consents = new[] { _receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.SubscriptionVersion2)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
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
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(_receivedConsent.InformationCode))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(SubscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.TemporaryConsentStopRevoked))));			
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.BGConsentId.Equals(_receivedConsent.ConsentId))));
		}

	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_IncorrectPersonalIdNumber : ReceiveConsentsTaskBase
	{
		private ReceivedConsent _receivedConsent;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;

		public When_receiving_IncorrectPersonalIdNumber()
		{
			Context = () =>
			{
				_receivedConsent = ConsentFactory.Get(SubscriptionId, ConsentCommentCode.IncorrectPersonalIdNumber);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);
				var consents = new[] { _receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.SubscriptionVersion2)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
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
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(_receivedConsent.InformationCode))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(SubscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.IncorrectPersonalIdNumber))));			
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.BGConsentId.Equals(_receivedConsent.ConsentId))));
		}

	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_IncorrectAccountNumber : ReceiveConsentsTaskBase
	{
		private ReceivedConsent _receivedConsent;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;

		public When_receiving_IncorrectAccountNumber()
		{
			Context = () =>
			{
				_receivedConsent = ConsentFactory.Get(SubscriptionId, ConsentCommentCode.IncorrectAccountNumber);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);
				var consents = new[] { _receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.SubscriptionVersion2)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
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
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(_receivedConsent.InformationCode))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(SubscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.IncorrectAccountNumber))));			
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.BGConsentId.Equals(_receivedConsent.ConsentId))));
		}

	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_MaxAmountNotAllowed : ReceiveConsentsTaskBase
	{
		private ReceivedConsent _receivedConsent;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;

		public When_receiving_MaxAmountNotAllowed()
		{
			Context = () =>
			{
				_receivedConsent = ConsentFactory.Get(SubscriptionId, ConsentCommentCode.MaxAmountNotAllowed);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);
				var consents = new[] { _receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.SubscriptionVersion2)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
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
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(_receivedConsent.InformationCode))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(SubscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.MaxAmountNotAllowed))));			
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.BGConsentId.Equals(_receivedConsent.ConsentId))));
		}

	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_IncorrectPaymentReceiverBankgiroNumber : ReceiveConsentsTaskBase
	{
		private ReceivedConsent _receivedConsent;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;

		public When_receiving_IncorrectPaymentReceiverBankgiroNumber()
		{
			Context = () =>
			{
				_receivedConsent = ConsentFactory.Get(SubscriptionId, ConsentCommentCode.IncorrectPaymentReceiverBankgiroNumber);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);
				var consents = new[] { _receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.SubscriptionVersion2)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
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
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(_receivedConsent.InformationCode))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(SubscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.IncorrectPaymentReceiverBankgiroNumber))));			
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.BGConsentId.Equals(_receivedConsent.ConsentId))));
		}

	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_PaymentReceiverBankgiroNumberMissing : ReceiveConsentsTaskBase
	{
		private ReceivedConsent _receivedConsent;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;

		public When_receiving_PaymentReceiverBankgiroNumberMissing()
		{
			Context = () =>
			{
				_receivedConsent = ConsentFactory.Get(SubscriptionId, ConsentCommentCode.PaymentReceiverBankgiroNumberMissing);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);
				var consents = new[] { _receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.SubscriptionVersion2)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
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
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(_receivedConsent.InformationCode))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(SubscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.PaymentReceiverBankgiroNumberMissing))));			
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.BGConsentId.Equals(_receivedConsent.ConsentId))));
		}

	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_receiving_Canceled : ReceiveConsentsTaskBase
	{
		private ReceivedConsent _receivedConsent;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;

		public When_receiving_Canceled()
		{
			Context = () =>
			{
				_receivedConsent = ConsentFactory.Get(SubscriptionId, ConsentCommentCode.Canceled);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);
				var consents = new[] { _receivedConsent };

				MockedWebServiceClient.Setup(x => x.GetConsents(AutogiroServiceType.SubscriptionVersion2)).Returns(consents);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
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
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(_receivedConsent.InformationCode))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Date.Equals(DateTime.Now.Date))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(SubscriptionId))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.Canceled))));			
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.BGConsentId.Equals(_receivedConsent.ConsentId))));
		}
	}

	[TestFixture, Category("ReceiveConsentsTaskTests")]
	public class When_executing_receive_consents_task_gets_exception_from_web_service : ReceiveConsentsTaskBase
	{
		private IEnumerable<ReceivedConsent> _expectedConsents;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;

		public When_executing_receive_consents_task_gets_exception_from_web_service()
		{
			Context = () =>
			{
				_expectedConsents = ConsentFactory.GetList(SubscriptionId);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);

				MockedWebServiceClient.Setup(x => x.GetConsents(It.IsAny<AutogiroServiceType>())).Returns(_expectedConsents.ToArray());
				MockedWebServiceClient.Setup(x => x.SetConsentHandled(It.IsAny<ReceivedConsent>())).Throws(new Exception("Test exception"));
				MockedWebServiceClient.SetupGet(x => x.State).Returns(CommunicationState.Faulted);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}
		protected override void SetUp()
		{
			TestRunnerDetector.Disable();
			base.SetUp();
		}

		protected override void TearDown()
		{
			base.TearDown();
			TestRunnerDetector.Enable();
		}

		[Test]
		public void Task_logs_error_for_each_exception()
		{
			LoggingService.AssertError(messages => messages.Count == _expectedConsents.Count());
		}

		[Test]
		public void Task_fetches_new_webclient_for_each_exception()
		{
		    A.CallTo(() => TaskRepositoryResolver.GetRepository<IBGWebServiceClient>()).MustHaveHappened(Repeated.Exactly.Times(_expectedConsents.Count()));
		}
	}
}