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

		public static object GetAnonymousPropertyValue(this object container, string propertyName)
		{
			if(container == null || propertyName == null) return null;
			var type = container.GetType();
			var p = type.GetProperty(propertyName);
			return p == null ? null : p.GetValue(container, null);
		}

		public static TType GetAnonymousPropertyValue<TType>(this object container, string propertyName)
		{
			return GetAnonymousPropertyValue(container, propertyName).ToTypeOrDefault<TType>();
		}
	}
}