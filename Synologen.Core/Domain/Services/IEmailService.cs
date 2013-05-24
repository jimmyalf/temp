namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IEmailService
	{
		void SendEmail(string from, string to, string subject, string body);
	}
}