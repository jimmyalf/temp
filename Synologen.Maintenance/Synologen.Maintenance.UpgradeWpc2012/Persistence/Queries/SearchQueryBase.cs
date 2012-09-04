namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries
{
	public abstract class SearchQueryBase : PersistenceBase
	{
		protected string InputQuery { get; set; }
		protected string ParsedQuery { get; set; }

		protected SearchQueryBase(string query)
		{
			InputQuery = query;
			ParsedQuery = query
				.Replace("[","[[]")
				.Replace("_","[_]")
				.Replace("%","[%]");
		}
	}
}