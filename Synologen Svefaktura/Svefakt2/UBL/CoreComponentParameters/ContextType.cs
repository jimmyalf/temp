using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CoreComponentParameters {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CoreComponentParameters:1:0")]
	[System.Xml.Serialization.XmlRoot("Context", Namespace="urn:oasis:names:tc:ubl:CoreComponentParameters:1:0", IsNullable=false)]
	public class ContextType {
    
		private List<string> industryClassificationField = new List<string>();

		private List<string> geopoliticalField = new List<string>();

		private List<string> businessProcessField = new List<string>();

		private List<string> officialConstraintField = new List<string>();
    
		private List<string> productClassificationField = new List<string>();
    
		private List<string> businessProcessRoleField = new List<string>();
    
		private List<string> supportingRoleField = new List<string>();
    
		private List<string> systemCapabilityField = new List<string>();
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("IndustryClassification")]
		public List<string> IndustryClassification {
			get {
				return industryClassificationField;
			}
			set {
				industryClassificationField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("Geopolitical")]
		public List<string> Geopolitical {
			get {
				return geopoliticalField;
			}
			set {
				geopoliticalField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("BusinessProcess")]
		public List<string> BusinessProcess {
			get {
				return businessProcessField;
			}
			set {
				businessProcessField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("OfficialConstraint")]
		public List<string> OfficialConstraint {
			get {
				return officialConstraintField;
			}
			set {
				officialConstraintField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("ProductClassification")]
		public List<string> ProductClassification {
			get {
				return productClassificationField;
			}
			set {
				productClassificationField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("BusinessProcessRole")]
		public List<string> BusinessProcessRole {
			get {
				return businessProcessRoleField;
			}
			set {
				businessProcessRoleField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("SupportingRole")]
		public List<string> SupportingRole {
			get {
				return supportingRoleField;
			}
			set {
				supportingRoleField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("SystemCapability")]
		public List<string> SystemCapability {
			get {
				return systemCapabilityField;
			}
			set {
				systemCapabilityField = value;
			}
		}
	}
}