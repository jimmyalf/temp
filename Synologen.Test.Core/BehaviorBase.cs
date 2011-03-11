using System;

namespace Synologen.Test.Core
{
	public abstract class BehaviorBase
	{
		protected Exception TryGetException(Action action)
		{
			try
			{
				action.Invoke();
			}
			catch (Exception ex)
			{
				return ex;
			}
			return null;
		}
	}
}