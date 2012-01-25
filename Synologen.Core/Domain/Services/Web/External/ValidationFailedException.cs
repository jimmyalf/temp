using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.Web.External
{
	public class ValidationFailedException : Exception
	{
		public ValidationFailedException(ValidationResult validationResult) : base(validationResult.GetErrorMessage()) { }
	}
}