using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test
{
	[TestFixture]
	public class When_sending_payments : SendPaymentsTaskTestBase
	{
		public When_sending_payments()
		{
			Context = () => { };
			Because = task => task.Execute();
		}

		[Test]
		public void Task_has_send_task_ordering()
		{
			Task.TaskOrder.ShouldBe(BGTaskSequenceOrder.SendTask.ToInteger());
		}

		//[Test]
		//public void Task_loggs_start_and_stop_messages()
		//{
		//    A.CallTo(() => Log.Info(A<string>.That.Contains("Started"))).MustHaveHappened();
		//    A.CallTo(() => Log.Info(A<string>.That.Contains("Finished"))).MustHaveHappened();
		//}

		//[Test]
		//public void Task_fetches_new_payments_from_repository()
		//{
		//    Assert.Inconclusive("No test");
		//}

		//[Test]
		//public void Task_parses_fetched_payments_into_payment_file()
		//{
		//    Assert.Inconclusive("No test");
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