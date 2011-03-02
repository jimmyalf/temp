using System.Collections;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.ServiceCoordinator.Core.Context
{
	public class ExecutingTaskContext
	{
		private readonly ITask _executingTask;
		public ExecutingTaskContext(ITask executingTask)
		{
			_executingTask = executingTask;
			Items = new Dictionary<object,object>();
		}
		public string ExecutingTask { get { return _executingTask.TaskName; } }
		public IDictionary Items { get; set; }

		public static ExecutingTaskContext Current { get; set; }
	}
}