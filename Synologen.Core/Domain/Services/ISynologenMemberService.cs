namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface ISynologenMemberService
	{
		int GetCurrentShopId();
		int GetCurrentMemberId();
		string GetPageUrl(int pageId);
	}
}