using System.Web.Mvc;

namespace Spinit.Wpc.Synologen.Presentation.Models.ContractSales
{
	public class ContractArticleView
	{
		public string SPCSAccountNumber { get; set; }
		public int SelectedArticleId { get; set; }
		public int ContractId { get; set; }
		public SelectList Articles { get; set; }
		public decimal PriceWithoutVAT { get; set; }
		public bool IsVATFreeArticle { get; set; }
		public bool AllowCustomPricing { get; set; }
		public bool IsActive { get; set; }
	}
}