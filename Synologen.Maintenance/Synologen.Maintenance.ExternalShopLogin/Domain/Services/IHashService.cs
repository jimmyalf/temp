namespace Synologen.Maintenance.ExternalShopLogin.Domain.Services
{
	public interface IHashService
	{
		string GetHash(string message);
	}
}