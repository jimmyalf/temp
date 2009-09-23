using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	public partial class SFTIAllowanceChargeType {
    
		private ChargeIndicatorType chargeIndicatorField;
    
		private AllowanceChargeReasonCodeType reasonCodeField;
    
		private MultiplierFactorNumericType multiplierFactorNumericField;
    
		private AmountType2 amountField;
    
		private List<SFTITaxCategoryType> taxCategoryField = new List<SFTITaxCategoryType>();
    
		private AmountType2 allowanceChargeBaseAmountField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public ChargeIndicatorType ChargeIndicator {
			get {
				return this.chargeIndicatorField;
			}
			set {
				this.chargeIndicatorField = value;
			}
		}
    
		/// <remarks/>
		public AllowanceChargeReasonCodeType ReasonCode {
			get {
				return this.reasonCodeField;
			}
			set {
				this.reasonCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public MultiplierFactorNumericType MultiplierFactorNumeric {
			get {
				return this.multiplierFactorNumericField;
			}
			set {
				this.multiplierFactorNumericField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public AmountType2 Amount {
			get {
				return this.amountField;
			}
			set {
				this.amountField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("TaxCategory")]
		public List<SFTITaxCategoryType> TaxCategory {
			get {
				return this.taxCategoryField;
			}
			set {
				this.taxCategoryField = value;
			}
		}
    
		/// <remarks/>
		public AmountType2 AllowanceChargeBaseAmount {
			get {
				return this.allowanceChargeBaseAmountField;
			}
			set {
				this.allowanceChargeBaseAmountField = value;
			}
		}
	}
}