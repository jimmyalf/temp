using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("Address", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class AddressType {
    
		private IdentifierType idField;
    
		private PostboxType postboxField;
    
		private FloorType floorField;
    
		private RoomType roomField;
    
		private StreetNameType streetNameField;
    
		private StreetNameType additionalStreetNameField;
    
		private BuildingNameType buildingNameField;
    
		private BuildingNumberType buildingNumberField;
    
		private MailType inhouseMailField;
    
		private DepartmentType departmentField;
    
		private CityNameType cityNameField;
    
		private ZoneType postalZoneField;
    
		private CountrySubentityType countrySubentityField;
    
		private CodeType countrySubentityCodeField;
    
		private RegionType regionField;
    
		private DistrictType districtField;
    
		private TimezoneOffsetType timezoneOffsetField;
    
		private List<LineType> addressLineField = new List<LineType>();
    
		private CountryType countryField;
    
		private LocationCoordinateType locationCoordinateField;
    
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
		public FloorType Floor {
			get {
				return this.floorField;
			}
			set {
				this.floorField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public RoomType Room {
			get {
				return this.roomField;
			}
			set {
				this.roomField = value;
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
		public StreetNameType AdditionalStreetName {
			get {
				return this.additionalStreetNameField;
			}
			set {
				this.additionalStreetNameField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public BuildingNameType BuildingName {
			get {
				return this.buildingNameField;
			}
			set {
				this.buildingNameField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public BuildingNumberType BuildingNumber {
			get {
				return this.buildingNumberField;
			}
			set {
				this.buildingNumberField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public MailType InhouseMail {
			get {
				return this.inhouseMailField;
			}
			set {
				this.inhouseMailField = value;
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
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public CountrySubentityType CountrySubentity {
			get {
				return this.countrySubentityField;
			}
			set {
				this.countrySubentityField = value;
			}
		}
    
		/// <remarks/>
		public CodeType CountrySubentityCode {
			get {
				return this.countrySubentityCodeField;
			}
			set {
				this.countrySubentityCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public RegionType Region {
			get {
				return this.regionField;
			}
			set {
				this.regionField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DistrictType District {
			get {
				return this.districtField;
			}
			set {
				this.districtField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public TimezoneOffsetType TimezoneOffset {
			get {
				return this.timezoneOffsetField;
			}
			set {
				this.timezoneOffsetField = value;
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
		public CountryType Country {
			get {
				return this.countryField;
			}
			set {
				this.countryField = value;
			}
		}
    
		/// <remarks/>
		public LocationCoordinateType LocationCoordinate {
			get {
				return this.locationCoordinateField;
			}
			set {
				this.locationCoordinateField = value;
			}
		}
	}
}