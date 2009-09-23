using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("cac:PaymentMeans", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class PaymentMeansType {
    
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
				return paymentMeansTypeCodeField;
			}
			set {
				paymentMeansTypeCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public PaymentDateType DuePaymentDate {
			get {
				return duePaymentDateField;
			}
			set {
				duePaymentDateField = value;
			}
		}
    
		/// <remarks/>
		public CodeType PaymentChannelCode {
			get {
				return paymentChannelCodeField;
			}
			set {
				paymentChannelCodeField = value;
			}
		}
    
		/// <remarks/>
		public CardAccountType CardAccount {
			get {
				return cardAccountField;
			}
			set {
				cardAccountField = value;
			}
		}
    
		/// <remarks/>
		public FinancialAccountType PayerFinancialAccount {
			get {
				return payerFinancialAccountField;
			}
			set {
				payerFinancialAccountField = value;
			}
		}
    
		/// <remarks/>
		public FinancialAccountType PayeeFinancialAccount {
			get {
				return payeeFinancialAccountField;
			}
			set {
				payeeFinancialAccountField = value;
			}
		}
    
		/// <remarks/>
		public CreditAccountType CreditAccount {
			get {
				return creditAccountField;
			}
			set {
				creditAccountField = value;
			}
		}
    
		/// <remarks/>
		public PaymentType Payment {
			get {
				return paymentField;
			}
			set {
				paymentField = value;
			}
		}
	}
}