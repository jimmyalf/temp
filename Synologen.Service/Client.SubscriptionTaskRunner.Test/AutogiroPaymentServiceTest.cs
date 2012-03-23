using System;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.Service.Client.SubscriptionTaskRunner.Test.TestHelpers;

namespace Synologen.Service.Client.SubscriptionTaskRunner.Test
{
	[TestFixture, Category("AutogiroPaymentServiceTests")]
	public class When_fetching_date_from_AutogiroPaymentService : AutogiroPaymentServiceTestBase
	{
		private DateTime _currentDate;
		private int _paymentDayInMonth;
		private DateTime _expectedPaymentDate;
		private int _paymentCutOffDayInMonth;

		public When_fetching_date_from_AutogiroPaymentService()
		{
			Context = () =>
			{
				_currentDate = new DateTime(2011, 01, 10);
				_paymentDayInMonth = 25;
				_paymentCutOffDayInMonth = 22;
				_expectedPaymentDate = new DateTime(_currentDate.Year, _currentDate.Month, _paymentDayInMonth);
				A.CallTo(() => ServiceCoordinatorSettingsService.GetPaymentDayInMonth()).Returns(_paymentDayInMonth);
				A.CallTo(() => ServiceCoordinatorSettingsService.GetPaymentCutOffDayInMonth()).Returns(_paymentCutOffDayInMonth);
			};
			Because = service => SystemTime.ReturnWhileTimeIs(_currentDate, service.GetPaymentDate);
		}

		[Test]
		public void Returned_payment_date_is_expected_date()
		{
			ReturnValue.ShouldBe(_expectedPaymentDate);
		}
	}
}