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
	[System.Xml.Serialization.XmlRoot("cac:FinancialAccount", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class FinancialAccountType {
    
		private IdentifierType idField;
    
		private NameType1 nameField;
    
		private CodeType accountTypeCodeField;
    
		private CurrencyCodeType currencyCodeField;
    
		private BranchType financialInstitutionBranchField;
    
		private CountryType countryField;
    
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
		public NameType1 Name {
			get {
				return nameField;
			}
			set {
				nameField = value;
			}
		}
    
		/// <remarks/>
		public CodeType AccountTypeCode {
			get {
				return accountTypeCodeField;
			}
			set {
				accountTypeCodeField = value;
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
		public BranchType FinancialInstitutionBranch {
			get {
				return financialInstitutionBranchField;
			}
			set {
				financialInstitutionBranchField = value;
			}
		}
    
		/// <remarks/>
		public CountryType Country {
			get {
				return countryField;
			}
			set {
				countryField = value;
			}
		}
	}
}