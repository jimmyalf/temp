using System;
using System.Diagnostics;

namespace Synologen.Migration.AutoGiro2.Validators
{
	public abstract class ValidatorBase<TOldItem,TNewItem>
	{
		public abstract void Validate(TOldItem oldItem, TNewItem newItem);

		[DebuggerStepThrough]
		protected void Validate<T>(T oldItem, T newItem, string entity = null)
		{
			if (Equals(oldItem, newItem)) return;
			throw GetException(entity);
		}

		[DebuggerStepThrough]
		protected void ValidateEnum<TOld,TNew>(TOld oldItem, TNew newItem, string entity = null) 
			where TOld : struct
			where TNew : struct
		{
			if( typeof(TOld).IsEnum && typeof(TNew).IsEnum &&
				Enum.GetName(typeof(TOld), oldItem) == Enum.GetName(typeof(TNew), newItem))
			{
				return;
			}
			throw GetException(entity);
		}

		private ApplicationException GetException(string property = null)
		{
			var entityName = typeof (TNewItem).Name;
			var message = property == null
				? string.Format("Validation of {0} failed.", entityName)
				: string.Format("Validation of {0} failed for {1}.", entityName, property);
			return new ApplicationException(message);
		}
	}
}