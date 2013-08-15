using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService
{
		public class FtpChangePasswordException : Exception
		{
			public FtpChangePasswordException(string ftpCommandResponse) 
				: base(string.Format("FTP Password could not be updated. Remote service returned the following FTP response \"{0}\".", ftpCommandResponse)) { }
		}
}