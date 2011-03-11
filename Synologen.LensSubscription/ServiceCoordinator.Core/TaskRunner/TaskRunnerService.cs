using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Data;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using StructureMap;

namespace Synologen.LensSubscription.ServiceCoordinator.Core.TaskRunner
{
	public class TaskRunnerService : ITaskRunnerService
	{
		private readonly ILoggingService _loggingService;
		private readonly ITaskRepositoryResolver _taskRepositoryResolver;
		private readonly IEnumerable<ITask> _tasks;


		public TaskRunnerService(ILoggingService loggingService, IEnumerable<ITask> tasks, ITaskRepositoryResolver taskRepositoryResolver)
		{
			_loggingService = loggingService;
			_taskRepositoryResolver = taskRepositoryResolver;
			_tasks = (tasks ?? Enumerable.Empty<ITask>())
				.OrderBy(x => x.TaskOrder);
		}


		public virtual void Run()
		{
			try
			{
				_loggingService.LogInfo(">>> Taskrunner started execution");
				RunTasks(_loggingService);
				_loggingService.LogInfo(">>> Taskrunner finished execution");
			}
			catch(Exception ex)
			{
				_loggingService.LogError("Taskrunner caught an exception", ex);
			}	
		}

		protected virtual void OnExecutingTask(ITask task) { }

		protected virtual void OnExecutedTask(ITask task)
		{
			var unitOfWork = ObjectFactory.GetInstance<IUnitOfWork>();
			if(unitOfWork == null || unitOfWork.IsDisposed) return;
			try
			{
				unitOfWork.Commit();
			}
			catch
			{
				unitOfWork.Rollback();
			}
			finally
			{
				unitOfWork.Dispose();
			}
		}

		protected virtual void RunTasks(ILoggingService loggingService)
		{
			_tasks.Each(task =>
			{
				ExecutingTaskContext.Current = new ExecutingTaskContext(task, _taskRepositoryResolver);
				OnExecutingTask(task);
				try
				{
					ExecuteLoggedTask(task, loggingService, ExecutingTaskContext.Current);
				}
				catch(Exception)
				{
					OnExecutedTask(task);
					throw;
				}
				finally
				{
					OnExecutedTask(task);
					ExecutingTaskContext.Current = null;
				}
			});
		}

		protected virtual void ExecuteLoggedTask(ITask task, ILoggingService loggingService, ExecutingTaskContext executingTaskContext)
		{
			try
			{
				loggingService.LogInfo("*** Taskrunner: Executing {0} task", task.TaskName);
				task.Execute(executingTaskContext);
				loggingService.LogInfo("*** Taskrunner: Finished executing {0} task", task.TaskName);
			}
			catch(Exception ex)
			{
				loggingService.LogError(String.Format("Taskrunner got exception while executing \"{0}\"", task.TaskName), ex);
			}
		}
	}
}