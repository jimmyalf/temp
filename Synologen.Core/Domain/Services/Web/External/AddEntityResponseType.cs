namespace Spinit.Wpc.Synologen.Core.Domain.Services.Web.External
{
	public enum AddEntityResponseType
	{
		EntityWasAdded = 1,
		AuthenticationFailed = 2,
		ValidationFailed = 4,
		EntityAlreadyExists = 8
	}
}