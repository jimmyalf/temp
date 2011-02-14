using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("LegalTotal", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class LegalTotalType {
    
		private ExtensionTotalAmountType lineExtensionTotalAmountField;
    
		private TotalAmountType taxExclusiveTotalAmountField;
    
		private TotalAmountType taxInclusiveTotalAmountField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public ExtensionTotalAmountType LineExtensionTotalAmount {
			get {
				return lineExtensionTotalAmountField;
			}
			set {
				lineExtensionTotalAmountField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public TotalAmountType TaxExclusiveTotalAmount {
			get {
				return taxExclusiveTotalAmountField;
			}
			set {
				taxExclusiveTotalAmountField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public TotalAmountType TaxInclusiveTotalAmount {
			get {
				return taxInclusiveTotalAmountField;
			}
			set {
				taxInclusiveTotalAmountField = value;
			}
		}
	}
}