using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.CustomEnumerations;
using Spinit.Wpc.Synologen.Svefaktura.CustomTypes;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	public class SFTIAllowanceChargeType {
    
		private ChargeIndicatorType chargeIndicatorField;
    
		private AllowanceChargeReasonCodeType reasonCodeField;
    
		private MultiplierFactorNumericType multiplierFactorNumericField;
    
		private AmountType amountField;

		private List<SFTITaxCategoryType> taxCategoryField;
    
		private AmountType allowanceChargeBaseAmountField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		[PropertyValidationRule("SFTIAllowanceChargeType.ChargeIndicator is missing",ValidationType.RequiredNotNull)]
		public ChargeIndicatorType ChargeIndicator {
			get {
				return chargeIndicatorField;
			}
			set {
				chargeIndicatorField = value;
			}
		}
    
		/// <remarks/>
		public AllowanceChargeReasonCodeType ReasonCode {
			get {
				return reasonCodeField;
			}
			set {
				reasonCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public MultiplierFactorNumericType MultiplierFactorNumeric {
			get {
				return multiplierFactorNumericField;
			}
			set {
				multiplierFactorNumericField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		[PropertyValidationRule("SFTIAllowanceChargeType.Amount is missing",ValidationType.RequiredNotNull)]
		public AmountType Amount {
			get {
				return amountField;
			}
			set {
				amountField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("TaxCategory")]
		public List<SFTITaxCategoryType> TaxCategory {
			get {
				return taxCategoryField;
			}
			set {
				taxCategoryField = value;
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