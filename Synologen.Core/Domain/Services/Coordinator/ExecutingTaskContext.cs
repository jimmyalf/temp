using System.Collections;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator
{
	public class ExecutingTaskContext
	{
		private readonly ITask _executingTask;
		private readonly ITaskRepositoryResolver _taskRepositoryResolver;

		public ExecutingTaskContext(ITask executingTask, ITaskRepositoryResolver taskRepositoryResolver)
		{
			_executingTask = executingTask;
			_taskRepositoryResolver = taskRepositoryResolver;
			Items = new Dictionary<object,object>();
		}
		public string ExecutingTask { get { return _executingTask.TaskName; } }
		public TRepository GetRepository<TRepository>() { return _taskRepositoryResolver.GetRepository<TRepository>(); }
		public IDictionary Items { get; set; }
		

		public static ExecutingTaskContext Current { get; set; }
	}
}