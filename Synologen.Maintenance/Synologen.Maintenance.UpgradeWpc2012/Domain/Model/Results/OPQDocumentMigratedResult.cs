using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Model;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Model.Results;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results
{
	public class OPQDocumentMigratedResult : IMigratedEntity<int>
	{
		protected OPQDocumentEntity Entity;

		public OPQDocumentMigratedResult() { }
		public OPQDocumentMigratedResult(OPQDocumentEntity entity)
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
			return string.Format("OPQDocument[{0}] had url \"{1}\" replaced with \"{2}\"", Entity.Id, OldUrl, NewUrl);
		}
	}
}