using Spinit.Wpc.Synologen.EDI.Common.BaseTypes;
using Spinit.Wpc.Synologen.EDI.Common.Types;

namespace Spinit.Wpc.Synologen.EDI.Types {
	public class Buyer : InvoiceComponent {
		public string BuyerIdentity { get; set; } //NAD+BY+<Nummer>:30'
		public string ContactName { get; set; } //CTA+PD+:<Kontaktperson-namn>'
		public string InvoiceIdentity { get; set; } //NAD+IV+<Nummer>:30'
		public string ReferenceNumber { get; set; } //RFF+IT+<Nummer>'
		public Address DeliveryAddress { get; set; } //NAD+CN+++<Address1>+<Address2>+<Ort>++<PostNr utan mellanslag>'
		//public string Leverantörskontra { get; set; } //CTA+GR:<text>'

		public new ParsedBuyer Parse() { return new ParsedBuyer(this); }

	}
}