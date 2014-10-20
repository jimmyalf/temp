namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IRoutingService
	{
		string GetPageUrl(int pageId);
		string GetPageUrl(int pageId, object requestParameters);
	}
}