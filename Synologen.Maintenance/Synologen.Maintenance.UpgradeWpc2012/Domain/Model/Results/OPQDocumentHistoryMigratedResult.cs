using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Model;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Model.Results;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results
{
	public class OPQDocumentHistoryMigratedResult : IMigratedEntity<int>
	{
		protected OPQDocumentHistoryEntity Entity;

		public OPQDocumentHistoryMigratedResult() { }
		public OPQDocumentHistoryMigratedResult(OPQDocumentHistoryEntity entity)
		{
			Entity = entity;
			Id = entity.Id;
		}

		public int Id { get; set; }
		public string OldUrl { get; set; }
		public string NewUrl { get; set; }

		public RenameEventArgs ToRenameEvent()
		{
			return new RenameEventArgs(OldUrl, NewUrl, ToString());
		}

		public override string ToString()
		{
			return string.Format("OPQDocumentHistory[{0}] had url \"{1}\" replaced with \"{2}\"", Entity.Id, OldUrl, NewUrl);
		}
	}
}