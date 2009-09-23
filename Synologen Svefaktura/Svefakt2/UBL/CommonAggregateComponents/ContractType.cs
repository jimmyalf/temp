using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("Contract", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class ContractType {
    
		private IdentifierType idField;
    
		private IssueDateType issueDateField;
    
		private CodeType contractTypeCodeField;
    
		private PeriodType validityPeriodField;
    
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
		public IssueDateType IssueDate {
			get {
				return this.issueDateField;
			}
			set {
				this.issueDateField = value;
			}
		}
    
		/// <remarks/>
		public CodeType ContractTypeCode {
			get {
				return this.contractTypeCodeField;
			}
			set {
				this.contractTypeCodeField = value;
			}
		}
    
		/// <remarks/>
		public PeriodType ValidityPeriod {
			get {
				return this.validityPeriodField;
			}
			set {
				this.validityPeriodField = value;
			}
		}
	}
}