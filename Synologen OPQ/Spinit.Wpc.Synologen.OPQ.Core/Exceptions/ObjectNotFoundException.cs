using Spinit.Exceptions;

namespace Spinit.Wpc.Synologen.OPQ.Core.Exceptions
{
	public class ObjectNotFoundException : BaseCodeException
	{
		private readonly ObjectNotFoundErrors _errorCode = ObjectNotFoundErrors.None;

		public ObjectNotFoundException (string message, ObjectNotFoundErrors errorCode) : base (message, (int) errorCode)
		{
			_errorCode = errorCode;
		}

		public override string LocalizationKey { get { return string.Concat ("ObjectNotFoundErrors_", _errorCode.ToString ()); } }
	}
}
