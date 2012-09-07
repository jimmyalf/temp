using System.Data;
using Spinit.Data.FluentParameters;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities
{
	public class MemberEntity : IEntity
	{
		public int Id { get; set; }
		public int MemberId { get; set; }
		public string Name { get; set; }
		public string Body { get; set; }

		public static MemberEntity Parse(IDataRecord record)
		{
			return new FluentDataParser<MemberEntity>(record)
				.Parse(x => x.Id)
				.Parse(x => x.MemberId)
				.Parse(x => x.Body)
				.GetValue();
		}		 
	}
}