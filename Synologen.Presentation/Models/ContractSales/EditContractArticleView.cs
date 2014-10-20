using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Spinit.Wpc.Synologen.Presentation.Models.ContractSales
{
	public class EditContractArticleView : IContractArticleView
	{
		[DisplayName("SPCS Kontonummer"), Required(ErrorMessage = "SPCS Kontonummer är obligatoriskt"), RegularExpression("^[0-9]{4}$", ErrorMessage = "Konto måste anges med fyra siffror")]
		public string SPCSAccountNumber { get; set; }
		
		public string ArticleName { get; set; }
		public string ContractName { get; set; }

		[DisplayName("Pris exklusive moms"), Required(ErrorMessage = "Pris är obligatoriskt"), RegularExpression("^[1-9]{1}[0-9]*$", ErrorMessage = "Pris måste anges som heltal utan decimaler")]
		public string PriceWithoutVAT { get; set; }

        [DisplayName("Kundens artikelnummer"), RegularExpression("^[1-9]{1}[0-9]*$")]
		public string CustomerArticelId { get; set; }

        [DisplayName("Förmåns Id"), RegularExpression("^[1-9]{1}[0-9]*$")]
		public string DiscountId { get; set; }

		[DisplayName("Momsfri")]
		public bool IsVATFreeArticle { get; set; }

		[DisplayName("Tillåt manuell prissättning")]
		public bool AllowCustomPricing { get; set; }

		[DisplayName("Tillgänglig för försäljning")]
		public bool IsActive { get; set; }

		public string ContractArticleListUrl { get; set; }
		public int Id { get; set; }
	}
}