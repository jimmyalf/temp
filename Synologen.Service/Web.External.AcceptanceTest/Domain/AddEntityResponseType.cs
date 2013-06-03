namespace Synologen.Service.Web.External.AcceptanceTest.Domain
{
	public enum AddEntityResponseType
	{
		EntityWasAdded = 1,
		AuthenticationFailed = 2,
		ValidationFailed = 4,
		EntityAlreadyExists = 8
	}
}