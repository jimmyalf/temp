using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Spinit.Wpc.Synologen.Presentation.Models.ContractSales
{
	public class ArticleView
	{
		public int Id { get; set; }
		public string FormLegend { get; set; }
		public string ArticleListUrl { get; set; }

		[Required, DisplayName("Namn")]
		public string Name { get; set; }

		[DisplayName("Beskrivning")]
		public string Description { get; set; }

		[Required, DisplayName("Artikelnummer")]
		public string ArticleNumber { get; set; }

		[Required, DisplayName("SPCS kontonummer"), RegularExpression("^[0-9]{4}$", ErrorMessage = "Kontonummer måste anges som fyra siffror")]
		public string DefaultSPCSAccountNumber { get; set; }
	}
}