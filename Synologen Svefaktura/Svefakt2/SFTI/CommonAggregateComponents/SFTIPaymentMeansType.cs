using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("PaymentMeans", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTIPaymentMeansType {
    
		private PaymentMeansCodeType paymentMeansTypeCodeField;
    
		private PaymentDateType duePaymentDateField;
    
		private SFTIFinancialAccountType payeeFinancialAccountField;
    
		private List<NameType1> payeePartyNameField = new List<NameType1>();
    
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
		public SFTIFinancialAccountType PayeeFinancialAccount {
			get {
				return this.payeeFinancialAccountField;
			}
			set {
				this.payeeFinancialAccountField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Name", Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0", IsNullable=false)]
		public List<NameType1> PayeePartyName {
			get {
				return this.payeePartyNameField;
			}
			set {
				this.payeePartyNameField = value;
			}
		}
	}
}