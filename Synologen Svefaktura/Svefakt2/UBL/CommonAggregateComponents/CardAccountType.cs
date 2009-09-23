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
	[System.Xml.Serialization.XmlRoot("cac:CardAccount", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class CardAccountType {
    
		private IdentifierType primaryAccountNumberIDField;
    
		private IdentifierType networkIDField;
    
		private CodeType cardTypeCodeField;
    
		private IdentifierType customerIDField;
    
		private ValidityStartDateType validityStartDateField;
    
		private ExpiryDateType expiryDateField;
    
		private IdentifierType issuerIDField;
    
		private IdentifierType issueNumberIDField;
    
		private IdentifierType cV2IDField;
    
		private ChipCodeType chipCodeField;
    
		private IdentifierType chipApplicationIDField;
    
		private HolderNameType holderNameField;
    
		/// <remarks/>
		public IdentifierType PrimaryAccountNumberID {
			get {
				return primaryAccountNumberIDField;
			}
			set {
				primaryAccountNumberIDField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType NetworkID {
			get {
				return networkIDField;
			}
			set {
				networkIDField = value;
			}
		}
    
		/// <remarks/>
		public CodeType CardTypeCode {
			get {
				return cardTypeCodeField;
			}
			set {
				cardTypeCodeField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType CustomerID {
			get {
				return customerIDField;
			}
			set {
				customerIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public ValidityStartDateType ValidityStartDate {
			get {
				return validityStartDateField;
			}
			set {
				validityStartDateField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public ExpiryDateType ExpiryDate {
			get {
				return expiryDateField;
			}
			set {
				expiryDateField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType IssuerID {
			get {
				return issuerIDField;
			}
			set {
				issuerIDField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType IssueNumberID {
			get {
				return issueNumberIDField;
			}
			set {
				issueNumberIDField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType CV2ID {
			get {
				return cV2IDField;
			}
			set {
				cV2IDField = value;
			}
		}
    
		/// <remarks/>
		public ChipCodeType ChipCode {
			get {
				return chipCodeField;
			}
			set {
				chipCodeField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType ChipApplicationID {
			get {
				return chipApplicationIDField;
			}
			set {
				chipApplicationIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public HolderNameType HolderName {
			get {
				return holderNameField;
			}
			set {
				holderNameField = value;
			}
		}
	}
}