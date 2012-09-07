using Spinit.Wpc.Maintenance.FileAndContentMigration.Persistence;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Commands
{
	public class RenameOPQDocumentHistoryCommand : PersistenceBase
	{
		private readonly OPQDocumentHistoryEntity _entity;

		public RenameOPQDocumentHistoryCommand(OPQDocumentHistoryEntity entity)
		{
			_entity = entity;
		}

		public OPQDocumentHistoryMigratedResult Execute(string search, string replace)
		{
			RenameContent(_entity.Id, search, replace, "SynologenOpqDocumentHistories", "DocumentContent", "Id");
			return new OPQDocumentHistoryMigratedResult(_entity) {OldUrl = search, NewUrl = replace};
		}
	}
}