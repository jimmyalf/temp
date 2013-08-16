namespace Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService
{
	public interface IBGFtpChangePasswordService
	{
		void Execute(string oldPassword, string newPassword);
	}
}