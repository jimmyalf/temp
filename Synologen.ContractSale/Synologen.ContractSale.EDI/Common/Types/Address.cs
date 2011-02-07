namespace Spinit.Wpc.Synologen.EDI.Common.Types {
	public class Address {
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string Zip { get; set; }
		public string City { get; set; }
		public bool IsEmpty {
			get {return (Address1 + Address2 + Zip + City).Length <= 0;}
		}
	}
}