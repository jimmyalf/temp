using System;
using System.ComponentModel.DataAnnotations;

namespace Spinit.Wpc.Synologen.Presentation.Helpers.Validation
{
	public class NotZeroAttribute : ValidationAttribute 
	{
		public override bool IsValid(object value) 
		{
			if (value == null) return true;
			var typedValue = Convert.ToDouble(value);
			return (typedValue > 0 || typedValue < 0);
		}
	}
}