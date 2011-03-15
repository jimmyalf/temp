using System;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator
{
	public abstract class TaskBase : ITask
	{
		private readonly ILoggingService _loggingService;
		private const string DefaultTaskName = "Untitled task";

		protected TaskBase(string taskName,  ILoggingService loggingService)
		{
			_loggingService = loggingService;
			TaskName = taskName ?? DefaultTaskName;
		}

		protected TaskBase(string taskName,  ILoggingService loggingService, Enum taskOrder) : this(taskName, loggingService)
		{
			TaskOrder = taskOrder.ToInteger();
		}

		public virtual void RunLoggedTask(Action action)
		{
			try
			{
				LogInfo("Started Task Execution", TaskName);
				action.Invoke();
				LogInfo("Finished Task Execution", TaskName);
			}
			catch(Exception ex)
			{
				LogError(String.Format("{0}: Caught exception while executing task", TaskName),ex);
			}
		}


		public virtual void LogDebug(string message)
		{
			LogDebug(message, new object[]{ });
		}

		public virtual void LogDebug(string format, params object[] parameters)
		{
			_loggingService.LogDebug("{0}: {1}", TaskName, String.Format(format, parameters));
		}

		public virtual void LogError(string message, Exception ex)
		{
			_loggingService.LogError(string.Format("{0}: {1}", TaskName, message), ex);
		}

		public virtual void LogInfo(string message)
		{
			LogInfo(message, new object[]{ });
		}

		public virtual void LogInfo(string format, params object[] parameters)
		{
			_loggingService.LogInfo("{0}: {1}", TaskName, String.Format(format, parameters));
		}

		public abstract void Execute(ExecutingTaskContext context);

		public virtual string TaskName { get; private set; }

		public int TaskOrder { get; protected set; }
	}
}