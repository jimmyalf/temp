using Spinit.Wpc.Maintenance.FileAndContentMigration.Persistence;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Commands
{
	public class RenameOPQDocumentCommand : PersistenceBase
	{
		private readonly OPQDocumentEntity _entity;

		public RenameOPQDocumentCommand(OPQDocumentEntity entity)
		{
			_entity = entity;
		}

		public OPQDocumentMigratedResult Execute(string search, string replace)
		{
			RenameContent(_entity.Id, search, replace, "SynologenOpqDocuments", "DocumentContent", "Id");
			return new OPQDocumentMigratedResult(_entity) {OldUrl = search, NewUrl = replace};
		}
	}
}