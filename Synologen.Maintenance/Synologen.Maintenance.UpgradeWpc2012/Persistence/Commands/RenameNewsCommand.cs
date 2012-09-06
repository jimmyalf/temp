using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Commands
{
	public class RenameNewsCommand : RenameTextCommand
	{
		private readonly NewsEntity _newsEntity;

		public RenameNewsCommand(NewsEntity newsEntity)
		{
			_newsEntity = newsEntity;
		}

		public NewsUpdateResult Execute(string search, string replace)
		{
			Execute(_newsEntity.Id, search, replace, "tblNews", "cBody");
			Execute(_newsEntity.Id, search, replace, "tblNews", "cFormatedBody");
			return new NewsUpdateResult(_newsEntity) {OldUrl = search, NewUrl = replace};
		}
	}
}