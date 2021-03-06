﻿using Spinit.Exceptions;

namespace Spinit.Wpc.Synologen.OPQ.Core.Exceptions
{
	public class FileException : BaseCodeException
	{
		private readonly FileErrors _errorCode = FileErrors.None;

		public FileException (string message, FileErrors errorCode) : base (message, (int) errorCode)
		{
			_errorCode = errorCode;
		}
		
		public override string LocalizationKey { get { return string.Concat ("FileErrors_", _errorCode.ToString ()); } }
	}
}
