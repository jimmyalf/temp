using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable()]
	[System.Diagnostics.DebuggerStepThrough()]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("Payment", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTIPaymentType {
    
		private IdentifierType idField;
    
		private AmountType2 paidAmountField;
    
		private DateType1 receivedDateField;
    
		/// <remarks/>
		public IdentifierType ID {
			get {
				return this.idField;
			}
			set {
				this.idField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public AmountType2 PaidAmount {
			get {
				return this.paidAmountField;
			}
			set {
				this.paidAmountField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DateType1 ReceivedDate {
			get {
				return this.receivedDateField;
			}
			set {
				this.receivedDateField = value;
			}
		}
	}
}