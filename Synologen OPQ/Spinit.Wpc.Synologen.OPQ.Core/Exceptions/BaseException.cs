using System;

namespace Spinit.Wpc.Synologen.Opq.Core.Exceptions
{
	public abstract class BaseException : Exception
	{
		protected BaseException (string message, int errorCode) : base (message)
		{
			ErrorCode = errorCode;
			ErrorMessage = message;
		}

		public int ErrorCode { get; private set; }

		public string ErrorMessage { get; private set; }

		public abstract string LocalizationKey { get; }
	}
}
