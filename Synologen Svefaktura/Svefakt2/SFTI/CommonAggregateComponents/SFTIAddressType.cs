using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("Address", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTIAddressType {
    
		private IdentifierType idField;
    
		private PostboxType postboxField;
    
		private StreetNameType streetNameField;
    
		private DepartmentType departmentField;
    
		private CityNameType cityNameField;
    
		private ZoneType postalZoneField;
    
		private List<LineType> addressLineField = new List<LineType>();
    
		private SFTICountryType countryField;
    
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
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public PostboxType Postbox {
			get {
				return this.postboxField;
			}
			set {
				this.postboxField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public StreetNameType StreetName {
			get {
				return this.streetNameField;
			}
			set {
				this.streetNameField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DepartmentType Department {
			get {
				return this.departmentField;
			}
			set {
				this.departmentField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public CityNameType CityName {
			get {
				return this.cityNameField;
			}
			set {
				this.cityNameField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public ZoneType PostalZone {
			get {
				return this.postalZoneField;
			}
			set {
				this.postalZoneField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Line", Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0", IsNullable=false)]
		public List<LineType> AddressLine {
			get {
				return this.addressLineField;
			}
			set {
				this.addressLineField = value;
			}
		}
    
		/// <remarks/>
		public SFTICountryType Country {
			get {
				return this.countryField;
			}
			set {
				this.countryField = value;
			}
		}
	}
}