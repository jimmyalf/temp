using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using AmountType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.AmountType;
using IndicatorType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.IndicatorType;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("cac:AllowanceCharge", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class AllowanceChargeType {
    
		private IdentifierType idField;
    
		private ChargeIndicatorType chargeIndicatorField;
    
		private AllowanceChargeReasonCodeType reasonCodeField;
    
		private MultiplierFactorNumericType multiplierFactorNumericField;
    
		private CurrencyCodeType currencyCodeField;
    
		private IndicatorType prepaidIndicatorField;
    
		private SequenceNumericType sequenceNumericField;
    
		private AmountType amountField;
    
		private List<TaxCategoryType> taxCategoryField = new List<TaxCategoryType>();

		private List<PaymentMeansType> paymentMeansField = new List<PaymentMeansType>();
    
		/// <remarks/>
		public IdentifierType ID {
			get {
				return idField;
			}
			set {
				idField = value;
			}
		}
    
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
		public CurrencyCodeType CurrencyCode {
			get {
				return currencyCodeField;
			}
			set {
				currencyCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public IndicatorType PrepaidIndicator {
			get {
				return prepaidIndicatorField;
			}
			set {
				prepaidIndicatorField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public SequenceNumericType SequenceNumeric {
			get {
				return sequenceNumericField;
			}
			set {
				sequenceNumericField = value;
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
		[System.Xml.Serialization.XmlElement("TaxCategory")]
		public List<TaxCategoryType> TaxCategory {
			get {
				return taxCategoryField;
			}
			set {
				taxCategoryField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("PaymentMeans")]
		public List<PaymentMeansType> PaymentMeans {
			get {
				return paymentMeansField;
			}
			set {
				paymentMeansField = value;
			}
		}
	}
}