using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Factories;
using Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test
{
	[TestFixture]
	public class When_sending_payments : SendPaymentsTaskTestBase
	{
		private IEnumerable<BGPaymentToSend> paymentsToSend;

		public When_sending_payments()
		{
			Context = () =>
			{
				paymentsToSend = PaymentsFactory.GetList();
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

		//[Test]
		//public void Task_parses_fetched_payments_into_payment_file()
		//{
		//    //A.CallTo(() => PaymentFileWriter.Write(
		//    //    A<PaymentsFile>
		//    //    //.That.Matches(x => MatchConsents(x.Posts, consentsToSend, paymentRecieverBankGiroNumber))
		//    //    //.And.Matches(x => x.Reciever.BankgiroNumber.Equals(paymentRecieverBankGiroNumber))
		//    //    //.And.Matches(x => x.Reciever.CustomerNumber.Equals(paymentRecieverCustomerNumber))
		//    //    //.And.Matches(x => x.WriteDate.Date.Equals(DateTime.Now.Date))
		//    //)).MustHaveHappened();
		//    Assert.Ignore("Add test");
		//}

		//[Test]
		//public void Task_stores_payments_as_a_file_section_to_send()
		//{
		//    Assert.Inconclusive("No test");
		//}

		//[Test]
		//public void Task_updates_payments_as_sent()
		//{

		//    Assert.Inconclusive("No test");
		//}
	}
}