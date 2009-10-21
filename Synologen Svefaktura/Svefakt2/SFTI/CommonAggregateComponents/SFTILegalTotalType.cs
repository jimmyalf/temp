using Spinit.Wpc.Synologen.Svefaktura.CustomEnumerations;
using Spinit.Wpc.Synologen.Svefaktura.CustomTypes;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("LegalTotal", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public class SFTILegalTotalType {
    
		private ExtensionTotalAmountType lineExtensionTotalAmountField;
    
		private TotalAmountType taxExclusiveTotalAmountField;
    
		private TotalAmountType taxInclusiveTotalAmountField;
    
		private AmountType roundOffAmountField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		[PropertyValidationRule("SFTILegalTotalType.LineExtensionTotalAmount is missing.", ValidationType.RequiredNotNull)]
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
		[PropertyValidationRule("SFTILegalTotalType.TaxInclusiveTotalAmount is missing.", ValidationType.RequiredNotNull)]
		public TotalAmountType TaxInclusiveTotalAmount {
			get {
				return taxInclusiveTotalAmountField;
			}
			set {
				taxInclusiveTotalAmountField = value;
			}
		}
    
		/// <remarks/>
		public AmountType RoundOffAmount {
			get {
				return roundOffAmountField;
			}
			set {
				roundOffAmountField = value;
			}
		}
	}
}