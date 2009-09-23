namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	public partial class SFTIInvoiceLineDeliveryType {
    
		private DeliveryDateTimeType actualDeliveryDateTimeField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DeliveryDateTimeType ActualDeliveryDateTime {
			get {
				return this.actualDeliveryDateTimeField;
			}
			set {
				this.actualDeliveryDateTimeField = value;
			}
		}
	}
}