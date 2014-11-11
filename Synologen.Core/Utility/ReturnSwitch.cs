using System;

namespace Spinit.Wpc.Synologen.Core.Utility
{
	public class ReturnSwitch<TType,TReturnType>
	{
		private readonly TType _value;
		private TReturnType _defaultValue;
		private readonly SwitchMapDictionary<TType,TReturnType> _dictionary;
		private bool _hasDefaultValue;

		public ReturnSwitch(TType value)
		{
			_dictionary = new SwitchMapDictionary<TType, TReturnType>();
			_value = value;
		}

		public virtual ReturnSwitch<TType,TReturnType> Case(Func<TType,bool> evaluate, TReturnType returnValue)
		{
			_dictionary.Add(evaluate, returnValue);
			return this;
		}


		public virtual ReturnSwitch<TType,TReturnType> Case(Func<TType,bool> evaluate, Func<TType,TReturnType> returnExpression)
		{
			var returnValue = returnExpression(_value);
			_dictionary.Add(evaluate, returnValue);
			return this;
		}

		public virtual ReturnSwitch<TType,TReturnType> Default(TReturnType defaultValue)
		{
			_defaultValue = defaultValue;
			_hasDefaultValue = true;
			return this;
		}

		public virtual ReturnSwitch<TType,TReturnType> Default(Func<TType,TReturnType> returnExpression)
		{
			var defaultValue = returnExpression(_value);
			_defaultValue = defaultValue;
			_hasDefaultValue = true;
			return this;
		}

		public virtual TReturnType Evaluate()
		{
			return _hasDefaultValue 
			       	? _dictionary.GetMap(_value, _defaultValue) 
			       	: _dictionary.GetMap(_value);
		}
	}
}