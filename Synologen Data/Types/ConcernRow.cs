using Spinit.Wpc.Synologen.Business.Interfaces;

namespace Spinit.Wpc.Synologen.Data.Types{
	public class ConcernRow : IConcernRow{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool? CommonOPQ { get; set; }
	}
}