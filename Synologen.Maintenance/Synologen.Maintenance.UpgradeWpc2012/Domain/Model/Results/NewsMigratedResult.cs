using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results
{
	public class NewsMigratedResult : IEntityMigratedResult
	{
		private readonly NewsEntity _newsEntity;

		public NewsMigratedResult(NewsEntity newsEntity)
		{
			_newsEntity = newsEntity;
		}

		public string OldUrl { get; set; }
		public string NewUrl { get; set; }

		public RenameEventArgs ToRenameEvent()
		{
			return new RenameEventArgs(OldUrl, NewUrl, ToString());
		}

		public override string ToString()
		{
			return string.Format("News[{0}] had url \"{1}\" replaced with \"{2}\"", _newsEntity.Id, OldUrl, NewUrl);
		}
	}
}