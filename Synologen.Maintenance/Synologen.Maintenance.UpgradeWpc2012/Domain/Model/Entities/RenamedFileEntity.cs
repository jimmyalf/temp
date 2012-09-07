using System.Data;
using Spinit.Data.FluentParameters;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities
{
	public class RenamedFileEntity : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string PreviousName { get; set; }
		public static RenamedFileEntity Parse(IDataRecord record)
		{
			return new FluentDataParser<RenamedFileEntity>(record)
				.Parse(x => x.Id)
				.Parse(x => x.Name)
				.Parse(x => x.PreviousName)
				.GetValue();
		}

		public override string ToString()
		{
			return Name;
		}		 
	}
}