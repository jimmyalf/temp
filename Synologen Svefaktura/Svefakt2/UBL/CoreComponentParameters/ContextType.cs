using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CoreComponentParameters:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("Context", Namespace="urn:oasis:names:tc:ubl:CoreComponentParameters:1:0", IsNullable=false)]
	public partial class ContextType {
    
		private List<string> industryClassificationField = new List<string>();

		private List<string> geopoliticalField = new List<string>();

		private List<string> businessProcessField = new List<string>();

		private List<string> officialConstraintField = new List<string>();
    
		private List<string> productClassificationField = new List<string>();
    
		private List<string> businessProcessRoleField = new List<string>();
    
		private List<string> supportingRoleField = new List<string>();
    
		private List<string> systemCapabilityField = new List<string>();
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("IndustryClassification")]
		public List<string> IndustryClassification {
			get {
				return this.industryClassificationField;
			}
			set {
				this.industryClassificationField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Geopolitical")]
		public List<string> Geopolitical {
			get {
				return this.geopoliticalField;
			}
			set {
				this.geopoliticalField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("BusinessProcess")]
		public List<string> BusinessProcess {
			get {
				return this.businessProcessField;
			}
			set {
				this.businessProcessField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("OfficialConstraint")]
		public List<string> OfficialConstraint {
			get {
				return this.officialConstraintField;
			}
			set {
				this.officialConstraintField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("ProductClassification")]
		public List<string> ProductClassification {
			get {
				return this.productClassificationField;
			}
			set {
				this.productClassificationField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("BusinessProcessRole")]
		public List<string> BusinessProcessRole {
			get {
				return this.businessProcessRoleField;
			}
			set {
				this.businessProcessRoleField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("SupportingRole")]
		public List<string> SupportingRole {
			get {
				return this.supportingRoleField;
			}
			set {
				this.supportingRoleField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("SystemCapability")]
		public List<string> SystemCapability {
			get {
				return this.systemCapabilityField;
			}
			set {
				this.systemCapabilityField = value;
			}
		}
	}
}