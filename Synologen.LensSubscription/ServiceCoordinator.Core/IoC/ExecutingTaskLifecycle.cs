using System.Collections;
using StructureMap.Pipeline;
using Synologen.LensSubscription.ServiceCoordinator.Core.Context;

namespace Synologen.LensSubscription.ServiceCoordinator.Core.IoC
{
	public class ExecutingTaskLifecycle : ILifecycle
	{
		public static readonly string ITEM_NAME = "STRUCTUREMAP-INSTANCES";

		public void EjectAll()
		{
			FindCache().DisposeAndClear();
		}

		public IObjectCache FindCache()
		{
			var items = FindExecutingTaskDictionary();
			if (!items.Contains(ITEM_NAME))
			{
				lock (items.SyncRoot)
				{
					if (!items.Contains(ITEM_NAME))
					{
						var cache = new MainObjectCache();
						items.Add(ITEM_NAME, cache);
						return cache;
					}
				}
			}
			return (IObjectCache) items[ITEM_NAME];
		}

		protected virtual IDictionary FindExecutingTaskDictionary()
		{
			return ExecutingTaskContext.Current.Items;
		}

		public string Scope
		{
			get { return GetType().Name; }
		}
	}
}