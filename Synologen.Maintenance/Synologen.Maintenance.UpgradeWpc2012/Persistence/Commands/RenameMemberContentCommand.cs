using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Commands
{
	public class RenameMemberContentCommand : RenameTextCommand
	{
		private readonly MemberContentEntity _memberContent;

		public RenameMemberContentCommand(MemberContentEntity memberContent)
		{
			_memberContent = memberContent;
		}

		public MemberContentUpdateResult Execute(string search, string replace)
		{
			Execute(_memberContent.Id, search, replace, "tblMembersContent", "cBody");
			return new MemberContentUpdateResult(_memberContent) {OldUrl = search, NewUrl = replace};
		}			 
	}
}