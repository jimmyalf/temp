using System;
using System.ComponentModel.DataAnnotations;

namespace Spinit.Wpc.Synologen.Presentation.App.Validation {

	public class GreaterThanZero : ValidationAttribute 
	{
		public override bool IsValid(object value) 
		{
			if (value == null) return true;
			var typedValue = Convert.ToDouble(value);
			return (typedValue > 0);
		}
	}
}