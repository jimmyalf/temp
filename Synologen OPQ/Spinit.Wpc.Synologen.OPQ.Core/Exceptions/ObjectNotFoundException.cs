namespace Spinit.Wpc.Synologen.Opq.Core.Exceptions
{
	public class ObjectNotFoundException : BaseException
	{
		private readonly ObjectNotFoundErrors _errorCode = ObjectNotFoundErrors.None;

		public ObjectNotFoundException (string message, ObjectNotFoundErrors errorCode) : base (message, (int) errorCode)
		{
			_errorCode = errorCode;
		}

		public override string LocalizationKey { get { return _errorCode.ToString (); } }
	}
}
