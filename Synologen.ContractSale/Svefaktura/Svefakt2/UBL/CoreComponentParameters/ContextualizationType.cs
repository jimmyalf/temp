using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CoreComponentParameters {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CoreComponentParameters:1:0")]
	[System.Xml.Serialization.XmlRoot("Contextualization", Namespace="urn:oasis:names:tc:ubl:CoreComponentParameters:1:0", IsNullable=false)]
	public class ContextualizationType {

		private List<ContextType> contextField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("Context")]
		public List<ContextType> Context {
			get {
				return contextField;
			}
			set {
				contextField = value;
			}
		}
	}
}