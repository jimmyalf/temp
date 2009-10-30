using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities{
	public class Concern : IConcern{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool? CommonOPQ { get; set; }
	}
}