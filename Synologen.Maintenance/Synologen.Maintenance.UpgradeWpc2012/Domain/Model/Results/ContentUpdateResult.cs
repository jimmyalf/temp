using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results
{
	public class ContentUpdateResult : IUrlReplacingResult
	{
		private readonly ContentEntity _contentEntity;

		public ContentUpdateResult(ContentEntity contentEntity)
		{
			_contentEntity = contentEntity;
		}

		public string OldUrl { get; set; }
		public string NewUrl { get; set; }

		public RenameEventArgs ToRenameEvent()
		{
			return new RenameEventArgs(OldUrl, NewUrl, GetDescription());
		}

		public string GetDescription()
		{
			return string.Format("Page[{0}] had url \"{1}\" replaced with \"{2}\"", _contentEntity.Url, OldUrl, NewUrl);
		}
	}
}