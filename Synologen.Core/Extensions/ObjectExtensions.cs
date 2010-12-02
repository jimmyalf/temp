namespace Spinit.Wpc.Synologen.Core.Extensions
{
	public static class ObjectExtensions
	{
		public static TType ToTypeOrDefault<TType>(this object value, TType defaultValue)
		{
			if(value is TType)
			{
				return (TType) value;
			}
			return defaultValue;
		}

		public static TType ToTypeOrDefault<TType>(this object value)
		{
			if(value is TType)
			{
				return (TType) value;
			}
			return default(TType);
		}
	}
}