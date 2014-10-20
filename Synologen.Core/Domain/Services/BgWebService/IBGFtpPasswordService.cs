namespace Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService
{
	public interface IBGFtpPasswordService
	{
		string GetCurrentPassword();
		string GenerateNewPassword();
		void StoreNewActivePassword(string newActivePassword);
	}
}