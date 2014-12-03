using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("TaxScheme", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class TaxSchemeType {
    
		private IdentifierType idField;
    
		private CodeType taxTypeCodeField;
    
		private CurrencyCodeType currencyCodeField;
    
		private AddressType jurisdictionAddressField;
    
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
		public CodeType TaxTypeCode {
			get {
				return taxTypeCodeField;
			}
			set {
				taxTypeCodeField = value;
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
		public AddressType JurisdictionAddress {
			get {
				return jurisdictionAddressField;
			}
			set {
				jurisdictionAddressField = value;
			}
		}
	}
}