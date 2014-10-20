using System.Data;
using Spinit.Data.FluentParameters;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities
{
	public class OPQDocumentEntity : IEntity
	{
		public int Id { get; set; }
		public string DocumentContent { get; set; }

		public static OPQDocumentEntity Parse(IDataRecord record)
		{
			return new FluentDataParser<OPQDocumentEntity>(record)
				.Parse(x => x.Id, "Id")
				.Parse(x => x.DocumentContent, "DocumentContent")
				.GetValue();
		}
	}
}