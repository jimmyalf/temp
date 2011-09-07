using System;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments
{
	public class ValidationEventArgs : EventArgs
	{
		public string Password { get; set; }
		public event EventHandler<ValidatedEventArgs> Validation;
		public event EventHandler<ValidatedEventArgs> ValidationSuccess;
		public event EventHandler<ValidatedEventArgs> ValidationFailure;

		public ValidationEventArgs(string password) { Password = password;  }

		public void InvokeValidation(ValidatedEventArgs args)
		{
			InvokeEvent(Validation, args);
		}
		public void InvokeValidationSuccess(ValidatedEventArgs args)
		{
			InvokeEvent(ValidationSuccess, args);
		}
		public void InvokeValidationFailure(ValidatedEventArgs args)
		{
			InvokeEvent(ValidationFailure, args);
		}

		private static void InvokeEvent(EventHandler<ValidatedEventArgs> eventToInvoke, ValidatedEventArgs args)
		{
			if(eventToInvoke != null) eventToInvoke.Invoke(null, args);
		}
	}
}