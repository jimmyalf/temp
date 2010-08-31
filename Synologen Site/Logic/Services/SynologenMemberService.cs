using Spinit.Wpc.Content.Data;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Site.Code;
using Spinit.Wpc.Utility.Core;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Services
{
	public class SynologenMemberService : ISynologenMemberService
	{
		private readonly ISqlProvider _sqlProvider;
		public SynologenMemberService(ISqlProvider sqlProvider) { _sqlProvider = sqlProvider; }

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
			var connectionString = Utility.Business.Globals.ConnectionString("WpcServer");
			var treeRepository = new Tree(connectionString);
			return treeRepository.GetFileUrlDownString(pageId);
		}
	}
}