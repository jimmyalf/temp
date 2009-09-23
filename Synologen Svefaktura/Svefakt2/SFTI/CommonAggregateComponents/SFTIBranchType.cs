namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("FinancialInstitutionBranch", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTIBranchType {
    
		private SFTIFinancialInstitutionType financialInstitutionField;
    
		/// <remarks/>
		public SFTIFinancialInstitutionType FinancialInstitution {
			get {
				return this.financialInstitutionField;
			}
			set {
				this.financialInstitutionField = value;
			}
		}
	}
}