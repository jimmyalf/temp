using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface ISynologenMemberService
	{
		int GetCurrentShopId();
		int GetCurrentMemberId();
		bool ShopHasAccessTo(ShopAccess accessOption);
		bool ValidateUserPassword(string password);
		string GetUserName();
	}
}