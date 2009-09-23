using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("AllowanceCharge", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class AllowanceChargeType {
    
		private IdentifierType idField;
    
		private ChargeIndicatorType chargeIndicatorField;
    
		private AllowanceChargeReasonCodeType reasonCodeField;
    
		private MultiplierFactorNumericType multiplierFactorNumericField;
    
		private CurrencyCodeType currencyCodeField;
    
		private IndicatorType1 prepaidIndicatorField;
    
		private SequenceNumericType sequenceNumericField;
    
		private AmountType2 amountField;
    
		private List<TaxCategoryType> taxCategoryField = new List<TaxCategoryType>();

		private List<PaymentMeansType> paymentMeansField = new List<PaymentMeansType>();
    
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
		public CurrencyCodeType CurrencyCode {
			get {
				return this.currencyCodeField;
			}
			set {
				this.currencyCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public IndicatorType1 PrepaidIndicator {
			get {
				return this.prepaidIndicatorField;
			}
			set {
				this.prepaidIndicatorField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public SequenceNumericType SequenceNumeric {
			get {
				return this.sequenceNumericField;
			}
			set {
				this.sequenceNumericField = value;
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
		public List<TaxCategoryType> TaxCategory {
			get {
				return this.taxCategoryField;
			}
			set {
				this.taxCategoryField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("PaymentMeans")]
		public List<PaymentMeansType> PaymentMeans {
			get {
				return this.paymentMeansField;
			}
			set {
				this.paymentMeansField = value;
			}
		}
	}
}