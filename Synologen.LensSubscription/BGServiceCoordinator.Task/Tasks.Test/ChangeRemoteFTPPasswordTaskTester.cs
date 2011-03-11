using NUnit.Framework;
using Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test
{
	[TestFixture, Category("ChangeRemoteFTPPasswordTaskTester")]
	public class When_changing_remote_ftp_password : ChangeRemoteFTPPasswordTaskTestBase
	{
		public When_changing_remote_ftp_password()
		{
			Context = () => { };
			Because = task => task.Execute(ExecutingTaskContext);
		}
	}
}