using System;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.SendConsents
{
	public class Task : TaskBase
	{
		public Task(ILoggingService loggingService) : base("SendConsents", loggingService) {}
		public override void Execute() { throw new NotImplementedException(); }
	}
}