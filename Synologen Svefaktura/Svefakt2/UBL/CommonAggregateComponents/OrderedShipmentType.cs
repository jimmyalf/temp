using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("OrderedShipment", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class OrderedShipmentType {
    
		private ShipmentType shipmentField;
    
		private List<PackageType> packageField = new List<PackageType>();
    
		/// <remarks/>
		public ShipmentType Shipment {
			get {
				return this.shipmentField;
			}
			set {
				this.shipmentField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Package")]
		public List<PackageType> Package {
			get {
				return this.packageField;
			}
			set {
				this.packageField = value;
			}
		}
	}
}