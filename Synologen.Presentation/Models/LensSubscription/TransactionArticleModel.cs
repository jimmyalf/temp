using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Spinit.Wpc.Synologen.Presentation.Models.LensSubscription
{
	public class TransactionArticleModel
	{
		public int Id { get; set; }

		[Required]
		[DisplayName("Namn")]
		public string Name { get; set; }

		[DisplayName("Aktiv")]
		public bool Active { get; set; }

		public string FormLegend
		{
			get { return (Id > 0) ? "Redigera transaktionsartikel" : "Skapa transaktionsartikel"; }
		}
	}
}