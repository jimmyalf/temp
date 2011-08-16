using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Spinit.Wpc.Synologen.Presentation.Models.ContractSales
{
	public class ArticleView
	{
		public int Id { get; set; }
		public string FormLegend { get; set; }
		public string ArticleListUrl { get; set; }

		[Required(ErrorMessage = "Namn är obligatoriskt"), DisplayName("Namn")]
		public string Name { get; set; }

		[DisplayName("Beskrivning")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Artikelnummer är obligatoriskt"), DisplayName("Artikelnummer")]
		public string ArticleNumber { get; set; }

		[Required(ErrorMessage = "SPCS kontonummer är obligatoriskt"), DisplayName("SPCS kontonummer"), RegularExpression("^[0-9]{4}$", ErrorMessage = "Kontonummer måste anges som fyra siffror")]
		public string DefaultSPCSAccountNumber { get; set; }
	}
}