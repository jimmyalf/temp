namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable()]
	[System.Diagnostics.DebuggerStepThrough()]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("Delivery", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTIDeliveryType {
    
		private DeliveryDateTimeType actualDeliveryDateTimeField;
    
		private SFTIAddressType deliveryAddressField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DeliveryDateTimeType ActualDeliveryDateTime {
			get {
				return this.actualDeliveryDateTimeField;
			}
			set {
				this.actualDeliveryDateTimeField = value;
			}
		}
    
		/// <remarks/>
		public SFTIAddressType DeliveryAddress {
			get {
				return this.deliveryAddressField;
			}
			set {
				this.deliveryAddressField = value;
			}
		}
	}
}