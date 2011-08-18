using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Spinit.Wpc.Synologen.Presentation.Models.ContractSales
{
	public class AddContractArticleView : IContractArticleView
	{
		[DisplayName("SPCS Kontonummer"), Required(ErrorMessage = "SPCS Kontonummer �r obligatoriskt"), RegularExpression("^[0-9]{4}$", ErrorMessage = "Konto m�ste anges med fyra siffror")]
		public string SPCSAccountNumber { get; set; }

		[DisplayName("Artikel"), Required(ErrorMessage = "Artikel �r obligatorisk")]
		public int ArticleId { get; set; }

		[DisplayName("Pris exklusive moms"), Required(ErrorMessage = "Pris �r obligatoriskt"), RegularExpression("^[1-9]{1}[0-9]*$", ErrorMessage = "Pris m�ste anges som heltal utan decimaler")]
		public string PriceWithoutVAT { get; set; }

		[DisplayName("Momsfri")]
		public bool IsVATFreeArticle { get; set; }

		[DisplayName("Till�t manuell priss�ttning")]
		public bool AllowCustomPricing { get; set; }

		[DisplayName("Tillg�nglig f�r f�rs�ljning")]
		public bool IsActive { get; set; }

		public string ContractArticleListUrl { get; set; }
		public string ContractName { get; set; }
		public int ContractId { get; set; }
		public SelectList Articles { get; set; }
	}
}