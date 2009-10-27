namespace Spinit.Wpc.Synologen.Opq.Core.Exceptions
{
	public class UserException : BaseException
	{
		private readonly UserErrors _errorCode = UserErrors.None;

		public UserException (string message, UserErrors errorCode) : base (message, (int) errorCode)
		{
			_errorCode = errorCode;
		}
		
		public override string LocalizationKey { get { return _errorCode.ToString (); } }
	}
}
