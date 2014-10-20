using System.Data;
using Spinit.Data.FluentParameters;

namespace Synologen.Maintenance.ExternalShopLogin.Domain.Model
{
	public class Shop
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Number { get; set; }
		public static Shop Parse(IDataRecord record)
		{
			return new FluentDataParser<Shop>(record)
				.Parse(x => x.Id)
				.Parse(x => x.Name,"cShopName")
				.Parse(x => x.Number, "cShopNumber")
				.GetValue();
		}

		public override string ToString()
		{
			return Name;
		}
	}
}