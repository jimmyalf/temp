using System.Collections.Generic;
using System.ComponentModel;

namespace Spinit.Wpc.Synologen.Presentation.Models.LensSubscription
{
	public class SubscriptionView
	{
		[DisplayName("Aktiverad")]
		public string Activated { get; set; }

		[DisplayName("Skapad")]
		public string Created { get; set; }

		[DisplayName("Adress 1")]
		public string AddressLineOne { get; set; }

		[DisplayName("Adress 2")]
		public string AddressLineTwo { get; set; }

		[DisplayName("Ort")]
		public string City { get; set; }

		[DisplayName("Postnummer")]
		public string PostalCode { get; set; }

		[DisplayName("Land")]
		public string Country { get; set; }

		[DisplayName("E-post")]
		public string Email { get; set; }

		[DisplayName("Mobiltelefon")]
		public string MobilePhone { get; set; }

		[DisplayName("Telefon")]
		public string Phone { get; set; }

		[DisplayName("Kund")]
		public string CustomerName { get; set; }

		[DisplayName("Personnummer")]
		public string PersonalIdNumber { get; set; }

		[DisplayName("Säljande butik")]
		public string ShopName { get; set; }

		[DisplayName("Transaktioner")]
		public IEnumerable<TransactionListItemView> TransactionList { get; set; }

		[DisplayName("Kontonummer")]
		public string AccountNumber { get; set; }

		[DisplayName("Clearingnummer")]
		public string ClearingNumber { get; set; }

		[DisplayName("Månadskostnad")]
		public string MonthlyAmount { get; set; }

		[DisplayName("Abonnemangstatus")]
		public string Status { get; set; }

		[DisplayName("Autogiro fel-lista")]
		public IEnumerable<ErrorListItemView> ErrorList { get; set; }

		[DisplayName("Kund-anteckningar")]
		public string CustomerNotes { get; set; }

		[DisplayName("Abonnemang-anteckningar")]
		public string SubscriptionNotes { get; set; }
	}
}