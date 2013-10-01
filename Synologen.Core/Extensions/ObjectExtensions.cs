using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
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

		public static IEnumerable<KeyValuePair<string,object>> ToProperties(this object value)
		{
			if (value == null) yield break;
			foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(value))
			{
				yield return new KeyValuePair<string, object>(descriptor.Name, descriptor.GetValue(value));
			}
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

        public static IExpressionChain And(this object value, object otherValue)
        {
            return new ExpressionChain(value).And(otherValue);
        }
	}

    public interface IExpressionChain
    {
        IExpressionChain And(object value);
        bool AreNullOrEmpty();
    }

    public class ExpressionChain : IExpressionChain
    {
        public ExpressionChain()
        {
            Items = new List<object>();
        }

        public ExpressionChain(object value)
        {
            Items = new List<object> { value };
        }

        protected IList<object> Items { get; set; }

        public IExpressionChain And(object value)
        {
            Items.Add(value);
            return this;
        }

        public bool AreNullOrEmpty()
        {
            foreach (var item in Items)
            {
                if (item == null)
                {
                    continue;
                }

                if (item.GetType().FullName == "System.String")
                {
                    var stringValue = (string)item;
                    if (string.IsNullOrEmpty(stringValue))
                    {
                        continue;
                    }
                }

                return false;
            }

            return true;
        }
    }
}