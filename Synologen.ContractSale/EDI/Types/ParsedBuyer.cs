using Spinit.Wpc.Synologen.EDI.Common;
using Spinit.Wpc.Synologen.EDI.Common.BaseTypes;

namespace Spinit.Wpc.Synologen.EDI.Types {
	public class ParsedBuyer : IParsedComponent {

		private readonly Buyer buyer;
		public ParsedBuyer(Buyer buyerToParse) { buyer = buyerToParse; }

		private const string BuyerIdentityFormat = "NAD+BY+{0}'";
		private const string ContactNameFormat = "CTA+PD+:{0}'";
		private const string InvoiceIdentityFormat = "NAD+IV+{0}:30'";
		private const string ReferenceNumberFormat = "RFF+IT+{0}'";
		private const string DeliveryAddressFormat = "NAD+CN+++{0}+{1}+{3}++{2}'";
		//public string Leverantörskontra = "CTA+GR:<text>'";

		protected string ParsedBuyerIdentity {
			get { return string.Format(BuyerIdentityFormat, buyer.BuyerIdentity); }
		}
		protected string ParsedContactName {
			get { return EDIUtility.ParseRow(ContactNameFormat, buyer.ContactName); }
		}
		protected string ParsedInvoiceIdentity {
			get { return EDIUtility.ParseRow(InvoiceIdentityFormat, buyer.InvoiceIdentity); }
		}
		protected string ParsedReferenceNumber {
			get { return EDIUtility.ParseRow(ReferenceNumberFormat, buyer.ReferenceNumber); }
		}
		protected string ParsedDeliveryAddress {
			get { return EDIUtility.ParseRow(DeliveryAddressFormat, buyer.DeliveryAddress); }
		}

		public override string ToString() {
			var block = string.Empty;
			block = EDIUtility.AddRowToBlock(block, ParsedBuyerIdentity);
			block = EDIUtility.AddRowToBlock(block, ParsedContactName);
			block = EDIUtility.AddRowToBlock(block, ParsedInvoiceIdentity);
			block = EDIUtility.AddRowToBlock(block, ParsedReferenceNumber);
			return  EDIUtility.AddRowToBlock(block, ParsedDeliveryAddress);
		}
	}
}
