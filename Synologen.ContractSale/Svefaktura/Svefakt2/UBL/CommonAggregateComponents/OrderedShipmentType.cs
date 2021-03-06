using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("OrderedShipment", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class OrderedShipmentType {
    
		private ShipmentType shipmentField;

		private List<PackageType> packageField;
    
		/// <remarks/>
		public ShipmentType Shipment {
			get {
				return shipmentField;
			}
			set {
				shipmentField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("Package")]
		public List<PackageType> Package {
			get {
				return packageField;
			}
			set {
				packageField = value;
			}
		}
	}
}