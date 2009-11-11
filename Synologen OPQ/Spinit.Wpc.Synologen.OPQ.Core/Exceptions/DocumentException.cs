namespace Spinit.Wpc.Synologen.Opq.Core.Exceptions
{
	public class DocumentException : BaseException
	{
		private readonly DocumentErrors _errorCode = DocumentErrors.None;

		public DocumentException (string message, DocumentErrors errorCode) : base (message, (int) errorCode)
		{
			_errorCode = errorCode;
		}

		public override string LocalizationKey { get { return _errorCode.ToString (); } }
	}
}
