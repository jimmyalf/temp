using System.Data;
using Spinit.Data.FluentParameters;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities
{
	public class NewsEntity : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Body { get; set; }
		public string FormatedBody { get; set; }

		public static NewsEntity Parse(IDataRecord record)
		{
			return new FluentDataParser<NewsEntity>(record)
				.Parse(x => x.Id)
				.Parse(x => x.Name, "cHeading")
				.Parse(x => x.Body)
				.Parse(x => x.FormatedBody)
				.GetValue();
		}
	}
}