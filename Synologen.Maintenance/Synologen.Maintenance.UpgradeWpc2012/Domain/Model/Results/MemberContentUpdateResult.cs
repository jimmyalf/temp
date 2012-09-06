using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results
{
	public class MemberContentUpdateResult
	{
		private readonly MemberContentEntity _memberContent;

		public MemberContentUpdateResult(MemberContentEntity memberContent)
		{
			_memberContent = memberContent;
		}

		public string OldUrl { get; set; }
		public string NewUrl { get; set; }

		public RenameEventArgs ToRenameEvent()
		{
			return new RenameEventArgs(OldUrl, NewUrl, GetDescription());
		}

		public string GetDescription()
		{
			return string.Format("MemberContent[{0}] had url \"{1}\" replaced with \"{2}\"", _memberContent.Id, OldUrl, NewUrl);
		}		 
	}
}