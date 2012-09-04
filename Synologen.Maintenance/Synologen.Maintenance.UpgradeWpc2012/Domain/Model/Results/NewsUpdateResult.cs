using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results
{
	public class NewsUpdateResult : IUrlReplacingResult
	{
		private readonly NewsEntity _newsEntity;

		public NewsUpdateResult(NewsEntity newsEntity)
		{
			_newsEntity = newsEntity;
		}

		public string OldUrl { get; set; }
		public string NewUrl { get; set; }

		public RenameEventArgs ToRenameEvent()
		{
			return new RenameEventArgs(OldUrl, NewUrl, GetDescription());
		}

		public string GetDescription()
		{
			return string.Format("News[{0}] had url \"{1}\" replaced with \"{2}\"", _newsEntity.Id, OldUrl, NewUrl);
		}
	}
}