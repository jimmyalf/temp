using System;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.Orders
{
	[TestFixture, Category("OrderWithdrawalService_Tests")]
	public class When_subscription_is_consent_and_created_before_transfer_date : OrderWithdrawalServiceTestbase
	{
		private readonly DateTime _subscriptionCreatedDate;
		private readonly DateTime _expectedWithdrawalDate;

		public When_subscription_is_consent_and_created_before_transfer_date()
		{
			_subscriptionCreatedDate = new DateTime(2012, 01, SubscriptionWithdrawalTransferDay).AddDays(-1);
			_expectedWithdrawalDate = new DateTime(2012, 01, SubscriptionWithdrawalDay);			
		}

		[Test]
		public void Withdrawal_Date_Should_Be_In_This_Month()
		{
			var withdrawalDate = Service.GetExpectedFirstWithdrawalDate(_subscriptionCreatedDate, true);
			withdrawalDate.ShouldBe(_expectedWithdrawalDate);
		}
	}

	[TestFixture, Category("OrderWithdrawalService_Tests")]
	public class When_subscription_is_consent_and_created_after_transfer_date : OrderWithdrawalServiceTestbase
	{
		private readonly DateTime _subscriptionCreatedDate;
		private readonly DateTime _expectedWithdrawalDate;

		public When_subscription_is_consent_and_created_after_transfer_date()
		{
			_subscriptionCreatedDate = new DateTime(2012, 01, SubscriptionWithdrawalTransferDay).AddDays(1);
			_expectedWithdrawalDate = new DateTime(2012, 02, SubscriptionWithdrawalDay);			
		}

		[Test]
		public void Withdrawal_Date_Should_Be_In_Next_Month()
		{
			var withdrawalDate = Service.GetExpectedFirstWithdrawalDate(_subscriptionCreatedDate, true);
			withdrawalDate.ShouldBe(_expectedWithdrawalDate);
		}
	}

	[TestFixture, Category("OrderWithdrawalService_Tests")]
	public class When_subscription_is_not_consented_and_created_before_consent_cut_off_date : OrderWithdrawalServiceTestbase
	{
		private readonly DateTime _subscriptionCreatedDate;
		private readonly DateTime _expectedWithdrawalDate;

		public When_subscription_is_not_consented_and_created_before_consent_cut_off_date()
		{
			_subscriptionCreatedDate = new DateTime(2012, 01, SubscriptionConsentCutoffDay).AddDays(-1);
			_expectedWithdrawalDate = new DateTime(2012, 01, SubscriptionWithdrawalDay);			
		}

		[Test]
		public void Withdrawal_Date_Should_Be_In_This_Month()
		{
			var withdrawalDate = Service.GetExpectedFirstWithdrawalDate(_subscriptionCreatedDate, false);
			withdrawalDate.ShouldBe(_expectedWithdrawalDate);
		}
	}

	[TestFixture, Category("OrderWithdrawalService_Tests")]
	public class When_subscription_is_not_consented_and_created_after_consent_cut_off_date : OrderWithdrawalServiceTestbase
	{
		private readonly DateTime _subscriptionCreatedDate;
		private readonly DateTime _expectedWithdrawalDate;

		public When_subscription_is_not_consented_and_created_after_consent_cut_off_date()
		{
			_subscriptionCreatedDate = new DateTime(2012, 01, SubscriptionConsentCutoffDay).AddDays(1);
			_expectedWithdrawalDate = new DateTime(2012, 02, SubscriptionWithdrawalDay);
		}

		[Test]
		public void Withdrawal_Date_Should_Be_In_Next_Month()
		{
			var withdrawalDate = Service.GetExpectedFirstWithdrawalDate(_subscriptionCreatedDate, false);
			withdrawalDate.ShouldBe(_expectedWithdrawalDate);
		}
	}

	public abstract class OrderWithdrawalServiceTestbase
	{
		protected readonly ISynologenSettingsService SettingsService;
		protected readonly OrderWithdrawalService Service;
		protected const int SubscriptionConsentCutoffDay = 15;
		protected const int SubscriptionWithdrawalTransferDay = 23;
		protected const int SubscriptionWithdrawalDay = 28;

		protected OrderWithdrawalServiceTestbase()
		{

			SettingsService = A.Fake<ISynologenSettingsService>();
			Service = new OrderWithdrawalService(SettingsService);
			A.CallTo(() => SettingsService.SubscriptionConsentCutoffDay).Returns(SubscriptionConsentCutoffDay);
			A.CallTo(() => SettingsService.SubscriptionWithdrawalTransferDay).Returns(SubscriptionWithdrawalTransferDay);
			A.CallTo(() => SettingsService.SubscriptionWithdrawalDay).Returns(SubscriptionWithdrawalDay);		
		}
	}
}