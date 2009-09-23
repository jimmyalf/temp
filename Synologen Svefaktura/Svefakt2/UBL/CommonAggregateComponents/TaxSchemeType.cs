using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("TaxScheme", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class TaxSchemeType {
    
		private IdentifierType idField;
    
		private CodeType taxTypeCodeField;
    
		private CurrencyCodeType currencyCodeField;
    
		private AddressType jurisdictionAddressField;
    
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
		public CodeType TaxTypeCode {
			get {
				return this.taxTypeCodeField;
			}
			set {
				this.taxTypeCodeField = value;
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
		public AddressType JurisdictionAddress {
			get {
				return this.jurisdictionAddressField;
			}
			set {
				this.jurisdictionAddressField = value;
			}
		}
	}
}