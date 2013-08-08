using Spinit.Exceptions;
namespace Spinit.Wpc.Synologen.OPQ.Core.Exceptions
{
	public class DocumentException : BaseCodeException
	{
		private readonly DocumentErrors _errorCode = DocumentErrors.None;

		public DocumentException (string message, DocumentErrors errorCode) : base (message, (int) errorCode)
		{
			_errorCode = errorCode;
		}

		public override string LocalizationKey { get { return string.Concat ("DocumentErrors_", _errorCode.ToString ()); } }
	}
}
