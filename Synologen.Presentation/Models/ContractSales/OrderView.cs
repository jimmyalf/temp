using System.ComponentModel;

namespace Spinit.Wpc.Synologen.Presentation.Models.ContractSales
{
	public class OrderView
	{
		[DisplayName("WPC-Id")]
		public int Id { get; set; }

		[DisplayName("VISMA Fakturanr")]
		public string VISMAInvoiceNumber { get; set; }

		[DisplayName("Status")]
		public string Status { get; set; }

		public bool DisplayCancelButton { get; set; }
		public string BackUrl { get; set; }
	}
}