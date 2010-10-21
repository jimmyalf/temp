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
			if( String.IsNullOrEmpty(_lastName) == false)
			{
				return  _lastName;
			}
			return String.Empty;
		}
	}
}