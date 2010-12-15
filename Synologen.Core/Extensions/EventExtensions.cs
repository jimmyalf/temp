using System;

namespace Spinit.Wpc.Synologen.Core.Extensions
{
	public static class EventExtensions
	{
		public static void TryInvoke(this EventHandler eventHandler, object sender, EventArgs eventArgs)
		{
			if(eventHandler != null) eventHandler.Invoke(sender, eventArgs);
		}

		public static void TryInvoke<TEventArgs>(this EventHandler<TEventArgs> eventHandler, object sender, TEventArgs eventArgs) where TEventArgs : EventArgs 
		{
			if(eventHandler != null) eventHandler.Invoke(sender, eventArgs);
		}
	}
}