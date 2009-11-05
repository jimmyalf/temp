namespace Spinit.Wpc.Synologen.Opq.Core.Exceptions
{
	public class FileException : BaseException
	{
		private readonly FileErrors _errorCode = FileErrors.None;

		public FileException (string message, FileErrors errorCode) : base (message, (int) errorCode)
		{
			_errorCode = errorCode;
		}
		
		public override string LocalizationKey { get { return _errorCode.ToString (); } }
	}
}
