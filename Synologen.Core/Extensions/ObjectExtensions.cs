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


		//public static PropertyMatch<object> GetAnonymousPropertyValue(this object container, string propertyName)
		//{
		//    var returnValue = new PropertyMatch<object> {PropertyName = propertyName};
		//    if(container == null || propertyName == null) return null;
		//    var type = container.GetType();
		//    var p = type.GetProperty(propertyName);
		//    returnValue.FoundProperty = (p != null);
		//    if(p != null)
		//    {
		//        returnValue.PropertyValue = p.GetValue(container, null);
		//    }
		//    return returnValue;
		//}

		public static PropertyMatch<TType> GetAnonymousPropertyValue<TType>(this object container, string propertyName)
		{
			var returnValue = new PropertyMatch<TType> {PropertyName = propertyName};
			if(container == null || propertyName == null) return null;
			var type = container.GetType();
			var p = type.GetProperty(propertyName);
			returnValue.FoundProperty = (p != null);
			if(p != null)
			{
				returnValue.PropertyValue = (TType) p.GetValue(container, null);
			}
			return returnValue;
		}

		public class PropertyMatch<TType>
		{
			public string PropertyName { get; set; }
			public TType PropertyValue { get; set; }
			public bool FoundProperty { get; set; }
		}
	}
}