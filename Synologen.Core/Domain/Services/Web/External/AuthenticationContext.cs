namespace Spinit.Wpc.Synologen.Core.Domain.Services.Web.External
{
	public class AuthenticationContext
	{
		public string UserName { get; set; }
		public string Password { get; set; }

		public override string ToString()
		{
			return "{UserName: \"" + UserName + "\", Password: \"" + Password + "\"}";
		}
	}
}