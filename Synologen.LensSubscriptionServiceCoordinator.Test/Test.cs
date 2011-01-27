using System;
using Moq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.ServiceCoordinator.Test
{
	[TestFixture]
	public class Test
	{
		[Test]
		public void TestSomething()
		{
			var loggingService = new Mock<ILoggingService>();
			Log(x => x.LogInfo("message"), loggingService.Object);
		}

		public virtual void Log(Action<ILoggingService> action, ILoggingService loggingService)
		{
			var parameters = action.Method.GetParameters();
			action.Invoke(loggingService);
		}
		
	}
}