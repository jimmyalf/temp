using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Spinit.Wpc.Synologen.Presentation.Models.ContractSales
{
	public class ArticleView
	{
		public int Id { get; set; }

		[Required, DisplayName("Namn")]
		public string Name { get; set; }

		[DisplayName("Beskrivning")]
		public string Description { get; set; }

		[Required, DisplayName("Artikelnummer")]
		public string ArticleNumber { get; set; }

		[Required, DisplayName("Förifyllt SPCS kontonummer")]
		public string DefaultSPCSAccountNumber { get; set; }
	}
}