using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

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

		public static OutputStringBuilder<TType> BuildStringOutput<TType>(this TType value) where TType : class
		{
			return new OutputStringBuilder<TType>(value);
		}

		public class OutputStringBuilder<TType> where TType : class
		{
			public TType Value { get; private set; }
			public IList<Expression<Func<TType, object>>> propertyExpressionList;

			public OutputStringBuilder(TType value)
			{
				Value = value;
				propertyExpressionList = new List<Expression<Func<TType, object>>>();
			}

			public OutputStringBuilder<TType> With(Expression<Func<TType, object>> propertyExpression)
			{
				propertyExpressionList.Add(propertyExpression);
				return this;
			}

			public override string ToString()
			{
				return "{ " + propertyExpressionList.Select(GetKeyValueString).Aggregate((item, next) => item + ", " + next) + " }";
			}

			private string GetKeyValueString(Expression<Func<TType, object>> propertyExpression)
			{
				return string.Format("{0} = {1}", propertyExpression.GetName(), propertyExpression.Compile()(Value));
			}
		}

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