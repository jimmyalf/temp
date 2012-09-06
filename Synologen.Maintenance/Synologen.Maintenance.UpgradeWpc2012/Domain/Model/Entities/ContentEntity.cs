using System.Data;
using Spinit.Data.FluentParameters;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities
{
	public class ContentEntity
	{
		public int Id { get; set; }
		public int LocationId { get; set; }
		public string Url { get; set; }
		public string Name { get; set; }
		public string Content { get; set; }

		public static ContentEntity Parse(IDataRecord record)
		{
			return new FluentDataParser<ContentEntity>(record)
				.Parse(x => x.Id)
				.Parse(x => x.LocationId, "cLocId")
				.Parse(x => x.Name)
				.Parse(x => x.Url)
				.Parse(x => x.Content)
				.GetValue();
		}
	}
}