using System;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator
{
	public abstract class TaskBase : ITask
	{
		private readonly ILoggingService _loggingService;
		private readonly ITaskRepositoryResolver _taskRepositoryResolver;
		private const string DefaultTaskName = "Untitled task";

		protected TaskBase(string taskName,  ILoggingService loggingService, ITaskRepositoryResolver taskRepositoryResolver)
		{
			_loggingService = loggingService;
			_taskRepositoryResolver = taskRepositoryResolver;
			TaskName = taskName ?? DefaultTaskName;
		}

		protected TaskBase(string taskName,  ILoggingService loggingService, ITaskRepositoryResolver taskRepositoryResolver, Enum taskOrder) 
			: this(taskName, loggingService, taskRepositoryResolver)
		{
			TaskOrder = taskOrder.ToInteger();
		}

		public virtual void RunLoggedTask(Action<ITaskRepositoryResolver> action)
		{
			try
			{
				LogInfo("Started Task Execution", TaskName);
				action.Invoke(_taskRepositoryResolver);
				LogInfo("Finished Task Execution", TaskName);
			}
			catch(Exception ex)
			{
				LogError(String.Format("{0}: Caught exception while executing task", TaskName),ex);
			}
		}


		public virtual void LogDebug(string message)
		{
			_loggingService.LogDebug("{0}: {1}", TaskName, message);
		}

		public virtual void LogDebug(string format, params object[] parameters)
		{
			_loggingService.LogDebug("{0}: {1}", TaskName, String.Format(format, parameters));
		}

		public virtual void LogError(string message, Exception ex)
		{
			_loggingService.LogError("{0}: {1}", TaskName, message, ex);
		}

		public virtual void LogInfo(string message)
		{
			_loggingService.LogInfo("{0}: {1}", TaskName, message);
		}

		public virtual void LogInfo(string format, params object[] parameters)
		{
			_loggingService.LogInfo("{0}: {1}", TaskName, String.Format(format, parameters));
		}

		public abstract void Execute();

		public virtual string TaskName { get; private set; }

		public int TaskOrder { get; protected set; }
	}
}