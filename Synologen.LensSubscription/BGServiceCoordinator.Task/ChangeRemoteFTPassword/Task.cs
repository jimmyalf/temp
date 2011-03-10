using System;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.ChangeRemoteFTPPassword
{
	public class Task : TaskBase
	{
		public Task(ILoggingService loggingService, ITaskRepositoryResolver taskRepositoryResolver) 
			: base("ChangeRemoteFTPPassword", loggingService, taskRepositoryResolver) {}
		public override void Execute() { throw new NotImplementedException(); }
	}
}