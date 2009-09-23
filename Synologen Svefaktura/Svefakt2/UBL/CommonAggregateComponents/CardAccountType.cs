using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("CardAccount", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class CardAccountType {
    
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
				return this.primaryAccountNumberIDField;
			}
			set {
				this.primaryAccountNumberIDField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType NetworkID {
			get {
				return this.networkIDField;
			}
			set {
				this.networkIDField = value;
			}
		}
    
		/// <remarks/>
		public CodeType CardTypeCode {
			get {
				return this.cardTypeCodeField;
			}
			set {
				this.cardTypeCodeField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType CustomerID {
			get {
				return this.customerIDField;
			}
			set {
				this.customerIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public ValidityStartDateType ValidityStartDate {
			get {
				return this.validityStartDateField;
			}
			set {
				this.validityStartDateField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public ExpiryDateType ExpiryDate {
			get {
				return this.expiryDateField;
			}
			set {
				this.expiryDateField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType IssuerID {
			get {
				return this.issuerIDField;
			}
			set {
				this.issuerIDField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType IssueNumberID {
			get {
				return this.issueNumberIDField;
			}
			set {
				this.issueNumberIDField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType CV2ID {
			get {
				return this.cV2IDField;
			}
			set {
				this.cV2IDField = value;
			}
		}
    
		/// <remarks/>
		public ChipCodeType ChipCode {
			get {
				return this.chipCodeField;
			}
			set {
				this.chipCodeField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType ChipApplicationID {
			get {
				return this.chipApplicationIDField;
			}
			set {
				this.chipApplicationIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public HolderNameType HolderName {
			get {
				return this.holderNameField;
			}
			set {
				this.holderNameField = value;
			}
		}
	}
}