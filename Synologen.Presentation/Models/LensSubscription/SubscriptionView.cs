using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Spinit.Wpc.Synologen.Presentation.Models.LensSubscription
{
	public class SubscriptionView
	{
		[DisplayName("Autogiro medgiven")]
		public string ConsentDate { get; set; }

		[DisplayName("Skapad")]
		public string Created { get; set; }

		[DisplayName("Adress 1")]
		[Required(ErrorMessage = "Adress 1 m�ste anges")]
		public string AddressLineOne { get; set; }

		[DisplayName("Adress 2")]
		public string AddressLineTwo { get; set; }

		[DisplayName("Ort")]
		public string City { get; set; }

		[DisplayName("Postnummer")]
		[Required(ErrorMessage = "Postnummer m�ste anges")]
		public string PostalCode { get; set; }

		[DisplayName("Land")]
		public string Country { get; set; }

		[DisplayName("E-post")]
		[Required(ErrorMessage = "E-post m�ste anges")]
		[RegularExpression(@"^[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$", ErrorMessage = "Ogiltig e-post-adress")]
		public string Email { get; set; }

		[DisplayName("Mobiltelefon")]
		public string MobilePhone { get; set; }

		[DisplayName("Telefon")]
		public string Phone { get; set; }

		[DisplayName("Kund")]
		public string CustomerName { get; set; }

		[DisplayName("Personnummer")]
		[Required(ErrorMessage = "Personnummer m�ste anges")]
		[RegularExpression(@"\b(19\d{2}|20\d{2})(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])\d{4}\b", ErrorMessage = "Personnummer m�ste anges som ����MMDDXXXX")]
		public string PersonalIdNumber { get; set; }

		[DisplayName("S�ljande butik")]
		public string ShopName { get; set; }

		[DisplayName("Transaktioner")]
		public IEnumerable<TransactionListItemView> TransactionList { get; set; }

		[DisplayName("Kontonummer")]
		[Required(ErrorMessage = "Kontonummer m�ste anges")]
		[RegularExpression("^[0-9]{5,12}$", ErrorMessage = "Kontonummer m�ste anges som heltal med 5-12 siffror")]
		public string AccountNumber { get; set; }

		[DisplayName("Clearingnummer")]
		[Required(ErrorMessage = "Clearingnummer m�ste anges")]
		[RegularExpression("^[0-9]{4}$", ErrorMessage = "Clearingnummer m�ste anges som heltal med 4 siffror")]
		public string ClearingNumber { get; set; }

		[DisplayName("M�nadskostnad (kr)")]
		[Required(ErrorMessage = "M�nadsavgift m�ste anges")]
		[Range(typeof(decimal), "0", "99999,99", ErrorMessage = "M�nadsavgift m�ste anges som ett positivt tal med kommatecken som decimalavgr�nsare")]
		public string MonthlyAmount { get; set; }

		[DisplayName("Abonnemangstatus")]
		public string Status { get; set; }

		[DisplayName("Autogiro fel-lista")]
		public IEnumerable<ErrorListItemView> ErrorList { get; set; }

		[DisplayName("Kund-anteckningar")]
		public string CustomerNotes { get; set; }

		[DisplayName("Abonnemang-anteckningar")]
		public string SubscriptionNotes { get; set; }

		public int CustomerId { get; set; }

		[DisplayName("F�rnamn")]
		[Required(ErrorMessage = "F�rnamn m�ste anges")]
		public string FirstName { get; set; }

		[DisplayName("Efternamn")]
		[Required(ErrorMessage = "Efternamn m�ste anges")]
		public string LastName { get; set; }

		[DisplayName("Medgivandestatus")]
		public string ConsentStatus { get; set; }
	}
}