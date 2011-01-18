using System;

namespace Spinit.Wpc.Synologen.Core.Extensions
{
	public static class GenericsExtensions
	{
		public static string ParseName<TModel>(this TModel model, Func<TModel,string> firstName, Func<TModel,string> lastName)
		{
			var _firstName = firstName.Invoke(model);
			var _lastName = lastName.Invoke(model);
			if(String.IsNullOrEmpty(_firstName) == false && String.IsNullOrEmpty(_lastName) == false)
			{
				return String.Format("{0} {1}", _firstName, _lastName);
			}
			if(String.IsNullOrEmpty(_firstName) == false)
			{
				return _firstName;
			}
			return String.IsNullOrEmpty(_lastName) == false ? _lastName : String.Empty;
		}

		public static TResult With<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator) 
		    where TResult : class 
		    where TInput : class
		{
		    return o == null ? null : evaluator(o);
		}
		public static TResult? With<TInput, TResult>(this TInput o, Func<TInput, TResult?> evaluator) 
			where TResult : struct 
			where TInput : class
		{
			return o == null ? null : evaluator(o);
		}

		public static TResult Return<TInput,TResult>(this TInput o, Func<TInput, TResult> evaluator, TResult failureValue) 
			where TInput: class
		{
			return o == null ? failureValue : evaluator(o);
		}
		public static TResult Return<TInput,TResult>(this TInput? o, Func<TInput?, TResult> evaluator, TResult failureValue) 
		    where TInput: struct
		{
		    return o == null ? failureValue : evaluator(o);
		}

		public static string SafeToString(this object value)
		{
			return value == null ? null : value.ToString();
		}
	}
}