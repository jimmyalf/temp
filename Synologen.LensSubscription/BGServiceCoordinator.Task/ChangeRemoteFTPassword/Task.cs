using System;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.ChangeRemoteFTPPassword
{
	public class Task : TaskBase
	{
		private readonly IFtpCommandService _ftpCommandService;

		public Task(ILoggingService loggingService, /*ITaskRepositoryResolver taskRepositoryResolver,*/ IFtpCommandService ftpCommandService) 
			: base("ChangeRemoteFTPPassword", loggingService /*, taskRepositoryResolver*/) { _ftpCommandService = ftpCommandService; }

		public override void Execute(ExecutingTaskContext context) { throw new NotImplementedException(); }
	}
}