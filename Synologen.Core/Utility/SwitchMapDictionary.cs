using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Spinit.Wpc.Synologen.Core.Utility
{
	public class SwitchMapDictionary<TModel,TReturnType> : IEnumerable<KeyValuePair<Func<TModel, bool>, TReturnType>>
	{
		protected readonly Dictionary<Func<TModel,bool>,TReturnType> Dictionary;

		public SwitchMapDictionary() : this(new Dictionary<Func<TModel, bool>, TReturnType>()) { }

		public SwitchMapDictionary(Dictionary<Func<TModel, bool>, TReturnType> dictionaryMap)
		{
			Dictionary = dictionaryMap;
		}

		public virtual TReturnType GetMap(TModel subscription, TReturnType defaultValue)
		{
			var key = Dictionary.Keys.FirstOrDefault(findMessageKey => findMessageKey(subscription));
			return (key == null) ? defaultValue : Dictionary[key];
		}

		public virtual TReturnType GetMap(TModel subscription)
		{
			var key = Dictionary.Keys.FirstOrDefault(findMessageKey => findMessageKey(subscription));
			if (key == null) throw new SwitchMapDictionaryException("Cannot find matching case and no default value has been set.");
			return Dictionary[key];
		}

		public virtual IEnumerator<KeyValuePair<Func<TModel, bool>, TReturnType>> GetEnumerator()
		{
			return Dictionary.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public virtual SwitchMapDictionary<TModel,TReturnType> Add(Func<TModel, bool> func, TReturnType returnValue)
		{
			Dictionary.Add(func, returnValue);
			return this;
		}

		public class SwitchMapDictionaryException : Exception
		{
			public SwitchMapDictionaryException(string message) : base(message) { }
		}
	}
}