using System.Data;
using Spinit.Data.FluentParameters;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities
{
	public class NewsEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public static NewsEntity Parse(IDataRecord record)
		{
			return new FluentDataParser<NewsEntity>(record)
				.Parse(x => x.Id)
				.Parse(x => x.Name, "cHeading")
				.GetValue();
		}
	}
}