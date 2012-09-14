using Spinit.Data.SqlClient.SqlBuilder;

namespace Synologen.Maintenance.UpgradeWpc2012.Test.Persistence
{
	public class Database : Spinit.Wpc.Maintenance.FileAndContentMigration.Test.Database
	{
		public Database(string connectionString) : base(connectionString) {}

		public override void CreateSchema()
		{
			base.CreateSchema();
			Execute(Sql.CreateOPQDocumentTable);
			Execute(Sql.CreateOPQDocumentHistoryTable);
		}

		public void CreateOPQDocumentEntry(string documentContent)
		{
			var command = CommandBuilder
				.Build(@"INSERT INTO SynologenOpqDocuments(NdeId,ShpId,CncId,ShopGroupId,DocTpeId,DocumentContent,IsActive,CreatedById,CreatedByName,CreatedDate)
					VALUES(0,0,0,0,0,@DocumentContent,1,0,'TestUser',GETDATE())")
				.AddParameters(new{ DocumentContent = documentContent});
			PersistenceInstance.Execute(command);
		}

		public void CreateOPQDocumentHistoryEntry(string documentContent)
		{
			var command = CommandBuilder
				.Build(@"INSERT INTO SynologenOpqDocumentHistories(Id,HistoryDate,HistoryId,HistoryName,NdeId,DocTpeId,DocumentContent,IsActive,CreatedById,CreatedByName,CreatedDate)
					VALUES(1,GETDATE(),0,'Name',0,0,@DocumentContent,1,0,'TestUser',GETDATE())")
				.AddParameters(new{ DocumentContent = documentContent});
			PersistenceInstance.Execute(command);
		}
	}
}