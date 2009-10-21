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
	[System.Xml.Serialization.XmlRoot("PaymentMeans", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public class SFTIPaymentMeansType {
    
		private PaymentMeansCodeType paymentMeansTypeCodeField;
    
		private PaymentDateType duePaymentDateField;
    
		private SFTIFinancialAccountType payeeFinancialAccountField;

		private List<NameType> payeePartyNameField;
    
		/// <remarks/>
		[PropertyValidationRule("SFTIPaymentMeansType.PaymentMeansTypeCode is missing.", ValidationType.RequiredNotNull)]
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
		public SFTIFinancialAccountType PayeeFinancialAccount {
			get {
				return payeeFinancialAccountField;
			}
			set {
				payeeFinancialAccountField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItem("Name", Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0", IsNullable=false)]
		public List<NameType> PayeePartyName {
			get {
				return payeePartyNameField;
			}
			set {
				payeePartyNameField = value;
			}
		}
	}
}