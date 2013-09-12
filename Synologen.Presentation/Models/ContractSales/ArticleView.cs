using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Spinit.Wpc.Synologen.Presentation.Models.ContractSales
{
	public class ArticleView
	{
		public int Id { get; set; }
		public string FormLegend { get; set; }
		public string ArticleListUrl { get; set; }

		[Required(ErrorMessage = "Namn �r obligatoriskt"), DisplayName("Namn")]
		public string Name { get; set; }

		[DisplayName("Beskrivning")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Artikelnummer �r obligatoriskt"), DisplayName("Artikelnummer")]
		public string ArticleNumber { get; set; }

		[Required(ErrorMessage = "SPCS kontonummer �r obligatoriskt"), DisplayName("SPCS kontonummer"), RegularExpression("^[0-9]{4}$", ErrorMessage = "Kontonummer m�ste anges som fyra siffror")]
		public string DefaultSPCSAccountNumber { get; set; }
	}
}