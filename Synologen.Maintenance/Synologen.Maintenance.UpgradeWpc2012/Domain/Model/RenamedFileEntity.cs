namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model
{
	public class RenamedFileEntity
	{
		public int Id { get; set; }
		public string OldName { get; set; }
		public string NewName { get; set; }
	}
}