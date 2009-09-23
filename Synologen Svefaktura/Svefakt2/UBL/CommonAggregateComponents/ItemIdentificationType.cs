using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("AdditionalItemIdentification", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class ItemIdentificationType {
    
		private IdentifierType idField;
    
		private List<PhysicalAttributeType> physicalAttributeField = new List<PhysicalAttributeType>();
    
		private List<DimensionType> measurementDimensionField = new List<DimensionType>();
    
		private PartyType issuerPartyField;
    
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
		[System.Xml.Serialization.XmlElementAttribute("PhysicalAttribute")]
		public List<PhysicalAttributeType> PhysicalAttribute {
			get {
				return this.physicalAttributeField;
			}
			set {
				this.physicalAttributeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("MeasurementDimension")]
		public List<DimensionType> MeasurementDimension {
			get {
				return this.measurementDimensionField;
			}
			set {
				this.measurementDimensionField = value;
			}
		}
    
		/// <remarks/>
		public PartyType IssuerParty {
			get {
				return this.issuerPartyField;
			}
			set {
				this.issuerPartyField = value;
			}
		}
	}
}