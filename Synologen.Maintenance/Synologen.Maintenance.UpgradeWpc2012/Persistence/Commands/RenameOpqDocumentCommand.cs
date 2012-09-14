using Spinit.Wpc.Maintenance.FileAndContentMigration.Persistence.Commands;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Commands
{
	public class RenameOPQDocumentCommand : RenameContentCommand<OPQDocumentMigratedResult>
	{
		private readonly OPQDocumentEntity _entity;

		public RenameOPQDocumentCommand(OPQDocumentEntity entity, string search, string replace) : base(search, replace)
		{
			_entity = entity;
		}

		public override void Execute()
		{
			Database.RenameContent(_entity.Id, Search, Replace, "SynologenOpqDocuments", "DocumentContent", "Id");
			Result =  new OPQDocumentMigratedResult(_entity) {OldUrl = Search, NewUrl = Replace};
		}
	}
}