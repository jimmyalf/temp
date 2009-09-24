using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("AllowanceCharge", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public class SFTIInvoiceLineAllowanceCharge {
    
		private ChargeIndicatorType chargeIndicatorField;
    
		private AmountType amountField;
    
		private AmountType allowanceChargeBaseAmountField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public ChargeIndicatorType ChargeIndicator {
			get {
				return chargeIndicatorField;
			}
			set {
				chargeIndicatorField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public AmountType Amount {
			get {
				return amountField;
			}
			set {
				amountField = value;
			}
		}
    
		/// <remarks/>
		public AmountType AllowanceChargeBaseAmount {
			get {
				return allowanceChargeBaseAmountField;
			}
			set {
				allowanceChargeBaseAmountField = value;
			}
		}
	}
}