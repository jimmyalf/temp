using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("AddressLine", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public class SFTIAddressLineType {
    
		private List<LineType> lineField = new List<LineType>();
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("Line", Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public List<LineType> Line {
			get {
				return lineField;
			}
			set {
				lineField = value;
			}
		}
	}
}