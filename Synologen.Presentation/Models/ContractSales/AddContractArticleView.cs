using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Spinit.Wpc.Synologen.Presentation.Models.ContractSales
{
	public class AddContractArticleView : IContractArticleView
	{
		[DisplayName("SPCS Kontonummer"), Required(ErrorMessage = "SPCS Kontonummer är obligatoriskt"), RegularExpression("^[0-9]{4}$", ErrorMessage = "Konto måste anges med fyra siffror")]
		public string SPCSAccountNumber { get; set; }

		[DisplayName("Artikel"), Required(ErrorMessage = "Artikel är obligatorisk")]
		public int ArticleId { get; set; }

		[DisplayName("Pris exklusive moms"), Required(ErrorMessage = "Pris är obligatoriskt"), RegularExpression("^[1-9]{1}[0-9]*$", ErrorMessage = "Pris måste anges som heltal utan decimaler")]
		public string PriceWithoutVAT { get; set; }

		[DisplayName("Momsfri")]
		public bool IsVATFreeArticle { get; set; }

		[DisplayName("Tillåt manuell prissättning")]
		public bool AllowCustomPricing { get; set; }

		[DisplayName("Tillgänglig för försäljning")]
		public bool IsActive { get; set; }

		public string ContractArticleListUrl { get; set; }
		public string ContractName { get; set; }
		public int ContractId { get; set; }
		public SelectList Articles { get; set; }
	}
}