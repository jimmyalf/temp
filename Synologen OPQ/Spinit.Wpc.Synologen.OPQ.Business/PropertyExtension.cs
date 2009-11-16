using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Spinit.Wpc.Synologen.OPQ.Business
{
	public static class PropertyExtension
	{
		/// <summary>
		/// Extension for getting property by name.
		/// </summary>
		/// <typeparam name="T">The class to fetch the property for.</typeparam>
		/// <typeparam name="TU">The propery-type.</typeparam>
		/// <param name="property">The property-name.</param>
		/// <returns>A function used for calling the property.</returns>

		public static Func<T, TU> CreateWith<T, TU> (string property)
		{
			PropertyInfo propInfo = typeof (T).GetProperty (
				property,
				BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);

			ParameterExpression t = Expression.Parameter (typeof (T), "t");
			MemberExpression prop = Expression.Property (t, propInfo);
			return (Func<T, TU>) Expression.Lambda (prop, t).Compile ();
		}
	}
}
