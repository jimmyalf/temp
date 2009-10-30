namespace Spinit.Wpc.Synologen.Business.Domain.Interfaces{
	public interface IConcern{
		int Id { get; set; }
		string Name { get; set; }
		bool? CommonOPQ { get; set; }
	}
}