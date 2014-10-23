namespace Synologen.Maintenance.ExternalShopLogin.Domain.Model
{
	public class SetExternalLoginForShopResult
	{
		public Shop Shop { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string HashedPassword { get; set; }
	}
}