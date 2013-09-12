using System;

namespace Spinit.Wpc.Synologen.Core.Extensions
{
	public static class ActionExtensions
	{
		public static void Times(this Action action, int numberOfTimesToPerformAction)
		{
			for (var i = 0; i < numberOfTimesToPerformAction; i++)
			{
				action.Invoke();
			}
		}
	}
}