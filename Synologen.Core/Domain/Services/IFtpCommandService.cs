using System;
using System.Net;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IFtpCommandService : IDisposable
	{
		void Open(string domainName);
		void Open(IPAddress ipAddress);
		string Execute(string command);
		void Close();
	}
}