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
		private AutogiroPayer payer;

		public When_sending_payments()
		{
			Context = () =>
			{
				paymentRecieverBankGiroNumber = "555555";
				paymentRecieverCustomerNumber = "123456";
				fileData = PaymentsFactory.GetTestPaymentFileData();
				payer = PayerFactory.Get();
				paymentsToSend = PaymentsFactory.GetList(payer);
				A.CallTo(() => BGPaymentToSendRepository.FindBy(A<AllNewPaymentsToSendCriteria>.Ignored)).Returns(paymentsToSend);
				A.CallTo(() => BgServiceCoordinatorSettingsService.GetPaymentRecieverBankGiroNumber()).Returns(paymentRecieverBankGiroNumber);
				A.CallTo(() => BgServiceCoordinatorSettingsService.GetPaymentRevieverCustomerNumber()).Returns(paymentRecieverCustomerNumber);
				A.CallTo(() => PaymentFileWriter.Write(A<PaymentsFile>.Ignored)).Returns(fileData);
				A.CallTo(() => AutogiroPayerRepository.Get(A<int>.Ignored)).Returns(payer);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Task_has_send_task_ordering()
		{
			Task.TaskOrder.ShouldBe(BGTaskSequenceOrder.SendTask.ToInteger());
		}

		[Test]
		public void Task_loggs_start_and_stop_messages()
		{
		    LoggingService.AssertInfo("Started");
		    LoggingService.AssertInfo("Finished");
		}

		[Test]
		public void Task_fetches_new_payments_from_repository()
		{
			A.CallTo(() => BGPaymentToSendRepository.FindBy(A<AllNewPaymentsToSendCriteria>.Ignored))
				.MustHaveHappened();
		}

		[Test]
		public void Task_parses_fetched_payments_into_payment_file()
		{
		    A.CallTo(() => PaymentFileWriter.Write(A<PaymentsFile>.That.Matches(x => 
				MatchPayments(x.Posts, paymentsToSend, paymentRecieverBankGiroNumber)
				&& x.Reciever.BankgiroNumber.Equals(paymentRecieverBankGiroNumber)
				&& x.Reciever.CustomerNumber.Equals(paymentRecieverCustomerNumber)
				&& x.WriteDate.Date.Equals(DateTime.Now.Date))
		    )).MustHaveHappened();
		}

		[Test]
		public void Task_stores_payments_as_a_file_section_to_send()
		{
			A.CallTo(() => FileSectionToSendRepository.Save(A<FileSectionToSend>.That.Matches(x => 
				x.CreatedDate.Date.Equals(DateTime.Now.Date)
				&& x.HasBeenSent.Equals(false)
				&& x.SectionData.Equals(fileData)
				&& Equals(x.SentDate, null)
				&& x.Type.Equals(SectionType.PaymentsToSend)
				&& x.TypeName.Equals("PaymentsToSend"))
			)).MustHaveHappened();
		}

		[Test]
		public void Task_updates_payments_as_sent()
		{
			paymentsToSend.Each(paymentToSend => A.CallTo(() => BGPaymentToSendRepository.Save(A<BGPaymentToSend>.That.Matches(x => 
				x.Id.Equals(paymentToSend.Id)
        		&& Equals(x.SendDate.Value.Date, DateTime.Now.Date)
			))).MustHaveHappened());
		}
	}
}