using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace Task_Testing
{
	[TestFixture]
	public class Test_Task_Ordering
	{
		private readonly TaskDefinitions _taskDefinitions;

		public Test_Task_Ordering()
		{
			_taskDefinitions = new TaskDefinitions();
			_taskDefinitions.Add<TaskA>();
			_taskDefinitions.Add<TaskB>();
			_taskDefinitions.Add<TaskC>();
			_taskDefinitions.Add<TaskD>();
			_taskDefinitions.Add<TaskE>();
		}

		[Test]
		public void Test_ordering()
		{
			var definitions = _taskDefinitions.GetOrderedTasks("Priority");
			Debug.WriteLine("Fetched unenumerated definitions");
			foreach (var definition in definitions)
			{
				Debug.WriteLine(definition.GetType().Name + " was enumerated.");
			}
		}
	}

	public interface ITask { void Execute(); }

	public enum TaskPriority{ Low = 1, Medium = 2, High = 3}

	public class TaskA : TaskBase, ITask
	{
		public TaskA() : base(typeof(TaskA), Priority) { }
		public void Execute() { }

		public static TaskPriority Priority
		{
			get { return TaskPriority.Low; }
		}
	}

	public class TaskB : TaskBase, ITask
	{
		public TaskB() : base(typeof(TaskB), Priority) { }
		public void Execute() { }

		public static TaskPriority Priority
		{
			get { return TaskPriority.High; }
		}
	}

	public class TaskC : TaskBase, ITask
	{
		public TaskC() : base(typeof(TaskC), Priority) { }

		public void Execute() {}

		public static TaskPriority Priority
		{
			get { return TaskPriority.Low; }
		}
	}

	public class TaskD : TaskBase, ITask
	{
		public TaskD() : base(typeof(TaskD), Priority) { }
		public void Execute() { }

		public static TaskPriority Priority
		{
			get { return TaskPriority.Medium; }
		}
	}

	public class TaskE : TaskBase, ITask
	{
		public TaskE() : base(typeof(TaskE)) { }
		public void Execute() { }
	}

	public abstract class TaskBase
	{
		protected TaskBase(Type inheritor)
		{
			Debug.WriteLine(inheritor.Name + " was created with out priority");
		}
		protected TaskBase(Type inheritor, TaskPriority priority)
		{
			Debug.WriteLine(inheritor.Name + " was created with prio: " + priority);
		}
	}

	public class TaskDefinitions
	{
		private readonly List<Type> _tasks;

		public TaskDefinitions()
		{
			_tasks = new List<Type>();
		}

		public void Add<TType>() where TType : ITask
		{
			Add(typeof(TType));
		}

		public void Add(Type type)
		{
			_tasks.Add(type);
		}

		public IEnumerable<ITask> GetOrderedTasks(string orderPropertyName, bool orderAscending = false)
		{
			var orderItems = _tasks.Select(type => new OrderedType(type, orderPropertyName));
			return orderAscending 
				? orderItems.OrderByDescending(x => x.Order).Select(x => x.Type).Select(x => (ITask) Activator.CreateInstance(x)) 
				: orderItems.OrderBy(x => x.Order).Select(x => x.Type).Select(x => (ITask) Activator.CreateInstance(x));
		}

		public IEnumerable<ITask> GetTasks()
		{
			return _tasks.Select(x => (ITask) Activator.CreateInstance(x));
		}

		private class OrderedType
		{
			public OrderedType(Type type, string orderPropertyName)
			{
				Type = type;
				Order = TryGetStaticOrderValue(type, orderPropertyName) ?? default(int);
			}

			private int? TryGetStaticOrderValue(IReflect type, string orderPropertyName)
			{
				var info = type.GetProperty(orderPropertyName, BindingFlags.Public | BindingFlags.Static );
				if(info == null) return null;
				return (int) info.GetValue(null, null);
			}

			public Type Type { get; private set; }
			public int Order { get; private set; }
		}

		public int Count { get { return _tasks.Count; } }
	}
}