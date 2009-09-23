using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("PayeeFinancialAccount", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTIFinancialAccountType {
    
		private IdentifierType idField;
    
		private SFTIBranchType financialInstitutionBranchField;
    
		private SFTISimpleIdentifierType paymentInstructionIDField;
    
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
		public SFTIBranchType FinancialInstitutionBranch {
			get {
				return this.financialInstitutionBranchField;
			}
			set {
				this.financialInstitutionBranchField = value;
			}
		}
    
		/// <remarks/>
		public SFTISimpleIdentifierType PaymentInstructionID {
			get {
				return this.paymentInstructionIDField;
			}
			set {
				this.paymentInstructionIDField = value;
			}
		}
	}
}