using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("PaymentMeans", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class PaymentMeansType {
    
		private PaymentMeansCodeType paymentMeansTypeCodeField;
    
		private PaymentDateType duePaymentDateField;
    
		private CodeType paymentChannelCodeField;
    
		private CardAccountType cardAccountField;
    
		private FinancialAccountType payerFinancialAccountField;
    
		private FinancialAccountType payeeFinancialAccountField;
    
		private CreditAccountType creditAccountField;
    
		private PaymentType paymentField;
    
		/// <remarks/>
		public PaymentMeansCodeType PaymentMeansTypeCode {
			get {
				return this.paymentMeansTypeCodeField;
			}
			set {
				this.paymentMeansTypeCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public PaymentDateType DuePaymentDate {
			get {
				return this.duePaymentDateField;
			}
			set {
				this.duePaymentDateField = value;
			}
		}
    
		/// <remarks/>
		public CodeType PaymentChannelCode {
			get {
				return this.paymentChannelCodeField;
			}
			set {
				this.paymentChannelCodeField = value;
			}
		}
    
		/// <remarks/>
		public CardAccountType CardAccount {
			get {
				return this.cardAccountField;
			}
			set {
				this.cardAccountField = value;
			}
		}
    
		/// <remarks/>
		public FinancialAccountType PayerFinancialAccount {
			get {
				return this.payerFinancialAccountField;
			}
			set {
				this.payerFinancialAccountField = value;
			}
		}
    
		/// <remarks/>
		public FinancialAccountType PayeeFinancialAccount {
			get {
				return this.payeeFinancialAccountField;
			}
			set {
				this.payeeFinancialAccountField = value;
			}
		}
    
		/// <remarks/>
		public CreditAccountType CreditAccount {
			get {
				return this.creditAccountField;
			}
			set {
				this.creditAccountField = value;
			}
		}
    
		/// <remarks/>
		public PaymentType Payment {
			get {
				return this.paymentField;
			}
			set {
				this.paymentField = value;
			}
		}
	}
}