using System;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.Service.Client.SubscriptionTaskRunner.Test.TestHelpers;

namespace Synologen.Service.Client.SubscriptionTaskRunner.Test
{
	[TestFixture, Category("AutogiroPaymentServiceTests")]
	public class When_fetching_date_from_AutogiroPaymentService_before_cut_off_date : AutogiroPaymentServiceTestBase
	{
		private DateTime currentDate;
		private int paymentDayInMonth;
		private DateTime expectedPaymentDate;
		private int paymentCutOffDayInMonth;

		public When_fetching_date_from_AutogiroPaymentService_before_cut_off_date()
		{
			Context = () =>
			{
				currentDate = new DateTime(2011, 01, 10);
				paymentDayInMonth = 25;
				paymentCutOffDayInMonth = 22;
				expectedPaymentDate = new DateTime(currentDate.Year, currentDate.Month, paymentDayInMonth);
				A.CallTo(() => ServiceCoordinatorSettingsService.GetPaymentDayInMonth()).Returns(paymentDayInMonth);
				A.CallTo(() => ServiceCoordinatorSettingsService.GetPaymentCutOffDayInMonth()).Returns(paymentCutOffDayInMonth);
			};
			Because = service => SystemTime.ReturnWhileTimeIs(currentDate, () => service.GetPaymentDate());
		}

		[Test]
		public void Returned_payment_date_is_in_current_month()
		{
			ReturnValue.ShouldBe(expectedPaymentDate);
		}
	}

	[TestFixture, Category("AutogiroPaymentServiceTests")]
	public class When_fetching_date_from_AutogiroPaymentService_after_cut_off_date : AutogiroPaymentServiceTestBase
	{
		private DateTime currentDate;
		private DateTime expectedPaymentDate;
		private int paymentDayInMonth;
		private int paymentCutOffDayInMonth;

		public When_fetching_date_from_AutogiroPaymentService_after_cut_off_date()
		{
			Context = () =>
			{
				currentDate = new DateTime(2011, 01, 23);
				paymentDayInMonth = 25;
				paymentCutOffDayInMonth = 22;
				expectedPaymentDate = new DateTime(currentDate.Year, currentDate.Month, paymentDayInMonth).AddMonths(1);
				A.CallTo(() => ServiceCoordinatorSettingsService.GetPaymentDayInMonth()).Returns(paymentDayInMonth);
				A.CallTo(() => ServiceCoordinatorSettingsService.GetPaymentCutOffDayInMonth()).Returns(paymentCutOffDayInMonth);
			};
			Because = service => SystemTime.ReturnWhileTimeIs(currentDate, () => service.GetPaymentDate());
		}

		[Test]
		public void Returned_payment_date_is_in_next_month()
		{
			ReturnValue.ShouldBe(expectedPaymentDate);
		}
	}
}