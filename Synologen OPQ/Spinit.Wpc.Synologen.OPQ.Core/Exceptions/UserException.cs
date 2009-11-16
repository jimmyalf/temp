using Spinit.Exceptions;

namespace Spinit.Wpc.Synologen.OPQ.Core.Exceptions
{
	public class UserException : BaseCodeException
	{
		private readonly UserErrors _errorCode = UserErrors.None;

		public UserException (string message, UserErrors errorCode) : base (message, (int) errorCode)
		{
			_errorCode = errorCode;
		}
		
		public override string LocalizationKey { get { return string.Concat ("UserErrors_", _errorCode.ToString ()); } }
	}
}
