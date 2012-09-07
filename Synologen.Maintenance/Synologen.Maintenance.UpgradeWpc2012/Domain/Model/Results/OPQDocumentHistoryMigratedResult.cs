using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Model;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Model.Results;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results
{
	public class OPQDocumentHistoryMigratedResult : IEntityMigratedResult
	{
		private readonly OPQDocumentHistoryEntity _entity;

		public OPQDocumentHistoryMigratedResult(OPQDocumentHistoryEntity entity)
		{
			_entity = entity;
		}

		public string OldUrl { get; set; }
		public string NewUrl { get; set; }

		public RenameEventArgs ToRenameEvent()
		{
			return new RenameEventArgs(OldUrl, NewUrl, ToString());
		}

		public override string ToString()
		{
			return string.Format("OPQDocumentHistory[{0}] had url \"{1}\" replaced with \"{2}\"", _entity.Id, OldUrl, NewUrl);
		}
	}
}