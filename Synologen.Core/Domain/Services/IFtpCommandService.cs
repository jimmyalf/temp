using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IFtpCommandService : IDisposable
	{
		string Execute(string command);
	}
}