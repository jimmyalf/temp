using Spinit.Wpc.Maintenance.FileAndContentMigration.Persistence.Commands;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Commands
{
	public class RenameOPQDocumentHistoryCommand : RenameContentCommand<OPQDocumentHistoryMigratedResult>
	{
		private readonly OPQDocumentHistoryEntity _entity;

		public RenameOPQDocumentHistoryCommand(OPQDocumentHistoryEntity entity, string search, string replace) : base(search,replace)
		{
			_entity = entity;
		}

		public override void Execute()
		{
			Database.RenameContent(_entity.Id, Search, Replace, "SynologenOpqDocumentHistories", "DocumentContent", "Id");
			Result = new OPQDocumentHistoryMigratedResult(_entity) {OldUrl = Search, NewUrl = Replace};
		}
	}
}