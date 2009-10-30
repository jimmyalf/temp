namespace Spinit.Wpc.Synologen.Business.Domain.Interfaces{
	public interface IArticleRow {
		int Id { get; set; }
		string Name { get; set; }
		string Number { get; set; }
		string Description { get; set; }
		bool NoVAT { get; set; }
	}
}