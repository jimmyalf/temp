using System;
using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Factories;
using Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test
{
	[TestFixture, Category("SendPaymentsTaskTests")]
	public class When_sending_payments : SendPaymentsTaskTestBase
	{
		private IList<BGPaymentToSend> paymentsToSend;
		private string paymentRecieverBankGiroNumber;
		private string paymentRecieverCustomerNumber;
		private string fileData;

		public When_sending_payments()
		{
			Context = () =>
			{
				paymentRecieverBankGiroNumber = "555555";
				paymentRecieverCustomerNumber = "123456";
				fileData = PaymentsFactory.GetTestPaymentFileData();
				paymentsToSend = PaymentsFactory.GetList();
				A.CallTo(() => BGPaymentToSendRepository.FindBy(A<AllNewPaymentsToSendCriteria>.Ignored.Argument)).Returns(paymentsToSend);
				A.CallTo(() => BgConfigurationSettingsService.GetPaymentRecieverBankGiroNumber()).Returns(paymentRecieverBankGiroNumber);
				A.CallTo(() => BgConfigurationSettingsService.GetPaymentRevieverCustomerNumber()).Returns(paymentRecieverCustomerNumber);
				A.CallTo(() => PaymentFileWriter.Write(A<PaymentsFile>.Ignored)).Returns(fileData);
			};
			Because = task => task.Execute();
		}

		[Test]
		public void Task_has_send_task_ordering()
		{
			Task.TaskOrder.ShouldBe(BGTaskSequenceOrder.SendTask.ToInteger());
		}

		[Test]
		public void Task_loggs_start_and_stop_messages()
		{
		    A.CallTo(() => Log.Info(A<string>.That.Contains("Started"))).MustHaveHappened();
		    A.CallTo(() => Log.Info(A<string>.That.Contains("Finished"))).MustHaveHappened();
		}

		[Test]
		public void Task_fetches_new_payments_from_repository()
		{
			A.CallTo(() => BGPaymentToSendRepository.FindBy(A<AllNewPaymentsToSendCriteria>.Ignored.Argument))
				.MustHaveHappened();
		}

		[Test]
		public void Task_parses_fetched_payments_into_payment_file()
		{
		    A.CallTo(() => PaymentFileWriter.Write(
		        A<PaymentsFile>
					.That.Matches(x => MatchPayments(x.Posts, paymentsToSend, paymentRecieverBankGiroNumber))
					.And.Matches(x => x.Reciever.BankgiroNumber.Equals(paymentRecieverBankGiroNumber))
					.And.Matches(x => x.Reciever.CustomerNumber.Equals(paymentRecieverCustomerNumber))
					.And.Matches(x => x.WriteDate.Date.Equals(DateTime.Now.Date))
		    )).MustHaveHappened();
		}

		[Test]
		public void Task_stores_payments_as_a_file_section_to_send()
		{
			A.CallTo(() => FileSectionToSendRepository.Save(
				A<FileSectionToSend>
				.That.Matches(x => x.CreatedDate.Date.Equals(DateTime.Now.Date))
				.And.Matches(x => x.HasBeenSent.Equals(false))
				.And.Matches(x => x.SectionData.Equals(fileData))
				.And.Matches(x => Equals(x.SentDate, null))
				.And.Matches(x => x.Type.Equals(SectionType.PaymentsToSend))
				.And.Matches(x => x.TypeName.Equals("PaymentsToSend"))
			)).MustHaveHappened();
		}

		[Test]
		public void Task_updates_payments_as_sent()
		{
			paymentsToSend.Each(paymentToSend => A.CallTo(() => BGPaymentToSendRepository.Save(
        		A<BGPaymentToSend>
        			.That.Matches(x => x.Id.Equals(paymentToSend.Id))
        			.And.Matches(x => Equals(x.SendDate.Value.Date, DateTime.Now.Date))
            )).MustHaveHappened());
		}
	}
}