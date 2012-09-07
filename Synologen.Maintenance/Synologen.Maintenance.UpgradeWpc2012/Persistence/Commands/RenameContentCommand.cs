using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Commands
{
	public class RenameContentCommand : PersistenceBase
	{
		private readonly ContentEntity _contentEntity;

		public RenameContentCommand(ContentEntity contentEntity)
		{
			_contentEntity = contentEntity;
		}

		public ContentMigratedResult Execute(string search, string replace)
		{
			RenameContent(_contentEntity.Id, search, replace, "tblContPage", "cContent");
			return new ContentMigratedResult(_contentEntity) {OldUrl = search, NewUrl = replace};
		}
	}
}