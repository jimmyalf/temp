using System;
using System.Collections;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public class EventAggregator : IEventAggregator
	{
		private readonly IList<object> _listeners;

		public EventAggregator()
		{
			_listeners = new List<object>();
		}

		public void SendMessage<T>(T message)
		{
			sendAction(() => _listeners.CallOnEach<IListener<T>>(x => x.Handle(message)));
		}

		public void SendMessage<T>() where T : new()
		{
			sendAction(() => _listeners.CallOnEach<ITypeListener<T>>(x => x.Handle()));
		}

		public void AddListener(object listener)
		{
			_listeners.Add(listener);
		}

		public void RemoveListener(object listener)
		{
			_listeners.Remove(listener);
		}

		protected virtual void sendAction(Action action)
        {
			//TODO: threading
			action.Invoke();
        }
	}

	public static class EventAggregatorExtensions
	{
		public static void CallOn<T>(this object target, Action<T> action) where T : class
        {
            var subject = target as T;
            if (subject != null)
            {
                action(subject);
            }
        }
 
        public static void CallOnEach<T>(this IEnumerable enumerable, Action<T> action) where T : class
        {
            foreach (var o in enumerable)
            {
                o.CallOn(action);
            }
        }
	}
}