using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer
{
	public interface IBGFtpPasswordRepository
	{
		void Add(BGFtpPassword password);
		BGFtpPassword GetLast();
		bool PasswordExists(string password);
	}
}