using System.Runtime.Serialization;

namespace Spinit.Wpc.Synologen.Business.Domain.Interfaces{
	public interface IOrderItem{
		[DataMember] int Id { get; set; }
		[DataMember] int ArticleId { get; set; }
		[DataMember] string ArticleDisplayName { get; set; }
		[DataMember] float SinglePrice { get; set; }
		[DataMember] int NumberOfItems { get; set; }
		[DataMember] string Notes { get; set; }
		[DataMember] string ArticleDisplayNumber { get; set; }
		[DataMember] float DisplayTotalPrice { get; set; }
		[DataMember] int OrderId { get; set; }
		[DataMember] bool NoVAT { get; set;}
		[DataMember] string SPCSAccountNumber { get; set; }
	}
}