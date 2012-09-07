using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Model;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Model.Results;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results
{
	public class OPQDocumentMigratedResult : IEntityMigratedResult
	{
		private readonly OPQDocumentEntity _entity;

		public OPQDocumentMigratedResult(OPQDocumentEntity entity)
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
			return string.Format("OPQDocument[{0}] had url \"{1}\" replaced with \"{2}\"", _entity.Id, OldUrl, NewUrl);
		}
	}
}