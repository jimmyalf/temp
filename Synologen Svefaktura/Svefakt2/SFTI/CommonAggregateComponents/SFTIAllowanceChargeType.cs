using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable()]
	[System.Diagnostics.DebuggerStepThrough()]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	public partial class SFTIAllowanceChargeType {
    
		private ChargeIndicatorType chargeIndicatorField;
    
		private AllowanceChargeReasonCodeType reasonCodeField;
    
		private MultiplierFactorNumericType multiplierFactorNumericField;
    
		private AmountType2 amountField;
    
		private List<SFTITaxCategoryType> taxCategoryField = new List<SFTITaxCategoryType>();
    
		private AmountType2 allowanceChargeBaseAmountField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
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
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public MultiplierFactorNumericType MultiplierFactorNumeric {
			get {
				return this.multiplierFactorNumericField;
			}
			set {
				this.multiplierFactorNumericField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public AmountType2 Amount {
			get {
				return this.amountField;
			}
			set {
				this.amountField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("TaxCategory")]
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