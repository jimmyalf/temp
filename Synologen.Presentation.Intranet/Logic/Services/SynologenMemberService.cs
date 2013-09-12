using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Code;
using Spinit.Wpc.Utility.Core;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services
{
	public class SynologenMemberService : ISynologenMemberService
	{
		private readonly ISqlProvider _sqlProvider;
		private readonly IRoutingService _routingService;

		public SynologenMemberService(ISqlProvider sqlProvider, IRoutingService routingService)
		{
			_sqlProvider = sqlProvider;
			_routingService = routingService;
		}

		public int GetCurrentShopId()
		{
			if (SynologenSessionContext.MemberShopId > 0)
			{
				return SynologenSessionContext.MemberShopId;
			}
			var memberId = GetCurrentMemberId();
			if (memberId <= 0)
			{
				SynologenSessionContext.MemberShopId = 0;
				return 0;
			}
			var shops = _sqlProvider.GetAllShopIdsPerMember(memberId);
			if (shops == null || shops.Count == 0) return 0;
			SynologenSessionContext.MemberShopId = shops[0];
			return shops[0];
		}

		public int GetCurrentMemberId() 
		{
			if (SynologenSessionContext.MemberId > 0) {
				return SynologenSessionContext.MemberId;
			}
			var memberId = 0;
			try {
				var context = PublicUser.Current;
				if (context != null) {
					var userId = context.User.Id;
					memberId = _sqlProvider.GetMemberId(userId);
					SynologenSessionContext.MemberId = memberId;
					return memberId;
				}
			}
			catch {
				if (memberId>0) {
					SynologenSessionContext.MemberId = memberId;
					return memberId;
				}
			}
			return 0;
		}

		public string GetPageUrl(int pageId)
		{
			return _routingService.GetPageUrl(pageId);
		}

		public bool ShopHasAccessTo(ShopAccess accessOption)
		{
			if(SynologenSessionContext.MemberShopAccessOptions.HasOption(accessOption)) return true;
			var currentShopId = GetCurrentShopId();
			var shop = _sqlProvider.GetShop(currentShopId);
			SynologenSessionContext.MemberShopAccessOptions = shop.Access;
			return shop.Access.HasOption(accessOption);
		}

		public bool ValidateUserPassword(string password) 
		{ 
			var connectionString = Utility.Business.Globals.ConnectionString("WpcServer");
			var userRepository = new Base.Data.User(connectionString);
			var userName = GetUserName();
			return userRepository.PasswordIsValidated(userName, password);
		}

		public string GetUserName()
		{
			return PublicUser.Current.User.UserName;
		}
	}
}