using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Commands
{
	public class RenameMemberContentCommand : PersistenceBase
	{
		private readonly MemberEntity _member;

		public RenameMemberContentCommand(MemberEntity member)
		{
			_member = member;
		}

		public MemberMigratedResult Execute(string search, string replace)
		{
			RenameContent(_member.Id, search, replace, "tblMembersContent", "cBody");
			return new MemberMigratedResult(_member) {OldUrl = search, NewUrl = replace};
		}			 
	}
}