namespace Spinit.Wpc.Synologen.Presentation.Models.ContractSales
{
	public class OrderView
	{
		public int Id { get; set; }
		public string VISMAInvoiceNumber { get; set; }
		public string Status { get; set; }
		public bool DisplayCancelButton { get; set; }
		public string BackUrl { get; set; }
	}
}