using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results
{
	public class MemberMigratedResult : IEntityMigratedResult
	{
		private readonly MemberEntity _member;

		public MemberMigratedResult(MemberEntity member)
		{
			_member = member;
		}

		public string OldUrl { get; set; }
		public string NewUrl { get; set; }

		public RenameEventArgs ToRenameEvent()
		{
			return new RenameEventArgs(OldUrl, NewUrl, ToString());
		}

		public override string ToString()
		{
			return string.Format("MemberContent[{0}] had url \"{1}\" replaced with \"{2}\"", _member.Id, OldUrl, NewUrl);
		}		 
	}
}