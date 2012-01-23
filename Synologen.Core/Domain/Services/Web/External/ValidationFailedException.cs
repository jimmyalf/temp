using System;
using System.Linq;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.Web.External
{
	public class ValidationFailedException : Exception
	{
		public ValidationFailedException(ValidationResult validationResult) : base(GetErrorMessage(validationResult)) { }

		private static string GetErrorMessage(ValidationResult validationResult)
		{
			if(!validationResult.HasErrors) return null;
			return validationResult.Errors
				.Select(x => x.ErrorMessage)
				.Aggregate((item, next) => item + "\r\n" + next);
		}
	}
}