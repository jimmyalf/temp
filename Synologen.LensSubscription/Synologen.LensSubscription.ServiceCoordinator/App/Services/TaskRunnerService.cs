using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using StructureMap;

namespace Spinit.Wpc.Synologen.LensSubscription.ServiceCoordinator.App.Services
{
	public class TaskRunnerService : TaskRunnerServiceBase
	{
		public TaskRunnerService(ILoggingService loggingService) : base(loggingService) {}

		protected override IEnumerable<ITask> GetTasks()
		{
			return ObjectFactory.GetAllInstances<ITask>();
		}
	}
}