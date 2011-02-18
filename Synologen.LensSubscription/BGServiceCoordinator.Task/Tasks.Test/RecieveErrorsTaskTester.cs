using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test
{
	[TestFixture]
	public class When_receiveing_errors : ReceiveErrorsTaskTestBase
	{
		public When_receiveing_errors()
		{
			Context = () => {};
		
			Because = task => { task.Execute();};
		}

        [Test]
        public void Task_has_receive_task_ordering()
        {
            Task.TaskOrder.ShouldBe(BGTaskSequenceOrder.ReadTask.ToInteger());
        }

        [Test]
        public void Task_loggs_start_and_stop_messages()
        {
            A.CallTo(() => Log.Info(A<string>.That.Contains("Started"))).MustHaveHappened();
			A.CallTo(() => Log.Info(A<string>.That.Contains("Finished"))).MustHaveHappened();
        }
	}
}