using System;

namespace Spinit.Wpc.Synologen.Core.Utility
{
	public static class Switch
	{
		public static ReturnSwitch<TType,TReturnType> On<TType,TReturnType>(TType value)
		{
			return new ReturnSwitch<TType, TReturnType>(value);
		}

		public static ReturnSwitch<TType,TReturnType> On<TType,TReturnType>(TType value, TReturnType defaultValue)
		{
			return new ReturnSwitch<TType, TReturnType>(value).Default(defaultValue);
		}

		public static ActionSwitch<TType> On<TType>(TType value)
		{
			return new ActionSwitch<TType>(value);
		}

		public static ActionSwitch<TType> On<TType>(TType value, Action defaultAction)
		{
			return new ActionSwitch<TType>(value).Default(defaultAction);
		}
	}
}